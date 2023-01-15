using Gestionale_Albergo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestionale_Albergo.Controllers
{
    public class QueriesController : Controller
    {
        // GET: Queries
        public ActionResult QueriesPage()
        {
            return View();
        }

        // JSONRESULT GET BOOKINGS BY CF
        public JsonResult GetBookingsByCF(string CF)
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();
            List<Prenotazioni> PrenotazioniCliente = new List<Prenotazioni>();

            try
            {
                SqlCommand com = Connessione.GetCommand("SELECT * FROM PRENOTAZIONE AS P INNER JOIN CLIENTI AS C " +
                    "ON C.IDCLIENTE = P.IDCLIENTE WHERE Cod_Fiscale = @CF", sql);
                com.Parameters.AddWithValue("CF", CF);

                SqlDataReader reader = com.ExecuteReader();



                while (reader.Read())
                {
                    Prenotazioni b = new Prenotazioni();
                    b.IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"]);
                    b.Cliente = reader["Cognome"].ToString() + " " + reader["Nome"].ToString();
                    b.NrCamera = Convert.ToInt32(reader["NrCamera"]);
                    b.DataPren = Convert.ToDateTime(reader["DataPrenotazione"]);
                    b.CheckIn = Convert.ToDateTime(reader["DataArrivo"]);
                    b.CheckOut = Convert.ToDateTime(reader["DataUscita"]);
                    b.Acconto = Convert.ToDecimal(reader["Acconto"]);
                    b.Prezzo = Convert.ToDecimal(reader["PrezzoSoggiorno"]);
                    b.Pensione = reader["TipoPensione"].ToString();
                    b.Tot = Prenotazioni.TotServizi(Convert.ToInt32(reader["IdPrenotazione"]));
                    b.Saldo = Prenotazioni.DaSaldare(Convert.ToDecimal(reader["PrezzoSoggiorno"]), Convert.ToDecimal(reader["Acconto"]), Prenotazioni.TotServizi(Convert.ToInt32(reader["IdPrenotazione"])));
                    PrenotazioniCliente.Add(b);
                }
            }
            catch
            {

            }
            finally { sql.Close(); }

            return Json(PrenotazioniCliente, JsonRequestBehavior.AllowGet);
        }

        
    }
}
