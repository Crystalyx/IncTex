using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IncTex
{
	public class TexMouth
	{
		/// <summary>
		/// Simple method to read file
		/// </summary>
		/// <param name="path"> name of the file to read</param>
		/// <returns> String array which contains lines of file</returns>
		private static string ReadFile(string path)
		{
			return System.IO.File.ReadAllText(path, Encoding.UTF8);
		}

		/// <summary>
		/// TeX file preprocessing
		/// </summary>
		public static List<Token> DoChewing(string filename)
		{
			string line = ReadFile(filename);

			List<Token> tokens = new List<Token>();
			List<char> rawToken = new List<char>();

			for (int j = 0; j < line.Length; j++)
			{
				char chr = line[j];
				if (TexCore.GetCategory(chr) != Category.Comment)
				{
					if (TexCore.GetCategory(chr) != Category.Special)
					{
						tokens.Add(new Token(chr, TexCore.GetCategory(chr)));
					}
					else
					{
						while (j + 1 < line.Length && IsLetter(line[j + 1]))
						{
							rawToken.Add(line[j + 1]);
							j++;
						}
						
						tokens.Add(new Token(rawToken.ToArray(), Category.Command));
						rawToken.Clear();
					}
				}
				else
				{
					break;
				}
			}

			return tokens;
		}

		private static bool IsLetter(char chr)
		{
			return (chr >= 'a' && chr <= 'z') || (chr >= 'A' && chr <= 'Z');
		}
	}
}