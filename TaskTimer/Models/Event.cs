using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTimer.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int UserId { get; set; }

        public string Description { get; set; }
    }
}