using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HareKrishnaNamaHattaWebApp.Controllers
{
    [AdminAuthorize]
    public class BaseController : Controller
    {
        private readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected bool IsAdminLoggedIn()
        {
            try
            {
                var sessionValue = HttpContext?.Session?.GetString("AdminLoggedIn");

                // Validate session value
                if (!string.IsNullOrEmpty(sessionValue) && sessionValue.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return false safely
                _logger.LogError(ex, "Error while checking admin login session.");
            }

            return false;
        }
    }
}
