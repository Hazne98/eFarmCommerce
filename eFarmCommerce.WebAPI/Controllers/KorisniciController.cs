using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;
using eFarmCommerce.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace eFarmCommerce.WebAPI.Controllers;

[Authorization("Admin", "Desktop")]
public class KorisniciController
    : BaseCRUDController<
        KorisnikResponse,
        KorisnikSearchObject,
        KorisnikInsertRequest,
        KorisnikUpdateRequest>
{
    private readonly IKorisnikService _korisnikService;

    public KorisniciController(IKorisnikService service)
        : base(service)
    {
        _korisnikService = service;
    }

    [HttpPut("{id}/password")]
    public async Task ChangePasswordAsync(int id, [FromBody] KorisnikPasswordChangeRequest request)
    {
        await _korisnikService.ChangePasswordAsync(id, request);
    }
}