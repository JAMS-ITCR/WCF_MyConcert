using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using WCFServiceWebRole1.AUX_OBJ;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        string country ="test";
        public string connectionString = "Data Source=peds.database.windows.net;Initial Catalog = MyConcert-DB;Integrated Security=False;User ID = Jams2017; Password=Peds2017";
        public String login(string username, string password)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
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
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Éxito sesión iniciada admin" });
                }
                else if (result == 104)
                {
                    //No existe usuario
                    Console.Write("Éxito inicio sesión fanático");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Éxito inicio sesión fanático" });
                }
                else if (result == 105)
                {

                    Console.Write("Usuario Sesión Activa");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Usuario Sesión Activa" });
                }
                else if (result == 106)
                {

                    Console.Write("Credenciales incorrectas");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Credenciales incorrectas" });
                }
            }          
            return json;
        }

        
        public String logout(string username)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
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
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Exito cerrando sesión" });
                }
                else if (result == 101)
                {
                    Console.Write("no era necesario cerrar sesión");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "no era necesario cerrar sesión" });
                }
                else if (result == 102)
                {
                    Console.Write("usuario no existe");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "no era necesario cerrar sesión" });
                }
            }
                else {
                    Console.Write("Something happened");
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "no era necesario cerrar sesión" });
                }
            
            return json;
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
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
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
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = description });                  
                }
                catch (Exception e) {
                    json = new JavaScriptSerializer().Serialize(new Result { id = -2, value = "", info = "Error en la consulta." });
                }              
            }
            return json;
        }

        public String getInfoUsuario(string idusuario, string idrol) {
            /*execute getInfoUsuario @IdUsuario = 1, @IdRol = 1*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();            
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("getInfoUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value =Int32.Parse(idusuario);
                cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = Int32.Parse(idrol);
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        User user  = new User();
                        /*
                        Usuario.IdUsuario: int
                        Usuario.Nombre: varchar(30)
                        Usuario.Apellido1: varchar(30)
                        Usuario.Apellido2: varchar(100)
                        Usuario.CorreoElectronico: varchar(50)
                        Usuario.FechaInscripcion: datetime
                        Usuario.NombreUsuario: varchar(30)
                        Usuario.Contraseña: varchar(8)
                        Usuario.IdRol: int
                        Usuario.SesionActiva: bit
                        Usuario.Estado: bit*/
                        user.IdUsuario = Int32.Parse(reader["IdUsuario"].ToString());
                        user.Nombre = reader["Nombre"].ToString();
                        user.Apellido1 = reader["Apellido1"].ToString();
                        user.Apellido2 = reader["Apellido2"].ToString();
                        user.CorreoElectronico = reader["CorreoElectronico"].ToString();
                        user.FechaInscripcion = reader["FechaInscripcion"].ToString();
                        user.NombreUsuario = reader["NombreUsuario"].ToString();
                        user.Contrasena = reader["Contraseña"].ToString();
                        user.IdRol = Int32.Parse(reader["IdRol"].ToString());
                        user.SesionActiva = (bool)reader["SesionActiva"];
                        user.Estado = (bool)reader["Estado"];

                        String objectResult = new JavaScriptSerializer().Serialize(user);
                        return new JavaScriptSerializer().Serialize(new Result { id = 100, value = objectResult, info = "Exito en la consulta." });
                    }

                }
                catch (Exception e)
                {
                    json = new JavaScriptSerializer().Serialize(new Result { id = -2, value = e.ToString(), info = "Error en la consulta." });
                }
            }
            return json;            
        }

        public String getDetalleFanatico(string IdUsuario) {
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            try
            {
                /*execute getDetalleFanatico @IdUsuario = 3*/
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
               
                if (con.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand("getDetalleFanatico", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Int32.Parse(IdUsuario);

                    SqlDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            /*DetalleFanatico.IdUsuario: int
                            DetalleFanatico.FechaNacimiento: int
                            DetalleFanatico.IdPais: int
                            DetalleFanatico.Ubicacion: varchar(100)
                            DetalleFanatico.Universidad: varchar(30)
                            DetalleFanatico.Telefono: varchar
                            DetalleFanatico.Foto: nvarchar
                            DetalleFanatico.Descripcion: varchar*/
                            user.IdUsuario = Int32.Parse(reader["IdUsuario"].ToString());
                            user.FechaNacimiento = reader["FechaNacimiento"].ToString();
                            user.IdPais = Int32.Parse(reader["IdPais"].ToString());
                            user.Ubicacion = reader["Ubicacion"].ToString();
                            user.Universidad = reader["Universidad"].ToString();
                            user.Telefono = reader["Telefono"].ToString();
                            user.Foto = reader["Foto"].ToString();
                            user.Descripcion = reader["DescripcionPersonal"].ToString();

                            String objectResult = new JavaScriptSerializer().Serialize(user);
                            return new JavaScriptSerializer().Serialize(new Result { id = 100, value = objectResult, info = "Exito en la consulta." });
                        }

                    }
                    catch (Exception e)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = -2, value = e.ToString(), info = "Error en la consulta. [2]" });
                    }
                }
            }
            catch (Exception e)
            {
                json = new JavaScriptSerializer().Serialize(new Result { id = -2, value = e.ToString(), info = "Error en la consulta. [1]" });
            }
            return json;
        }

        public String asignarGeneroFanatico(string idusuario, string idgenero) {
            /*execute asignarGeneroFanatico @IdUsuario = 2, @IdGenero = 1*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("asignarGeneroFanatico", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Int32.Parse(idusuario);
                cmd.Parameters.Add("@IdGenero", SqlDbType.Int).Value = Int32.Parse(idgenero);

                SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                result = (int)returnParameter.Value;

                if (result == 100)
                {
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Agregado exitosamenre" });
                    }
                else if (result == 101)
                {            
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Éxito inicio sesión fanático" });
                }
                else if (result == 102)
                {
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = ": El Género no existe" });
                }
                else if (result == 103)
                {
                    json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Usuario no existe o no es fanático" });
                }
            }
            return json;
        }

        public String eliminarGeneroFanatico(string idusuario, string idgenero) {
            /* execute eliminarGeneroFanatico @IdUsuario = 1, @IdGenero = 1*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand("eliminarGeneroFanatico", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Int32.Parse(idusuario);
                    cmd.Parameters.Add("@IdGenero", SqlDbType.Int).Value = Int32.Parse(idgenero);

                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;


                    if (result == 100)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Genero eliminado exitosamente" });
                    }
                    else if (result == 101)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " No se pudo eliminar el genero" });
                    }
                    else if (result == 102)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = ": El Genero no ha sido agregado al usuario" });
                    }
                    else if (result == 103)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " El Genero no existe" });
                    }
                    else if (result == 104)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " El Usuario no existe o no es Fanatico" });
                    }
                    else
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Resultado Desconocido" });
                    }
                }
            }
            catch (Exception e) {
                return new JavaScriptSerializer().Serialize(new Result { id = result, value = e.ToString(), info = " Error: Resultado Desconocido" });
            }
            return json;
        }

        public String getGenerosFanatico(string idusuario)
        {
            /*execute getGenerosFanatico @IdUsuario = 2*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("getGenerosFanatico", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Int32.Parse(idusuario.ToString());

                SqlDataReader reader = cmd.ExecuteReader();
                List<Gender> genders = new List<Gender>();
                try
                {
                    while (reader.Read())
                    {
                        Gender gender = new Gender();
                        /*member.IdMiembroBanda = Int32.Parse(reader["IdMiembroBanda"].ToString());
                        member.Nombre = reader["Nombre"].ToString();
                        member.IdBanda = Int32.Parse(reader["IdBanda"].ToString());+*/
                        genders.Add(gender);
                    }
                }
                catch (Exception e)
                {
                    return new JavaScriptSerializer().Serialize(new Result { id = -2, value = e.ToString(), info = "Error en la consulta." });
                }


                json = new JavaScriptSerializer().Serialize(genders);
            }
            //return json;
            return new JavaScriptSerializer().Serialize(new Result { id = 1, value = json, info = "Lista de Miembros" });
        }

       
        
    }
}
