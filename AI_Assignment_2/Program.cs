using System;

namespace AI_Assignment_2
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");

			//this obvioulsy needs to be better

			BuildKB KB = new BuildKB("test1.txt");

			TruthTable TT = new TruthTable(KB.Implies, KB.Vars,KB.TrueVars, "d");
			Console.WriteLine(TT.TruTab());

			BackwardChain BC = new BackwardChain(KB.Implies, KB.Vars, KB.TrueVars, "d");
           
			
			Console.ReadLine();
		}
	}
}
