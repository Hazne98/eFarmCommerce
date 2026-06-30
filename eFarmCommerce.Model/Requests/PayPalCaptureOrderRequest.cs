namespace eFarmCommerce.Model.Requests;

public class PayPalCaptureOrderRequest
{
    public string PayPalOrderId { get; set; } = null!;
}