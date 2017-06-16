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

        public String updateBand(string idbanda, string descripcion, string estado) {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            int result = -3;
            if (con.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("updateBanda", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value =Int32.Parse(idbanda.ToString());
                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = descripcion;
                    cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value =Int32.Parse(estado.ToString());
                    /*excecute crearBanda @NBanda = 'Metallica', @IdGenero = 1,@Descripcion = 'Alguna descripcion'*/
                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    if (result == 100)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Actualización correcta " });
                    }
                    else if (result == 101)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "No se pudo actualizar la descripcion, verifique los datos" });
                    }
                    else if (result == 102)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "La Banda no existe" });
                    }
                   
                    else
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = -7, value = "", info = "Resultado desconocido de la respuesta" });
                    }
                }
                catch (Exception e)
                {
                    return new JavaScriptSerializer().Serialize(new Result { id = -8, value = "", info = "Resultado desconocido" });
                }


            }
            return json;
        }

        public String getBand(String bandid) {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("getBandaById", con);
                cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value = Int32.Parse(bandid.ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                Band band = new Band();
                band.IdBanda = Int32.Parse(reader["IdBanda"].ToString());
                band.Nombre = reader["Nombre"].ToString();
                band.IdGenero = Int32.Parse(reader["IdGenero"].ToString());
                band.DescripcionBanda = reader["DescripcionBanda"].ToString();
                band.Estado = (bool)reader["Estado"];               
                json = new JavaScriptSerializer().Serialize(band);
            }
            return json;
        }

        public String getBands() {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("getBandas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                List<Band> bands = new List<Band>();
                while (reader.Read())
                {
                    Band band = new Band();

                    band.IdBanda = Int32.Parse(reader["IdBanda"].ToString());
                    band.Nombre = reader["Nombre"].ToString();
                    band.IdGenero = Int32.Parse(reader["IdGenero"].ToString());
                    band.DescripcionBanda = reader["DescripcionBanda"].ToString();
                    band.Estado = (bool)reader["Estado"];

                    bands.Add(band);
                }
                json = new JavaScriptSerializer().Serialize(bands);
            }
            return json;
        }


        public String addMemberToBanda(string nmiembro, string idbanda ) {
            /*execute asignarMiembroBanda @NMiembro = 'Algun Nombre', @IdBanda = 1*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            int result = -3;
            if (con.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("asignarMiembroBanda", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NMiembro", SqlDbType.VarChar).Value = nmiembro.ToString();
                    cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value = Int32.Parse(idbanda.ToString());
                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    if (result == 100)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Exito añadiendo miembro" });
                    }
                    else if (result == 101)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Fallo Añadir miembro (miembro añadido previamente" });
                    }
                    else if (result == 102)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Banda no existe" });
                    }

                    else
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = -7, value = "", info = "Resultado desconocido de la respuesta" });
                    }
                }
                catch (Exception e)
                {
                    return new JavaScriptSerializer().Serialize(new Result { id = -8, value = "", info = "ERROR: Resultado desconocido" });
                }
            }
            return json;
            
        }

        public String deleteBandMember(string idbanda, string idmiembro) {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            int result = -3;
            if (con.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    /*execute eliminarMiembroBanda @IdBanda = 1, @IdMiembro =1*/
                    SqlCommand cmd = new SqlCommand("eliminarMiembroBanda", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value = Int32.Parse(idbanda.ToString());
                    cmd.Parameters.Add("@IdMiembro", SqlDbType.Int).Value = Int32.Parse(idmiembro.ToString());
                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    if (result == 100)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Eliminación correcta" });
                    }
                    else if (result == 101)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "No se Pudo eliminar el miembro" });
                    }
                    else if (result == 102)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Miembro no existe" });
                    }
                    else if (result == 103)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Banda no existe" });
                    }

                    else
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = -7, value = "", info = "Resultado desconocido de la respuesta" });
                    }
                }
                catch (Exception e)
                {
                    return new JavaScriptSerializer().Serialize(new Result { id = -8, value = "", info = "ERROR: Resultado desconocido" });
                }
            }
            return json;
            
        }

        public String getMembersByBand(string idbanda)
        {
            /*execute getMiembrosByBandaId @IdBanda = 1*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int result = -1;
            String json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Error en la consulta." });
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("getMiembrosByBandaId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value = Int32.Parse(idbanda.ToString());
               
                SqlDataReader reader = cmd.ExecuteReader();
                List<BandMember> bandmebers = new List<BandMember>();
                try
                {
                    while (reader.Read())
                    {
                        BandMember member = new BandMember();
                        member.IdMiembroBanda = Int32.Parse(reader["IdMiembroBanda"].ToString());
                        member.Nombre = reader["Nombre"].ToString();
                        member.IdBanda = Int32.Parse(reader["IdBanda"].ToString());
                        bandmebers.Add(member);
                    }
                }
                catch (Exception e) {                
                    return new JavaScriptSerializer().Serialize(new Result { id = -2, value = "[]", info = "Error en la consulta." });
                }
                

                json = new JavaScriptSerializer().Serialize(bandmebers);
            }
            //return json;
            return new JavaScriptSerializer().Serialize(new Result { id = 1, value = json, info = "Lista de Miembros" });
        }


        public String addImageToBand(string idbanda, string urlimagen)
        {
            /*
             execute addImagenBanda @IdBanda = 1, @UrlImagen = 'http://revistakuadro.com/wp-content/uploads/2016/11/wpid-metallica1.jpg'
             */
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            int result = -3;
            if (con.State == System.Data.ConnectionState.Open)
            {
                try
                {                
                    SqlCommand cmd = new SqlCommand("addImagenBanda", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value = Int32.Parse(idbanda.ToString());
                    cmd.Parameters.Add("@UrlImagen", SqlDbType.VarChar).Value = urlimagen.ToString();
                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    if (result == 100)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Exito imagen insertada" });
                    }
                    else if (result == 101)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " No se pudo agregar la imagen" });
                    }
                    else if (result == 102)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Banda no existe" });
                    }
                    else if (result == 103)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "La Url de la imagen ya ha sido agregada para esta banda" });
                    }

                    else
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = -7, value = "", info = "Resultado desconocido de la respuesta" });
                    }
                }
                catch (Exception e)
                {
                    return new JavaScriptSerializer().Serialize(new Result { id = -8, value = e.ToString(), info = "ERROR: Resultado desconocido" });
                }
            }
            return json;
        }


        public String deleteImageFrom(string idbanda, string idimagen) {
            /*execute eliminarImagenBanda @IdBanda = 1, @IdImagen = 1*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            int result = -3;
            if (con.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("eliminarImagenBanda", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value = Int32.Parse(idbanda.ToString());
                    cmd.Parameters.Add("@IdImagen", SqlDbType.Int).Value =Int32.Parse(idimagen.ToString());
                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    if (result == 100)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Exito eliminando imagen" });
                    }
                    else if (result == 101)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "No se pudo eliminar" });
                    }
                    else if (result == 102)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " La imagen no está asociada a esta banda o no existe" });
                    }
                    else if (result == 103)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " No existe banda" });
                    }

                    else
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = -7, value = "", info = "Resultado desconocido de la respuesta" });
                    }
                }
                catch (Exception e)
                {
                    return new JavaScriptSerializer().Serialize(new Result { id = -8, value = e.ToString(), info = "ERROR: Resultado desconocido" });
                }
            }
            return json;
        }

        public String addSongToBand(string ncancion, string preview, string image, string idbanda, string idalbum, string estado) {
            /*execute addCancionBanda @NCancion = 'NombreCancion', @Preview = 'algun link del preview', @Imagen = 'sfdsfasgesgdfdvds', @IdBanda = 1, @IdAlbum = 1, @Estado = 1*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            int result = -3;
            if (con.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("addCancionBanda", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@NCancion", SqlDbType.VarChar).Value = ncancion.ToString();
                    cmd.Parameters.Add("@Preview", SqlDbType.VarChar).Value = preview.ToString();
                    cmd.Parameters.Add("@Imagen", SqlDbType.VarChar).Value = image.ToString();
                    cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value =Int32.Parse( idbanda.ToString());
                    cmd.Parameters.Add("@IdAlbum", SqlDbType.Int).Value = Int32.Parse(idalbum.ToString());
                    cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Int32.Parse(estado.ToString());
                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    if (result == 100)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "Exito canción insertada" });
                    }
                    else if (result == 101)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " No se pudo agregar la canción" });
                    }
                    else if (result == 102)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " La canción ya fue agregada a la banda" });
                    }
                    else if (result == 103)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "La Url de la imagen ya ha sido agregada para esta banda" });
                    }

                    else
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = -7, value = "", info = " La banda no existe" });
                    }
                }
                catch (Exception e)
                {
                    return new JavaScriptSerializer().Serialize(new Result { id = -8, value = e.ToString(), info = "ERROR: Resultado desconocido" });
                }
            }
            return json;
        }

        public  String deleteSongToBand(string idbanda, string idcancion) {
            /*execute eliminarCancionBanda @IdBanda = 1, @IdCancion = 1*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            String json = new JavaScriptSerializer().Serialize(new Result { id = -1, value = "", info = "Error en la consulta." });
            int result = -3;
            if (con.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("eliminarCancionBanda", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdBanda", SqlDbType.Int).Value = Int32.Parse(idbanda.ToString());
                    cmd.Parameters.Add("@IdCancion", SqlDbType.Int).Value = Int32.Parse(idcancion.ToString());

                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    if (result == 100)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " Eliminación exitosa" });
                    }
                    else if (result == 101)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "No se pudo eliminar" });
                    }
                    else if (result == 102)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = "La canción no pertenece a la banda" });
                    }
                    else if (result == 103)
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = result, value = "", info = " La banda no existe" });
                    }

                    else
                    {
                        json = new JavaScriptSerializer().Serialize(new Result { id = -7, value = "", info = " La banda no existe" });
                    }
                }
                catch (Exception e)
                {
                    return new JavaScriptSerializer().Serialize(new Result { id = -8, value = e.ToString(), info = "ERROR: Resultado desconocido" });
                }
            }
            return json;
        }


    }


}
