using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarketData
{
	class IMarket
	{
		private readonly SqlConnection sqlConnection;
		private readonly SqlCommand sqlCommand;
		private readonly SqlDataAdapter adapter;
		private readonly DataTable dataTable;
		public IMarket(DataTable dataTable)
		{
			try
			{
				sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + @"Market.mdf;Integrated Security=True");
				sqlCommand = new SqlCommand("", sqlConnection);
				adapter = new SqlDataAdapter(new SqlCommand("SELECT * FROM [dbo].[Products];", sqlConnection));
				FillAdapter();
				this.dataTable = dataTable;
				sqlConnection.Open();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
				Application.Current.MainWindow.Close();
			}
		}
		private void FillAdapter()
		{
			adapter.InsertCommand = new SqlCommand("InsertProduct", sqlConnection);
			adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
			adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Name"));
			adapter.InsertCommand.Parameters.Add(new SqlParameter("@amount", SqlDbType.Int, 0, "Amount"));
			adapter.InsertCommand.Parameters.Add(new SqlParameter("@price", SqlDbType.Int, 0, "Price"));
			SqlParameter totalPriceParam = adapter.InsertCommand.Parameters.Add("@totalPrice", SqlDbType.Int, 0, "TotalPrice");
			totalPriceParam.Direction = ParameterDirection.Output;
			SqlParameter idParam = adapter.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 0, "Id");
			idParam.Direction = ParameterDirection.Output;
		}
		public void UpdateData()
		{
			new SqlCommandBuilder(adapter);
			adapter.Update(dataTable);
			dataTable.Clear();
			adapter.Fill(dataTable);
		}
		public string GetTotalSum()
		{
			sqlCommand.CommandText = $"SELECT SUM([TotalPrice]) FROM [Products];";
			return sqlCommand.ExecuteScalar().ToString();
		}
	}
}
