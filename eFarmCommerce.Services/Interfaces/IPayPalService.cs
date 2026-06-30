using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;

namespace eFarmCommerce.Services.Interfaces;

public interface IPayPalService
{
    Task<PayPalCreateOrderResponse> CreateOrderAsync(PayPalCreateOrderRequest request);
    Task<PlacanjeResponse> CaptureOrderAsync(PayPalCaptureOrderRequest request);
    Task<PovratRefundResponse> RefundAsync(PovratRefundInsertRequest request);
}