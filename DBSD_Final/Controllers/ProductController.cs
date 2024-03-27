using DBSD_Final.DAL.Models;
using DBSD_Final.DAL.Repos;
using Microsoft.AspNetCore.Mvc;

namespace DBSD_Final.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var list = await _productRepository.GetAllAsync();
            return View(list);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(_productRepository.GetById(id));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                int id = _productRepository.Insert(product);
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(product);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_productRepository.GetById(id));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            try
            {
                _productRepository.Update(product);
                return RedirectToAction("Details", new { id = product.ProductID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(product);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(_productRepository.GetById(id));
        }


        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                _productRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
