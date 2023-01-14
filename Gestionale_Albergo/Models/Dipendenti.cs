using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestionale_Albergo.Models
{
    public class Dipendenti
    {
        public int IdUtente { get; set; }

        [Remote("UsernameEsistente","Users",ErrorMessage ="Username esistente")]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}