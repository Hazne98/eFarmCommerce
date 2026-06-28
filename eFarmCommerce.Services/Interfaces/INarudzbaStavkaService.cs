using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;

namespace eFarmCommerce.Services.Interfaces;

public interface INarudzbaStavkaService
    : IBaseCRUDService<
        NarudzbaStavkaResponse,
        NarudzbaStavkaSearchObject,
        NarudzbaStavkaInsertRequest,
        NarudzbaStavkaUpdateRequest>
{
}
