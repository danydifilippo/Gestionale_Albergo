using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Gestionale_Albergo.Models
{
    public class Clienti
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        [Display(Name = "Codice Fiscale")]
        public string CF { get; set;}

        [Display(Name = "Città")]
        public string Citta { get; set; }
        public string Prov { get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "Nr Pren.")]
        public int IdPrenotazione { get; set; }

    }
}