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
		private string trueask;
		private bool negate = true;
		private int askplace;
		private bool debug;

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
			if (debug)
			{
				foreach (string s in Vars)
				{
					Console.Write("{0}\t", s);
				}
				foreach (string s in Implies)
				{
					Console.Write("{0}\t", s);
				}
				Console.Write("KB");
				Console.WriteLine();
				Console.WriteLine("**************************************************************************************");
			}
			string result = "No";
			int maxsize = (int)Math.Pow(2, Vars.Count);
			//the table is the variables plus the implications 
			bool[,] TT = new bool[Vars.Count + Implies.Count + 1, maxsize];
			int i, j, k;
			i = 0;
			j = 0;
			askplace = -1;
			bool flip = true;
			if (ask.Contains('!'))
			{
				ask = ask.TrimStart('!');
				negate = false;
			}
			foreach (string s in Vars)
			{
				i++;
				if (s == ask)
				{
					askplace = i-1;
					break;
				}


			}

			if(askplace == -1)
			{
				return "Error in ASK";
			}
			i = 0;
			//fill the array by looping over it and filling normally
			for (k = 0; k < Vars.Count; k++)
			{
				flip = false;

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
				j += j;
				i = j - 1;
			}
			//here I fill the implies at the end of the table
			for (i = 0; i < Implies.Count; i++)
			{
				//first I need to check for what variables are in the equation

				List<string> splitem = Implies[i].Split('=').ToList();
				splitem[1] = splitem[1].TrimStart('>');
				splitem.Add("-1");							//2 the operator or the value
				splitem.Add("-1");							//3 The second value
				splitem.Add("-1");							//4	the left side of the operator
				splitem.Add("-1");							//5 the right side of the operator
				splitem.Add("-1");							//6 the right side is not'd
				splitem.Add("-1");							//7 the left side is not'd
				splitem.Add("-1");							//8 the implied side is not'd

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

						if (temp[0].Contains('!'))
						{
							//do a thing here.
							s[6] = "false";
							temp[0] = temp[0].TrimStart('!');
						}
						if (temp[0] == Vars[i]) s[4] = i.ToString();

						if (temp[1].Contains('!'))
						{
							//do a thing here.
							s[7] = "false";
							temp[1] = temp[1].Trim('!');
						}
						if (temp[1] == Vars[i]) s[5] = i.ToString();
					}
					else if (s[0].Contains("|"))
					{
						string[] temp = s[0].Split('|');
						s[2] = "|";
						if (temp[0].Contains('!'))
						{
							//do a thing here.
							s[6] = "false";
							temp[0] = temp[0].TrimStart('!');
						}
						if (temp[0] == Vars[i]) s[4] = i.ToString();

						if (temp[1].Contains('!'))
						{
							//do a thing here.
							s[7] = "false";
							temp[1] = temp[1].Trim('!');
						}
						if (temp[1] == Vars[i]) s[5] = i.ToString();
					}
					else
					{
						if (s[0].Contains('!'))
						{
							s[6] = "false";
							s[0] = s[0].TrimStart('!');
						}
						if (s[0] == Vars[i]) s[2] = i.ToString();
					}
					if (s[1].Contains('!'))
					{
						s[8] = "false";
						s[1] = s[1].TrimStart('!');
					}
					if (s[1] == Vars[i]) s[3] = i.ToString();
				}
			}
			//is it a 2 variable value or 3?
			bool norm = true;
			i = Vars.Count;
			foreach (List<string> s in imp)
			{

				int numval1;
				int numval2;
				int numval3;
				//int numval4;
				//if it's not an int then there 2 3 values
				norm = Int32.TryParse(s[2], out numval1);
				Int32.TryParse(s[3], out numval2);

				if (norm)
				{

					for (j = 0; j < maxsize; j++)
					{
						bool firstvar = TT[numval1, j];
						bool implicat = TT[numval2, j];
							if (s[6] == "false") firstvar = !firstvar;
							if (s[8] == "false") implicat = !implicat;
						if ((firstvar) && (!implicat))
							{
								TT[i, j] = false;
							}
							else
							{
								TT[i, j] = true;
							}
					}
				}
				//not normal. figure out what the conditions are.
				if (!norm)
				{
					
					Int32.TryParse(s[4], out numval1);  //the first of the conditions
					Int32.TryParse(s[5], out numval3);  //the second of the conditions
				
					if (s[2] == "&")	//and then.....
					{
						for (j = 0; j < maxsize; j++)
						{
							bool conditional = false;
							bool firstvar = TT[numval1, j];
							bool secondvar = TT[numval3, j];
							bool implicat = TT[numval2, j];
							if (s[6] == "false") firstvar = !firstvar;
							if (s[7] == "false") secondvar = !secondvar;
							if (s[8] == "false") implicat = !implicat;
							if (( firstvar ) && ( secondvar ))
							{
								conditional = true;
							}

							if (conditional && !(implicat ))
							{
								TT[i, j] = false;
							}
							else
							{
								TT[i, j] = true;
							}
						}
					}

					if (s[2] == "|")	//or then.....
					{
						for (j = 0; j < maxsize; j++)
						{
							bool conditional = false;
							bool firstvar = TT[numval1, j];
							bool secondvar = TT[numval3, j];
							bool implicat = TT[numval2, j];
							if (s[6] == "false") firstvar = !firstvar;
							if (s[7] == "false") secondvar = !secondvar;
							if (s[8] == "false") implicat = !implicat;
							if ((firstvar) || (secondvar))
							{
								conditional = true;
							}

							if (conditional && !(implicat))
							{
								TT[i, j] = false;
							}
							else
							{
								TT[i, j] = true;
							}
						}
					}
				}
				i++;
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
				bool asked = TT[askplace, j];
				if (!negate) asked = !asked;
				//checking the asked variable and that the others also got through
				if (((asked)) && (allthethings))
				{

					for (i = Vars.Count; i < TT.GetUpperBound(0) ; i++)
					{

						allthethings = TT[i, j];

						if (!allthethings)
						{
							break;
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



			}

			//printing the array to the console. Remove this later.
			if (debug)
			{
				for (int l = 0; l < TT.GetUpperBound(0) + 1; l++)
				{
					Console.Write("{0}\t", l);
				}
				Console.WriteLine();
				for (k = 0; k < maxsize; k++)
				{
					//for (int l = Vars.Count + imp.Count-1; l >= 0;l--)
					//Console.Write(k);
					//print only the ones that are true
					if (TT[TT.GetUpperBound(0), k])
					{
						for (int l = 0; l <= TT.GetUpperBound(0); l++)
						{
							Console.Write("{0}\t", TT[l, k]);
						}
						Console.WriteLine("\t{0}", k);
					}


				}
				//Console.WriteLine(TT[Vars.Count-1,0]);
				Console.WriteLine("Ask {0}",trueask);
			}
			string finesimonyouareright = "YES";
			if (!madeit) finesimonyouareright = "NO";
			result = finesimonyouareright + ": " + howmany;
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

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AI_Assignment_2.TruthTable"/> class.
		/// </summary>
		/// <param name="imp">Imp.</param>
		/// <param name="vari">Vari.</param>
		/// <param name="_truevars">Truevars.</param>
		/// <param name="_condvar">Condvar.</param>
		/// <param name="asking">Asking.</param>
		public TruthTable(List<string> imp, List<string> vari, List<string> _truevars, List<string> _condvar,bool show,string asking)
		{
			Implies = imp;
			Vars = vari;
			ask = asking;
			trueask = asking;
			TrueVars = _truevars;
			CondVars = _condvar;
			debug = show;
			//BuildTT();
		}
	}
}
