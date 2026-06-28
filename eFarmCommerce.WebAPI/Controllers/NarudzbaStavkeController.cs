using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class NarudzbaStavkeController
    : BaseCRUDController<NarudzbaStavkaResponse, NarudzbaStavkaSearchObject, NarudzbaStavkaInsertRequest, NarudzbaStavkaUpdateRequest>
{
    public NarudzbaStavkeController(INarudzbaStavkaService service)
        : base(service)
    {
    }
}