using Gestionale_Albergo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Gestionale_Albergo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult CustomList()
        {
                SqlConnection sql = Connessione.GetConnection();
                sql.Open();
                List<Clienti> Customers = new List<Clienti>();

                try
                {
                    SqlCommand com = Connessione.GetStoreProcedure("GetCustomers", sql);
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                    Clienti c = new Clienti();

                            c.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                            c.Nome = reader["Cognome"].ToString() + " " + reader["Nome"].ToString();
                            c.CF = reader["Cod_Fiscale"].ToString();
                            c.Citta = reader["Citta"].ToString();
                            c.Prov = reader["Prov"].ToString();
                            c.Contatto = reader["Tel_Cell"].ToString();
                            c.email = reader["email"].ToString();
                                if (reader["DataArrivo"] != DBNull.Value) {
                                    c.CheckIn = Convert.ToDateTime(reader["DataArrivo"]);
                                } 
                            c.IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"]);
                        
                        Customers.Add(c);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.msgerror = ex.Message;
                }
                finally { sql.Close(); }

                return View(Customers);
            }

            // GET: Customer/Details/5
            public ActionResult Details(int id)
            {
                SqlConnection sql = Connessione.GetConnection();
                sql.Open();
                Clienti c = new Clienti();

                try
                {
                    SqlCommand com = Connessione.GetStoreProcedure("GetCustomerById", sql);
                    com.Parameters.AddWithValue("IdCliente", id);

                    SqlDataReader reader = com.ExecuteReader();



                    while (reader.Read())
                    {
                        c.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                        c.Nome = reader["Nome"].ToString();
                        c.Cognome = reader["Cognome"].ToString();
                        c.CF = reader["Cod_Fiscale"].ToString();
                        c.Citta = reader["Citta"].ToString();
                        c.Prov = reader["Prov"].ToString();
                        c.Contatto = reader["Tel_Cell"].ToString();
                        c.email = reader["email"].ToString();
                        c.IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"]);                
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.msgerror = ex.Message;
                }
                finally { sql.Close(); }

                return View(c);
            }

        public JsonResult GetBookingsById(int id)
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();
            List<Prenotazioni> PrenotazioniCliente = new List<Prenotazioni>();

            try
            {
                SqlCommand com = Connessione.GetStoreProcedure("GetBookingByIdCustom", sql);
                com.Parameters.AddWithValue("IdCliente", id);

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

        public JsonResult CFEsistente(string CF)
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();

                SqlCommand com = Connessione.GetCommand("SELECT * FROM CLIENTI WHERE Cod_Fiscale=@CF", sql);
                com.Parameters.AddWithValue("CF", CF);

                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            sql.Close(); 
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Clienti c)
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();

            try
            {
                SqlCommand com = Connessione.GetStoreProcedure("CreateCustomer", sql);


                com.Parameters.AddWithValue("Cod_Fiscale", c.CF);
                com.Parameters.AddWithValue("Nome", c.Nome);
                com.Parameters.AddWithValue("Cognome", c.Cognome);
                com.Parameters.AddWithValue("Citta", c.Citta);
                com.Parameters.AddWithValue("Prov", c.Prov);
                com.Parameters.AddWithValue("Tel_Cell", c.Contatto);
                com.Parameters.AddWithValue("email", c.email);

                int row = com.ExecuteNonQuery();
            }
            catch 
            {

            }
            finally { sql.Close(); }
            return RedirectToAction("CustomList");
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
