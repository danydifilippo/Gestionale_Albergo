using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Gestionale_Albergo.Models
{
    public class Servizi
    {
        public int IdServizio { get; set; }

        public string Servizio { get; set; }

        [Display(Name = "Data Richiesta")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Display(Name = "Quantità")]
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }

        public int IdPrenotazione { get; set; }
    }
}