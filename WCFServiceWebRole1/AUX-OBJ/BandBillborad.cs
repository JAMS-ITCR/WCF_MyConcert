using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1.AUX_OBJ
{
    public class BandBillborad
    {
        /*Cartelera.IdCartelera: int
        Categoria.IdCategoria: int
        Categoria.Nombre: varchar
        Banda.IdBanda: int
        Banda.Nombre: varchar*/
        public int IdCartelera { get; set; }
        public int IdCategoria { get; set; }
        public String CategoriaNombre { get; set; }
        public String BandaNombre { get; set; }
        public int IdBanda { get; set; }
        public String NombrePais { get; set; }


    }
}