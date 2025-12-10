using System.ComponentModel.DataAnnotations;

namespace Facturacion_27.Models
{
    public class FacturaItem
    {
        public int Id { get; set; }

        [Required]
        public int FacturaId { get; set; }
        public Factura? Factura { get; set; }

        [Required]
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal LineSubtotal { get; set; }
    }
}
