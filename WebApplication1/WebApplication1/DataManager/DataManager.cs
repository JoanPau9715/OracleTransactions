using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebApplication1.DTOs;

namespace WebApplication1.DataManager
{
    public class DataManager : IDataManager
    {
        public IEnumerable<GenreDTO> GetGenres()
        {
            using (
                var connection =
                    new OracleConnection(
                        ConfigurationManager.ConnectionStrings["ConnectionStringBooks"].ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "P_SELECT_GENRES";
                var pRes = new OracleParameter("RES", OracleType.Cursor);
                pRes.Direction = ParameterDirection.Output;

                command.Parameters.Add(pRes);

                var dr = command.ExecuteReader(CommandBehavior.SingleResult);

                while (dr.Read())
                {
                    yield return new GenreDTO
                        {
                            Id = dr["IdGenre"].ToString(),
                            Genre = dr["Genre"].ToString()
                        };
                }
            }
        }

        public int InsertCustomer(string firstName, string lastName, IEnumerable<ListItem> genres)
        {
            using (
                var connection =
                    new OracleConnection(
                        ConfigurationManager.ConnectionStrings["ConnectionStringBooks"].ConnectionString))
            {
                connection.Open();
                OracleCommand comando = connection.CreateCommand();
                OracleTransaction trax_new_customer = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    comando.Transaction = trax_new_customer;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "NEW_CUSTOMER";

                    comando.Parameters.Add(new OracleParameter("v_FirstName", firstName));
                    comando.Parameters.Add(new OracleParameter("v_LastName", lastName));

                    OracleParameter v_IdCustomer = new OracleParameter("v_IdCustomer", OracleType.Int32);
                    v_IdCustomer.Direction = ParameterDirection.Output;

                    comando.Parameters.Add(v_IdCustomer);

                    comando.ExecuteNonQuery();

                    int IdCustomer = (int)v_IdCustomer.Value;

                    foreach (ListItem genre in genres)
                    {
                        OracleCommand comandoInteres = connection.CreateCommand();

                        comandoInteres.Transaction = trax_new_customer;
                        comandoInteres.CommandType = CommandType.StoredProcedure;
                        comandoInteres.CommandText = "NEW_PREFERENCE";

                        comandoInteres.Parameters.Add(new OracleParameter("v_IdCustomer", IdCustomer));
                        comandoInteres.Parameters.Add(new OracleParameter("v_IdGenre", genre.Value));

                        comandoInteres.ExecuteNonQuery();
                    }

                    trax_new_customer.Commit();

                    return 1;
                }
                catch (Exception ex)
                {
                    trax_new_customer.Rollback();
                    throw;
                }
            }            
        }
    }
}