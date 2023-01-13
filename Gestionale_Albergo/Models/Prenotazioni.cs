using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Gestionale_Albergo.Models
{
    public class Prenotazioni
    {
        [Display(Name ="Nr Pren.")]
        public int IdPrenotazione { get; set; }

        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        [Display(Name = "Data Pren.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataPren { get; set; }
        
        [Display(Name = "Dal")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Al")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckOut { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Acconto { get; set; }

        [Display(Name = "Tariffa Soggiorno")]

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Prezzo { get; set; }

        public string Pensione { get; set; }
        public string Cliente { get; set; }

        [Display(Name = "Nr Camera")]
        public int NrCamera { get; set; }

        [Display(Name = "Totale Servizi")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Tot { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]

        [Display(Name = "Da Saldare")]
        public decimal Saldo { get; set; }

        public static decimal TotServizi(int id)
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();

            decimal t = 0;

            try
            {
                SqlCommand com = Connessione.GetStoreProcedure("TotServices", sql);
                com.Parameters.AddWithValue("IdPrenotazione", id);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                   t = Convert.ToDecimal(reader["ServiziTot"]);

                }
            }
            catch
            {
                return t=0;
            }
            finally { sql.Close(); }

            return t;
        }

        public static decimal DaSaldare(decimal Price, decimal Deposit, decimal Service)
        {
            return Price - Deposit + Service;
        }

    }
}