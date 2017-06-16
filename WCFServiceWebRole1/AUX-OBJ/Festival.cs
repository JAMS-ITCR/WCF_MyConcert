using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1.AUX_OBJ
{
    public class Festival
    {

        public int IdFestival { get; set; }
        public String Nombre { get; set; }
        public int IdPais { get; set; }
        public String Lugar{ get; set; }
        public String FechaInicio { get; set; }
        public String FechaFinal{ get; set; }
        public String Transporte{ get; set; }
        public String Comida{ get; set; }
        public String Servicios{ get; set; }
        public int IdCartelera{ get; set; }
        public int IdBanda{ get; set; }
        public bool Estado{ get; set; }
    }
}