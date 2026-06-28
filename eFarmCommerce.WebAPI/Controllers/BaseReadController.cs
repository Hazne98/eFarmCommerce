using eFarmCommerce.Model.Responses;
using eFarmCommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eFarmCommerce.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseReadController<TResponse, TSearch> : ControllerBase
{
    protected readonly IBaseReadService<TResponse, TSearch> Service;

    public BaseReadController(IBaseReadService<TResponse, TSearch> service)
    {
        Service = service;
    }

    [HttpGet]
    public virtual async Task<PagedResult<TResponse>> GetAsync([FromQuery] TSearch search)
    {
        return await Service.GetAsync(search);
    }

    [HttpGet("{id}")]
    public virtual async Task<TResponse?> GetByIdAsync(int id)
    {
        return await Service.GetByIdAsync(id);
    }
}