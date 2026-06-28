using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class KorpeController
    : BaseCRUDController<KorpaResponse, KorpaSearchObject, KorpaInsertRequest, KorpaUpdateRequest>
{
    public KorpeController(IKorpaService service)
        : base(service)
    {
    }
}