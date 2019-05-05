using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sample.Models
{
    public class Tour
    {
        [Display(Name = "Tour ID")]
        public int TourID{ get; set; }
        public string TourName { get; set; }
        public int TourLeaderID { get; set; }
        public string TourDestination { get; set; }
        public int TourLeaderName { get; set; }
    }
}