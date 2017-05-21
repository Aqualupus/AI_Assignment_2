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
			string result = "";
			//use a queue to iterate through until a solution is found
			Stack<string> checkthese = new Stack<string>();
			Stack<string> endgoal = new Stack<string>();
			foreach (string s in trues)
			{
				checkthese.Push(s);
			}

			//while loop to go through the stack. break the loop when the solution is found
			while (checkthese.Count > 0)
			{
				//pull a value from the queue
				string checking = checkthese.Pop();

				endgoal.Push(checking);
				//compare this value with ones from the vars.
				//this stack will be the one that finds the goal
				while (endgoal.Count > 0)
				{
					foreach (string s in implies)
					{
						if (s.Contains(checking))
						{
							//pull it apart and see where to follow from.
							//split it
							string[] temp = s.Split('=');
							//check the easy one first
							if (temp[0] == checking)
							{
								temp[1] = temp[1].TrimStart('>');
								//assume after the imply is only one variable
								checking = temp[1];
								endgoal.Push(temp[1]);
								break;
							}
						}
					}
					if (checking == ask)
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
				result = "YES";
				//reverse the list first
				List<string> rightorder = new List<string>();
				rightorder = endgoal.ToList();
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
