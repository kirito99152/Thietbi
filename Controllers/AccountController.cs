using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thietbi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Thietbi.Controllers
{
    [AllowAnonymous] // Cho phép truy cập mà không cần xác thực
    public class AccountController : Controller
    {
        //private readonly string _apiBaseUrl = "http://10.0.28.54:8080/api/user";

        //Chạy trên internet
        private readonly string _apiBaseUrl = "http://14.0.22.12:8080/api/user";

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var jsonData = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"{_apiBaseUrl}/register", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Đăng ký thành công, hãy đăng nhập.";
                    return RedirectToAction("Login");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = JsonDocument.Parse(responseBody).RootElement.GetProperty("message").GetString();
                    return View(model);
                }
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var jsonData = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"{_apiBaseUrl}/login", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                using (var doc = JsonDocument.Parse(responseBody))
                {
                    var root = doc.RootElement;
                    int success = root.GetProperty("success").GetInt32();
                    string message = root.GetProperty("message").GetString();

                    if (success == 1)
                    {
                        int idUser = root.GetProperty("idUser").GetInt32();

                        // Tạo các Claim
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, idUser.ToString()),
                            new Claim(ClaimTypes.Name, model.Username)
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true, // Giữ đăng nhập khi đóng trình duyệt
                            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                        };

                        // Đăng nhập người dùng
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties
                        );

                        TempData["Message"] = message;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Error = message;
                        return View(model);
                    }
                }
            }
        }

        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}