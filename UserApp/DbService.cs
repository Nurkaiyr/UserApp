using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp
{
    public class DbService
    {
        public static DbConnection Connection
        {
            get
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(
                            ConfigurationManager.ConnectionStrings["UsersDB"].ProviderName);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;

                return connection;
            }
        }

        public DbProviderFactory GetFactory()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(
                           ConfigurationManager.ConnectionStrings["UsersDB"].ProviderName);

            return factory;
        }

        public void CreateUser(User user)
        {
            DbDataAdapter adapter = GetFactory().CreateDataAdapter();
            DbCommand selectCommand = Connection.CreateCommand();
            selectCommand.CommandText = "select * from dbo.users";

            DbCommandBuilder builder = GetFactory().CreateCommandBuilder();
            builder.DataAdapter = adapter;

            adapter.SelectCommand = selectCommand;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            DataTable table = dataSet.Tables[0];
            DataRow newRow = table.NewRow();
            newRow["id"] = user.Id;
            newRow["login"] = user.Login;
            newRow["password"] = user.Password;
            newRow["address"] = user.Address;
            newRow["phone"] = user.Phone;
            newRow["isAdmin"] = Convert.ToInt32(user.IsAdmin);
            table.Rows.Add(newRow);

            adapter.Update(dataSet);
            dataSet.AcceptChanges();
        }

        public List<User> GetUsers(bool isAdmin)
        {
            List<User> users = new List<User>();

            DbDataAdapter adapter = GetFactory().CreateDataAdapter();
            DbCommand selectCommand = Connection.CreateCommand();

            if (isAdmin)
            {
                DbParameter isAdminParameter = selectCommand.CreateParameter();
                isAdminParameter.DbType = DbType.Int32;
                isAdminParameter.Value = Convert.ToInt32(isAdmin);
                isAdminParameter.ParameterName = "@isAdmin";

                selectCommand.Parameters.Add(isAdminParameter);
                selectCommand.CommandText = "select * from dbo.users where isAdmin = @isAdmin";
            }
            else
            {
                selectCommand.CommandText = "select * from dbo.users";
            }

            DbCommandBuilder builder = GetFactory().CreateCommandBuilder();
            builder.DataAdapter = adapter;

            adapter.SelectCommand = selectCommand;
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            DataTable table = ds.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                users.Add(new User
                {
                    Id = (int)row["id"],
                    Login = row["login"].ToString(),
                    Password = row["password"].ToString(),
                    Address = row["address"].ToString(),
                    Phone = row["phone"].ToString(),
                    IsAdmin = Convert.ToBoolean(row["isAdmin"])
                });
            }
            ds.AcceptChanges();

            return users;
        }
    }
}
