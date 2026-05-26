using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.Account;
using Phoenix.Provider;
using System.Security.Claims;

namespace Phoenix.Web.UI.Controllers {
    public class AccountController : BaseController {

        [HttpGet]
        public IActionResult Login(string? returnUrl = "") {
            ViewBag.ReturnUrl = returnUrl;
            var dto = new LoginDTO();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto, string? returnUrl) { 
            try {
                if(ModelState.IsValid) {
                    var provider = AccountProvider.GetSingleton();
                    bool isAuthenticated = provider.IsAuthenticated(dto);
                    if (isAuthenticated) {
                        var claimPrincipal = GetClaim(dto);
                        await HttpContext.SignInAsync(claimPrincipal); // Proses SIGN IN (Authenticationnya)

                        if (returnUrl == null) {
                            return RedirectToAction("Index", "Home");
                        }
                        return Redirect(returnUrl);
                    }
                }
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data. {ex.Message}" });
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        #region Register

        [HttpGet]
        public IActionResult Register() {
            var dto = new UpsertGuestDTO();
            return View(dto);
        }

        [HttpPost]
        public IActionResult Register(UpsertGuestDTO dto) {
            try {
                if(ModelState.IsValid) {
                    AccountProvider.GetSingleton().RegisterGuest(dto);
                    return RedirectToAction("Login");
                }
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data. {ex.Message}" });
            }
            return View(dto);
        }

        #endregion

        private ClaimsPrincipal GetClaim(LoginDTO dto)
        {
            var provider = AccountProvider.GetSingleton();

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, dto.Username),
                //new Claim("username", dto.Username) => jika ingin menggunakan custom key
                new Claim(ClaimTypes.Role, dto.Role)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }

        [HttpPost]
        public IActionResult ChangePassword([FromBody] ChangeAccountPasswordDTO dto) { 
            try {
                if (ModelState.IsValid) {
                    var provider = AccountProvider.GetSingleton();
                    provider.ChangePassword(dto);
                    return Json(new { isException = false, isValidation = false, data = dto });
                }
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
            var errors = GetValidationErrorMessage(ModelState);
            return Json(new { isException = false, isValidation = true, validations = errors });
        }

        [HttpGet]
        public IActionResult AccessDenied() { 
            return View();
        }
    }
}
