using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Interfaces;

namespace eFarmCommerce.WebAPI.Controllers;

public class FavoritiController
    : BaseCRUDController<FavoritResponse, FavoritSearchObject, FavoritInsertRequest, FavoritUpdateRequest>
{
    public FavoritiController(IFavoritService service)
        : base(service)
    {
    }
}