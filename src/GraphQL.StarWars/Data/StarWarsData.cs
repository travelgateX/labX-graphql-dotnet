using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.StarWars.Enum;
using GraphQL.StarWars.Types;

namespace GraphQL.StarWars.Data
{
    public class StarWarsData
    {
        private readonly List<Human> _humans = new List<Human>();
        private readonly List<Droid> _droids = new List<Droid>();
        private readonly Dictionary<Episodes, List<Review>> _reviews = new Dictionary<Episodes, List<Review>>();

        private readonly List<Starship> _starShips = new List<Starship>();
        public StarWarsData()
        {
            foreach (Episodes ep in System.Enum.GetValues(typeof(Episodes)))
            {
                _reviews.Add(ep, new List<Review>());
            }

            _starShips.Add(new Starship
            {
                Id = "3000",
                Name = "Millennium Falcon",
                Length = 34.37F
            });

            _starShips.Add(new Starship
            {
                Id = "3001",
                Name = "X-Wing",
                Length = 12.5F
            });

            _starShips.Add(new Starship
            {
                Id = "3002",
                Name = "TIE Advanced x1",
                Length = 9.2F
            });

            _starShips.Add(new Starship
            {
                Id = "3003",
                Name = "Imperial shuttle",
                Length = 20F
            });

            _humans.Add(new Human
            {
                Id = "1000",
                Name = "Luke",
                Mass = 77,
                Height = 1.72F,
                AppearsIn = new[] { Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI },
                Starships = new Starship[] { _starShips[1], _starShips[3] }
            });

            _humans.Add(new Human
            {
                Id = "1001",
                Name = "Darth Vader",
                Mass = 136,
                Height = 2.02F,
                AppearsIn = new[] { Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI },
                Starships = new Starship[] { _starShips[2] }
            });

            _humans.Add(new Human
            {
                Id = "1002",
                Name = "Han Solo",
                Mass = 80,
                Height = 1.8F,
                AppearsIn = new[] { Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI },
                Starships = new Starship[] { _starShips[0], _starShips[3] }
            });

            _humans.Add(new Human
            {
                Id = "1003",
                Name = "Leia Organa",
                Mass = 49,
                Height = 1.5F,
                AppearsIn = new[] { Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI }
            });

            _humans.Add(new Human
            {
                Id = "1004",
                Name = "Wilhuff Tarkin",
                Mass = 0,
                Height = 1.8F,
                AppearsIn = new[] { Episodes.NEWHOPE }
            });

            _droids.Add(new Droid
            {
                Id = "2000",
                Name = "C-3PO",
                AppearsIn = new[] { Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI },
                PrimaryFunction = "Protocol"
            });
            _droids.Add(new Droid
            {
                Id = "2001",
                Name = "R2-D2",
                AppearsIn = new[] { Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI },
                PrimaryFunction = "Astromech"
            });

            _humans[0].Friends = new[] { (StarWarsCharacter)_humans[2], _humans[2], _droids[0], _droids[1] };
            _humans[1].Friends = new[] { (StarWarsCharacter)_humans[4] };
            _humans[2].Friends = new[] { (StarWarsCharacter)_humans[0], _humans[3], _droids[1] };
            _humans[3].Friends = new[] { (StarWarsCharacter)_humans[0], _humans[2], _droids[0], _droids[1] };
            _humans[4].Friends = new[] { (StarWarsCharacter)_humans[0], _humans[2], _droids[0], _droids[1] };
            _humans[3].Friends = new[] { (StarWarsCharacter)_humans[1] };

            _droids[0].Friends = new[] { (StarWarsCharacter)_humans[0], _humans[2], _humans[3], _droids[1] };
            _droids[1].Friends = new[] { (StarWarsCharacter)_humans[0], _humans[2], _humans[3] };
        }


        public Task<List<object>> GetSearchResult(string text)
        {
            List<object> l = new List<object>();

            foreach(var h in _droids)
            {
                if (h.Name.Contains(text))
                {
                    l.Add(h);
                }
            }

            foreach (var h in _humans)
            {
                if (h.Name.Contains(text))
                {
                    l.Add(h);
                }
            }

            foreach (var h in _starShips)
            {
                if (h.Name.Contains(text))
                {
                    l.Add(h);
                }
            }

            return Task.FromResult(l);
        }


        public Task<List<Review>> GetReview(Episodes ep)
        {
            return Task.FromResult(_reviews[ep]);
        }
        public Task<Human> GetHero(Episodes ep)
        {
            return Task.FromResult(_humans[0]);
        }

        public Task<StarWarsCharacter> GetCharacter(string id)
        {
            StarWarsCharacter c = null;
            foreach (var h in _humans)
            {
                if (h.Id == id)
                {
                    c = (StarWarsCharacter)h;
                    break;
                }
            }

            if (c == null)
            {
                foreach (var h in _droids)
                {
                    if (h.Id == id)
                    {
                        c = (StarWarsCharacter)h;
                        break;
                    }
                }
            }
            return Task.FromResult(c);
        }

        public Task<float> GetHeighOrLenght(LengthUnit lu, float meters)
        {
            if (lu == LengthUnit.FOOT)
            {
                return Task.FromResult(meters * 3.28084F);
            }
            return Task.FromResult(meters);
        }

        public Task<Human> GetHumanByIdAsync(string id)
        {
            return Task.FromResult(_humans.FirstOrDefault(h => h.Id == id));
        }

        public Task<Droid> GetDroidByIdAsync(string id)
        {
            return Task.FromResult(_droids.FirstOrDefault(h => h.Id == id));
        }

        public Task<Starship> GetStarShipByIdAsync(string id)
        {
            return Task.FromResult(_starShips.FirstOrDefault(h => h.Id == id));
        }

        public Review AddReview(Episodes ep, Review ri)
        {
            _reviews[ep].Add(ri);
            return ri;
        }
    }
}
