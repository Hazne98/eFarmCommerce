using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;

namespace eFarmCommerce.Services.Interfaces;

public interface ILokacijaService
    : IBaseCRUDService<LokacijaResponse, LokacijaSearchObject, LokacijaInsertRequest, LokacijaUpdateRequest>
{
}