using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class DrzaveController
    : BaseCRUDController<DrzavaResponse, DrzavaSearchObject, DrzavaInsertRequest, DrzavaUpdateRequest>
{
    public DrzaveController(IDrzavaService service)
        : base(service)
    {
    }
}