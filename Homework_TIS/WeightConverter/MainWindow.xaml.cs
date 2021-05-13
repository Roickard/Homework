using System;
using System.Collections.Generic;
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

namespace WeightConverter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<double> Coefficients = new List<double>(5) { 0.002205, 0.00006105, 0.035274, 0.564383, 15.43236 };
		public MainWindow()
		{
			InitializeComponent();
		}
		private void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			FillResult();
		}
		private void tbChanging(object sender, KeyEventArgs e)
		{
			FillResult();
		}
		private void FillResult()
		{
			try
			{
				lbResult.Items.Clear();
				foreach (double weight in SplitWeights())
				{
					string type = (cbConverterType.SelectedItem as TextBlock).Text;
					string abbr = type.Substring(type.Length - 3, 2);
					lbResult.Items.Add($"{weight} g = {Convert(weight, cbConverterType.SelectedIndex)} {abbr}");
				}
			}
			catch (Exception) { }
		}
		private IEnumerable<double> SplitWeights()
		{
			string[] weigths = tbWeight.Text.Split(' ');
			foreach(string weight in weigths)
			{
				if(double.TryParse(weight, out double result))
				{
					yield return result;
				}
			}
		}
		private void SwitchStyle(object sender, RoutedEventArgs e)
		{
			lbResult.FontStyle = (cbItalic.IsChecked.Value) ? (FontStyles.Italic) : (FontStyles.Normal);
			lbResult.Foreground = (cbRed.IsChecked.Value) ? (Brushes.Red) : (Brushes.Black);
		}
		private double Convert(double source, int type)
		{
			double result = 0.0;
			try
			{
				result = source * Coefficients[type];
			}
			catch(Exception) {}

			return result;
		}
	}
}
