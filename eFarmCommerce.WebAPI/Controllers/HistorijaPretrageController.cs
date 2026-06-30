using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class HistorijaPretrageController
    : BaseCRUDController<
        HistorijaPretrageResponse,
        HistorijaPretrageSearchObject,
        HistorijaPretrageInsertRequest,
        HistorijaPretrageUpdateRequest>
{
    public HistorijaPretrageController(IHistorijaPretrageService service)
        : base(service)
    {
    }
}