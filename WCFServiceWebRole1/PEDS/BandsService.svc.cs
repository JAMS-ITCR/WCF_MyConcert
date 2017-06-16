using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;
using WCFServiceWebRole1.AUX_OBJ;

namespace WCFServiceWebRole1.PEDS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BandsService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BandsService.svc or BandsService.svc.cs at the Solution Explorer and start debugging.
    public class BandsService : IBandsService
    {
        public String connectionString = "Data Source=peds.database.windows.net;Initial Catalog = MyConcert-DB;Integrated Security=False;User ID = Jams2017; Password=Peds2017";
        public String getGenders() {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open) {
                SqlCommand cmd = new SqlCommand("getGeneros", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                List<Gender> genders = new List<Gender>();
                while (reader.Read())
                {
                    Gender gender = new Gender();
                    gender.IdGenero = Int32.Parse(reader["IdGenero"].ToString());
                    gender.Nombre = reader["Nombre"].ToString();
                    genders.Add(gender);
                }
                json = new JavaScriptSerializer().Serialize(genders);
            }
            return json;
        }
        public String createBand(string nbanda, string idgenero, string description)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            int result = -3;
            if (con.State == System.Data.ConnectionState.Open)
            {
                try {
                    SqlCommand cmd = new SqlCommand("crearBanda", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NBanda", SqlDbType.VarChar).Value = nbanda;
                    cmd.Parameters.Add("@IdGenero", SqlDbType.Int).Value = Int32.Parse(idgenero);
                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = description;
                    /*excecute crearBanda @NBanda = 'Metallica', @IdGenero = 1,@Descripcion = 'Alguna descripcion'*/
                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    if (result == 100)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Creación exitosa" });
                    }
                    else if (result == 101)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " No se pudo crear, verifique los datos" });
                    }
                    else if (result == 102)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "El género musical no existe" });
                    }
                    else if (result == 103)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "La Banda ya existe" });
                    }
                    else
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = -7, value = "", info = "Resultado desconocido de la respuesta" });
                    }
                }
                catch (Exception e) {
                   return new JavaScriptSerializer().Serialize(new Result { id = -8, value = "", info = "Resultado desconocido" });
                }

                
            }
            return json;
        }

    }
}
