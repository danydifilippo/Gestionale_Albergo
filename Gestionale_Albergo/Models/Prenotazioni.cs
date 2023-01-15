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
        [Display(Name = "Nr Pren.")]
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

        [NoChar(ErrorMessage = "Formato solo numerico")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Acconto { get; set; }

        [Display(Name = "Tariffa Soggiorno")]
        [NoChar(ErrorMessage = "Formato solo numerico")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
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

        public int TotPren { get; set; }

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
                return t = 0;
            }
            finally { sql.Close(); }

            return t;
        }

        public static decimal DaSaldare(decimal Price, decimal Deposit, decimal Service)
        {
            return Price - Deposit + Service;
        }

        public static List<SelectListItem> ListaPrenotazioni
        {
            get
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                SqlConnection sql = Connessione.GetConnection();
                sql.Open();
                SqlCommand com = Connessione.GetCommand("SELECT * FROM PRENOTAZIONE AS P INNER JOIN CLIENTI AS C " +
                    "ON C.IdCliente= P.IdCliente", sql);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    SelectListItem s = new SelectListItem
                    {
                        Text = "Camera nr " + reader["NrCamera"].ToString() + " - " + reader["Cognome"].ToString() + " " + reader["Nome"].ToString(),
                        Value = reader["IdPrenotazione"].ToString()
                    };

                    selectListItems.Add(s);
                }

                return selectListItems;
            }

        }
    }
}
