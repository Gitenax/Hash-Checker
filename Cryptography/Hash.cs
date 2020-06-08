using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Cryptography
{
	/*
	 * MD5		- OK
	 * SHA1		- OK
	 * SHA256	- OK
	 * SHA384	- OK
	 * SHA512 - OK
	 * CRC32
	 * CRC64
	 */
	public static class Hash
	{
		
		public static string CalculateMD5(File file)
		{
			using (var service = new MD5CryptoServiceProvider())
				return CalculateOutput(service.ComputeHash(file.GetBytes()));
		}

		public static string CalculateSHA1(File file)
		{
			using (var service = new SHA1CryptoServiceProvider())
				return CalculateOutput(service.ComputeHash(file.GetBytes()));
		}

		public static string CalculateSHA256(File file)
		{
			using (var service = new SHA256CryptoServiceProvider())
				return CalculateOutput(service.ComputeHash(file.GetBytes()));
		}

		public static string CalculateSHA384(File file)
		{
			using (var service = new SHA384CryptoServiceProvider())
				return CalculateOutput(service.ComputeHash(file.GetBytes()));
		}

		public static string CalculateSHA512(File file)
		{
			using (var service = new SHA512CryptoServiceProvider())
				return CalculateOutput(service.ComputeHash(file.GetBytes()));
		}

		private static string CalculateOutput(byte[] bytes)
		{
			return BitConverter.ToString(bytes).Replace("-", String.Empty);
		}

	}


}
