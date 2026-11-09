// RaceDayMvc/Controllers/ParticipantController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using RaceDayMvc.Models;

namespace RaceDayMvc.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ParticipantController(IHttpClientFactory httpClientFactory)
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

        private bool IsParticipant() => HttpContext.Session.GetString("UserRole") == "Participant";

        // 1. Home Page: Browse Upcoming Events
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Auth");

            var client = GetAuthenticatedClient();
            var response = await client.GetAsync("api/events");

            var events = response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<List<EventViewModel>>()
                : new List<EventViewModel>();

            return View(events);
        }

        // 2. View Event Details
        [HttpGet]
        public async Task<IActionResult> EventDetails(int id)
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Auth");

            var client = GetAuthenticatedClient();
            var response = await client.GetAsync($"api/events/{id}");

            if (response.IsSuccessStatusCode)
            {
                var ev = await response.Content.ReadFromJsonAsync<EventViewModel>();
                return View(ev);
            }

            return NotFound();
        }

        // 3. Enroll in an Event
        [HttpPost]
        public async Task<IActionResult> Enroll(int eventId, string category)
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Auth");

            var client = GetAuthenticatedClient();
            var enrollmentData = new { EventId = eventId, Category = category };

            var response = await client.PostAsJsonAsync("api/enrollments", enrollmentData);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MyEvents");
            }

            TempData["Error"] = "Failed to enroll. Please try again.";
            return RedirectToAction("EventDetails", new { id = eventId });
        }

        // 4. View My Enrolled Events
        [HttpGet]
        public async Task<IActionResult> MyEvents()
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Auth");

            var client = GetAuthenticatedClient();
            var response = await client.GetAsync("api/enrollments/my-events");

            var enrollments = response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<List<EnrollmentViewModel>>()
                : new List<EnrollmentViewModel>();

            return View(enrollments);
        }

        // 5. View My Results
        [HttpGet]
        public async Task<IActionResult> MyResults()
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Auth");

            var client = GetAuthenticatedClient();
            var response = await client.GetAsync("api/results/my-results");

            var results = response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<List<ResultViewModel>>()
                : new List<ResultViewModel>();

            return View(results);
        }
    }
}