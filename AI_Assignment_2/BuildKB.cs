using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AI_Assignment_2
{
	public class BuildKB
	{
		private List<string>  _kbbits = new List<string>();
		private List<string> _allbits = new List<string>();
		private string _file;
		private string _kb;
		private string _ask;
		//not too keen on using a list for this. Can't really think of anything better though. 
		public List<string> Implies = new List<string>();
		public List<string> Vars = new List<string>();
		public List<string> CondVars = new List<string>();
		public List<string> TrueVars = new List<string>();

		/// <summary>
		/// Parses the Knowledge Base from the text file.
		/// </summary>
		/// <returns><c>true</c>, if kb was parsed, <c>false</c> otherwise.</returns>
		public bool ParseKB()
		{
			try
			{
				// Create an instance of StreamReader to read from a file.
				// The using statement also closes the StreamReader.
				using (StreamReader sr = new StreamReader(_file))
				{
					string read;
					// Read and display lines from the file until the end of 
					// the file is reached.
					while (((read = sr.ReadLine()) != null))
					{
						_kbbits.Add(read);
					}

				}
			}
			catch (Exception e)
			{
				// Let the user know what went wrong.
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
				return false;
			}
			for (int i = 0; i < _kbbits.Count; i++)
			{
				if (_kbbits[i] == "")
				{
					_kbbits.RemoveAt(i);
				}
			}
			//kinda don't need these variables
			//maybe make the way we read the file better form here
			_kb = _kbbits[1];
			_ask = _kbbits[3];
			return true;
		}
		/// <summary>
		/// Build the Knowledge base
		/// </summary>
		private void build()
		{
			//by splitting at ; you create an empty value at the end.
			_allbits = _kb.Split(';').ToList();
			_allbits.RemoveAt(_allbits.Count-1);
			List<string> movehere = new List<string>();
			//remove all the spaces.
			foreach (string s in _allbits)
			{
				string temp = "";
				for (int i = 0; i < s.Length; i++)
				{
					if (s[i] != ' ')
					{
						temp += s[i].ToString();
					}
				}
				//temp = temp.TrimStart(' ');
				//Console.WriteLine("<{0}>", temp);
				movehere.Add(temp);

			}
			//copy cleaner variables to the _allbits list
			_allbits = movehere;

			//split up the variables into usable bits
			foreach (string s in _allbits)
			{
				if (s.Contains("=>"))
				{
					Implies.Add(s);
					string[] splitem = s.Split('=');
					splitem[1] = splitem[1].TrimStart('>');
					if (!Vars.Contains(splitem[0]) && !splitem[0].Contains("&") && !splitem[0].Contains("|") )
						Vars.Add(splitem[0]);
					if (!Vars.Contains(splitem[1]) && !splitem[1].Contains("&") && !splitem[1].Contains("|"))
						Vars.Add(splitem[1]);
					//split them at the and
					if (splitem[0].Contains("&"))
					{
						CondVars.Add(splitem[0]);
						string[] temp = splitem[0].Split('&');
						if (!Vars.Contains(temp[0]))
						Vars.Add(temp[0]);
						if (!Vars.Contains(temp[1]))
						Vars.Add(temp[1]);
					}
					if (splitem[1].Contains("&"))
					{
						CondVars.Add(splitem[1]);
						string[] temp = splitem[1].Split('&');
						if (!Vars.Contains(temp[0]))
							Vars.Add(temp[0]);
						if (!Vars.Contains(temp[1]))
						Vars.Add(temp[1]);
					}
					//split them at the or
					if (splitem[0].Contains("|"))
					{
						CondVars.Add(splitem[0]);
						string[] temp = splitem[0].Split('|');
						if (!Vars.Contains(temp[0]))
						Vars.Add(temp[0]);
						if (!Vars.Contains(temp[1]))
						Vars.Add(temp[1]);
					}
					if (splitem[1].Contains("|"))
					{
						CondVars.Add(splitem[1]);
						string[] temp = splitem[1].Split('|');
						if (!Vars.Contains(temp[0]))
						Vars.Add(temp[0]);
						if (!Vars.Contains(temp[1]))
						Vars.Add(temp[1]);
					}

				}
				else
				{
					if (!Vars.Contains(s)) Vars.Add(s);
					if(!TrueVars.Contains(s)) TrueVars.Add(s);
				}
			}




		}

		public BuildKB(string filename)
		{
			_file = filename;
			if (ParseKB())
			{
				build();
			}
		}
	}
}
