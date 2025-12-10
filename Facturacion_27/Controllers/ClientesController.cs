using Facturacion._27.Data;
using Facturacion_27.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Facturacion._27.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ClientesController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index() => View(await _db.Clientes.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (!ModelState.IsValid) return View(cliente);
            _db.Clientes.Add(cliente);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var c = await _db.Clientes.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest();
            if (!ModelState.IsValid) return View(cliente);
            _db.Update(cliente);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var c = await _db.Clientes.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var c = await _db.Clientes.FindAsync(id);
            if (c == null) return NotFound();
            _db.Clientes.Remove(c);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var c = await _db.Clientes.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }
    }
}