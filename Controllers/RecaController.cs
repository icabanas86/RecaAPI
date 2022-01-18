using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecaAPi.Interfaces;
using RecaAPi.Models.Response;
using Microsoft.AspNetCore.Http;
using SpreadsheetLight;
using RecaAPi.Data.Models;
using RecaAPi.Models.Request;
using System.Collections.Generic;

namespace RecaAPi.Controllers
{
    public class RecaController : ControllerBase
    {
        IRecaServices recaServices;

        public RecaController(IRecaServices _recaServices)
        {
            recaServices = _recaServices;
        }

        [HttpPost]
        [Route("api/AgregaFrutasLayout")]
        public async Task<ActionResult<InsertResponse>> AddFrutasLayout(IFormFile file)
        {
            int Fila = 2;
            SLDocument archivo = new SLDocument(file.OpenReadStream());
            List<string> precioLista = new List<string>();
            AddFrutasRequest unaFruta = new AddFrutasRequest();
            List<AddFrutasRequest> frutosList = new List<AddFrutasRequest>();
            while(!string.IsNullOrEmpty(archivo.GetCellValueAsString(Fila,1)))
            {
                unaFruta.Clave = archivo.GetCellValueAsString(Fila,1);
                unaFruta.Nombre = archivo.GetCellValueAsString(Fila,2);
                var precios = archivo.GetCellValueAsString(Fila,3);
                foreach(var p in precios.Split(","))
                {
                    precioLista.Add(p);
                }
                unaFruta.Precios = precioLista;
                precioLista = new List<string>();
                if(archivo.GetCellValueAsString(Fila,4).ToUpper() =="X")
                {
                    unaFruta.Estatus = true;
                }
                else
                {
                    unaFruta.Estatus = false;
                }
                frutosList.Add(unaFruta);
                unaFruta = new AddFrutasRequest();
                 Fila += 1;
            }

            var response = await recaServices.AddFrutas(frutosList);
            if(response.CodigoEstatus!=200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("api/ObtieneTodasLasFrutas")]
        public async Task<ActionResult<GetFrutasResponse>> GetFrutas()
        {
            var response = await recaServices.GetFrutas();
            if(response.CodigoEstatus!=200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("api/ObtieneFrutaPorClave")]
        public async Task<ActionResult<GetFrutasResponse>> GetFrutaPorClave(string clave)
        {
            var response = await recaServices.GetFrutaPorClave(clave);
            if(response.CodigoEstatus!=200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPut]
        [Route("api/ActualizaFruta")]
        public async Task<ActionResult<UpdateResponse>> Updatefruta([FromBody]UpdateFrutaRequest request)
        {
            var response = await recaServices.UpdateFruta(request);
            if(response.CodigoEstatus!=200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("api/ObtieneFrutasPaginadas")]
        public async Task<ActionResult<FrutaPaginadoResponse>> GetPageableFruta(int? pagina)
        {
            var response = await recaServices.GetFrutaPageable(pagina);
            if(response.CodigoEstatus!=200)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
