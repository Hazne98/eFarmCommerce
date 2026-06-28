using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;

namespace eFarmCommerce.Services.Interfaces;

public interface IGradService
    : IBaseCRUDService<GradResponse, GradSearchObject, GradInsertRequest, GradUpdateRequest>
{
}