using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.StarWars.Types;

namespace GraphQL.StarWars
{
    public class StarWarsData
    {
        private readonly List<Human> _humans = new List<Human>();
        private readonly List<Droid> _droids = new List<Droid>();

        private readonly List<Starship> _starShips = new List<Starship>();
        public StarWarsData()
        {

            _starShips.Add(new Starship
            {
                Id = "3000", Name = "Millennium Falcon",
                Length = 34.37F
            });

            _starShips.Add(new Starship
            {
                Id = "3001", Name = "X-Wing",
                Length = 12.5F
            });

            _starShips.Add(new Starship
            {
                Id = "3002", Name = "TIE Advanced x1",
                Length = 9.2F
            });

            _starShips.Add(new Starship
            {
                Id = "3003", Name = "Imperial shuttle",
                Length = 20F
            });

            _humans.Add(new Human
            {
                Id = "1000", Name = "Luke",
                Friends = new[] {"1002", "1003", "2000", "2001"},
                Mass = 77,
                Height = 1.72F,
                AppearsIn = new[] {Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI},
                Starships = new Starship[] { _starShips[1], _starShips[3]}
            });

            _humans.Add(new Human
            {
                Id = "1001", Name = "Darth Vader",
                Friends = new[] {"1004"},
                Mass = 136,
                Height = 2.02F,
                AppearsIn = new[] {Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI},
                Starships = new Starship[] { _starShips[2]}
            });

            _humans.Add(new Human
            {
                Id = "1002", Name = "Han Solo",
                Friends = new[] {"1000", "1003", "2001"},
                Mass = 80,
                Height = 1.8F,
                AppearsIn = new[] {Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI},
                Starships = new Starship[] { _starShips[0], _starShips[3]}
            });

            _humans.Add(new Human
            {
                Id = "1003", Name = "Leia Organa",
                Friends = new[] {"1000", "1002", "2000", "2001"},
                Mass = 49,
                Height = 1.5F,
                AppearsIn = new[] {Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI}
            });

            _humans.Add(new Human
            {
                Id = "1004", Name = "Wilhuff Tarkin",
                Friends = new[] {"1001"},
                Mass = 0,
                Height = 1.8F,
                AppearsIn = new[] {Episodes.NEWHOPE}
            });

            _droids.Add(new Droid
            {
                Id = "2000", Name = "C-3PO",
                Friends = new[] {"1000", "1002", "1003", "2001"},
                AppearsIn = new[] {Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI},
                PrimaryFunction = "Protocol"
            });
            _droids.Add(new Droid
            {
                Id = "2001", Name = "R2-D2",
                Friends = new[] {"1000", "1002", "1003"},
                AppearsIn = new[] {Episodes.NEWHOPE, Episodes.EMPIRE, Episodes.JEDI},
                PrimaryFunction = "Astromech"
            });
        }

        public IEnumerable<StarWarsCharacter> GetFriends(StarWarsCharacter character)
        {
            if (character == null)
            {
                return null;
            }

            var friends = new List<StarWarsCharacter>();
            var lookup = character.Friends;
            if (lookup != null)
            {
                _humans.Where(h => lookup.Contains(h.Id)).Apply(friends.Add);
                _droids.Where(d => lookup.Contains(d.Id)).Apply(friends.Add);
            }
            return friends;
        }

        public Task<Human> GetHero(Episodes ep)
        {
            return Task.FromResult(_humans[0]);
        }

        public Task<StarWarsCharacter> GetCharacter(string id)
        {   StarWarsCharacter c = null;
            foreach(var h in _humans){
                if (h.Id == id){
                    c = (StarWarsCharacter)h;
                    break;
                }
            }

            if (c == null){
                foreach(var h in _droids){
                    if (h.Id == id){
                        c = (StarWarsCharacter)h;
                        break;
                    }
                 }
            }
            return Task.FromResult(c);
        }

        public Task<float> GetHeighOrLenght(LengthUnit lu, float meters)
        {
            if (lu == LengthUnit.FOOT){
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

        public Human AddHuman(Human human)
        {
            human.Id = Guid.NewGuid().ToString();
            _humans.Add(human);
            return human;
        }
    }
}
