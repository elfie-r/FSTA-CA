using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample.DataAccess;

namespace Sample.Models
{
    public class PartTimeTourLeader : TourLeader
    {
        public override double GetRate()
        {
            TourLeaderDAO tlDAO = new TourLeaderDAO();
            tlDAO.OpenConnection();
            dailyRate = tlDAO.RetrieveDailyRate("PT");
            tlDAO.CloseConnection();
            return dailyRate;
        }
    }
}