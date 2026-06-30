using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class PovratiRefundController
    : BaseCRUDController<PovratRefundResponse, PovratRefundSearchObject, PovratRefundInsertRequest, PovratRefundUpdateRequest>
{
    public PovratiRefundController(IPovratRefundService service)
        : base(service)
    {
    }
}