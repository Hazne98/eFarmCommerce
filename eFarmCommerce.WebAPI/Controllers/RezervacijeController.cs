using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class RezervacijeController
    : BaseCRUDController<RezervacijaResponse, RezervacijaSearchObject, RezervacijaInsertRequest, RezervacijaUpdateRequest>
{
    public RezervacijeController(IRezervacijaService service)
        : base(service)
    {
    }
}