using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;

namespace eFarmCommerce.Services.Interfaces;

public interface IUlogaService
    : IBaseCRUDService<UlogaResponse, UlogaSearchObject, UlogaInsertRequest, UlogaUpdateRequest>
{
}