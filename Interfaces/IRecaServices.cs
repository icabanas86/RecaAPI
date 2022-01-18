using System.Threading.Tasks;
using RecaAPi.Models.Response;
using RecaAPi.Models.Request;
using System.Collections.Generic;

namespace RecaAPi.Interfaces
{
    public interface IRecaServices
    {
        Task<InsertResponse> AddFrutas(List<AddFrutasRequest> request);
        Task<GetFrutasResponse> GetFrutas();
        Task<GetFrutasResponse> GetFrutaPorClave(string clave);
        Task<UpdateResponse> UpdateFruta(UpdateFrutaRequest request);
        Task<FrutaPaginadoResponse> GetFrutaPageable(int? page);
    }
}