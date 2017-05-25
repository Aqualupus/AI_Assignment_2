using System;

namespace AI_Assignment_2
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");

			//this obvioulsy needs to be better


			//		BuildKB KB = new BuildKB("test2.txt");

			if (args.Length == 0)
			{
				BuildKB KB = new BuildKB("test1.txt");
				//TruthTable TT = new TruthTable(KB.Implies, KB.Vars, KB.TrueVars, KB.CondVars, true,KB.ASK());
				//ForwardChain fwdChain =  new ForwardChain(KB.Implies, KB.Vars, KB.TrueVars, KB.CondVars, true,KB.ASK());
				//Console.WriteLine(TT.TruTab());

				//Console.WriteLine(fwdChain.FwdChain());
				BackwardChain BC = new BackwardChain(KB.Implies, KB.Vars, KB.TrueVars, true, KB.ASK());
			}
			else
			{
				BuildKB KB = new BuildKB(args[1]);

				switch (args[0])
				{
					case "TT":
						{
							TruthTable TT = new TruthTable(KB.Implies, KB.Vars, KB.TrueVars, KB.CondVars, true, KB.ASK());
							Console.WriteLine(TT.TruTab());
							break;
						}
					case "BC":
						{
							BackwardChain BC = new BackwardChain(KB.Implies, KB.Vars, KB.TrueVars, true, KB.ASK());
							break;
						}
						default:
						{
							ForwardChain fwdChain = new ForwardChain(KB.Implies, KB.Vars, KB.TrueVars, KB.CondVars, true,KB.ASK());
							Console.WriteLine(fwdChain.FwdChain());
							break;
						}
				}
			}

			Console.ReadLine();
		}
	}
}
