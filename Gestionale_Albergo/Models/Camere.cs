using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Gestionale_Albergo.Models
{
    public class Camere
    {
        [Display(Name = "Nr Camera")]
        public int NrCamera { get; set; }
        public string Tipo { get; set; }

        public string Descrizione { get; set; }

     
    }
}