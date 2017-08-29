using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessTheAnimal
{
    internal class AnimalFact
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
            Adjective, //is
            Noun, //has
            Verb //does
        }

        public bool CompareToOtherFact(AnimalFact otherFact)
        {
            return CurrentWordType == otherFact.CurrentWordType && Value.Equals(otherFact.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsFactInOtherFacts(List<AnimalFact> otherFacts)
        {
            return otherFacts.Any(CompareToOtherFact);
        }

        public string ToColonSeparatedString()
        {
            return $"{CurrentWordType.ToString()}:{Value}";
        }

    }
}
