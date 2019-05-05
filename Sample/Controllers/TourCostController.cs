using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Models;
using Sample.DataAccess;


namespace Sample.Controllers
{
    public class TourCostController : Controller
    {
        // GET: TourCost
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TourCost()
        {
            TourLeaderDAO tourLeaderDAO = new TourLeaderDAO();
            tourLeaderDAO.OpenConnection();

            FormTourCost ftc = new FormTourCost();
            ftc.tourLeader = new TourLeader();
            
            tourLeaderDAO.CloseConnection();

            

            return View(ftc);
        }

        [HttpPost]
        public ActionResult TourCost(FormTourCost formTourCost)
        {
            TourLeaderDAO tourLeaderDAO = new TourLeaderDAO();
            tourLeaderDAO.OpenConnection();
            formTourCost.tourLeader = tourLeaderDAO.RetrieveLeaderRank(Convert.ToString(formTourCost.tourLeader.TourLeaderId));


            if (formTourCost.tourLeader == null)
            {
                formTourCost.tourLeader = new TourLeader();
            }

            //polymorphism applied on line 53. tourleader.GetRate() executes different method body depending on whether tourleader is part time or full time.
            formTourCost.tourLeadCost = formTourCost.tourLeader.GetRate()* formTourCost.noOfDays;

            tourLeaderDAO.CloseConnection();

            return View(formTourCost);
        }
    }
}