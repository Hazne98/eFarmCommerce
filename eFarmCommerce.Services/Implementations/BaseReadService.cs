using eFarmCommerce.Model.Responses;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Interfaces;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace eFarmCommerce.Services.Implementations;

public class BaseReadService<TResponse, TSearch, TEntity>
    : IBaseReadService<TResponse, TSearch>
    where TEntity : class
{
    protected readonly eFarmCommerceDbContext Context;
    protected readonly IMapper Mapper;

    public BaseReadService(eFarmCommerceDbContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public virtual async Task<PagedResult<TResponse>> GetAsync(TSearch search)
    {
        var query = Context.Set<TEntity>().AsQueryable();

        query = AddFilter(query, search);

        var count = await query.CountAsync();

        query = AddPaging(query, search);

        var list = await query.ToListAsync();

        return new PagedResult<TResponse>
        {
            Count = count,
            Result = Mapper.Map<List<TResponse>>(list)
        };
    }

    public virtual async Task<TResponse?> GetByIdAsync(int id)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);

        if (entity == null)
            return default;

        return Mapper.Map<TResponse>(entity);
    }

    protected virtual IQueryable<TEntity> AddFilter(IQueryable<TEntity> query, TSearch search)
    {
        return query;
    }

    protected virtual IQueryable<TEntity> AddPaging(IQueryable<TEntity> query, TSearch search)
    {
        var pageProperty = search?.GetType().GetProperty("Page");
        var pageSizeProperty = search?.GetType().GetProperty("PageSize");

        if (pageProperty == null || pageSizeProperty == null)
            return query;

        var page = (int)(pageProperty.GetValue(search) ?? 1);
        var pageSize = (int)(pageSizeProperty.GetValue(search) ?? 10);

        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = 10;

        if (pageSize > 100)
            pageSize = 100;

        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}