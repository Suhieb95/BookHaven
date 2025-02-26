using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace BookHaven.API.Controllers;
[AllowAnonymous]
public class CheckoutController(IOptions<StripeSettings> stripeSettings) : BaseController
{
    private readonly StripeSettings _stripeSettings = stripeSettings.Value;
    [HttpPost]
    public async Task<IActionResult> Index()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);

            // Handle the event
            // If on SDK version < 46, use class Events instead of EventTypes
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                // Then define and call a method to handle the successful payment intent.
                // handlePaymentIntentSucceeded(paymentIntent);
            }
            else if (stripeEvent.Type == EventTypes.PaymentMethodAttached)
            {
                var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                // Then define and call a method to handle the successful attachment of a PaymentMethod.
                // handlePaymentMethodAttached(paymentMethod);
            }
            // ... handle other event types
            else
            {
                // Unexpected event type
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("confirm-payment")]
    public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentRequest request)
    {
        StripeConfiguration.ApiKey = _stripeSettings.Secretkey;

        var service = new SessionService();
        Session session = await service.GetAsync(request.SessionId);

        if (session.PaymentStatus == "paid")
        {
            // Handle success (e.g., update order status, notify user, etc.)
            return Ok(new { message = "Payment successful" });
        }

        return BadRequest(new { message = "Payment failed" });
    }
    [HttpPost("create-session")]
    public async Task<ActionResult> CreateCheckoutSession([FromBody] CheckoutSessionRequest request)
    {
        StripeConfiguration.ApiKey = _stripeSettings.Secretkey;

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = ["card"],
            LineItems = request.LineItems.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Name,
                    },
                    UnitAmount = (long)item.Price * 100, // Stripe expects price in cents
                },
                Quantity = item.Quantity,
            }).ToList(),
            Mode = "payment",
            SuccessUrl = $"{request.SuccessUrl}?session_id={{CHECKOUT_SESSION_ID}}",
            CancelUrl = request.CancelUrl,
        };

        var service = new SessionService();
        Session session = await service.CreateAsync(options);

        return Ok(new { id = session.Id });
    }
}
public class ConfirmPaymentRequest
{
    public string SessionId { get; set; } = default!;
}
public class CheckoutSessionRequest
{
    public List<LineItem> LineItems { get; set; } = default!;
    public string SuccessUrl { get; set; } = default!;
    public string CancelUrl { get; set; } = default!;
}

public class LineItem
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}