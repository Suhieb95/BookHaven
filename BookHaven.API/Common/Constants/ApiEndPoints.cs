using Stripe.Tax;

namespace BookHaven.API.Common.Constants;
internal static class ApiEndPoints
{
    internal const string BaseController = "/api/v1/[controller]";
    internal abstract class BaseEndpoint
    {
        internal const string GetById = BaseController + "/{id}";
        internal const string Delete = BaseController + "/{id}";
    }
    internal sealed class Books : BaseEndpoint
    {
        internal const string DeleteBookImages = BaseController + "/book-images";
        internal const string UpdateBookImages = BaseController + "/update-book-images";
    }
    internal sealed class Person : BaseEndpoint
    {
        internal const string ConfirmEmailAddress = BaseController + "/confirm-email/{id}";
        internal const string Register = BaseController + "/register";
        internal const string Login = BaseController + "/login";
        internal const string ChangePassword = BaseController + "/change-password";
        internal const string VerifyResetPasswordToken = BaseController + "/verify-password-reset-token";
        internal const string ResetPasswordRequest = BaseController + "/reset-password-request";
        internal const string Logout = BaseController + "/logout";
        internal const string RemoveProfilePicture = BaseController + "/remove-profile-picture";
        internal const string RefreshToken = BaseController + "/refresh-token";
    }
    internal sealed class Stripe
    {
        internal const string CreateSession = BaseController + "/create-session";
        internal const string CheckoutSuccess = BaseController + "/checkout-success";
    }
}