using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _client = null;
        private readonly PRN231_AS1Context _context;
        private string ProductData = "";

        [BindProperty]
        public Product Products { get; set; }

        public ProductsController(PRN231_AS1Context context)
        {
            _context = context;
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductData = "http://localhost:5020/api/product-management/products";
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _client.GetAsync(ProductData);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(strData, options);
            return View(products);
        }

        // GET: Products/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,ProductName,Weight,UnitPrice,UnitsInStock")] Product product)
        {
            using (var respone = await _client.PostAsJsonAsync(ProductData, product))
            {
                string apiResponse = await respone.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,ProductName,Weight,UnitPrice,UnitsInStock")] Product product)
        {
            using (var respone = await _client.PutAsJsonAsync(ProductData +"/" + id, product))
            {
                string apiResponse = await respone.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productId = _context.Products.Find(id);
            if (productId != null)
            {
                String url = ProductData + "/" + id;
                await _client.DeleteAsync(url);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
