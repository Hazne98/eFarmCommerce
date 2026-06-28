using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class ProizvodjaciController
    : BaseCRUDController<ProizvodjacResponse, ProizvodjacSearchObject, ProizvodjacInsertRequest, ProizvodjacUpdateRequest>
{
    public ProizvodjaciController(IProizvodjacService service)
        : base(service)
    {
    }
}