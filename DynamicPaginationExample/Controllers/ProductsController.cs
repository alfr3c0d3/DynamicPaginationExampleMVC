using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DynamicPaginationExample.Models;

namespace DynamicPaginationExample.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            var productsPerPage = 12;
            var start = (page - 1) * productsPerPage;
            var products = _context.Products;
            
            ViewBag.PageCount = Math.Ceiling(products.Count() / (double)productsPerPage);
            var paginatedProducts = products.Skip(start).Take(productsPerPage);

            await Task.CompletedTask;

            return View(paginatedProducts);
        }
    }
}
