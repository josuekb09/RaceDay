// RaceDayMvc/Models/OrganiserModels.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RaceDayMvc.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event Name is required")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = "Distance must be greater than 0")]
        public double Distance { get; set; }

        [Required]
        [Display(Name = "Event Type (Run, Walk, Cycle)")]
        public string EventType { get; set; }

        // This receives the Azure Blob URL from your API
        public string BannerImageUrl { get; set; }

        // This handles the physical file upload from the browser to your MVC app
        [Display(Name = "Upload Banner Image")]
        public IFormFile BannerImage { get; set; }
    }

    public class DashboardViewModel
    {
        public int TotalEvents { get; set; }
        public int TotalEnrollments { get; set; }
        public List<EventViewModel> UpcomingEvents { get; set; } = new List<EventViewModel>();
    }
}