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
			string[] strings =
			{
				"Проверка файла на MD5:",
				$"Файл: {PATH}",
				"Размер файла: 562,688 bytes",
				"MD5 файла: EBC13A2B29EB457B0B54140ED9E3B33E",
				"\n\n"
			};


			Console.WriteLine(String.Join("\n", strings));


			Delay();
		}

		public static void Delay()
		{
			Console.ReadKey();
		}
	}
}
