using System.Collections.Generic;
using System;
namespace RecaAPi.Models.Response
{
    public class GetFrutasResponse
    {
        public int CodigoEstatus { get; set; }
        public string Mensaje { get; set; }
        public List<FrutasModel> frutas { get; set; }
    }

    public class  FrutasModel
    {
        public int  id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public List<string> Precios { get; set; }
        public bool? Estatus { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }

    }
}