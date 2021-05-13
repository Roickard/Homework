using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Collections;
using System.Globalization;

namespace ZNAKApp
{
    class MainFile
    {
		delegate void Mode();
		static Mode[] modes = new Mode[2]
		{
			Write,
			Read
		};
        static void Main(string[] args)
        {
			while (true)
			{
				Console.Clear();
				Console.Write("Mode\n" +
							  "1. Write.\n" +
							  "2. Read.\n" +
							  "Another number - close.\n" +
							  "Enter: ");
				if (int.TryParse(Console.ReadLine(), out int mode))
				{
					Console.Clear();
					if (mode > 2 || mode < 1)
					{
						break;
					}
					modes[mode - 1]();
				}
				
				Console.ReadKey();
			}
		}
		public static void Write()
		{
			ZNAK znak = new ZNAK();
			znak.SetData();
			using (FileStream file = new FileStream("file.json", FileMode.Append))
			{
				using (StreamWriter stream = new StreamWriter(file))
				{
					stream.WriteLine(znak.Serrialize());
				}
			}
		}
		public static void Read()
		{
			List<ZNAK> znaks = new List<ZNAK>();

			using (FileStream file = new FileStream("file.json", FileMode.Open))
			{
				using (StreamReader stream = new StreamReader(file))
				{
					while (!stream.EndOfStream)
					{
						znaks.Add(ZNAK.Desserialize(stream.ReadLine()));
					}
				}
			}
			while(true)
			{
				Console.Write("Month (0 to print the whole list): ");
				if(int.TryParse(Console.ReadLine(), out int month) && month >= 0 && month <= 12)
				{
					Print(month, znaks);
					break;
				}
			}
			
			
		}
		private static void Print(int month, List<ZNAK> znaks)
		{
			bool isElemsExist = false;
			znaks.Sort();
			foreach (ZNAK znak in znaks)
			{
				if(znak.Date.Month == month || month == 0)
				{
					Console.WriteLine($"{znak}\n");
					isElemsExist = true;
				}
			}
			if(!isElemsExist)
			{
				Console.WriteLine("No record exists in the list.");
			}
		}
    }
}
