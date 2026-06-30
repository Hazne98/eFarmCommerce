using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class RezervacijaStavkeController
    : BaseCRUDController<RezervacijaStavkaResponse, RezervacijaStavkaSearchObject, RezervacijaStavkaInsertRequest, RezervacijaStavkaUpdateRequest>
{
    public RezervacijaStavkeController(IRezervacijaStavkaService service)
        : base(service)
    {
    }
}