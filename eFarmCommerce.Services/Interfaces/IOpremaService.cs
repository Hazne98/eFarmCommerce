using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;

namespace eFarmCommerce.Services.Interfaces;

public interface IOpremaService
    : IBaseCRUDService<OpremaResponse, OpremaSearchObject, OpremaInsertRequest, OpremaUpdateRequest>
{
}