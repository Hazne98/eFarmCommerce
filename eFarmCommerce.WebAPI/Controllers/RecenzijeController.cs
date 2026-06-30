using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class RecenzijeController
    : BaseCRUDController<RecenzijaResponse,
        RecenzijaSearchObject,
        RecenzijaInsertRequest,
        RecenzijaUpdateRequest>
{
    public RecenzijeController(IRecenzijaService service)
        : base(service)
    {
    }
}