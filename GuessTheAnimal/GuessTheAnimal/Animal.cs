using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheAnimal
{
    class Animal
    {
        public string Name;
        public List<AnimalFact> Facts;
        string _fileName = "saved_animals.txt";

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
            using (StreamWriter writer = File.AppendText(_fileName))
            {
                var animalAsString = $"{Name}";

                foreach (var fact in Facts)
                {
                    animalAsString = $"{animalAsString},{fact.ToColonSeparatedString()}";
                }
                writer.WriteLine($"{animalAsString}");
            }
        }

        public void AddFactByColonSeparatedString(string fact)
        {
            var newFact = fact.Split(':');
            AnimalFact.WordType newFactWordType;
            if(newFact.Count()==2 && Enum.TryParse(newFact[0], out newFactWordType))
            {
                Facts.Add(new AnimalFact(newFactWordType, newFact[1]));
            }
        }
    }
}
