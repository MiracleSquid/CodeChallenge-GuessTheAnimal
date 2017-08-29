using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheAnimal
{
    class AnimalCreator
    {
        public Animal NewAnimal;
        public AnimalFact.WordType CurrentFactType;

        public AnimalCreator()
        {
            NewAnimal= new Animal();
            CurrentFactType = AnimalFact.WordType.Adjective;
        }

        public string GetNextQuestion()
        {
            string message;
            if (string.IsNullOrEmpty(NewAnimal.Name))
            {
                message = "What is the name of the animal would you like to add?\n(Examples: Pig, Donkey, Chicken)";
            }
            else
            {
                switch (CurrentFactType)
                {
                    case AnimalFact.WordType.Adjective:
                        message =
                            "What is something your animal is?\nFor example: a Pig is 'pink', a Giraffe is 'tall', a Chicken is 'a bird'";
                        break;
                    case AnimalFact.WordType.Noun:
                        message =
                            "What is something your animal has?\nFor example: a Pig has 'a curly tail', a Spider has 'eight legs'";
                        break;
                    case AnimalFact.WordType.Verb:
                        message =
                            "What is something your animal does?\nFor example: a Pig does 'oink', a Shark does 'eat meat'";
                        break;
                    default:
                        throw new ArgumentException("Invalid Word Type");
                }
            }
            return string.Concat(message,
                "\nType your answer below and Click 'Yes' when finished, or 'No' to skip this step");
        }

        public bool AnswerQuestion(bool answer, string message)
        {
            if (string.IsNullOrEmpty(NewAnimal.Name))
            {
                if (string.IsNullOrEmpty(message) == false && answer)
                {
                    NewAnimal.Name = message;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(message) == false && answer)
                {
                    NewAnimal.Facts.Add(new AnimalFact(CurrentFactType, message));
                }
                else if (NewAnimal.Facts.Count(f => f.CurrentWordType == CurrentFactType) >= 1 &&
                         answer == false)
                {
                    switch (CurrentFactType)
                    {
                        case AnimalFact.WordType.Adjective:
                            CurrentFactType = AnimalFact.WordType.Noun;
                            break;
                        case AnimalFact.WordType.Noun:
                            CurrentFactType = AnimalFact.WordType.Verb;
                            break;
                        case AnimalFact.WordType.Verb:
                            
                            return true;
                        default:
                            throw new ArgumentException("Invalid Word Type");
                    }
                }
            }
            return false;
        }
    }
}
