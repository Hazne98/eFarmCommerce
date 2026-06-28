using eFarmCommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eFarmCommerce.WebAPI.Controllers;

public class BaseCRUDController<TResponse, TSearch, TInsert, TUpdate>
    : BaseReadController<TResponse, TSearch>
{
    protected readonly IBaseCRUDService<TResponse, TSearch, TInsert, TUpdate> CrudService;

    public BaseCRUDController(IBaseCRUDService<TResponse, TSearch, TInsert, TUpdate> service)
        : base(service)
    {
        CrudService = service;
    }

    [HttpPost]
    public virtual async Task<TResponse> InsertAsync([FromBody] TInsert request)
    {
        return await CrudService.InsertAsync(request);
    }

    [HttpPut("{id}")]
    public virtual async Task<TResponse?> UpdateAsync(int id, [FromBody] TUpdate request)
    {
        return await CrudService.UpdateAsync(id, request);
    }

    [HttpDelete("{id}")]
    public virtual async Task<bool> DeleteAsync(int id)
    {
        return await CrudService.DeleteAsync(id);
    }
}