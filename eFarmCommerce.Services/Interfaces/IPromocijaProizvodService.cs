using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;

namespace eFarmCommerce.Services.Interfaces;

public interface IPromocijaProizvodService
    : IBaseCRUDService<
        PromocijaProizvodResponse,
        PromocijaProizvodSearchObject,
        PromocijaProizvodInsertRequest,
        PromocijaProizvodUpdateRequest>
{
}
