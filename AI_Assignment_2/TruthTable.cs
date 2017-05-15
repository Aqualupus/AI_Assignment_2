using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Assignment_2
{
	public class TruthTable
	{

		private List<string> Implies = new List<string>();
		private List<string> Vars = new List<string>();
		private List<string> CondVars = new List<string>();
		List<string> TrueVars;

		private string ask;
		private int askplace;

		/// <summary>
		/// check if the variable is true. If not look deeper
		/// </summary>
		/// <returns>The deeper.</returns>
		/// <param name="check">Check.</param>
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
		/// Trus the tab.
		/// </summary>
		/// <returns>The tab.</returns>
		public string TruTab()
		{
			int howmany = 0;
			bool madeit = false;
			List<List<string>> imp = new List<List<string>>();
		
			foreach (string s in Vars)
			{
				Console.Write("{0}\t",s);
			}
				foreach (string s in Implies)
			{
				Console.Write("{0}\t",s);
			}
			Console.Write("KB");
			Console.WriteLine();
			Console.WriteLine("**************************************************************************************");
			string result = "No";
			int maxsize = (int)Math.Pow(2, Vars.Count);
			//the table is the variables plus the implications 
			bool[,] TT = new bool[Vars.Count+Implies.Count+1,maxsize ];
			int i, j, k;
			i = 0;
			j = 0;
			bool flip = true;

			//fill the array by looping over it and filling normally
			for (k = 0; k <  Vars.Count; k++)
			{
				flip = false;
				if (Vars[k] == ask) askplace = k;
				for (int l = 0; l < maxsize; l++)
				{
					if (j == 0) j++;
					if (l == i)
					{
						i = l + j;
						flip = !flip;
					}
					TT[k, l] = flip;
				}
				j+=j;
				i = j-1;
			}
			//here I fill the implies at the end of the table
			for (i = 0; i < Implies.Count; i++)
			{
				//first I need to check for what variables are in the equation

				List<string> splitem = Implies[i].Split('=').ToList();
				splitem[1] = splitem[1].TrimStart('>');
				splitem.Add("-1");
				splitem.Add("-1");
				splitem.Add("-1");
				splitem.Add("-1");
				imp.Add(splitem);

			}

			//work out where in the list of variables the implies are
			for (i = 0; i < Vars.Count; i++)
			{
				foreach (List<string> s in imp)
				{
					if (s[0].Contains("&"))
					{
						string[] temp = s[0].Split('&');
						s[2] = "&";
						if (temp[0] == Vars[i]) s[4] = i.ToString();
						if (temp[1] == Vars[i]) s[5] = i.ToString();
					}
					else if (s[0].Contains("|"))
					{
						string[] temp = s[0].Split('|');
						s[2] = "|";
						if (temp[0] == Vars[i]) s[4] = i.ToString();
						if (temp[1] == Vars[i]) s[5] = i.ToString();
					}
					else
					{

						if (s[0] == Vars[i]) s[2] = i.ToString();
					}
					if (s[1] == Vars[i]) s[3] = i.ToString();
				}
			}
			//is it a 2 variable value or 3?
			bool norm = true;
			for (k = 0; k < maxsize; k++)
			{
				i = Vars.Count;
				foreach (List<string> s in imp)
				{
					
					int numval1;
					int numval2;
					int numval3;
					int numval4;
					norm = Int32.TryParse(s[2], out numval1);
					Int32.TryParse(s[3], out numval2);
					if (norm)
					{
						
						for (j = 0; j < maxsize; j++)
						{
							if (TT[numval1, j] && (TT[numval2, j]))
							{
								TT[i, j] = true;
							}
							else
							if (!TT[numval1, j] && (TT[numval2, j]))
							{
								TT[i, j] = true;
							}
							else
							if (!TT[numval1, j] && (!TT[numval2, j]))
							{
								TT[i, j] = true;
							}
							else
							{
								TT[i, j] = false;
							}
						}
					}
					if (!norm)
					{
						bool conditional = false;
						Int32.TryParse(s[4], out numval1);	//the first of the conditions
						Int32.TryParse(s[5], out numval3);  //the second of the conditions
						if (s[2] == "&")
						{
							for (j = 0; j < maxsize; j++)
							{
								if (TT[numval1, j] && TT[numval3, j])
								{
									conditional = true;
								}
							}
							for (j = 0; j < maxsize; j++)
							{
								if (conditional && (TT[numval2, j]))
								{
								TT[i, j] = true;
								}
								else
									if (!conditional && (TT[numval2, j]))
								{
									TT[i, j] = true;
								}
								else
										if (!conditional && (!TT[numval2, j]))
								{
									TT[i, j] = true;
								}
								else
								{
									TT[i, j] = false;
								}
							}
						}
					}
					i++;
				}
			}
			//be true to yourself people. Life lessons is what we learn here.
			List<int> truetoyourself = new List<int>();
			foreach (string s in TrueVars)
			{
				i = 0;
				foreach (string t in Vars)
				{
					if (s == t) truetoyourself.Add(i);
					i++;
				}
			}
			//here I need to check the KB
			for (j = 0; j < maxsize; j++)
			{
				bool allthethings = false;
				//checking all the true variables are true
				foreach (int r in truetoyourself)
				{
					allthethings = TT[r, j];
					if (!allthethings) break;
				}
				//checking the asked variable and that the others also got through
				if (TT[askplace, j] && allthethings)
				{
					
					for (i = Vars.Count-1; i < Vars.Count + imp.Count; i++)
					{

						allthethings = TT[i, j];

						if (!allthethings)
						{
							break;
						}

					}
				

				}
					//if it's gotten all the way through all of these things it should tick the howmany box
				if (allthethings)
				{
					howmany++;
					madeit = true;
				}
				TT[TT.GetUpperBound(0), j] = allthethings;

			}

				//printing the array to the console. Remove this later.
			for (int l = 0; l<TT.GetUpperBound(0) +1 ;l++){
					Console.Write("{0}\t",l);
				}
			Console.WriteLine();
				for (k = 0; k <  maxsize; k++)
			{
				//for (int l = Vars.Count + imp.Count-1; l >= 0;l--)
				//Console.Write(k);
				for (int l = 0; l < TT.GetUpperBound(0)+1;l++){
					Console.Write("{0}\t",TT[l, k]);
				}
				Console.WriteLine("\t{0}",k);

			}
			result = madeit + ": " + howmany;
			return result;
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

		public TruthTable(List<string> imp, List<string> vari, List<string> _truevars, List<string> _condvar,string asking)
		{
			Implies = imp;
			Vars = vari;
			ask = asking;
			TrueVars = _truevars;
			CondVars = _condvar;
			//BuildTT();
		}
	}
}
