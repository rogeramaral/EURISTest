using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;
using EURIS.Service.Contracts;
using EURISTest.Models;
using System.Net;

namespace EURISTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View(_unitOfWork.ProductManager.GetProducts());
        }

        public ActionResult Create(Product product)
        {
            var viewModel = new ProductViewModel
            {
                
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code, Description, Catalogs")]ProductViewModel product)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductManager.CreateProduct(new Product
                {
                    Code = product.Code,
                    Description = product.Description
                });

                _unitOfWork.Save();

                return RedirectToAction("Index", "Product");
            }

            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Product product = _unitOfWork.ProductManager.GetProduct((int)id);
                if (product == null)
                {
                    return HttpNotFound();
                }

                var data = _unitOfWork.ProductManager.GetProducts()
                    .Where(s => s.ProductId == id)
                    .Select(s => new
                    {
                        ViewModel = new ProductViewModel
                        {
                            ProductId = s.ProductId,
                            Code = s.Code,
                            Description = s.Description
                        }  
                    })
                    .SingleOrDefault();

                return View(data.ViewModel);
            }
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId, Code, Description, Catalogs")]ProductViewModel product)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.ProductManager.UpdateProduct(new Product
                {
                    ProductId = product.ProductId,
                    Code = product.Code,
                    Description = product.Description
                });

                _unitOfWork.Save();

                return RedirectToAction("Index", "Product");
            }
          
                return View(product);
            }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var product = _unitOfWork.ProductManager.GetProduct((int)id);
                if (product == null)
                {
                    return HttpNotFound();
                }

                var data = _unitOfWork.ProductManager.GetProducts()
                    .Where(s => s.ProductId == id)
                    .Select(s => new
                    {
                        ViewModel = new ProductViewModel
                        {
                            ProductId = s.ProductId,
                            Code = s.Code,
                            Description = s.Description
                        },
                        CatalogsIds = s.ProductCatalog.Select(c => c.CatalogId)

                    })
                    .SingleOrDefault();

                return View(data.ViewModel);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.ProductManager.DeleteProduct(id);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _unitOfWork.ProductManager.GetProduct((int)id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var data = _unitOfWork.ProductManager.GetProducts()
                .Where(s => s.ProductId == id)
                .Select(s => new
                {
                    ViewModel = new ProductViewModel
                    {
                        ProductId = s.ProductId,
                        Code = s.Code,
                        Description = s.Description
                    },
                    CatalogsIds = s.ProductCatalog.Select(c => c.CatalogId)

                })
                .SingleOrDefault();

            return View(data.ViewModel);
        }
    }
}
