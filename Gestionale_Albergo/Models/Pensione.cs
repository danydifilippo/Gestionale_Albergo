using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Gestionale_Albergo.Models
{
    public class Pensione
    {

        public int IdPensione { get; set; }
        public string Tipo { get; set; }

        public static List<SelectListItem> ListaPensione
        {
            get
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                SqlConnection sql = Connessione.GetConnection();
                sql.Open();
                SqlCommand com = Connessione.GetCommand("SELECT * FROM PENSIONE", sql);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    SelectListItem s = new SelectListItem
                    {
                        Text = reader["TipoPensione"].ToString(),
                        Value = reader["IdPensione"].ToString()
                    };

                    selectListItems.Add(s);
                }
                return selectListItems;
            }

        }

    }
}