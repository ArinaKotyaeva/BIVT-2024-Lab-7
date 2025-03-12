using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LAB_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;

            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] copy = new int[_scores.Length];
                    Array.Copy(_scores, copy, copy.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;

                    int total = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        total += _scores[i];
                    }
                    return total;
                }
            }

            protected Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (_scores == null) return;

                int[] newScores = new int[_scores.Length + 1];

                for (int i = 0; i < _scores.Length; i++)
                {
                    newScores[i] = _scores[i];
                }
                newScores[newScores.Length - 1] = result;
                _scores = newScores;
            }

            public void Print()
            {
                Console.WriteLine($"{Name}: {TotalScore}");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }

        public class Group
        {
            private string _name;
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;
            private int _manInd;
            private int _womanInd;

            public string Name => _name;
            public ManTeam[] ManTeams => _manTeams;
            public WomanTeam[] WomanTeams => _womanTeams;

            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _manInd = 0;
                _womanInd = 0;
            }

            public void Add(Team team)
            {
                if (team is ManTeam manTeam && _manInd < _manTeams.Length)
                {
                    _manTeams[_manInd++] = manTeam;
                }
                else if (team is WomanTeam womanTeam && _womanInd < _womanTeams.Length)
                {
                    _womanTeams[_womanInd++] = womanTeam;
                }
            }

            public void Add(Team[] teams)
            {
                foreach (var team in teams)
                {
                    Add(team);
                }
            }

            public void Sort()
            {
                SortTeams(_manTeams, _manInd);
                SortTeams(_womanTeams, _womanInd);
            }

            private void SortTeams(Team[] teams, int count)
            {
                if (teams == null || count == 0) return;

                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = 0; j < count - 1 - i; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                MergeTeams(group1.ManTeams, group2.ManTeams, result.ManTeams, size / 2);
                MergeTeams(group1.WomanTeams, group2.WomanTeams, result.WomanTeams, size / 2);
                return result;
            }

            private static void MergeTeams(Team[] teams1, Team[] teams2, Team[] resultTeams, int size)
            {
                int i = 0, j = 0, k = 0;
                while (i < size && j < size)
                {
                    if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    {
                        resultTeams[k++] = teams1[i++];
                    }
                    else
                    {
                        resultTeams[k++] = teams2[j++];
                    }
                }
                while (i < size)
                {
                    resultTeams[k++] = teams1[i++];
                }
                while (j < size)
                {
                    resultTeams[k++] = teams2[j++];
                }
            }

            public void Print()
            {
                Console.WriteLine($"Группа: {_name}");
                Console.WriteLine("Мужские команды:");
                PrintTeams(_manTeams, _manInd);
                Console.WriteLine("Женские команды:");
                PrintTeams(_womanTeams, _womanInd);
            }

            private void PrintTeams(Team[] teams, int count)
            {
                if (teams != null && count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Команда {i + 1}");
                        teams[i].Print();
                    }
                }
                else
                {
                    Console.WriteLine("Нет данных");
                }
            }
        }
    }
}