using System.Collections.Generic;
namespace RecaAPi.Models.Request
{
    public class AddFrutasRequest
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public List<string> Precios { get; set; }
        public bool Estatus { get; set; }
    }
}
