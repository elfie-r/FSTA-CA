using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sample.Models
{
    public class FormAssignLeader
    {
        public Tour Tour { get; set; } = new Tour();
        public TourLeader TourLeader { get; set; } = new TourLeader();
        public List<TourLeader> LeaderList { get; set; }
        [Display(Name = "Selected Leader ID")]
        [Range(0, double.MaxValue, ErrorMessage = "Tour LeaderID must be greater than zero")]
        public int selectedLeaderId { get; set; } = 0;
        [Display(Name = "Enter TourID ")]
        [Range(0, double.MaxValue, ErrorMessage = "TourID must be greater than zero")]
        public int selectedTourId { get; set; } = 0;

    }
}