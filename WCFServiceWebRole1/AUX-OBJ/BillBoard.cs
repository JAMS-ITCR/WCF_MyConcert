using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1.AUX_OBJ
{
    public class BillBoard
    {
        public int IdCartelera { get; set; }
        public String Nombre { get; set; }
        public int IdPais { get; set; }
        public String Lugar { get; set; }
        public String CierreVotacion{ get; set; }
        public bool Estado { get; set; }
        public String FechaInicio { get; set; }
        public String FechaFinal { get; set; }

    }
}