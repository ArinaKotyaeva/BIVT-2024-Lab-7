using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            protected Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;
            public abstract double[] Prize { get; } 

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0]; 
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;

                Participant[] participant2 = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    participant2[i] = _participants[i];
                }
                participant2[_participants.Length] = participant;
                _participants = participant2;
            }

            public void Add(Participant[] participants)
            {
                if (_participants == null || participants == null || participants.Length == 0) return;

                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
        }
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (this.Participants.Length < 3 || this.Participants == null) return default(double[]);

                    double[] reward = new double[3];

                    reward[0] = this.Bank * 0.5; 
                    reward[1] = this.Bank * 0.3; 
                    reward[2] = this.Bank * 0.2; 

                    return reward;
                }
            }
        }
        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                   if (this.Participants.Length < 3 || this.Participants == null) return default(double[]);

                    int counter;
                    double[] reward;
                    if (Participants.Length / 2 < 10)
                    {
                        reward = new double[Participants.Length / 2];
                        counter = Participants.Length / 2;
                    }
                    else
                    {
                        reward = new double[10];
                        counter = 10;
                    }
                    double share = 20.0 / counter;

                    for (int i = 0; i < counter; i++)
                    {
                        reward[i] = this.Bank * (share / 100);
                    }

                    reward[0] += this.Bank * 0.4; 
                    reward[1] += this.Bank * 0.25;
                    reward[2] += this.Bank * 0.15; 

                    return reward;

                }
            }
        }
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _ind;

            public string Name => _name;
            public string Surname => _surname;

            public int[,] Marks
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return null;
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            copy[i, j] = _marks[i, j];
                        }
                    }
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return 0;

                    int sum = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            sum += _marks[i, j];
                        }
                    }
                    return sum;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _ind = 0;
            }
            public void Jump(int[] result)
            {
                if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0 || result == null || result.Length == 0 || _ind > 1) return;

                if (_ind == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _marks[0, i] = result[i];
                    }
                    _ind++;
                }
                else if (_ind == 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _marks[1, i] = result[i];
                    }
                    _ind++;
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].TotalScore > array[j].TotalScore)
                        {
                            (array[j + 1], array[j]) = (array[j], array[j + 1]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Участник: {_name} {_surname}");
                Console.WriteLine("Оценки за прыжки:");

                if (_marks != null)
                {
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        Console.Write($"Прыжок {i + 1}: ");
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            Console.Write($"{_marks[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Оценки отсутствуют.");
                }

                Console.WriteLine($"Общий балл: {TotalScore}");
            }
        }
    }
}
