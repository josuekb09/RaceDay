namespace RaceDayApi.DTOs
{
    public record RegisterDto(string FullName, string Email, string Password, string Role);
    public record LoginDto(string Email, string Password);
    public record UserProfileDto(int UserId, string FullName, string Email, string Role);

    public record CreateEventDto(string Name, string Description, DateTime Date, string Location, string EventType);
    public record EventResponseDto(int EventId, string Name, string Description, DateTime Date, string Location, string EventType, int OrganiserId);

    public record CreateCategoryDto(int EventId, string Name, double DistanceKm);
    public record CreateEnrolmentDto(int EventId, int CategoryId);

    public record CaptureResultDto(int EnrolmentId, TimeSpan FinishTime, int FinishingPosition);
}