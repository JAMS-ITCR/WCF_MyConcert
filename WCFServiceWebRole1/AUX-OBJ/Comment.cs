using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1.AUX_OBJ
{
    /*
     Comentario.IdComentario: int
Comentario.IdUsuario: int
Comentario.IdBanda: int
Comentario.Rating: int
Comentario.Contenido: varchar
Comentario.FechaCreación: datetime
Comentario.Estado: bit*/
    public class Comment
    {
        public int IdComentario { get; set; }
        public int IdUsuario { get; set; }
        public int IdBanda { get; set; }
        public int Rating { get; set; }
        public String Contenido { get; set; }
        public String FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }
}