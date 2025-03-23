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
                        if (array[j] == null)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                        else if (array[j + 1] == null)
                        {
                            continue;
                        }
                        else if (array[j].Total > array[j + 1].Total) 
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
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
                    for (int k = 0; k < _penaltyTimes.Length; k++)
                    {
                        if (_penaltyTimes[k] >= 5)
                        {
                            matchesWithFiveFouls++;
                        }
                    }
                    if (matchesWithFiveFouls > 0.1 * _penaltyTimes.Length || this.Total > 2 * _penaltyTimes.Length)
                    {
                        return true;
                    }
                    return false;
                }
            }
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
            }

            public override void PlayMatch(int fouls)
            {
                if (_penaltyTimes == null || fouls < 0 || fouls > 5) return;

                if (fouls < 0 || fouls > 5)
                {
                    return;
                }
                base.PlayMatch(fouls);
            }
        }
        public class HockeyPlayer : Participant
        {
            private int _countPlayers;
            private int _time;
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;

                    for (int k = 0; k < _penaltyTimes.Length; k++)
                    {
                        if (_penaltyTimes[k] >= 10)
                        {
                            return true;
                        }
                    }
                    if (this.Total > 0.1 * _time / _countPlayers)
                    {
                        return true;
                    }
                    return false;
                }
            }
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
                _countPlayers++;
            }
            public override void PlayMatch(int penaltyMinutes)
            {
                if (_time == null) return;
                base.PlayMatch(penaltyMinutes);
                if (penaltyMinutes >= 0)
                {
                    _time += penaltyMinutes;
                }
            }
        }
    }
}
