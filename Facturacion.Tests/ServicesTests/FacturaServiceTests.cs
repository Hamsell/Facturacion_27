using Xunit;
using Facturacion._27.Data;
using Facturacion._27.Services;
using Facturacion_27.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Tests.ServicesTests
{
    public class FacturaServiceTests
    {
        [Fact]
        public async Task CrearFactura_CalculaTotalesCorrectamente()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            var db = new ApplicationDbContext(options);

            db.Clientes.Add(new Cliente { Id = 1, Nombre = "Juan" });
            db.Productos.Add(new Producto { Id = 1, Nombre = "Producto1", PrecioUnitario = 100, Stock = 10 });
            await db.SaveChangesAsync();

            var service = new FacturaService(db);
            var factura = await service.CrearFacturaAsync(1, new List<(int, int)> { (1, 2) });

            Assert.Equal(200, factura.Subtotal);
            Assert.Equal(26, factura.Impuesto);
            Assert.Equal(226, factura.Total);
            var p = await db.Productos.FirstAsync(x => x.Id == 1);
            Assert.Equal(8, p.Stock);
        }
    }
}
