using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EURIS.Entities;
using EURIS.Service;
using EURIS.Service.Contracts;
using EURISTest.Models;

namespace EURISTest.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CatalogController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public ActionResult Index()
        {
            return View(_unitOfWork.CatalogManager.GetCatalogs());
        }

        public ActionResult Create(Catalog catalog)
        {

            var viewModel = new CatalogViewModel
            {
                ProductsViewModel = _unitOfWork.ProductManager.
                    GetProducts().Select(c => new ProductSelectViewModel
                    {
                        ProductId = c.ProductId,
                        Code = c.Code,
                        Description = c.Description,
                        IsSelected = false
                    })
                    .ToList()
            };

          
            return View(viewModel);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code, Description, ProductsViewModel")]CatalogViewModel catalog)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CatalogManager.CreateCatalog(new Catalog
                {
                    Code = catalog.Code,
                    Description = catalog.Description
                });

               
                foreach (var p in catalog.ProductsViewModel)
                {

                    if(p.IsSelected)
                    { 
                        var productCatalog = new ProductCatalog();
                        productCatalog.CatalogId = catalog.CatalogId;
                        productCatalog.ProductId = p.ProductId;
                        _unitOfWork.ProductCatalogManager.AddProductCatalog(productCatalog);
                    }
                }
                _unitOfWork.Save();

                return RedirectToAction("Index", "Catalog");
            }

            return View(catalog);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Catalog catalog = _unitOfWork.CatalogManager.GetCatalog((int)id);
                if (catalog == null)
                {
                    return HttpNotFound();
                }

                var data = _unitOfWork.CatalogManager.GetCatalogs()
                    .Where(s => s.CatalogId == id)
                    .Select(s => new
                    {
                        ViewModel = new CatalogViewModel
                        {
                            CatalogId = s.CatalogId,
                            Code = s.Code,
                            Description = s.Description
                        },
                        ProductIds = s.ProductCatalog.Select(c => c.ProductId)

                    })
                    .SingleOrDefault();

                // Load all Products from the DB
                data.ViewModel.ProductsViewModel = _unitOfWork.ProductManager.GetProducts()
                    .Select(c => new ProductSelectViewModel
                    {
                        ProductId = c.ProductId,
                        Code = c.Code,
                        Description = c.Description
                    })
                    .ToList();


                foreach (var c in data.ViewModel.ProductsViewModel)
                    c.IsSelected = data.ProductIds.Contains(c.ProductId);


                return View(data.ViewModel);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatalogId, Code, Description, ProductsViewModel")]CatalogViewModel catalog)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CatalogManager.UpdateCatalog(new Catalog
                {
                    CatalogId = catalog.CatalogId,
                    Code = catalog.Code,
                    Description = catalog.Description
                });

                List<Product> products = catalog.ProductsViewModel
                    .Where(s => s.IsSelected == true)
                    .Select(x => new Product
                    {
                        ProductId = x.ProductId

                    }).ToList();

                _unitOfWork.ProductCatalogManager.UpdateProductCatalog(products, catalog.CatalogId);


                _unitOfWork.Save();

                return RedirectToAction("Index", "Catalog");
            }

            return View(catalog);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var catalog = _unitOfWork.CatalogManager.GetCatalog((int)id);
                if (catalog == null)
                {
                    return HttpNotFound();
                }

                var data = _unitOfWork.CatalogManager.GetCatalogs()
                    .Where(s => s.CatalogId == id)
                    .Select(s => new
                    {
                        ViewModel = new CatalogViewModel
                        {
                            CatalogId = s.CatalogId,
                            Code = s.Code,
                            Description = s.Description
                        },
                        ProductIds = s.ProductCatalog.Select(c => c.ProductId)

                    })
                    .SingleOrDefault();

                return View(data.ViewModel);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.CatalogManager.DeleteCatalog(id);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Catalog");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = _unitOfWork.CatalogManager.GetCatalog((int)id);

            if (catalog == null)
            {
                return HttpNotFound();
            }

            var data = _unitOfWork.CatalogManager.GetCatalogs()
                .Where(s => s.CatalogId == id)
                .Select(s => new
                {
                    ViewModel = new CatalogViewModel
                    {
                        CatalogId = s.CatalogId,
                        Code = s.Code,
                        Description = s.Description,
                        Products = _unitOfWork.ProductManager.GetProducts()
                            .Where(x => s.ProductCatalog.Any(y => y.ProductId == x.ProductId)).ToList()
                    }
                })
                .SingleOrDefault();
           
            return View(data.ViewModel);
        }
    }
}