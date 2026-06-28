using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class NaciniPlacanjaController
    : BaseCRUDController<
        NacinPlacanjaResponse,
        NacinPlacanjaSearchObject,
        NacinPlacanjaInsertRequest,
        NacinPlacanjaUpdateRequest>
{
    public NaciniPlacanjaController(INacinPlacanjaService service)
        : base(service)
    {
    }
}
