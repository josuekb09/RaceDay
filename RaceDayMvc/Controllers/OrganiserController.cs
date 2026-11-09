// RaceDayMvc/Controllers/OrganiserController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using RaceDayMvc.Models;
using System.Linq;

namespace RaceDayMvc.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrganiserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Helper to attach JWT token to API calls
        private HttpClient GetAuthenticatedClient()
        {
            var client = _httpClientFactory.CreateClient("RaceDayApi");
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }

        // Security Helper
        private bool IsOrganiser() => HttpContext.Session.GetString("UserRole") == "Organiser";

        public async Task<IActionResult> Dashboard()
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");

            var client = GetAuthenticatedClient();
            var response = await client.GetAsync("api/events"); // Adjust if your API endpoint differs

            var dashboardData = new DashboardViewModel();

            if (response.IsSuccessStatusCode)
            {
                var events = await response.Content.ReadFromJsonAsync<List<EventViewModel>>();
                if (events != null)
                {
                    dashboardData.TotalEvents = events.Count;
                    dashboardData.UpcomingEvents = events.OrderBy(e => e.Date).Take(5).ToList();
                    // If you have an endpoint for enrollments, call it here to populate TotalEnrollments
                }
            }

            return View(dashboardData);
        }

        public async Task<IActionResult> ManageEvents()
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");

            var client = GetAuthenticatedClient();
            var response = await client.GetAsync("api/events");

            var events = response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<List<EventViewModel>>()
                : new List<EventViewModel>();

            return View(events);
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventViewModel model)
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");

            ModelState.Remove("BannerImageUrl");
            ModelState.Remove("Id");

            if (!ModelState.IsValid) return View(model);

            var client = GetAuthenticatedClient();

            // Send as JSON to match your Part 2 API expectation
            var eventPayload = new
            {
                Name = model.Name,
                Description = model.Description ?? "",
                Date = model.Date,
                Location = model.Location,
                Distance = model.Distance,
                EventType = model.EventType,
                BannerImageUrl = "https://racedaystorage.blob.core.windows.net/banners/default.jpg"
            };

            var response = await client.PostAsJsonAsync("api/events", eventPayload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ManageEvents");
            }

            ModelState.AddModelError("", "Failed to create event. Ensure the API is running and connected.");
            return View(model);
        }
    }
}