using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class UlogeController
    : BaseCRUDController<UlogaResponse, UlogaSearchObject, UlogaInsertRequest, UlogaUpdateRequest>
{
    public UlogeController(IUlogaService service)
        : base(service)
    {
    }
}