using System.ComponentModel.DataAnnotations;

namespace Facturacion_27.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono { get; set; }

         
        public string RucNit { get; set; }

        public string Direccion { get; set; }

         
        public string Email { get; set; }
    }
}
