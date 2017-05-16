using System;
using System.Collections.Generic;

namespace AI_Assignment_2
{
	public class BackwardChain
	{
        private List<string> Implies = new List<string>();
        private List<string> Vars = new List<string>();
        private List<Parameter> Params = new List<Parameter>();
        private Stack<string> TrueParams = new Stack<string>();

        List<string> TrueVars;

        private List<string> PositiveVars = new List<string>();

        //Queue for Params to validate against implicit Horn clauses
        private Stack<string> Validate = new Stack<string>();

        private string ask;

        private int TruthCount;

        private Boolean findTheTruth(string check){
            List<string> result = new List<string>();
            string[] temp;

			foreach (string s in Implies)
			{

				Console.WriteLine("Check if Implies Statement: " + s + " contains: " + check);

				//looks for the check variable in Horn clauses.
				if (s.Contains(check))
				{
                    Console.WriteLine("Found that: " + s + " contains: " + check);

					temp = s.Split('=');              //separate variables into temp string array
					temp[1] = temp[1].TrimStart('>'); //strip second element of ">"
					temp[1] = temp[1].TrimStart(' ');

                    //Validate if matched on right of Horn clause
                    if(temp[1].Contains(check)){
                        
  		                // Find Parameter that matches implicit Horn clause driver, set to True
		                // If already True, report to Console
		                foreach (Parameter p in Params){
		                    if(p.GetName()==temp[0]){
		                        if(p.GetState()){
		                            Console.WriteLine("Attempted: " + p.GetName() + " = " + "True. Already True.");
		                        }
		                        
		                        else{
		                            Console.WriteLine("Setting to True: " + temp[0]);
		                            p.SetState(true);
		                            TrueParams.Push(temp[0]);
		                            TruthCount++;

		                            //Add the identified True parameter to the Validate queue
		                            Validate.Push(temp[0]);
		                            Console.WriteLine("Adding to Validate stack: " + temp[0]);

		                        }
		                    }
		                }
                    }
				}
			}
            return true;
        }


        public string BuildTT()
        {

            Console.WriteLine("Parameters");
            foreach (string s in Vars)
            {
                Console.Write("{0}\t", s);
            }
            Console.WriteLine();
            Console.WriteLine("**************************************************************************************");

            Console.WriteLine("Implies");
            foreach (string s in Implies)
            {
                Console.Write("{0}\t", s);
            }

            Console.WriteLine();
            Console.WriteLine("**************************************************************************************");
            Console.WriteLine("True-Vars");
            foreach (string s in TrueVars)
            {
                Console.Write("{0}\t", s);
            }
            Console.WriteLine();
            Console.WriteLine("**************************************************************************************");

            // Populates List of Parameters with Parameter Objects with initial False states

            Boolean duplicate;
            foreach (string s in Vars)
            {
                duplicate = false;
                foreach (Parameter p in Params)
                {
                    if (p.GetName() == s)
                    {

                        duplicate = true;

                        Console.WriteLine("Duplicate Param Avoided: " + s);

                        break;
                    }

                }

                if (!duplicate)
                {
                    Params.Add(new Parameter(s, false));
                }


            }

            //Add initial Ask Value to TrueParams Stack
            TrueParams.Push(ask);


            foreach (Parameter s in Params)
            {
                Console.WriteLine("Created: " + s.GetName() + " With State " + s.GetState());
            }

            //Push initial Ask value to end of Validation List (should be first item)
            Validate.Push(ask);


            //Default result 
            string result = "No";

            //Holding String
            string Check;
            while (Validate.Count != 0)
            {
                Check = Validate.Peek();
                //Pop Current parameter for Validation from stack
                Validate.Pop();
                Console.WriteLine("Commence Validation of: " + Check);
                if (findTheTruth(Check))
                {
                    Console.WriteLine(Check + " Validated");

                }
                else
                {
                    Console.WriteLine("Error: findTheTruth did not return True for: " + Check);
                }

            }

            //Compose result
            if (TruthCount > 0)
            {
                result = "YES: ";
            }

            while (TrueParams.Count > 0)
            {
                result += TrueParams.Peek();
                TrueParams.Pop();
                if (TrueParams.Count > 0)
                {
                    result += ", ";
                }
            }

            return result;

        }
	


		public BackwardChain(List<string> imp, List<string> vari, List<string> _truevars, string asking)
		{
            Implies = imp;
            Vars = vari;
            ask = asking;  //assigns hardcoded "d" from Program
            TruthCount = 0;
            TrueVars = _truevars;
			Console.WriteLine(BuildTT());

		}
	}
}
