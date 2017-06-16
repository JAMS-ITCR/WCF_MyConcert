using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFServiceWebRole1.AUX_OBJ;
using System.Web.Script.Serialization;
namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Countries" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Countries.svc or Countries.svc.cs at the Solution Explorer and start debugging.
    public class Countries : ICountriesService
    {
        public string connectionString = "Data Source=peds.database.windows.net;Initial Catalog = MyConcert-DB;Integrated Security=False;User ID = Jams2017; Password=Peds2017";
        String ICountriesService.getCountries()
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            String json = "";
            if (con.State == System.Data.ConnectionState.Open) {
                SqlCommand cmd = new SqlCommand("getPaises", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                List<Country> countries = new List<Country>();
                while (reader.Read())
                {
                    Country country = new Country();
                    country.IdPais = Int32.Parse(reader["IdPais"].ToString());
                    country.NombrePais = reader["NombrePais"].ToString();
                    countries.Add(country);
                }
                json = new JavaScriptSerializer().Serialize(countries);
            }
            return json;
        }

        int ICountriesService.test2(string username) {
            return -2;
        }
    }
}
