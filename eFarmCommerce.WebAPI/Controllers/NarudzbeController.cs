using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class NarudzbeController
    : BaseCRUDController<NarudzbaResponse, NarudzbaSearchObject, NarudzbaInsertRequest, NarudzbaUpdateRequest>
{
    public NarudzbeController(INarudzbaService service)
        : base(service)
    {
    }
}