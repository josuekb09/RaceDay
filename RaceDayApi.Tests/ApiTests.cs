using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RaceDayApi.DTOs;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace RaceDayApi.Tests
{
    public class ApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Test1_RegisterAndLogin_Success()
        {
            var regDto = new RegisterDto("Test Runner", "runner@test.com", "Secret123!", "Participant");
            var regRes = await _client.PostAsJsonAsync("/api/auth/register", regDto);
            Assert.Equal(HttpStatusCode.OK, regRes.StatusCode);

            var loginDto = new LoginDto("runner@test.com", "Secret123!");
            var loginRes = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
            Assert.Equal(HttpStatusCode.OK, loginRes.StatusCode);
        }

        [Fact]
        public async Task Test2_Organiser_CanManageEvents()
        {
            var loginDto = new LoginDto("organiser@raceday.co.za", "AdminPass123!");
            await _client.PostAsJsonAsync("/api/auth/login", loginDto);

            var eventDto = new CreateEventDto("Cape Town Cycle Tour", "Annual cycle race", DateTime.UtcNow.AddDays(30), "Cape Town", "Cycle");
            var res = await _client.PostAsJsonAsync("/api/events", eventDto);

            Assert.Equal(HttpStatusCode.Created, res.StatusCode);
        }

        [Fact]
        public async Task Test3_Participant_CannotCreateEvent_RoleRejected()
        {
            var loginDto = new LoginDto("participant@raceday.co.za", "UserPass123!");
            await _client.PostAsJsonAsync("/api/auth/login", loginDto);

            var eventDto = new CreateEventDto("Unauthorized Marathon", "Desc", DateTime.UtcNow.AddDays(10), "Durban", "Run");
            var res = await _client.PostAsJsonAsync("/api/events", eventDto);

            Assert.Equal(HttpStatusCode.Forbidden, res.StatusCode);
        }

        [Fact]
        public async Task Test4_Participant_Enrolment_Success()
        {
            var loginDto = new LoginDto("participant@raceday.co.za", "UserPass123!");
            await _client.PostAsJsonAsync("/api/auth/login", loginDto);

            var enrolDto = new CreateEnrolmentDto(1, 1);
            var res = await _client.PostAsJsonAsync("/api/enrolments", enrolDto);

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}