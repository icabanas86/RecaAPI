using System;
using System.Collections.Generic;

#nullable disable

namespace RecaAPi.Data.Models
{
    public partial class Fruta
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Precios { get; set; }
        public bool? Estatus { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
