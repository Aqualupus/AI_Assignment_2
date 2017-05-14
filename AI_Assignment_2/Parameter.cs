using System;

namespace AI_Assignment_2
{
    public class Parameter
    {
        private Boolean State;
        private String Name;

        public Parameter(String aName, Boolean aState){
            this.Name = aName;
            this.State = aState;

        }

        public Boolean GetState(){
            return State;
        }


		public String GetName()
		{
			return Name;
		}

		public void SetState(Boolean newState){
            State = newState;
        }

		public void SetName(String newName)
		{
			Name = newName;
		}


    }
}
