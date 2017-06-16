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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BillboardServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BillboardServices.svc or BillboardServices.svc.cs at the Solution Explorer and start debugging.
    public class BillboardServices : IBillboardServices
    {
        string country = "test";
        public string connectionString = "Data Source=peds.database.windows.net;Initial Catalog = MyConcert-DB;Integrated Security=False;User ID = Jams2017; Password=Peds2017";
        public String createBillboard(
                       string name,
                       string country,
                       string place,
                       string dateVotation,
                       string startdate,
                       string enddate,
                       string state
                       )
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open) {
                SqlCommand cmd = new SqlCommand("crearCartelera", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@IdPais", SqlDbType.Int).Value = Int32.Parse(country);
                cmd.Parameters.Add("@Lugar", SqlDbType.VarChar).Value = place;
                cmd.Parameters.Add("@CierreVotacion", SqlDbType.VarChar).Value = dateVotation;
                cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar).Value = startdate;
                cmd.Parameters.Add("@FechaFinal", SqlDbType.VarChar).Value = enddate;
                cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = Int32.Parse(state);
                /*
                    @Nombre varchar(100),
                    @IdPais int,
                    @Lugar varchar(100),
                    @CierreVotacion datetime,
                    @FechaInicio datetime,
                    @FechaFinal datetime,
                    @Estado bit
                */
                SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                result = (int)returnParameter.Value;
                if (result == 100)
                {
                    Console.Write("Creacion exitosa");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Creacion exitosa" });
                }
                else if (result == 101)
                {
                    //No existe usuario
                    Console.Write(" Problema al insertar");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Problema al insertar" });
                }
                else if (result == 102)
                {
                    Console.Write(" Nombre de la Cartelera ya ha sido usado");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Nombre de la Cartelera ya ha sido usado" });
                }
                else
                {
                    Console.Write("Resultado Desconocido");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Resultado desconocido" });
                }

            }
            return json;
        }



        public String addCategoryToBillboard(
            string idcategoria,
            string idcartelera) {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("asignarCategoriaCartelera", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = Int32.Parse(idcategoria);
                cmd.Parameters.Add("@IdCartelera", SqlDbType.Int).Value = Int32.Parse(idcartelera);
                /*
                  @IdCategoria int,
                  @IdCartelera int
                */
                SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                result = (int)returnParameter.Value;
                if (result == 100)
                {
                    Console.Write("Asignacion Exitosa");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Asignacion Exitosa" });
                }
                else if (result == 101)
                {
                    //No existe usuario
                    Console.Write(" No se pudo asignar(la categoria ya fue asignada a la cartelera, etc");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " No se pudo asignar(la categoria ya fue asignada a la cartelera, etc" });
                }
                else if (result == 102)
                {
                    Console.Write("Cartelera no existe");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Cartelera no existe" });
                }
                else
                {
                    Console.Write("Categoria no existe");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Categoria no existe" });
                }

            }

            return json;
        }

        public String addBandCategoryBillboard(
                string idcategoria,
                string idcartelera,
                string idbanda
            )
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("asignarBandaCategoriaCartelera", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = Int32.Parse(idcategoria);
                cmd.Parameters.Add("@IdCartelera", SqlDbType.Int).Value = Int32.Parse(idcartelera);
                cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value = Int32.Parse(idbanda);
                /*
                    @IdCategoria int,
                    @IdCartelera int,
                    @IdBanda int
                */
                SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                result = (int)returnParameter.Value;
                if (result == 100)
                {
                    Console.Write("Asignacion Exitosa");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Asignacion Exitosa" });
                }
                else if (result == 101)
                {
                    //No existe usuario
                    Console.Write("No se pudo asignar(la bandaya fue asignada a la categoria, etc)");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "No se pudo asignar(la bandaya fue asignada a la categoria, etc)" });
                }
                else if (result == 102)
                {
                    Console.Write("Cartelera no existe");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Cartelera no existe" });
                }
                else if (result == 103)
                {
                    Console.Write(" Categoria no existe");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Categoria no existe" });
                }
                else if (result == 104)
                {
                    Console.Write(" La Banda no Existe");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "  La Banda no Existe" });
                }
                else if (result == 105)
                {
                    Console.Write(": La categoría no ha sido asignada a la Cartelera");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " : La categoría no ha sido asignada a la Cartelera" });
                }
                else if (result == 106)
                {
                    Console.Write("La banda ya fue asignada a otra categoria");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "La banda ya fue asignada a otra categoria" });
                }
                else
                {
                    Console.Write("Categoria no existe");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Resultado Desconocido" });
                }

            }

            return json;
        }

        public String getBillBoard() {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand cmd = new SqlCommand("getCarteleras", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    reader = cmd.ExecuteReader();
                    
                }
                
                catch (Exception e) {
                    return new JavaScriptSerializer().Serialize(new Result { id = -11, value = "", info = "Error en la consulta CMD." });
                }
                List<BillBoard> billboards = new List<BillBoard>();
                while (reader.Read())

                {
                    BillBoard billboard = new BillBoard();
                    billboard.IdCartelera = Int32.Parse(reader["IdCartelera"].ToString());
                    billboard.Nombre = reader["Nombre"].ToString();
                    billboard.IdPais = Int32.Parse(reader["IdPais"].ToString());
                    billboard.Lugar = reader["Lugar"].ToString();

                    billboard.FechaInicio = reader["FechaInicio"].ToString();
                    billboard.FechaFinal = reader["FechaFinal"].ToString();
                    billboard.Estado = (bool)reader["Estado"];
                    
                    
                    

                    billboards.Add(billboard);
                }
                json = new JavaScriptSerializer().Serialize(billboards);
            }
            return json;
            
        }

        public String getFestivals() {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand cmd = new SqlCommand("getFestivales", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    reader = cmd.ExecuteReader();

                }

                catch (Exception e)
                {
                    return new JavaScriptSerializer().Serialize(new Result { id = -11, value = "", info = "Error en la consulta CMD." });
                }
                List<Festival> billboards = new List<Festival>();
                while (reader.Read())

                {
                    Festival festival = new Festival();
                    festival.IdFestival = Int32.Parse(reader["IdFestival"].ToString());
                    festival.Nombre = reader["Nombre"].ToString();
                    festival.IdPais = Int32.Parse(reader["IdPais"].ToString());

                    festival.Lugar = reader["Lugar"].ToString();
                    festival.FechaInicio = reader["FechaInicio"].ToString();
                    festival.FechaFinal = reader["FechaFinal"].ToString();
                    festival.Transporte = reader["Transporte"].ToString();
                    festival.Comida = reader["Comida"].ToString();
                    festival.Servicios = reader["Servicios"].ToString();
                    festival.IdCartelera = Int32.Parse(reader["IdCartelera"].ToString());
                    festival.IdBanda = Int32.Parse(reader["IdBanda"].ToString());                    
                    festival.Estado = (bool)reader["Estado"];

                    billboards.Add(festival);
                }
                json = new JavaScriptSerializer().Serialize(billboards);
            }
            return json;
        }
    }
}
