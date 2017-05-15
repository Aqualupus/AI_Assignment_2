using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Assignment_2
{
	public class TruthTable
	{

		private List<string> Implies = new List<string>();
		private List<string> Vars = new List<string>();
		//private List<string> TT = new List<string>();
		List<string> TrueVars;

		private string ask;

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
			Console.WriteLine();
			Console.WriteLine("**************************************************************************************");
			string result = "No";
			int maxsize = (int)Math.Pow(2, Vars.Count);
			//the table is the variables plus the implications 
			bool[,] TT = new bool[Vars.Count+Implies.Count,maxsize ];
			int i, j, k;
			i = 0;
			j = 0;
			bool flip = true;

			//fill the array by looping over it and filling normally
			for (k = 0; k <  Vars.Count; k++)
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
				j+=j;
				i = j-1;
			}
			//here I fill the implies at the end of the table
			for (i = 0; i < Implies.Count; i++)
			{
				//first I need to check for what variables are in the equation

				List<string> splitem = Implies[i].Split('=').ToList();
				splitem[1] = splitem[1].TrimStart('>');
				splitem.Add("");
				splitem.Add("");
				imp.Add(splitem);

			}
			//work out where in the list of variables the implies are
			for (i = 0; i < Vars.Count; i++)
			{
				foreach (List<string> s in imp)
				{
					if (s[0] == Vars[i]) s[2] = i.ToString();
					if (s[1] == Vars[i]) s[3] = i.ToString();
				}
			}
			for (k = 0; k < maxsize; k++)
			{
				i = Vars.Count;
				foreach (List<string> s in imp)
				{
					int numval1;
					int numval2;
					Int32.TryParse(s[2], out numval1);
					Int32.TryParse(s[3], out numval2);
					bool debug = false;

					for (j = 0; j < maxsize; j++)
					{
						if (TT[numval1, j] && (TT[numval2, j]))
						{
							TT[i, j] = true;
						if(debug)	Console.Write("{0} & {1} T\t", Vars[numval1], Vars[numval2]);
						}else
						if(!TT[numval1, j] && (TT[numval2, j]))
						{
							TT[i, j] = true;
							if(debug)	Console.Write("{0} & {1} T\t", Vars[numval1], Vars[numval2]);
						}else
						if (!TT[numval1, j] && (!TT[numval2, j]))
						{
							TT[i, j] = true;
							if(debug)	Console.Write("{0} & {1} T\t", Vars[numval1], Vars[numval2]);
						}
						else
						{
							TT[i, j] = false;
							if(debug)	Console.Write("{0} & {1} F\t", Vars[numval1], Vars[numval2]);
						}
					}
					if(debug) Console.WriteLine("new Line");
					i++;
				}
			}
			//bool var1 = false;
			//bool var2 = false;
			//work out where in the list of variables the implies are
			//for (i = 0;i <  Vars.Count; i++)
			//{
			//	foreach (List<string> s in imp)
			//	{
			//		if (s[0] == Vars[i]) s[2] = i.ToString();
			//		if (s[1] == Vars[i]) s[3] = i.ToString();
			//	}
			//}
			//i = Vars.Count;
			//foreach (List<string> s in imp)
			//{
			//	int numval1;
			//	int numval2;
			//	Int32.TryParse(s[2], out numval1);
			//	Int32.TryParse(s[3], out numval2);

			//	for (j = 0; j < maxsize; j++)
			//	{
			//		if (TT[numval1, j] && (TT[numval2, j]))
			//		{
			//			TT[i, j] = true;
			//		}
			//		if(!TT[numval1, j] && (TT[numval2, j]))
			//		{
			//			TT[i, j] = true;
			//		}
			//		if (!TT[numval1, j] && (!TT[numval2, j]))
			//		{
			//			TT[i, j] = true;
			//		}
			//		else
			//		{
			//			TT[i, j] = false;
			//		}
			//	}
			//	i++;
			//}
			//for (i = Vars.Count; i < Vars.Count + imp.Count; i++)
			//{
			//	int rownum1 = -1;
			//	int rownum2 = -1;
			//	for (j = 0; j < Vars.Count; j++)
			//	{
			//		//find them in the vars list
			//		if (Vars[j] == imp[i-Vars.Count][0]) rownum1 = j;
			//		if (Vars[j] == imp[i-Vars.Count][1]) rownum2 = j;
			//	}
			//	//it exists get the values
			//	if ((rownum1 > -1) && (rownum2 > -1))
			//	{
			//		//look at it from the rows perspective
			//		for (j = 0; j < maxsize; j++)
			//		{
			//			for (k = 0; k < Vars.Count; k++)
			//			{
			//				if ((k == rownum1) && (TT[k, j])) var1 = true;
			//				if ((k == rownum2) && (TT[k, j])) var2 = true;

				//			}
				//			if (var1 && !var2)
				//				{
				//					TT[i, j] = false;
				//				//Console.Write("{0} value is {1}", Vars[k],TT[i, j] );
				//				}
				//				else
				//				{
				//					TT[i, j] = true;
				//				}
				//			//Console.Write("{0} value is {1}", Vars[k],TT[i, j] );

				//		}
				//	}

				//}

				//int rownum = -1;
				//for (i = 0; i < Vars.Count;i++)
				//{
				//	if (Vars[i] == ask) rownum = i;
				//}
				//if (rownum > -1)
				//{
				//	for (i = 0; i < maxsize; i++)
				//	{
				//		if (TT[rownum, i])
				//		{
				//			for (j = 0; j < Vars.Count; j++)
				//			{
				//				if (!TT[j, i]) break;
				//				if (TT[j, i] && (j == Vars.Count-1))
				//				{
				//					howmany++;
				//					madeit = true;
				//				}
				//			}
				//		}
				//	}
				//}

				//printing the array to the console. Remove this later.
			for (int l = 0; l<Vars.Count + imp.Count ;l++){
					Console.Write("{0}\t",l);
				}
			Console.WriteLine();
				for (k = 0; k <  maxsize; k++)
			{
				//for (int l = Vars.Count + imp.Count-1; l >= 0;l--)
				//Console.Write(k);
				for (int l = 0; l < Vars.Count + imp.Count ;l++){
					Console.Write("{0}\t",TT[l, k]);
				}
				Console.WriteLine("\t{0}",k);

			}
			result = madeit + " " + howmany;
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
