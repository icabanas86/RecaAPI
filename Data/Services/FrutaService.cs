using RecaAPi.Data.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using RecaAPi.Data.Models;
using Microsoft.EntityFrameworkCore;
using RecaAPi.Models.Response;

namespace RecaAPi.Data.Services
{
    public class FrutaServices:IFrutaServices
    {
        RecaContext dbContext;

        public FrutaServices(RecaContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<Fruta>> GetPaegableFruta(int pagina,int registros)
        {
            var frutas = await dbContext.Frutas.Skip((pagina -1) * registros).Take(registros).ToListAsync();
            return frutas;
        }
        public async Task<InsertResponse> AddFruta(Fruta request)
        {
            try
            {
                dbContext.Frutas.Add(request);
                var inserted = await dbContext.SaveChangesAsync();
                if(Convert.ToBoolean(inserted))
                {
                    return new InsertResponse()
                    {
                        CodigoEstatus = 200,
                        Descripcion = "Exito",
                        Mensaje = ""
                    };
                }
                return new InsertResponse()
                {
                    CodigoEstatus = 400,
                    Descripcion = "Error",
                    Mensaje = "Al Insertar en la tabla frutas"
                };
            }
            catch(Exception ex)
            {
                return new InsertResponse()
                {
                    CodigoEstatus = 400,
                    Descripcion = "Error:" + ex.Message,
                    Mensaje = ex.InnerException!=null?ex.InnerException.Message:null
                };
            }
        }
        public async Task<List<Fruta>> Getfrutas()
        {
            var frutas = await dbContext.Frutas.ToListAsync();
            if(frutas.Count>0)
            {
                return frutas;
            }
            return null;
        }

        public async Task<Fruta> GetFrutaPorClave(string clave)
        {
            var fruta = await dbContext.Frutas.Where(c=>c.Clave.Equals(clave)).FirstOrDefaultAsync();
            if(fruta!=null)
            {
                return fruta;
            }
            return null;
        }

        public async Task<UpdateResponse> UpdateFruta(Fruta request)
        {
            try
            {
                var fruta = await dbContext.Frutas.Where(c=>c.Id.Equals(request.Id)).FirstOrDefaultAsync();
                if(fruta!=null)
                {
                    fruta.Nombre = request.Nombre;
                    fruta.Precios = request.Precios;
                    fruta.Clave = request.Clave;
                    fruta.Estatus = request.Estatus;
                    fruta.FechaModificacion = DateTime.Now;
                    fruta.FechaRegistro = request.FechaRegistro;
                    dbContext.Frutas.Update(fruta);
                    var updated = await dbContext.SaveChangesAsync();
                    if(Convert.ToBoolean(updated))
                    {
                        return new UpdateResponse()
                        {
                            CodigoEstatus = 200,
                            Descripcion = "Exito",
                            Mensaje = ""
                        };
                    }
                }
                return new UpdateResponse()
                {
                    CodigoEstatus = 400,
                    Descripcion = "Error",
                    Mensaje = "No se encontro información"
                };
            }
            catch(Exception ex)
            {
                return new UpdateResponse()
                {
                    CodigoEstatus = 400,
                    Descripcion = "Error:" + ex.Message,
                    Mensaje = ex.InnerException!=null?ex.InnerException.Message:null
                };
            }
        }
    }
}