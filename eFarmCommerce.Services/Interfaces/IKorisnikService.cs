using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;

namespace eFarmCommerce.Services.Interfaces;

public interface IKorisnikService
    : IBaseCRUDService<
        KorisnikResponse,
        KorisnikSearchObject,
        KorisnikInsertRequest,
        KorisnikUpdateRequest>
{
    Task ChangePasswordAsync(int korisnikId, KorisnikPasswordChangeRequest request);
}
