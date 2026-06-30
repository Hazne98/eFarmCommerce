using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class PlacanjaController
    : BaseCRUDController<PlacanjeResponse, PlacanjeSearchObject, PlacanjeInsertRequest, PlacanjeUpdateRequest>
{
    public PlacanjaController(IPlacanjeService service)
        : base(service)
    {
    }
}