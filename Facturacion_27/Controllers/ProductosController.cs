using Facturacion._27.Data;
using Facturacion_27.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Facturacion._27.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductosController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index() => View(await _db.Productos.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (!ModelState.IsValid) return View(producto);
            _db.Productos.Add(producto);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await _db.Productos.FindAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(producto);
            _db.Update(producto);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Productos.FindAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _db.Productos.FindAsync(id);
            if (p == null) return NotFound();
            _db.Productos.Remove(p);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await _db.Productos.FindAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }
    }
}