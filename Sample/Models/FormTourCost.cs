using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Sample.Models
{
    public class FormTourCost
    {
        public TourLeader tourLeader { get; set; }

        [Display(Name = "Days")]
        public int noOfDays { get; set; }
        [Display(Name = "Cost of Tour Leader")]
        public double tourLeadCost { get; set; } = 0;
    }
}