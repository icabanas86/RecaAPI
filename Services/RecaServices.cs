using RecaAPi.Interfaces;
using RecaAPi.Data.Interfaces;
using RecaAPi.Data.Models;
using System.Threading.Tasks;
using RecaAPi.Models.Response;
using RecaAPi.Models.Request;
using System.Collections.Generic;
using System;

namespace RecaAPi.Services
{
    public class RecaServices : IRecaServices
    {
        private IFrutaServices frutaServices;

        public RecaServices(IFrutaServices _frutaServices)
        {
            frutaServices = _frutaServices;
        }

        public async Task<FrutaPaginadoResponse> GetFrutaPageable(int? page)
        {
            int pagina = page ?? 1;
            var frutas = await frutaServices.Getfrutas();
            decimal totalRegistros = frutas.Count;
            int totalPaginas = Convert.ToInt32(Math.Ceiling(totalRegistros/5));
            var registros = await frutaServices.GetPaegableFruta(pagina,5);
            List<FrutasModel> frutaModel = new List<FrutasModel>();
            List<string> precio = new List<string>();
            foreach(var f in registros)
            {
                foreach(var p in f.Precios.Split(","))
                {
                    precio.Add(p);
                }
                FrutasModel fm = new FrutasModel()
                {
                    id = f.Id,
                    Clave = f.Clave,
                    Nombre = f.Nombre,
                    Estatus = f.Estatus,
                    Precios = precio,
                    FechaRegistro = f.FechaRegistro,
                    FechaModificacion = f.FechaModificacion
                };
                precio = new List<string>();
                frutaModel.Add(fm);
                fm = new FrutasModel();
            }
            return new FrutaPaginadoResponse()
            {
                CodigoEstatus = 200,
                paginas = totalPaginas,
                paginaActual = pagina,
                frutas = frutaModel
            };
        }
        public async Task<UpdateResponse> UpdateFruta(UpdateFrutaRequest request)
        {
            string precio = string.Empty;
            Fruta fruta = new Fruta();
            fruta.Id = request.id;
            fruta.Clave = request.Clave;
            fruta.Nombre = request.Nombre;
            foreach(var p in request.Precios)
            {
                precio += p + ",";
            }
            fruta.Precios = precio.TrimEnd(',');
            fruta.Estatus = request.Estatus;
            fruta.FechaModificacion = DateTime.Now;
            var updated = await frutaServices.UpdateFruta(fruta);
            if(updated.CodigoEstatus!=200)
            {
                return updated;
            }
            return updated;
        }
        public async Task<GetFrutasResponse> GetFrutaPorClave(string clave)
        {
            GetFrutasResponse response = new GetFrutasResponse();
            FrutasModel frutaModel = new FrutasModel();
            List<FrutasModel> frutaList = new List<FrutasModel>();
            List<string> precioList = new List<string>();
            var frutas = await frutaServices.Getfrutas();
            if(frutas!=null)
            {
                var fruta = frutas.Find(c=>c.Clave.Equals(clave));
                if(fruta!=null)
                {
                    frutaModel.id = fruta.Id;
                    frutaModel.Clave = fruta.Clave;
                    frutaModel.Nombre = fruta.Nombre;
                    frutaModel.Estatus = fruta.Estatus;
                    foreach(var p in fruta.Precios.Split(","))
                    {
                        precioList.Add(p);
                    };
                    frutaModel.Precios = precioList;
                    precioList = new List<string>();
                    frutaModel.FechaRegistro = fruta.FechaRegistro;
                    frutaModel.FechaModificacion = fruta.FechaModificacion;
                    frutaList.Add(frutaModel);
                }
            }
            return new GetFrutasResponse()
            {
                CodigoEstatus = 200,
                Mensaje = frutaList.Count>0?"Exito":"No hay datos",
                frutas = frutaList.Count>0?frutaList:null
            };
        }
        public async Task<GetFrutasResponse> GetFrutas()
        {
            GetFrutasResponse response = new GetFrutasResponse();
            FrutasModel frutaModel = new FrutasModel();
            List<FrutasModel> frutaList = new List<FrutasModel>();
            List<string> precioList = new List<string>();
            var fruta = await frutaServices.Getfrutas();
            if(fruta != null)
            {
                foreach(var unaFruta in fruta)
                {
                    frutaModel.id = unaFruta.Id;
                    frutaModel.Clave = unaFruta.Clave;
                    frutaModel.Nombre = unaFruta.Nombre;
                    foreach(var p in unaFruta.Precios.Split(","))
                    {
                        precioList.Add(p);
                    }
                    frutaModel.Precios = precioList;
                    precioList = new List<string>();
                    frutaModel.Estatus = unaFruta.Estatus;
                    frutaModel.FechaRegistro = unaFruta.FechaRegistro;
                    frutaModel.FechaModificacion = unaFruta.FechaModificacion;
                    frutaList.Add(frutaModel);
                    frutaModel = new FrutasModel();
                }
            }
            return new GetFrutasResponse()
            {
                CodigoEstatus = 200,
                Mensaje = frutaList.Count>0?"Exito":"No hay datos",
                frutas = frutaList.Count>0?frutaList:null
            };
        }
        public async Task<InsertResponse> AddFrutas(List<AddFrutasRequest> request)
        {
            var mensaje = string.Empty;
            foreach(var fruta in request)
            {
                string precios = string.Empty;
                foreach(var p in fruta.Precios)
                {
                    precios += p + ",";
                }
                Fruta unaFruta = new Fruta()
                {
                    Clave = fruta.Clave,
                    Nombre = fruta.Nombre,
                    Estatus = fruta.Estatus,
                    FechaRegistro = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Precios = precios.ToString().TrimEnd(',')
                };
                var inserted = await frutaServices.AddFruta(unaFruta);
                if(inserted.CodigoEstatus!=200)
                {
                    mensaje += inserted.Mensaje + "|";
                }
            }
            if(mensaje.Length > 0)
            {
                return new InsertResponse()
                {
                    CodigoEstatus = 400,
                    Descripcion = "Error",
                    Mensaje = mensaje
                };
            }
            return new InsertResponse()
            {
                CodigoEstatus = 200,
                Descripcion = "Exito",
                Mensaje = ""
            };
        }
    }
}