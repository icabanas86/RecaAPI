using System.Threading.Tasks;
using System.Collections.Generic;
using RecaAPi.Data.Models;
using RecaAPi.Models.Response;

namespace RecaAPi.Data.Interfaces
{
    public interface IFrutaServices
    {
        Task<List<Fruta>> Getfrutas();
        Task<Fruta> GetFrutaPorClave(string clave);
        Task<UpdateResponse> UpdateFruta(Fruta request);
        Task<InsertResponse> AddFruta(Fruta request);
        Task<List<Fruta>> GetPaegableFruta(int pagina,int registros);
    }
}