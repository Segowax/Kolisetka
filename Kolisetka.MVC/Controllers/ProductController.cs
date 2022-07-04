using Kolisetka.MVC.Contracts;
using Kolisetka.MVC.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Kolisetka.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var model = await _productService.GetProducts();

            return View(model);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCreateVM product)
        {
            try
            {
                var response = await _productService.CreateProduct(product);
                if (response.Success == true)
                    return RedirectToAction("Index");
                else if (response.ValidationError != null)
                    ModelState.AddModelError("", response.ValidationError);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Something went wrong!", "Error");
            }

            return View();
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
