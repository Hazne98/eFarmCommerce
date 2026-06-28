using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class StatusiRezervacijeController
    : BaseCRUDController<
        StatusRezervacijeResponse,
        StatusRezervacijeSearchObject,
        StatusRezervacijeInsertRequest,
        StatusRezervacijeUpdateRequest>
{
    public StatusiRezervacijeController(IStatusRezervacijeService service)
        : base(service)
    {
    }
}
