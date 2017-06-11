using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        string country ="test";
        public string connectionString = "Data Source=peds.database.windows.net;Initial Catalog = MyConcert-DB;Integrated Security=False;User ID = Jams2017; Password=Peds2017";
        public int login(string username, string password)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
          
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("LoginUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar).Value = password;
               
                SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                 result = (int)returnParameter.Value;
               
                if (result == 103)
                {
                 
                    Console.Write("Éxito sesión iniciada admin");
                }
                else if (result == 104)
                {
                    //No existe usuario
                    Console.Write("Éxito inicio sesión fanático");
                }
                else if (result == 105)
                {

                    Console.Write("Usuario Sesión Activa");
                }
                else if (result == 106)
                {

                    Console.Write("Credenciales incorrectas");
                }
            }          
            return result;

        }
        public int logout(string username)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("CerrarSesionUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NUsuario", SqlDbType.VarChar).Value = username;
                SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                result = (int) returnParameter.Value;
                if (result == 100)
                {
                    Console.Write("Exito cerrando sesión");
                }
                else if (result == 101)
                {
                    Console.Write("no era necesario cerrar sesión");
                }
                else if (result == 102)
                {
                    Console.Write("usuario no existe");
                }
                else {
                    Console.Write("Something happened");
                }
            }
            return result;
        }
    }
}
