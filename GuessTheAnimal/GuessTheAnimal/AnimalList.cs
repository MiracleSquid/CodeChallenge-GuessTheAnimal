using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuessTheAnimal
{
    internal class AnimalList
    {
        public List<Animal> Animals;
        private const string FileName = "saved_animals.txt";

        public AnimalList()
        {
            GetAnimalsFromFile();
        }

        private void GetAnimalsFromFile()
        {
            Animals = new List<Animal>();
            using (var reader = new StreamReader(FileName))
            {
                while (reader.EndOfStream == false)
                {

                    var line = reader.ReadLine();
                    var newAnimalFromLine = line.Split(',');
                    if (newAnimalFromLine.Length <= 1) continue;
                    var newAnimal = new Animal(newAnimalFromLine[0]);
                    for (var i = 1; i < newAnimalFromLine.Count(); i++)
                    {
                        newAnimal.AddFactByColonSeparatedString(newAnimalFromLine[i]);
                    }
                    Animals.Add(newAnimal);
                }
            }
        }

        public void FilterByFact(bool isFactCorrect, AnimalFact fact)
        {
            Animals = isFactCorrect
                ? Animals.Where(a => fact.IsFactInOtherFacts(a.Facts)).ToList()
                : Animals.Where(a => fact.IsFactInOtherFacts(a.Facts) == false).ToList();
        }

        public string GetAnimalNames()
        {
            return string.Join(", ", Animals.Select(a => a.Name));
        }
    }
}
