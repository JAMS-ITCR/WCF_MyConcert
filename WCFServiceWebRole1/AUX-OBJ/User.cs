using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1.AUX_OBJ
{
    public class User
    {
        /*Usuario.IdUsuario: int*/
        /*DetalleFanatico.IdUsuario: int*/
        /*
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
        public int IdUsuario { get; set; }
        public String Nombre { get; set; }
        public String Apellido1 { get; set; }
        public String Apellido2 { get; set; }
        public String CorreoElectronico { get; set; }
        public String FechaInscripcion { get; set; }
        public String NombreUsuario { get; set; }        
        public String Contrasena { get; set; }
        public int IdRol { get; set; }
        public bool SesionActiva { get; set; }
        public bool Estado { get; set; }

       
        /*DetalleFanatico.FechaNacimiento: int*/
        public String FechaNacimiento { get; set; }

        /*DetalleFanatico.IdPais: int*/
        public int IdPais { get; set; }


        /*DetalleFanatico.Ubicacion: varchar(100)*/
        public String Ubicacion { get; set; }


        /*DetalleFanatico.Universidad: varchar(30)*/
        public String Universidad { get; set; }

        /*DetalleFanatico.Telefono: varchar*/
        public String Telefono { get; set; }

        /*DetalleFanatico.Foto: nvarchar*/
        public String Foto { get; set; }

        /*DetalleFanatico.Descripcion: varchar*/
        public String Descripcion { get; set; }


    }
}