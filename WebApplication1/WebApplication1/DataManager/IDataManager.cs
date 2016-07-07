using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using WebApplication1.DTOs;

namespace WebApplication1.DataManager
{
    public interface IDataManager
    {
        IEnumerable<GenreDTO> GetGenres();

        int InsertCustomer(string firstName, string lastName, IEnumerable<ListItem> genres);
    }
}
