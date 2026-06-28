using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class LokacijeController
    : BaseCRUDController<LokacijaResponse, LokacijaSearchObject, LokacijaInsertRequest, LokacijaUpdateRequest>
{
    public LokacijeController(ILokacijaService service)
        : base(service)
    {
    }
}