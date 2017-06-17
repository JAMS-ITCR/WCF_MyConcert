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
                    billboard.NombrePais = reader["NombrePais"].ToString();
                    billboard.FechaInicio = reader["FechaInicio"].ToString();
                    billboard.CierreVotacion = reader["CierreVotacion"].ToString();
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
                try
                {


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
                catch (Exception e) {
                    return new JavaScriptSerializer().Serialize(new Result { id = -12, value = e.ToString(), info = "Error en la consulta CMD." });
                }
            }
            return json;
        }

       
        public String crearFestival( string transporte, string comida, string servicios, string idcartelera, string idbanda, string estado)
        {
            /*
             * execute crearFestival @NFestival = 'NombreFest', @Transporte = 'Info', @Comida = 'Info', @Servicios = 'Info', @IdCartelera = 1, @IdBanda int = 8, @Estado = 0
             * @Transporte varchar(500),
                @Comida varchar(500),
                @Servicios varchar(500),
                @IdCartelera int,
                @IdBanda int,
                @Estado bit
             */
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            try
            {

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                
                int result = -3;
                if (con.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("crearFestival", con);
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("@Transporte", SqlDbType.VarChar).Value = transporte.ToString();
                        cmd.Parameters.Add("@Comida", SqlDbType.VarChar).Value = comida.ToString();
                        cmd.Parameters.Add("@Servicios", SqlDbType.VarChar).Value = servicios.ToString();
                        cmd.Parameters.Add("@IdCartelera", SqlDbType.Int).Value = Int32.Parse(idcartelera.ToString());
                        cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value = Int32.Parse(idbanda.ToString());
                        cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Int32.Parse(estado.ToString());

                        SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();
                        result = (int)returnParameter.Value;
                        /*
                         * 100: Creacion Exitosa del festival
                        101: No se pudo crear el Festival, ya el nombre fue usado, no existe la recomendacion del chef, etc.
                        102: Las votaciones no han terminado
                        103: La cartelera ya ha sido usada para un festival
                        104: La cartelera no existe
                                             */
                        if (result == 100)
                        {
                            json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Creacion Exitosa del festival" });
                        }
                        else if (result == 101)
                        {
                            json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "  No se pudo crear el Festival, ya el nombre fue usado, no existe la recomendacion del chef, etc." });
                        }
                        else if (result == 102)
                        {
                            json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Las votaciones no han terminado" });
                        }
                        else if (result == 103)
                        {
                            json = new JavaScriptSerializer().Serialize(new Result { id = -7, value = "", info = "La cartelera ya ha sido usada para un festival" });
                        }
                        else
                        {
                            json = new JavaScriptSerializer().Serialize(new Result { id = -7, value = "", info = "La cartelera no existe" });
                        }
                    }
                    catch (Exception e)
                    {
                        return new JavaScriptSerializer().Serialize(new Result { id = -8, value = e.ToString(), info = "ERROR: Resultado desconocido" });
                    }
                }
            }
            catch (Exception e)
            {
                return new JavaScriptSerializer().Serialize(new Result { id = -8, value = e.ToString(), info = "ERROR: Resultado desconocido [1]" });
            }
            return json;
        }

        public String getPreInfoFestival(string IdCartelera) {
            /*getPreInfoFestival @IdCartelera = 1*/
            /*101: No se pudo obtener la informacion
            102: Las votaciones no han terminado
            103: La Cartelera está inactiva
            104: La cartelera no existe*/

            return "ok";
        }

        public String asignarBandaCategoriaFestival(string IdCategoria) {
            /* asignarBandaCategoriaFestival @IdCategoria = 2*/
            return "ok";
        }

        public String getBandasCategoriasFestival(string IdFestival) {
            return "ok";
        }

        public String eliminarBandaFestival(string idfestival, string idbanda)
        {
            /*execute eliminarBandaFestival @IdFestival = 1, @IdBanda = 3*/
            SqlConnection con;
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    /*
                     * @IdFestival int,
                       @IdBanda int
                     */
                    SqlCommand cmd = new SqlCommand("eliminarBandaFestival", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdFestival", SqlDbType.Int).Value = Int32.Parse(idfestival);
                    cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value = Int32.Parse(idbanda);
               

                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    /*
                     100: Eliminacion exitosa
                    101: No se pudo eliminar
                    102: La Banda  debe tener al menos 1 Banda
                    103: La Banda no ha sido asignada a este festival
                    104: La Banda no existe
                    105: Festival no existe*/

                    if (result == 100)
                    {
                    
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Eliminacion exitosa" });
                    }
                    else if (result == 101)
                    {
                    
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " No se pudo eliminar" });
                    }
                    else if (result == 102)
                    {
                    
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "La Banda  debe tener al menos 1 Banda" });
                    }
                    else if (result == 103)
                    {

                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "La Banda no ha sido asignada a este festival" });
                    }
                    else if (result == 104)
                    {

                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "La Banda no existe" });
                    }
                    else if (result == 105)
                    {

                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Festival no existe" });
                    }
                    else
                    {
                        Console.Write("Categoria no existe");
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Categoria no existe" });
                    }

                }
            }
            catch (Exception e)
            {
                return new JavaScriptSerializer().Serialize(new Result { id = result, value =e.ToString(), info = "Categoria no existe" });

            }
            return json;
        }

        public String desactivarCartelera(string idcartelera) {
            /*execute  desactivarCartelera @IdCartelera = 1*/
            SqlConnection con;
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    /*
                     * @IdCartelera int
                     */
                    SqlCommand cmd = new SqlCommand("desactivarCartelera", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdCartelera", SqlDbType.Int).Value = Int32.Parse(idcartelera);
                    


                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    /*
                    100: Desactivación exitosa
                    101: No se pudo desactivar
                    102: Cartelera no existe
                     */

                    if (result == 100)
                    {

                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Desactivación exitosa" });
                    }
                    else if (result == 101)
                    {

                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " No se pudo desactivar" });
                    }
                    else if (result == 102)
                    {

                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Cartelera no existe" });
                    }
                    else
                    {
                        Console.Write("Categoria no existe");
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Resultado desconocido" });
                    }

                }
            }
            catch (Exception e)
            {
                return new JavaScriptSerializer().Serialize(new Result { id = result, value = e.ToString(), info = "Resultado Desconocido [2]" });

            }
            return json;
        }
        public String updateEstadoFestival(string idfestival, string estado) {
            /*execute updateEstadoFestival @IdFestival = 1, @Estado = 1*/
            SqlConnection con;
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    /*
                     *@IdFestival int,
                     *@Estado int
                     */
                    SqlCommand cmd = new SqlCommand("updateEstadoFestival", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdFestival", SqlDbType.Int).Value = Int32.Parse(idfestival);
                    cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Int32.Parse(estado);
                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    if (result == 100)
                    {

                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Actualizado correctamente" });
                    }
                    else if (result == 101)
                    {

                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "No se pudo actualizar" });
                    }
                    else if (result == 102)
                    {

                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "  Festival no existe" });
                    }
                    else
                    {
                        Console.Write("Categoria no existe");
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Resultado desconocido" });
                    }

                }
            }
            catch (Exception e)
            {
                return new JavaScriptSerializer().Serialize(new Result { id = result, value = e.ToString(), info = "Resultado Desconocido [2]" });

            }
            return json;
        }

    }
}
