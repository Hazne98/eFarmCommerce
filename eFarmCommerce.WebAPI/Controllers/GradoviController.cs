using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class GradoviController
    : BaseCRUDController<GradResponse, GradSearchObject, GradInsertRequest, GradUpdateRequest>
{
    public GradoviController(IGradService service)
        : base(service)
    {
    }
}