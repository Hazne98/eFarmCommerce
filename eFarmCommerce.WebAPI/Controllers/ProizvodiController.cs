using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class ProizvodiController
    : BaseCRUDController<ProizvodResponse, ProizvodSearchObject, ProizvodInsertRequest, ProizvodUpdateRequest>
{
    public ProizvodiController(IProizvodService service)
        : base(service)
    {
    }
}