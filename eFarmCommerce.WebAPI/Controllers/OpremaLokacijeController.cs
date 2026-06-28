using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class OpremaLokacijeController
    : BaseCRUDController<OpremaLokacijaResponse, OpremaLokacijaSearchObject, OpremaLokacijaInsertRequest, OpremaLokacijaUpdateRequest>
{
    public OpremaLokacijeController(IOpremaLokacijaService service)
        : base(service)
    {
    }
}