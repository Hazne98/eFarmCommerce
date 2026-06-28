using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class OpremaController
    : BaseCRUDController<OpremaResponse, OpremaSearchObject, OpremaInsertRequest, OpremaUpdateRequest>
{
    public OpremaController(IOpremaService service)
        : base(service)
    {
    }
}