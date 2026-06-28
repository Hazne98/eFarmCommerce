using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class StatusiPovrataController
    : BaseCRUDController<
        StatusPovrataResponse,
        StatusPovrataSearchObject,
        StatusPovrataInsertRequest,
        StatusPovrataUpdateRequest>
{
    public StatusiPovrataController(IStatusPovrataService service)
        : base(service)
    {
    }
}
