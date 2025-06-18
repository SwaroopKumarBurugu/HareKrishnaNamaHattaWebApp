using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class AdminAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var session = context.HttpContext.Session;
        var isAdmin = session?.GetString("AdminLoggedIn");

        if (string.IsNullOrEmpty(isAdmin) || !isAdmin.Equals("true", StringComparison.OrdinalIgnoreCase))
        {
            // Not logged in, redirect to login
            context.Result = new RedirectToActionResult("Login", "Admin", null);
        }

        base.OnActionExecuting(context);
    }
}
