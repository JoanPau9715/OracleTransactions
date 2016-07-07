using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using System.Collections;
using Autofac.Integration.Web.Forms;
using WebApplication1.DTOs;
using WebApplication1.DataManager;

namespace WebApplication1
{
    [InjectProperties]
    public partial class Customers : System.Web.UI.Page
    {
        public IDataManager dataManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                displayGenres();                
        }

        private void displayGenres()
        {
            try
            {
                var allGenres = getGenres();

                foreach (var genre in allGenres)
                {
                    clbGenres.Items.Add(new ListItem
                    {
                        Value = genre.Id,
                        Text = genre.Genre
                    });
                }
            }
            catch (OracleException oex)
            {
                Response.Redirect("error.aspx?desc=" + oex.Message);
            }
        }

        private IEnumerable<GenreDTO> getGenres()
        {
            return dataManager.GetGenres();
        }

        private int insertCustomer()
        {
            var genresSelected = clbGenres.Items.Cast<ListItem>().Where(l => l.Selected);

            int result = dataManager.InsertCustomer(txtFirstName.Text, txtLastName.Text, genresSelected);

            return result;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            insertCustomer();
        }
    }
}