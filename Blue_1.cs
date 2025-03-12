using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LAB_7
{
    public class Blue_1
    {
        public class Response
        {
            private string _name;
            public string Name => _name;

            protected int _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                _votes = 0;
                foreach (var response in responses)
                {
                    if (response.Name == _name)
                    {
                        _votes++;
                    }
                }
                return _votes;
            }

            public virtual void Print()
            {
                Console.WriteLine($"Кандидат: {_name}, Голосов: {_votes}");
            }
        }

        public class HumanResponse : Response
        {
            private string _surname;
            public string Surname => _surname;


            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                _votes = 0;
                foreach (var response in responses)
                {
                    if (response is HumanResponse humanResponse && humanResponse.Name == Name && humanResponse.Surname == Surname)
                    {
                        _votes++;
                    }
                }
                return _votes;
            }
            public override void Print()
            {
                Console.WriteLine($"Кандидат: {Name} {Surname}, Голосов: {_votes}");
            }
        }
    }
}