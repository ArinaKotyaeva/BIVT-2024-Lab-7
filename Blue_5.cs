using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LAB_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            public string Name { get; }
            public string Surname { get; }
            public int Place { get; private set; }
            public bool IsPlaceSet { get; private set; }

            public Sportsman(string name, string surname)
            {
                Name = name;
                Surname = surname;
                Place = 0;
                IsPlaceSet = false;
            }

            public void SetPlace(int place)
            {
                if (IsPlaceSet)
                {
                    Console.WriteLine("Место уже установлено.");
                    return;
                }
                Place = place;
                IsPlaceSet = true;
            }

            public void Print()
            {
                Console.WriteLine($"Спортсмен: {Name} {Surname}, Место: {Place}");
            }
        }

        public abstract class Team
        {
            public string Name { get; }
            public Sportsman[] Sportsmen { get; private set; }
            private int _count;

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
                Name = name;
                Sportsmen = new Sportsman[6];
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