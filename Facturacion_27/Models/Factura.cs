using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Facturacion_27.Models
{
    public class Factura
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
         
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; } // IVA
        public decimal Total { get; set; }

        public List<FacturaItem> Items { get; set; } = new List<FacturaItem>();
    }
}
