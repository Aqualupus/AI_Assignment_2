using System;

namespace AI_Assignment_2
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");

			//this obvioulsy needs to be better

			BuildKB KB = new BuildKB("apple.txt");

			TruthTable TT = new TruthTable(KB.Implies, KB.Vars,KB.TrueVars, "apple");

			Console.WriteLine(TT.BuildTT());
			Console.ReadLine();

		}
	}
}
