using eFarmCommerce.Model.Responses;

namespace eFarmCommerce.Services.Interfaces;

public interface IBaseReadService<TResponse, TSearch>
{
    Task<PagedResult<TResponse>> GetAsync(TSearch search);

    Task<TResponse?> GetByIdAsync(int id);
}