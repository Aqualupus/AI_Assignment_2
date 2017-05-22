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


			BuildKB KB = new BuildKB("test2.txt");
			//first the list of implies. then the variables then the true variables, then the conditional variables
			//then the debug mode and then the ask
			//TruthTable TT = new TruthTable(KB.Implies, KB.Vars, KB.TrueVars, KB.CondVars, true,KB.ASK());
			ForwardChain fwdChain =  new ForwardChain(KB.Implies, KB.Vars, KB.TrueVars, KB.CondVars, true,KB.ASK());

			//Console.WriteLine(TT.TruTab());
			Console.WriteLine(fwdChain.FwdChain());
			//BackwardChain BC = new BackwardChain(KB.Implies, KB.Vars, KB.TrueVars, false, KB.ASK());
			Console.ReadLine();
		}
	}
}
