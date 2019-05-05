using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sample.Models
{

    
    public class TourLeader
    {
        [Display(Name = "Tour Leader ID")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter an ID number greater than 0")]
        public int TourLeaderId { get; set; }
        public string Name { get; set; } = "null";
        [Display(Name = "Contact No")]
        public int ContactNo { get; set; } = 0;
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = "null";
        public string Rank { get; set; } = "null";
        [Display(Name = "Daily Rate")]
        public double dailyRate { get; set; } = 0;
        
        public int Availability { get; set; } = 0;
        
        public List<string> destinationOpted { get; set; }

        public int ComputeCost(int days)
        {
            return 4;
        }

        public virtual double GetRate()
        {
            return 0;
        }
    }
}