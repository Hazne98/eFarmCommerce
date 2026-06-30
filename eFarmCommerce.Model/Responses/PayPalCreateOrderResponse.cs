namespace eFarmCommerce.Model.Responses;

public class PayPalCreateOrderResponse
{
    public int PlacanjeId { get; set; }
    public string PayPalOrderId { get; set; } = null!;
    public string ApprovalUrl { get; set; } = null!;
}