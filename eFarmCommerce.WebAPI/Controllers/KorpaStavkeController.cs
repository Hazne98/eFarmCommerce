using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class KorpaStavkeController
    : BaseCRUDController<KorpaStavkaResponse, KorpaStavkaSearchObject, KorpaStavkaInsertRequest, KorpaStavkaUpdateRequest>
{
    public KorpaStavkeController(IKorpaStavkaService service)
        : base(service)
    {
    }
}