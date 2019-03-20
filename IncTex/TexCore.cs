using System;
using System.Collections.Generic;

namespace IncTex
{
	internal static class TexCore
	{
		/// <summary>
		/// Current state of TeX
		/// </summary>
		private static TexState State { get; } = new TexState();

		private static void Main(string[] args)
		{
			Token t = new Token(new[] {'a'}, 0);
			Console.WriteLine(t.Chars[0]);
			Console.WriteLine(Convert.ToInt16('A'));
			Console.WriteLine("Provide filename to read");
			string filename = "Test1.txt";//Read();
			List<Token> tokens = TexMouth.DoChewing(filename);
			var sum = "[";
			for (int i = 0; i < tokens.Count; i++)
			{
				sum += tokens[i].ToString();
				if (i < tokens.Count - 1)
				{
					sum += ',';
				}
			}
			Console.WriteLine(sum);
		}

		/// <summary>
		/// Wrapper Method for <see cref="TexState.GetCategory"/>
		/// </summary>
		/// <param name="chr"></param>
		/// <returns></returns>
		public static Category GetCategory(char chr)
		{
			return State.GetCategory(chr);
		}
	}

	public struct Token
	{
		/// <summary>
		/// An array that contains all simbols of this token
		/// </summary>
		///
		/// <remarks>
		/// Storage for simbols which were met by Tex
		/// </remarks>
		public char[] Chars { get; }
		
		/// <summary>
		/// Category number for this token
		/// </summary>
		///
		/// <examples>
		/// Ex:
		/// 	"par" - 16
		/// 	"{" - 1
		/// 	"}" - 2
		/// </examples>
		private Category Category { get; }
		
		public Token(char[] chars, Category category)
		{
			Chars = chars;
			Category = category;
		}
		
		public Token(char chr, Category category)
		{
			Chars = new []{chr};
			Category = category;
		}

		public override string ToString()
		{
			string ret = "";
			for (int i = 0; i < Chars.Length; i++)
			{
				ret += Chars[i];
				if (i + 1 < Chars.Length)
				{
					ret += ",";
				}
			}

			return "("+ret+"," + Category + ")";
		}
	}

	public enum Category
	{
		Special,
		GStart,
		GEnd,
		Maths,
		
		Tabulation,
		NewLine,
		Parameter,
		UpperIndex,
		
		LowerIndex,
		IgnoreChar,
		Space,
		Letter,
		
		Other,
		Active,
		Comment,
		Command,
		
		Null
	}
}