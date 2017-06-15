using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using WCFServiceWebRole1.AUX_OBJ;

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

        public String createUser (
                       string nameUser,
                       string surname1User,
                       string surname2User,
                       string mailUser,
                       string nicknameUser,
                       string passUser,
                       string roleUser,
                       string birthdateUser,
                       string countryUser,
                       string addressUser,
                       string collegeUser,
                       string phoneUser,
                       string photoUser,
                       string descriptionUser) {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = JsonConvert.SerializeObject(new Result { id = -1, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
               SqlCommand cmd = new SqlCommand("Registro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = nameUser;
                cmd.Parameters.Add("@Apellido1", SqlDbType.VarChar).Value = surname1User;
                cmd.Parameters.Add("@Apellido2", SqlDbType.VarChar).Value = surname2User;
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar).Value = mailUser;
                cmd.Parameters.Add("@NUsuario", SqlDbType.VarChar).Value = nicknameUser;
                cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar).Value = passUser;
                cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = Int32.Parse(roleUser)  ;
                cmd.Parameters.Add("@FNacimiento", SqlDbType.VarChar).Value = birthdateUser;
                cmd.Parameters.Add("@IdPais", SqlDbType.Int).Value =Int32.Parse(countryUser);
                cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar).Value = addressUser;
                cmd.Parameters.Add("@Universidad", SqlDbType.VarChar).Value = collegeUser;
                cmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = phoneUser;
                cmd.Parameters.Add("@Foto", SqlDbType.VarChar).Value = photoUser;
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = descriptionUser;
               
                try {
                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    String description = "" ;
                    if (result == 100)
                    {
                        description = " Usuario Registrado exitosamente.";
                    }
                    else if (result == 101)
                    {
                        description = "El correo ya ha sido registrado.";
                    }
                    else if (result == 102)
                    {
                        description = "Nombre de Usuario no disponible.";
                    }
                    else if (result == 103) {

                        description = " No se pudo registrar, por favor verifique sus datos";
                    }
                    json = JsonConvert.SerializeObject(new Result { id = result, value = "", info = description });                  
                }
                catch (Exception e) {
                    json = JsonConvert.SerializeObject(new Result { id = -2, value = "", info = "Error en la consulta." });
                }              
            }
            return json;
        }

    }
}
