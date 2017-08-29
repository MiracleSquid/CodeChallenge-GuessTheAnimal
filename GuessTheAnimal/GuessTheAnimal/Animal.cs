﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheAnimal
{
    class Animal
    {
        public string Name;
        public List<AnimalFact> Facts;

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