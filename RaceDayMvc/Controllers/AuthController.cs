// RaceDayMvc/Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RaceDayMvc.Models;

namespace RaceDayMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var client = _httpClientFactory.CreateClient("RaceDayApi");

            // Call the Part 2 API Login endpoint
            var response = await client.PostAsJsonAsync("api/auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

                // Store authentication state and role in Session
                // PASTE THIS INSTEAD:
                HttpContext.Session.SetString("UserRole", result.Role);
                HttpContext.Session.SetString("UserName", result.FullName);

                // Redirect based on user role
                if (result.Role == "Organiser")
                {
                    return RedirectToAction("Dashboard", "Organiser");
                }
                else
                {
                    return RedirectToAction("Index", "Participant");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your credentials.");
            return View(model);
        }

        public IActionResult Logout()
        {
            // Clear the session entirely
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}