using System;

namespace AI_Assignment_2
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");

			//this obvioulsy needs to be better

			BuildKB KB = new BuildKB("test3.txt");

			TruthTable TT = new TruthTable(KB.Implies, KB.Vars,KB.TrueVars, "d");

			Console.WriteLine(TT.TruTab());
			Console.ReadLine();

		}
	}
}
