using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheAnimal
{
    class AnimalList
    {
        public List<Animal> Animals;
        string _fileName = "saved_animals.txt";
        
        public AnimalList()
        {
            GetAnimalsFromFile();
        }
        private void GetAnimalsFromFile()
        {
            Animals = new List<Animal>();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(_fileName))
            {
                while (reader.EndOfStream == false)
                {
                    
                    string line = reader.ReadLine();
                    var newAnimalFromLine = line.Split(',');
                    if (newAnimalFromLine.Count() > 1)
                    {
                        var newAnimal = new Animal(newAnimalFromLine[0]);
                        for (int i = 1; i < newAnimalFromLine.Count(); i++)
                        {
                            newAnimal.AddFactByColonSeparatedString(newAnimalFromLine[i]);
                        }
                        Animals.Add(newAnimal);
                    }
                }
            }
        }
        public void FilterByFact(bool isFactCorrect, AnimalFact fact)
        {
            if(isFactCorrect)
            {
                Animals = Animals.Where(a => fact.IsFactInOtherFacts(a.Facts)).ToList();
            }
            else
            {
                Animals = Animals.Where(a => fact.IsFactInOtherFacts(a.Facts) == false).ToList();
            }
        }

        public string GetAnimalNames()
        {
            return string.Join(", ",Animals.Select(a=>a.Name));
        }
    }
}
