using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.DataAccess;
using Sample.Models;

namespace Sample.Controllers
{
    public class AssignTourLeaderController : Controller
    {
        // GET: AssignTourLeader

        public static int selectedSessionTourId = 0;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AssignSuccess()
        {
            return View();
        }

        public ActionResult AssignTourLeader()
        {
            TourDAO tourDAO = new TourDAO();
            tourDAO.OpenConnection();
            FormAssignLeader formAssignLeader = new FormAssignLeader();
            formAssignLeader.LeaderList = tourDAO.findAvailableTourLeaders(Convert.ToString(formAssignLeader.selectedTourId));
          
            tourDAO.CloseConnection();

            return View(formAssignLeader);
        }

        [HttpPost]
        public ActionResult AssignTourLeader(FormAssignLeader formAssignLeader)
        {
            // refresh page to show available tourLeaders
            TourDAO tourDAO = new TourDAO();
            tourDAO.OpenConnection();
            formAssignLeader.LeaderList = tourDAO.findAvailableTourLeaders(Convert.ToString(formAssignLeader.selectedTourId));

            //assignleader if leader is selected.
            if (formAssignLeader.selectedLeaderId != 0)
            {
                tourDAO.assignLeader(Convert.ToString(selectedSessionTourId), Convert.ToString(formAssignLeader.selectedLeaderId));                
                return RedirectToAction("AssignSuccess");
            }

            tourDAO.CloseConnection();
            selectedSessionTourId = formAssignLeader.selectedTourId;

            if (formAssignLeader.LeaderList.Count == 0)
            {
                ViewData["ErrorMessage"] = "TourID has been assigned a TourLeader or TourID does not exist. Please select another TourID";
            }
            return View(formAssignLeader);
        }
    }
}