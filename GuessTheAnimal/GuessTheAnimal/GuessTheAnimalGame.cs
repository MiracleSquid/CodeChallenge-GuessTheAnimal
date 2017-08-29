using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheAnimal
{
    class GuessTheAnimalGame
    {
        public AnimalFact CurrentAnimalQuestion;
        public GamePhase CurrentGamePhase;
        private AnimalCreator _newAnimalCreator;
        List<AnimalFact> _askedAnimalQuestions;
        AnimalList _possibleAnimals;
        bool? _guessedCorrectly;

        public GuessTheAnimalGame()
        {
            InitializeGame();
        }

        public enum GamePhase
        {
            AskToPlay,
            AskToAddAnimal,
            AddAnimal,
            StartPlay,
            AskAnimalQuestions,
            GuessAnimal,
            EndGame
        }

        private void InitializeGame()
        {
            CurrentAnimalQuestion = new AnimalFact();
            CurrentGamePhase = GamePhase.AskToPlay;
            _askedAnimalQuestions = new List<AnimalFact>();
            _guessedCorrectly = null;
            _possibleAnimals = new AnimalList();
            _newAnimalCreator = new AnimalCreator();
        }

        public string GetQuestion()
        {
            switch(CurrentGamePhase)
            {
                case GamePhase.AskToPlay:
                    return $"{GetSuccessOrFailReaction()} Would you like to play Guess the Animal?";
                case GamePhase.AskToAddAnimal:
                    return "Would you like to add a new animal?";
                case GamePhase.AddAnimal:
                    return _newAnimalCreator.GetNextQuestion();
                case GamePhase.StartPlay:
                    return $"Choose one of these animals: {_possibleAnimals.GetAnimalNames()}. Now remember that animal, and click 'Yes' when you're ready";
                case GamePhase.AskAnimalQuestions:
                    _askedAnimalQuestions.Add(CurrentAnimalQuestion);
                    return GetNextAnimalQuestion();
                case GamePhase.GuessAnimal:
                    return GetAnimalGuess();
                case GamePhase.EndGame:
                    return "Goodbye";
                default:
                    throw new ArgumentException("Invalid Game Phase");
            }
        }

        public void AnswerQuestion(bool answer, string message)
        {
            switch (CurrentGamePhase)
            {
                case GamePhase.AskToPlay:
                    InitializeGame();
                    CurrentGamePhase = answer ? GamePhase.StartPlay : GamePhase.AskToAddAnimal;
                    break;
                case GamePhase.AskToAddAnimal:
                    CurrentGamePhase = answer ? GamePhase.AddAnimal : GamePhase.EndGame;
                    break;
                case GamePhase.AddAnimal:
                    if (_newAnimalCreator.AnswerQuestion(answer, message))
                    {
                        CurrentGamePhase = GamePhase.AskToPlay;
                    }
                    break;
                case GamePhase.StartPlay:
                    if (answer) CurrentGamePhase = GamePhase.AskAnimalQuestions;
                    break;
                case GamePhase.AskAnimalQuestions:
                    _possibleAnimals.FilterByFact(answer, CurrentAnimalQuestion);
                    if (NeedMoreAnimalFacts() == false)
                    {
                        if (IsAnimalUnknown())
                        {
                            _guessedCorrectly = false;
                            CurrentGamePhase = GamePhase.AskToPlay;
                        }
                        else
                        {
                            CurrentGamePhase = GamePhase.GuessAnimal;
                        }
                    }
                    break;
                case GamePhase.GuessAnimal:
                    _guessedCorrectly = answer;
                    CurrentGamePhase = GamePhase.AskToPlay;
                    break;
                case GamePhase.EndGame:
                    break;
                default:
                    throw new ArgumentException("Invalid Game Phase");
            }
        }

        private bool NeedMoreAnimalFacts()
        {
            return _possibleAnimals.Animals.Count() > 1;
        }

        private bool IsAnimalUnknown()
        {
            if (_possibleAnimals.Animals.Any() == false)
            {
                return true;
            }
            return false;
        }

        private string GetSuccessOrFailReaction()
        {
            switch (_guessedCorrectly)
            {
                case true:
                    return "Yay! I guessed correctly.";
                case false:
                    return "Hmm. I don't know which animal you picked! Maybe I don't have enough information.";
                default:
                    return "Hi!";
            }
        }

        private string GetNextAnimalQuestion()
        {
            var possibleFacts = new List<AnimalFact>();
            foreach (var animal in _possibleAnimals.Animals)
            {
                foreach(var fact in animal.Facts)
                {
                    possibleFacts.Add(fact);
                }
            }
            possibleFacts = possibleFacts.Where(newFact => _askedAnimalQuestions.Any(askedFact => askedFact == newFact) == false).Distinct().ToList();
            CurrentAnimalQuestion = possibleFacts.FirstOrDefault();
            return ConstructQuestionFromFact(CurrentAnimalQuestion);
        }

        public string ConstructQuestionFromFact(AnimalFact fact)
        {
            string prefix;
            string suffix = "";
            switch (fact.CurrentWordType)
            {
                case AnimalFact.WordType.Adjective:
                    prefix = "is";
                    break;
                case AnimalFact.WordType.Noun:
                    prefix = "does";
                    suffix = "have";
                    break;
                case AnimalFact.WordType.Verb:
                    prefix = "does";
                    break;
                default:
                    throw new ArgumentException("Invalid Word Type");

            }
            return $"{prefix} your animal {suffix} {fact.Value}?";
        }

        public string GetAnimalGuess()
        {
            string guessedAnimal = _possibleAnimals.Animals.FirstOrDefault().Name;
            string aOrAn = "aeiouAEIOU".IndexOf(guessedAnimal.Substring(0, 1)) >= 0 ? "an" : "a";
            return $"Is your animal {aOrAn} {guessedAnimal}?";
        }
    }
}
