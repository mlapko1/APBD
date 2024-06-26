using System;
using System.ComponentModel.DataAnnotations;
using APBD_Task8.Models;

namespace APBD_Task8.Models
{
    public class ClientTrip
    {
        [Required]
        public int IdClient { get; set; }
        [Required]
        public int IdTrip { get; set; }
        [Required]
        public DateTime RegisteredAt { get; set; }
        public DateTime? PaymentDate { get; set; }

        public Client Client { get; set; }
        public Trip Trip { get; set; }
    }
}