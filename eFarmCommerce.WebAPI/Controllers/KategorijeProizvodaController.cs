using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class KategorijeProizvodaController
    : BaseCRUDController<KategorijaProizvodaResponse, KategorijaProizvodaSearchObject, KategorijaProizvodaInsertRequest, KategorijaProizvodaUpdateRequest>
{
    public KategorijeProizvodaController(IKategorijaProizvodaService service)
        : base(service)
    {
    }
}