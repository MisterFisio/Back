using System.ComponentModel.DataAnnotations;

namespace back.Models
{
    public class Info
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
