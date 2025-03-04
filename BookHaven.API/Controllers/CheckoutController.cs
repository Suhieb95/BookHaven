using BookHaven.API.Common.Constants;
using BookHaven.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Stripe.Checkout;

namespace BookHaven.API.Controllers;

[AllowAnonymous] // To be changed
public class CheckoutController(IOptions<StripeSettings> stripeSettings, IOptions<SpecifiedOriginCorsPolicy> specifiedOriginCorsPolicy, IWebHostEnvironment _env) : BaseController
{
    private readonly StripeSettings _stripeSettings = stripeSettings.Value;
    private readonly string _clientURL = _env.IsDevelopment()
                                        ? specifiedOriginCorsPolicy.Value.LocalURL!
                                        : specifiedOriginCorsPolicy.Value.ProductionURL!;

    [HttpPost(ApiEndPoints.Stripe.CreateSession)]
    public async Task<IActionResult> CheckoutOrder(ProductRequest product)
    {
        string? publicKey = _stripeSettings.PublishableKey;
        var referer = Request.Headers.Referer.ToString();
        if (_clientURL + "/" != referer)
            return Forbid();

        var sessionId = await CreateCheckoutSession(product);
        return string.IsNullOrEmpty(sessionId)
            ? StatusCode(500, "Failed to create Stripe checkout session.")
            : Ok(new { SessionId = sessionId, PubKey = publicKey });
    }

    [HttpGet(ApiEndPoints.Stripe.CheckoutSuccess)]
    public async Task<IActionResult> CheckoutSuccess([FromQuery] string sessionId)
    {
        var sessionService = new SessionService();
        var session = await sessionService.GetAsync(sessionId);
        var total = session.AmountTotal;
        var customerEmail = session.CustomerDetails.Email;

        // Save order details to database here...

        return Ok(new { total, customerEmail });
    }
    private async Task<string> CreateCheckoutSession(ProductRequest request)
    {
        var options = new SessionCreateOptions
        {
            SuccessUrl = $"{_clientURL}checkout/success?sessionId={{CHECKOUT_SESSION_ID}}",
            CancelUrl = $"{_clientURL}checkout/cancel",
            PaymentMethodTypes = ["card"],
            LineItems = ProductRequest.ToLineItems(request.Products),
            Mode = "payment",
            CustomerEmail = request.CustomerEmail, // Prefill the customer's email
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);
        return session.Id;
    }
}

public class ProductRequest
{
    public List<Product> Products { get; set; } = default!;
    public string CustomerEmail { get; set; } = default!;
    public static List<SessionLineItemOptions> ToLineItems(List<Product> products)
    {
        List<SessionLineItemOptions> sessionLineItemOptions = [];
        foreach (Product product in products)
            sessionLineItemOptions.Add(
                  new SessionLineItemOptions()
                  {
                      PriceData = new SessionLineItemPriceDataOptions
                      {
                          UnitAmount = product.Price * 100, // Convert to fills
                          Currency = "AED",
                          ProductData = new SessionLineItemPriceDataProductDataOptions
                          {
                              Name = product.Title,
                              Description = product.Description,
                              Images = [product.ImageUrl]
                          },
                      },
                      Quantity = product.Quantity,
                  }
            );

        return sessionLineItemOptions;
    }
}