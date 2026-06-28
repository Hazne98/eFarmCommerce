using System;
using System.Collections.Generic;
using System.Text;

namespace eFarmCommerce.Services.Interfaces;

public interface IBaseCRUDService<TResponse, TSearch, TInsert, TUpdate>
    : IBaseReadService<TResponse, TSearch>
{
    Task<TResponse> InsertAsync(TInsert request);

    Task<TResponse?> UpdateAsync(int id, TUpdate request);

    Task<bool> DeleteAsync(int id);
}
