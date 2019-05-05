using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample.DataAccess;

namespace Sample.Models
{
    public class FullTimeTourLeader : TourLeader
    {
        public int Salary { get; set; }
        public int LeaveEntitled { get; set; }

        public override double GetRate()
        {
            TourLeaderDAO tlDAO = new TourLeaderDAO();
            tlDAO.OpenConnection();
            
            switch (Rank) {
                case "M1":
                    dailyRate = tlDAO.RetrieveDailyRate("M1");
                    break;
                case "M2":
                     dailyRate = tlDAO.RetrieveDailyRate("M2");
                    break;
                case "M3":
                    dailyRate = tlDAO.RetrieveDailyRate("M3");
                    break;
                default:
                    return 0;
            }

            tlDAO.CloseConnection();
            return dailyRate;


        }
    }
}