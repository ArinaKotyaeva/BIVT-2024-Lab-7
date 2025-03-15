using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltyTimes;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_penaltyTimes == null) return null;
                    int[] copy = new int[_penaltyTimes.Length];
                    Array.Copy(_penaltyTimes, copy, copy.Length);
                    return copy;
                }
            }

            public int Total
            {
                get
                {
                    if (_penaltyTimes == null) return 0;

                    int total = 0;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        total += _penaltyTimes[i];
                    }
                    return total;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];
            }

            public virtual void PlayMatch(int penaltyMinutes)
            {
                if (_penaltyTimes == null) return;

                int[] newArray = new int[_penaltyTimes.Length + 1];
                Array.Copy(_penaltyTimes, newArray, _penaltyTimes.Length);
                _penaltyTimes = newArray;
                _penaltyTimes[_penaltyTimes.Length - 1] = penaltyMinutes;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            var temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Участник: {Name} {Surname}");
                Console.WriteLine("Штрафные минуты:");

                if (_penaltyTimes != null)
                {
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        Console.Write($"{_penaltyTimes[i]} ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine($"Общее штрафное время: {Total}");

                if (IsExpelled)
                {
                    Console.WriteLine("Статус: Дисквалифицирован");
                }
                else
                {
                    Console.WriteLine("Статус: Активен");
                }
            }
        }

        public class BasketballPlayer : Participant
        {
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;

                    int matchesWithFiveFouls = 0;
                    foreach (var fouls in _penaltyTimes)
                    {
                        if (fouls == 5)
                        {
                            matchesWithFiveFouls++;
                        }
                    }

                    bool condition1 = matchesWithFiveFouls > _penaltyTimes.Length * 0.1;
                    bool condition2 = Total > _penaltyTimes.Length * 2;

                    return condition1 || condition2;
                }
            }
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
            }

            public override void PlayMatch(int fouls)
            {
                if (_penaltyTimes == null || fouls < 0 || fouls > 5) return;

                int[] newArray = new int[_penaltyTimes.Length + 1];
                Array.Copy(_penaltyTimes, newArray, _penaltyTimes.Length);
                _penaltyTimes = newArray;
                _penaltyTimes[_penaltyTimes.Length - 1] = fouls;
            }
        }

        public class HockeyPlayer : Participant
        {
            private int totalPenaltyMinutes;
            private static int countPlayers;
            private static int totalPenaltyMinutesAllPlayers;
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;

                    foreach (int penalty in _penaltyTimes)
                    {
                        if (penalty >= 10)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
                countPlayers++;
            }

            public override void PlayMatch(int penaltyMinutes)
            {
                if (_penaltyTimes == null || _penaltyTimes.Length == 0) return;
                base.PlayMatch(penaltyMinutes);
                if (penaltyMinutes >= 0)
                {
                    totalPenaltyMinutesAllPlayers += penaltyMinutes;
                }
            }
        }
    }
}
