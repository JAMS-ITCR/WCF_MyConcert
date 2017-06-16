using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1.AUX_OBJ
{
    public class BandMember
    {
        /*MiembroBanda.IdMiembroBanda int*/
        public int IdMiembroBanda { get; set; }
        /*MiembroBanda.Nombre varchar*/
        public String Nombre { get; set; }
        /*MiembroBanda.IdBanda int*/
        public int IdBanda { get; set; }
    }
}