using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarketData
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly IMarket market;
		private readonly DataTable dataTable;
		public MainWindow()
		{
			InitializeComponent();
			dataTable = new DataTable();
			market = new IMarket(dataTable);
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			MarketGrid.ItemsSource = dataTable.DefaultView;
			UpdateProducts();
		}
		private void UpdateProducts()
		{
			market.UpdateData();
			Sum.Content = "Sum: " + market.GetTotalSum();
		}
		private void AddProduct(object sender, RoutedEventArgs e)
		{
			UpdateProducts();
		}
		private void DeleteProduct(object sender, RoutedEventArgs e)
		{
			if (MarketGrid.SelectedItems != null)
			{
				for (int selectedProduct = 0; selectedProduct < MarketGrid.SelectedItems.Count; ++selectedProduct)
				{
					DataRowView rowView = MarketGrid.SelectedItems[selectedProduct] as DataRowView;
					if (rowView != null)
					{
						rowView.Row.Delete();
						--selectedProduct;
					}
				}
			}

			UpdateProducts();
		}
	}
}
