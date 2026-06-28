using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class StatusiPlacanjaController
    : BaseCRUDController<
        StatusPlacanjaResponse,
        StatusPlacanjaSearchObject,
        StatusPlacanjaInsertRequest,
        StatusPlacanjaUpdateRequest>
{
    public StatusiPlacanjaController(IStatusPlacanjaService service)
        : base(service)
    {
    }
}
