using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class StatusiNarudzbeController
    : BaseCRUDController<StatusNarudzbeResponse, StatusNarudzbeSearchObject, StatusNarudzbeInsertRequest, StatusNarudzbeUpdateRequest>
{
    public StatusiNarudzbeController(IStatusNarudzbeService service)
        : base(service)
    {
    }
}