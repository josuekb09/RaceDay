// RaceDayMvc/Models/ParticipantModels.cs
using System;
using System.Collections.Generic;

namespace RaceDayMvc.Models
{
    public class EnrollmentViewModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Category { get; set; }
        public string Status { get; set; } // "Pending", "Confirmed"
    }

    public class ResultViewModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string FinishTime { get; set; }
        public string FinishingPosition { get; set; }
    }
}