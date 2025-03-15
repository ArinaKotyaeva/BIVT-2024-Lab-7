using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;


            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            public void SetPlace(int place)
            {
                if (place > 0)
                {
                    _place = place;
                }
                else return;
            }

            public void Print()
            {
                Console.WriteLine($"Спортсмен: {Name} {Surname}, Место: {Place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return null;
                    return _sportsmen;
                }
            }

            public int SummaryScore
            {
                get
                {
                    if (Sportsmen == null || Sportsmen.Length == 0)
                        return 0;

                    int totalScore = 0;
                    foreach (var sportsman in Sportsmen)
                    {
                        switch (sportsman.Place)
                        {
                            case 1: totalScore += 5; break;
                            case 2: totalScore += 4; break;
                            case 3: totalScore += 3; break;
                            case 4: totalScore += 2; break;
                            case 5: totalScore += 1; break;
                            default: break;
                        }
                    }
                    return totalScore;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (Sportsmen == null || Sportsmen.Length == 0)
                        return 0;

                    int topPlace = int.MaxValue;
                    foreach (var sportsman in Sportsmen)
                    {
                        if (sportsman.Place < topPlace && sportsman.Place != 0)
                        {
                            topPlace = sportsman.Place;
                        }
                    }
                    return topPlace == int.MaxValue ? 0 : topPlace;
                }
            }

            protected Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_count < 6)
                {
                    Sportsmen[_count] = sportsman;
                    _count++;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            if (teams[j].TopPlace > teams[j + 1].TopPlace) (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                    }
                }
            }
            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0)
                    return null;

                Team champion = teams[0];
                double maxStrength = champion.GetTeamStrength();

                for (int i = 1; i < teams.Length; i++)
                {
                    double currentStrength = teams[i].GetTeamStrength();
                    if (currentStrength > maxStrength)
                    {
                        maxStrength = currentStrength;
                        champion = teams[i];
                    }
                }

                return champion;
            }
            public void Print()
            {
                Console.WriteLine($"Команда: {Name}");
                Console.WriteLine($"Суммарный балл: {SummaryScore}");
                Console.WriteLine($"Наивысшее место: {TopPlace}");
                Console.WriteLine("Спортсмены:");

                if (Sportsmen != null && Sportsmen.Length > 0)
                {
                    foreach (var sportsman in Sportsmen)
                    {
                        sportsman?.Print();
                    }
                }
                else
                {
                    Console.WriteLine("Нет данных о спортсменах.");
                }
            }

        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0)
                    return 0;

                double sumPlaces = 0;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman?.Place != 0)
                    {
                        sumPlaces += sportsman.Place;
                        count++;
                    }
                }

                if (count == 0)
                    return 0;

                return 100 / (sumPlaces / count);
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0)
                    return 0;

                double sumPlaces = 0;
                double productPlaces = 1;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman?.Place != 0)
                    {
                        sumPlaces += sportsman.Place;
                        productPlaces *= sportsman.Place;
                        count++;
                    }
                }

                if (count == 0)
                    return 0;

                return 100 * sumPlaces * count / productPlaces;
            }
        }
    }
}
