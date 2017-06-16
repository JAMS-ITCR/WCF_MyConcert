using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1.AUX_OBJ
{
    public class Band
    {
        public int IdBanda { get; set; }
        public String Nombre { get; set; }
        public int IdGenero { get; set; }
        public String DescripcionBanda { get; set; }
        public bool Estado { get; set; }
    }
}