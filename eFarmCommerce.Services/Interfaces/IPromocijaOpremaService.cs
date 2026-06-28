using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;

namespace eFarmCommerce.Services.Interfaces;

public interface IPromocijaOpremaService
    : IBaseCRUDService<
        PromocijaOpremaResponse,
        PromocijaOpremaSearchObject,
        PromocijaOpremaInsertRequest,
        PromocijaOpremaUpdateRequest>
{
}
