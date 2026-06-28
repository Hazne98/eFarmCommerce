using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class KategorijeOpremeController
    : BaseCRUDController<KategorijaOpremeResponse, KategorijaOpremeSearchObject, KategorijaOpremeInsertRequest, KategorijaOpremeUpdateRequest>
{
    public KategorijeOpremeController(IKategorijaOpremeService service)
        : base(service)
    {
    }
}