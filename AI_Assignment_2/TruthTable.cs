using System;
using System.Collections.Generic;

namespace AI_Assignment_2
{
	public class TruthTable
	{

		private List<string> Implies = new List<string>();
		private List<string> Vars = new List<string>();
		private List<string> TT = new List<string>();
		List<string> TrueVars;

		private string ask;

		private bool deeper(string check)
		{
			foreach (string s in TrueVars)
			{
				if (s == check)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Builds the Truth Table.
		/// </summary>
		/// <returns>string telling if the statement is true.</returns>
		public string BuildTT()
		{
			string result = "No";
			string[] temp;
			int levels = 0;
			string nextlvl ="";
			int howdeep = 0;
			bool lookDeeper = false;
			foreach (string s in Implies)
			{
				//look for the ask first.
				if (s.Contains(ask))
				{
					temp = s.Split('=');
					temp[1] = temp[1].TrimStart('>');
					temp[1] = temp[1].TrimStart(' ');
					if (temp[1] == ask)
					{
						levels++;
						//result += "yes";
						if (deeper(ask))
						{
							result = "Yes " + levels;
							break;
						}
						nextlvl = temp[0].TrimStart(' ');
						lookDeeper = true;
						break;
					}
					howdeep++;
				}
			}

			while ((howdeep != (Implies.Count))&&(lookDeeper))
			{
				howdeep = 0;
				foreach (string s in Implies)
				{
					if (s.Contains(nextlvl))
					{
						temp = s.Split('=');
						temp[1] = temp[1].TrimStart('>');
						temp[1] = temp[1].TrimStart(' ');
						temp[1] = temp[1].TrimEnd(' ');
						if (temp[1] == nextlvl)
						{
							levels++;
							nextlvl = temp[0].TrimStart(' ');
							nextlvl = nextlvl.TrimEnd(' ');
							break;
						}
					}
					howdeep++;
				}

				result = "No ";
			}
			if (deeper(ask))
						{
							result = "Yes " + levels;
						}
			foreach (string s in TrueVars)
				{

				if (s == ask)
					{
						levels++;
						result = "Yes " + levels;
						break;
					}

				}
			return result;
		}

		public TruthTable(List<string> imp, List<string> vari, List<string> _truevars, string asking)
		{
			Implies = imp;
			Vars = vari;
			ask = asking;
			TrueVars = _truevars;
			//BuildTT();
		}
	}
}
