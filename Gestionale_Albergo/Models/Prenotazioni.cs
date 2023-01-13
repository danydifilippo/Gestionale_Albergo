using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Gestionale_Albergo.Models
{
    public class Prenotazioni
    {
        [Display(Name ="Nr Pren.")]
        public int IdPrenotazione { get; set; }
        
        [Display(Name = "Data Pren.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataPren { get; set; }
        
        [Display(Name = "Data Pren.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Data Pren.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckOut { get; set; }
        public decimal Acconto { get; set; }

        [Display(Name = "Tariffa Soggiorno")]
        public decimal Prezzo { get; set; }

        public int IdPensione { get; set; }
        public int IdCliente { get; set; }
        public int IdCamera { get; set; }
    }
}