using eFarmCommerce.Model.Exceptions;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Interfaces;
using FluentValidation;
using MapsterMapper;

namespace eFarmCommerce.Services.Implementations;

public class BaseCRUDService<TResponse, TSearch, TEntity, TInsert, TUpdate>
    : BaseReadService<TResponse, TSearch, TEntity>,
      IBaseCRUDService<TResponse, TSearch, TInsert, TUpdate>
    where TEntity : class
{
    private readonly IValidator<TInsert>? _insertValidator;
    private readonly IValidator<TUpdate>? _updateValidator;

    public BaseCRUDService(
        eFarmCommerceDbContext context,
        IMapper mapper,
        IValidator<TInsert>? insertValidator = null,
        IValidator<TUpdate>? updateValidator = null)
        : base(context, mapper)
    {
        _insertValidator = insertValidator;
        _updateValidator = updateValidator;
    }

    public virtual async Task<TResponse> InsertAsync(TInsert request)
    {
        if (_insertValidator != null)
        {
            var result = await _insertValidator.ValidateAsync(request);

            if (!result.IsValid)
                throw new ClientException(result.Errors.First().ErrorMessage);
        }

        var entity = Mapper.Map<TEntity>(request);

        Context.Set<TEntity>().Add(entity);

        await Context.SaveChangesAsync();

        return Mapper.Map<TResponse>(entity);
    }

    public virtual async Task<TResponse?> UpdateAsync(int id, TUpdate request)
    {
        if (_updateValidator != null)
        {
            var result = await _updateValidator.ValidateAsync(request);

            if (!result.IsValid)
                throw new ClientException(result.Errors.First().ErrorMessage);
        }

        var entity = await Context.Set<TEntity>().FindAsync(id);

        if (entity == null)
            return default;

        Mapper.Map(request, entity);

        await Context.SaveChangesAsync();

        return Mapper.Map<TResponse>(entity);
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);

        if (entity == null)
            return false;

        Context.Set<TEntity>().Remove(entity);

        await Context.SaveChangesAsync();

        return true;
    }
}