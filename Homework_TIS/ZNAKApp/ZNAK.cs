using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections;
using System.Globalization;

namespace ZNAKApp
{
	enum Signs : byte
	{
		Aries,
		Taurus,
		Gemini,
		Cancer,
		Leo,
		Virgo,
		Libra,
		Scorpio,
		Sagittarius,
		Capricorn,
		Aquarius,
		Pisces,
		None
	}
	class AstroSign
	{
		private const byte SIGNS_CAPACITY = 12;
		private static Hashtable hashTabe;
		static AstroSign()
		{
			hashTabe = new Hashtable(SIGNS_CAPACITY);
			hashTabe.Add(Signs.Aries, 21);
			hashTabe.Add(Signs.Taurus, 20);
			hashTabe.Add(Signs.Gemini, 21);
			hashTabe.Add(Signs.Cancer,21);
			hashTabe.Add(Signs.Leo, 23);
			hashTabe.Add(Signs.Virgo, 23);
			hashTabe.Add(Signs.Libra, 23);
			hashTabe.Add(Signs.Scorpio, 23);
			hashTabe.Add(Signs.Sagittarius, 23);
			hashTabe.Add(Signs.Capricorn, 22);
			hashTabe.Add(Signs.Aquarius, 20);
			hashTabe.Add(Signs.Pisces, 19);
		}
		public static Signs GetSign(DateTime date)
		{
			int signDate = (int)hashTabe[(Signs)((date.Month + 9) % SIGNS_CAPACITY)];
			int prevDate = (int)hashTabe[(Signs)((date.Month + 8) % SIGNS_CAPACITY)];
			if (signDate <= date.Day && prevDate > date.Day)
			{
				return (Signs)((date.Month + 9) % SIGNS_CAPACITY);
			}
			else
			{
				return (Signs)((date.Month + 8) % SIGNS_CAPACITY);
			}
		}
	}
    class ZNAK : IComparable
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public Signs Sign { get; set; }
		public DateTime Date { get; set; }
		public ZNAK()
		{
			FirstName = "";
			SecondName = "";
			Sign = Signs.None;
			Date = DateTime.MinValue;
		}
		public void SetData()
		{
			
			FirstName = SetField("First name: ");
			SecondName = SetField("Second name: ");
			Date = ParseDate();
			Sign = AstroSign.GetSign(Date);
		}
		private string SetField(string message)
		{
			Console.Write(message);
			return Console.ReadLine();
		}
		private DateTime ParseDate()
		{
			DateTime date;
			while (true)
			{
				Console.Write("Date: ");
				if (DateTime.TryParse(Console.ReadLine(), out date))
				{
					break;
				}
			}
			return date;
		}
        public string Serrialize()
        {
            return JsonSerializer.Serialize(this);
        }
        public static ZNAK Desserialize(string json)
        {
            return JsonSerializer.Deserialize<ZNAK>(json);
        }
		public override string ToString()
		{
			return $"First name: {FirstName}\n" +
				   $"Second name: {SecondName}\n" +
				   $"Sign: {Sign}\n" +
				   $"Date: {Date.ToShortDateString()}";
		}

		public int CompareTo(object obj)
		{
			ZNAK znak = obj as ZNAK;
			return Sign.CompareTo(znak.Sign);
		}
	}
}
