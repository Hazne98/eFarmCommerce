namespace eFarmCommerce.Model.Responses;

public class PagedResult<T>
{
    public int Count { get; set; }
    public List<T> Result { get; set; } = new();
}