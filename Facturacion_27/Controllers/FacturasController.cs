using Facturacion._27.Services;
using Microsoft.AspNetCore.Mvc;
using Facturacion_27.Models;
using Microsoft.EntityFrameworkCore;
using Facturacion._27.Data;

namespace Facturacion._27.Controllers
{
    public class FacturasController : Controller
    {
        private readonly FacturaService _facturaService;
        private readonly ApplicationDbContext _db;

        public FacturasController(FacturaService facturaService, ApplicationDbContext db)
        {
            _facturaService = facturaService;
            _db = db;
        }

        // Listado de facturas
        public async Task<IActionResult> Index()
        {
            var facturas = await _db.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Items)
                .ThenInclude(i => i.Producto)
                .ToListAsync();
            return View(facturas);
        }

        // Vista crear factura
        public IActionResult CrearFactura()
        {
            ViewBag.Clientes = _db.Clientes.ToList();
            ViewBag.Productos = _db.Productos.ToList();
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var factura = await _db.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Items)
                .ThenInclude(i => i.Producto)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (factura == null)
                return NotFound();

            return View("FacturaCreada", factura);
        }


        // Acción procesar la factura
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearFactura(int clienteId, List<int> productos, List<int> cantidades)
        {
            try
            {
                if (productos.Count != cantidades.Count)
                {
                    ViewBag.Error = "Cantidad de productos y cantidades no coinciden";
                    ViewBag.Clientes = _db.Clientes.ToList();
                    ViewBag.Productos = _db.Productos.ToList();
                    return View();
                }

                var items = new List<(int productoId, int cantidad)>();
                for (int i = 0; i < productos.Count; i++)
                    items.Add((productos[i], cantidades[i]));

                var factura = await _facturaService.CrearFacturaAsync(clienteId, items);
                return View("FacturaCreada", factura);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Clientes = _db.Clientes.ToList();
                ViewBag.Productos = _db.Productos.ToList();
                return View();
            }
        }
    }
}

