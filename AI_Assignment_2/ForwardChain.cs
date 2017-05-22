using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AI_Assignment_2
{
	public class ForwardChain
	{
		private List<string> implies;
		private List<string> vars;
		private List<string> trues;
		private List<string> conditionalvars;
		private bool debug;
		private string ask;

		/// <summary>
		/// this checks the variables in the conditional statements
		/// </summary>
		/// <returns>The conditional.</returns>
		/// <param name="val">Value.</param>
		private bool conditional(string val)
		{
			
			bool result = false;
			return result;
		}

		/// <summary>
		/// Fwds the chain.
		/// </summary>
		/// <returns>The chain.</returns>
		public string FwdChain()
		{
			string result = "NO";
			//use a queue to iterate through until a solution is found
			Stack<string> checkthese = new Stack<string>();
			Stack<string> endgoal = new Stack<string>();
			List<string> vaildvars = new List<string>();
			foreach (string s in trues)
			{
				checkthese.Push(s);
				vaildvars.Add(s);
			}

			//while loop to go through the stack. break the loop when the solution is found
			while (checkthese.Count > 0)
			{
				//pull a value from the queue
				string checking = checkthese.Pop();

				endgoal.Push(checking);
				//compare this value with ones from the vars.
				//this stack will be the one that finds the goal
				bool getout = false;
				while (endgoal.Count > 0)
				{
					List<string> removablelist = implies;
					int i;
					for ( i = 0; i < removablelist.Count;i++)
					{
						if (removablelist[i].Contains(checking))
						{
							//pull it apart and see where to follow from.
							//split it
							string[] temp = removablelist[i].Split('=');
							temp[1] = temp[1].TrimStart('>');
							//check the easy one first
							if (temp[0] == checking)
							{
								temp[1] = temp[1].TrimStart('>');
								//assume after the imply is only one variable
								checking = temp[1];
								endgoal.Push(temp[1]);
								removablelist = implies;
								//break;
								getout = true;
							}
							else
							{
								if (temp[0].Contains('&') && temp[0].Contains(checking))
								{
									string[] othertemp = temp[0].Split('&');
									if (othertemp[0] != checking)
									{
										if (!vaildvars.Contains(othertemp[0]))
										{
											getout = true;
											//endgoal.Clear();
											break;
										}
										//is the other variable in the list and true?
										//do I implement a backwards chaining algorithm here? 
									}
								}
								//looping because of this 
								if (temp[1] == checking)
							{
									removablelist.RemoveAt(i);
								//endgoal.Clear();
								//break;
							}
								if (temp[0].Contains(checking))
								{
									temp[1] = temp[1].TrimStart('>');
									//assume after the imply is only one variable
									checking = temp[1];
									endgoal.Push(temp[1]);
									removablelist = implies;
									break;
								}
							}


						}
						if (i == removablelist.Count) endgoal.Clear();
					}

					if (checking == ask || getout)
					{
						break;
					}

				}
				if (checking == ask)
					{
						break;
					}
			}


			if (endgoal.Count > 0)
			{
				result = "YES:";
				//reverse the list first
				List<string> rightorder = new List<string>();
				rightorder = endgoal.ToList();
				//rightorder.Add("other");
				foreach (string s in checkthese) rightorder.Add(s);
				rightorder.Reverse();
				foreach (String s in rightorder)
				{
					result += " "+s;
				}
			}
			return result;
		}


	public ForwardChain(List<string> imp, List<string> vari, List<string> _truevars, List<string> _condvar,bool show,string asking)
		{
			implies = imp;
			vars = vari;
			trues = _truevars;
			conditionalvars = _condvar;
			debug = show;
			ask = asking;
		}
	}
}
