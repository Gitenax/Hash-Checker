using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cryptography;

namespace HashCkeckerConsole
{
	class Program
	{
		private static string PATH = $"{Environment.CurrentDirectory}\\Test.exe";

		static void Main(string[] args)
		{
			File sample = new File(PATH);
			Console.WriteLine(sample.ToString());


			Console.WriteLine(Hash.CalculateMD5(sample));
			Console.WriteLine(Hash.CalculateSHA1(sample));
			Console.WriteLine(Hash.CalculateSHA256(sample));
			Console.WriteLine(Hash.CalculateSHA384(sample));
			Console.WriteLine(Hash.CalculateSHA512(sample));

			Delay();
		}

		public static void Delay()
		{
			Console.ReadKey();
		}
	}
}
