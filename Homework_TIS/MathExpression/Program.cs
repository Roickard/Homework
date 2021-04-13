using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace MathExpression
{
	class MathExpression
	{
		private readonly string filePath;
		private readonly List<string> expressions;
		private int exceptions = 0;

		object locker = new object();
		public List<string> Results { get; private set; }
		public MathExpression(string filePath)
		{
			this.filePath = filePath;
			expressions = new List<string>();
			Results = new List<string>();
		}
		public void ReadData()
		{
			expressions.Clear();
			using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate))
			{
				using (StreamReader stream = new StreamReader(file))
				{
					while (!stream.EndOfStream)
					{
						expressions.Add(stream.ReadLine());
					}
				}
			}
		}
		public void GenerateExpressions(long amountOfExpressions, int maxOperations, int minValue = -100, int maxValue = 100)
		{
			expressions.Clear();
			List<string> operations = new List<string>(4) { "+", "-", "*", "/" };
			Random random = new Random(DateTime.Now.Second);

			for (long currentExpression = 0; currentExpression < amountOfExpressions; ++currentExpression)
			{
				string expression = random.Next(minValue, maxValue).ToString();

				for(int currentOperation = 0; currentOperation < random.Next(2, maxOperations); ++currentOperation)
				{
					expression += operations[random.Next(0, 4)];
					int num = random.Next(minValue, maxValue);
					if (num < 0)
					{
						expression += $"({num})";
					}
					else
					{
						expression += num.ToString();
					}
				}

				expressions.Add(expression);
			}
			using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate))
			{
				using (StreamWriter stream = new StreamWriter(file))
				{
					foreach (string expression in expressions)
					{
						stream.WriteLine(expression);
					}
				}
			}
		}
		public void Calculate()
		{
			foreach(string expression in expressions)
			{
				Calculate(expression);
			}
			while (expressions.Count - exceptions != Results.Count) ;
		}
		private async void Calculate(string expression)
		{
			await Task.Run(() =>
			{
				try
				{
					lock (locker)
					{
						Results.Add($"{expression} = {new DataTable().Compute(expression, null)}");
					}
				}
				catch (Exception)
				{
					++exceptions;
				}
			});
		}
	}
	class Program
	{
		static void Main()
		{
			MathExpression mathExpression = new MathExpression("Test.txt");

			mathExpression.GenerateExpressions(1000, 5);
			mathExpression.ReadData();
			mathExpression.Calculate();
			List<string> answers = mathExpression.Results;
			foreach (string answer in answers)
			{
				Console.WriteLine(answer);
			}

			Console.ReadKey();
		}
    }
}
