using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheAnimal
{
    class AnimalFact
    {
        public WordType CurrentWordType;
        public string Value;

        public AnimalFact()
        {

        }

        public AnimalFact(WordType wordType, string value)
        {
            CurrentWordType = wordType;
            Value = value;
        }
        
        public enum WordType
        {
            Adjective,  //is
            Noun,       //has
            Verb        //does
        }

        public bool CompareToOtherFact(AnimalFact otherFact)
        {
            return CurrentWordType == otherFact.CurrentWordType && Value.Equals(otherFact.Value);
        }

        public bool IsFactInOtherFacts(List<AnimalFact> otherFacts)
        {
            foreach(var otherFact in otherFacts)
            {
                if(CompareToOtherFact(otherFact))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
