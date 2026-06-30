using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eFarmCommerce.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PayPalController : ControllerBase
{
    private readonly IPayPalService _payPalService;

    public PayPalController(IPayPalService payPalService)
    {
        _payPalService = payPalService;
    }

    [HttpPost("create-order")]
    public async Task<PayPalCreateOrderResponse> CreateOrderAsync([FromBody] PayPalCreateOrderRequest request)
    {
        return await _payPalService.CreateOrderAsync(request);
    }

    [HttpPost("capture-order")]
    public async Task<PlacanjeResponse> CaptureOrderAsync([FromBody] PayPalCaptureOrderRequest request)
    {
        return await _payPalService.CaptureOrderAsync(request);
    }

    [HttpPost("refund")]
    public async Task<PovratRefundResponse> RefundAsync([FromBody] PovratRefundInsertRequest request)
    {
        return await _payPalService.RefundAsync(request);
    }
}