using Facturacion._27.Data;
using Facturacion_27.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion._27.Services
{
    public class FacturaService
    {
        private readonly ApplicationDbContext _db;
        private readonly decimal iva = 0.13m;

        public FacturaService(ApplicationDbContext db) { _db = db; }

        public async Task<Factura> CrearFacturaAsync(int clienteId, List<(int productoId, int cantidad)> items, bool actualizarStock = true)
        {
            if (items == null || items.Count == 0) throw new ArgumentException("Factura sin items");
            var cliente = await _db.Clientes.FindAsync(clienteId);
            if (cliente == null) throw new Exception("Cliente no existe");

            var factura = new Factura { ClienteId = clienteId, Fecha = DateTime.Now };

            decimal subtotal = 0;
            foreach (var it in items)
            {
                var p = await _db.Productos.FindAsync(it.productoId);
                if (p == null) throw new Exception($"Producto {it.productoId} no existe");
                if (actualizarStock && p.Stock < it.cantidad) throw new Exception($"Stock insuficiente producto {p.Nombre}");
                var lineSubtotal = p.PrecioUnitario * it.cantidad;
                subtotal += lineSubtotal;
                factura.Items.Add(new FacturaItem
                {
                    ProductoId = p.Id,
                    Cantidad = it.cantidad,
                    PrecioUnitario = p.PrecioUnitario,
                    LineSubtotal = lineSubtotal
                });
                if (actualizarStock) p.Stock -= it.cantidad;
            }

            factura.Subtotal = subtotal;
            factura.Impuesto = Math.Round(subtotal * iva, 2);
            factura.Total = factura.Subtotal + factura.Impuesto;

            _db.Facturas.Add(factura);
            await _db.SaveChangesAsync();
            return factura;
        }
    }
}