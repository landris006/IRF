using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using szimulacio_pdiw2h.Entities;

namespace szimulacio_pdiw2h
{
    public partial class Form1 : Form
    {
        List<Person> population = new List<Person>();
        List<BirthProbability> birthProbabilities = new List<BirthProbability>();
        List<DeathProbability> deathProbabilities = new List<DeathProbability>();

        Random rng = new Random(1234);

        public Form1()
        {
            InitializeComponent();

            LoadPopulation("../../../../data/szimulacio/nép-teszt.csv");
            LoadBirthBrobabilities("../../../../data/szimulacio/születés.csv");
            LoadDeathProbabilites("../../../../data/szimulacio/halál.csv");

            for (int year = 2005; year <= 2024; year++)
            {
                // Végigmegyünk az összes személyen
                for (int i = 0; i < population.Count; i++)
                {
                    // Ide jön a szimulációs lépés
                }

                int nbrOfMales = (from x in population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }
        }

        List<Person> LoadPopulation(string filePath)
        {
            StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                string[] row = sr.ReadLine().Split(';');
                population.Add(new Person
                {
                    BirthYear = uint.Parse(row[0]),
                    Gender = (Gender)uint.Parse(row[1]),
                    NumberOfChildren = uint.Parse(row[2]),
                });
            }

            sr.Close();

            return population;
        }

        List<BirthProbability> LoadBirthBrobabilities(string filePath)
        {
            StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                string[] row = sr.ReadLine().Split(';');
                birthProbabilities.Add(new BirthProbability
                {
                    Age = uint.Parse(row[0]),
                    NumberOfChildren = uint.Parse(row[1]),
                    Propbability = double.Parse(row[2]),

                });
            }

            sr.Close();

            return birthProbabilities;
        }

        List<DeathProbability> LoadDeathProbabilites(string filePath)
        {
            StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                string[] row = sr.ReadLine().Split(';');
                deathProbabilities.Add(new DeathProbability
                {
                    Gender = (Gender)uint.Parse(row[0]),
                    Age = uint.Parse(row[1]),
                    Propbability = double.Parse(row[2]),
                });
            }

            sr.Close();

            return deathProbabilities;
        }
    }
}
