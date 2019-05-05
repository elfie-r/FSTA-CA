using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Sample.Models;

namespace Sample.DataAccess
{
    public class TourDAO
    {
        SqlConnection cn;
        SqlCommand cmdFindAvailLeader;
        SqlCommand cmdAssignLeader;
        SqlParameter pTourId;
        SqlParameter pTourLeaderId;

        public TourDAO()
        {
            string connectionString = "Data Source=;Initial Catalog=Sample;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cn = new SqlConnection(connectionString);
            
        }

        public void OpenConnection()
        {
            cn.Open();
        }

        public void CloseConnection()
        {
            if (cn != null)
            {
                cn.Close();
            }

        }
        public List<TourLeader> findAvailableTourLeaders (string id)
        {
            List<TourLeader> leaderList = new List<TourLeader>();
            bool tourAssigned = false;
            cmdFindAvailLeader = new SqlCommand();

            if (id == "0")
            {
                return leaderList;
            }
            

            //selects all available full time and partimers who opted for destination
            cmdFindAvailLeader.CommandText =
                "select tourleaderId, tourleaderName, contactno, EmailAddress, rank, availability from tourLeader" +
                " where tourleader.Availability = 0 and NOT(Rank = 'PT') UNION" +
                " select tourleader.tourleaderId, tourleaderName, contactno, EmailAddress, rank, availability from tourleader, tourLeaderDestination, tour" +
                " where tourleader.TourLeaderID = TourLeaderDestination.TourLeaderID AND Tour.TourID = @TourID" +
                " AND tour.TourDestination = TourLeaderDestination.DestinationOpted" +
                " AND tourleader.Availability = 0 AND tour.TourLeaderID IS NULL;" +
                " select count(*) as Counter from tour where tourID = @TourID and tourLeaderID IS NULL";


            cmdFindAvailLeader.Connection = cn;

            pTourId = new SqlParameter();
            pTourId.ParameterName = "@TourID";
            cmdFindAvailLeader.Parameters.Add(pTourId);

            
            pTourId.Value = id;

            SqlDataReader rd = cmdFindAvailLeader.ExecuteReader();

            while (rd.Read())
            {
                TourLeader t = new TourLeader();

                t.TourLeaderId = (int)rd["TourLeaderId"];
                t.Name = (string)rd["TourLeaderName"];
                t.ContactNo = (int)rd["ContactNo"];
                t.EmailAddress = (string)rd["EmailAddress"];
                t.Rank = (string)rd["rank"];
                t.Availability = (int)rd["Availability"];

                leaderList.Add(t);
            }
            rd.NextResult();

            //checks if tour has already been assigned.
            while (rd.Read())
            {
                if ((int)rd["counter"] == 1)
                {
                    tourAssigned = false;
                }
                else
                {
                    tourAssigned = true;
                }

            }

            //close reader
            rd.Close();

            if (tourAssigned)
            {
                leaderList = new List<TourLeader>();
                return leaderList;
            }
            else
            {
                return leaderList;
            }
        }

        

        public void assignLeader (string tourId, string tourLeaderId)
        {

            // code to assign TourLeaderID into database
            cmdAssignLeader = new SqlCommand();
            cmdAssignLeader.CommandText = "update TourLeader set Availability = 1 where TourLeaderID = @TourLeaderID;" + 
                                             "update tour set TourLeaderID = @TourLeaderID where tourID = @TourID; ";
            cmdAssignLeader.Connection = cn;

            pTourId = new SqlParameter();
            pTourId.ParameterName = "@TourID";
            cmdAssignLeader.Parameters.Add(pTourId);

            pTourLeaderId = new SqlParameter();
            pTourLeaderId.ParameterName = "@TourLeaderID";
            cmdAssignLeader.Parameters.Add(pTourLeaderId);


            pTourId.Value = tourId;
            pTourLeaderId.Value = tourLeaderId;

            cmdAssignLeader.ExecuteNonQuery();

            


        }
    }
}