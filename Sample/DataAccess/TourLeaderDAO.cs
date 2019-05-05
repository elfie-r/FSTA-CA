using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Sample.Models;

namespace Sample.DataAccess
{
    public class TourLeaderDAO
    {
        SqlConnection cn;
        SqlCommand cmdRetrieveLeaderRank;
        SqlCommand cmdRetrieveDailyRate;
        SqlParameter pTourLeaderId;
        SqlParameter pTourLeaderRank;

        public TourLeaderDAO()
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

        public TourLeader RetrieveLeaderRank(string id)
        {
            cmdRetrieveLeaderRank = new SqlCommand();
            cmdRetrieveLeaderRank.CommandText = "Select * from TourLeader WHERE TourLeaderID = @TourLeaderID";
            cmdRetrieveLeaderRank.Connection = cn;

            pTourLeaderId = new SqlParameter();
            pTourLeaderId.ParameterName = "@TourLeaderID";
            cmdRetrieveLeaderRank.Parameters.Add(pTourLeaderId);

            TourLeader t;
            pTourLeaderId.Value = id;

            SqlDataReader rd = cmdRetrieveLeaderRank.ExecuteReader();

            // through SQL, if rank is PT, assign new PartTimeTourLeader object to TourLeader variable.
            // else, assign new FullTimeTourLeader object to TourLeader variable.
            if (rd.Read())
            {
                if ((string)rd["rank"] == "PT")
                {
                    t = new PartTimeTourLeader();
                    
                }
                else
                {
                    t = new FullTimeTourLeader();
                }

                t.Rank = (string)rd["rank"];
                t.Name = (string)rd["TourLeaderName"];
                t.ContactNo = (int)rd["ContactNo"];
                t.EmailAddress = (string)rd["EmailAddress"];
                t.Availability = (int)rd["Availability"];

            }
            else
            {
                rd.Close();
                return null;
            }

            //close reader
            rd.Close();
            return t;
        }


        public double RetrieveDailyRate(string id)
        {
            cmdRetrieveDailyRate = new SqlCommand();
            cmdRetrieveDailyRate.CommandText = "Select * from RankSalary WHERE Rank = @TourLeaderRank";
            cmdRetrieveDailyRate.Connection = cn;

            pTourLeaderRank = new SqlParameter();
            pTourLeaderRank.ParameterName = "@TourLeaderRank";
            cmdRetrieveDailyRate.Parameters.Add(pTourLeaderRank);

            Double leaderRate;
            pTourLeaderRank.Value = id;

            SqlDataReader rd = cmdRetrieveDailyRate.ExecuteReader();

            
            if (rd.Read())
            {
                leaderRate = Convert.ToDouble((int)rd["DailyRate"]);

            }
            else
            {
                rd.Close();
                return 0;
            }

            //close reader
            rd.Close();
            return leaderRate;
        }
    }
}