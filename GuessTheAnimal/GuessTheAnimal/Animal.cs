using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuessTheAnimal
{
    internal class Animal
    {
        public string Name;
        public List<AnimalFact> Facts;
        private const string FileName = "saved_animals.txt";

        public Animal()
        {
            Facts = new List<AnimalFact>();
        }

        public Animal(string name)
        {
            Name = name;
            Facts = new List<AnimalFact>();
        }

        public void WriteAnimalToFile()
        {
            using (var writer = File.AppendText(FileName))
            {
                var animalAsString = $"{Name}";

                animalAsString = Facts.Aggregate(animalAsString, (current, fact) => $"{current},{fact.ToColonSeparatedString()}");
                writer.WriteLine($"{animalAsString}");
            }
        }

        public void AddFactByColonSeparatedString(string fact)
        {
            var newFact = fact.Split(':');
            AnimalFact.WordType newFactWordType;
            if(newFact.Length==2 && Enum.TryParse(newFact[0], out newFactWordType))
            {
                Facts.Add(new AnimalFact(newFactWordType, newFact[1]));
            }
        }
    }
}
