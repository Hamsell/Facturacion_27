using Facturacion_27.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Facturacion_27.Tests
{
    public class UnitTestFactura
    {
        [Fact]
        public void CalcularTotales_DeberiaCalcularSubtotalImpuestoYTotal()
        {
            // Arrange
            var factura = new Factura
            {
                ClienteId = 1,
                Items = new List<FacturaItem>
                {
                    new FacturaItem
                    {
                        ProductoId = 1,
                        Cantidad = 2,
                        PrecioUnitario = 50m,
                        LineSubtotal = 100m
                    },
                    new FacturaItem
                    {
                        ProductoId = 2,
                        Cantidad = 1,
                        PrecioUnitario = 80m,
                        LineSubtotal = 80m
                    }
                }
            };

            // Act
            factura.Subtotal = factura.Items.Sum(i => i.LineSubtotal);
            factura.Impuesto = factura.Subtotal * 0.18m;  // 18% ITBIS
            factura.Total = factura.Subtotal + factura.Impuesto;

            // Assert
            Assert.Equal(180m, factura.Subtotal);      // 100 + 80
            Assert.Equal(32.4m, factura.Impuesto);     // 180 * 0.18
            Assert.Equal(212.4m, factura.Total);       // 180 + 32.4
        }
    }
}
