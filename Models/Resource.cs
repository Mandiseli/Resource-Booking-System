using ResourceBookingSystem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternalResourceBookingSystem.Models
{
    public class Resource
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<Booking>? Bookings { get; set; }
    }
}
