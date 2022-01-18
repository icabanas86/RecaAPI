using System.Collections.Generic;
using System;
namespace RecaAPi.Models.Response
{
    public class FrutaPaginadoResponse
    {
        public int CodigoEstatus { get; set; }
        public int paginas { get; set; }
        public int paginaActual { get; set; }
        public List<FrutasModel> frutas { get; set; }
    }
}