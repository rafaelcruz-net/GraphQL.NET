using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLNET
{
    public class StarWarsData
    {
        #region Types
        public class HumanType : ObjectGraphType<Human>
        {
            public HumanType(StarWarsData data)
            {
                Name = "Human";

                Field(h => h.Id).Description("The id of the human.");
                Field(h => h.Name, nullable: true).Description("The name of the human.");

                Field<ListGraphType<CharacterInterface>>(
                    "friends",
                    resolve: context => data.GetFriends(context.Source)
                );
                Field<ListGraphType<EpisodeEnum>>("appearsIn", "Which movie they appear in.");

                Field(h => h.HomePlanet, nullable: true).Description("The home planet of the human.");

                Interface<CharacterInterface>();
            }
        }

        public class DroidType : ObjectGraphType<Droid>
        {
            public DroidType(StarWarsData data)
            {
                Name = "Droid";
                Description = "A mechanical creature in the Star Wars universe.";

                Field(d => d.Id).Description("The id of the droid.");
                Field(d => d.Name, nullable: true).Description("The name of the droid.");

                Field<ListGraphType<CharacterInterface>>(
                    "friends",
                    resolve: context => data.GetFriends(context.Source)
                );
                Field<ListGraphType<EpisodeEnum>>("appearsIn", "Which movie they appear in.");
                Field(d => d.PrimaryFunction, nullable: true).Description("The primary function of the droid.");

                Interface<CharacterInterface>();
            }
        }

        #endregion

        private readonly List<Human> _humans = new List<Human>();
        private readonly List<Droid> _droids = new List<Droid>();

        public StarWarsData()
        {
            _humans.Add(new Human
            {
                Id = "1",
                Name = "Luke",
                Friends = new[] { "3", "4" },
                AppearsIn = new[] { 4, 5, 6 },
                HomePlanet = "Tatooine"
            });
            _humans.Add(new Human
            {
                Id = "2",
                Name = "Vader",
                AppearsIn = new[] { 4, 5, 6 },
                HomePlanet = "Tatooine"
            });

            _droids.Add(new Droid
            {
                Id = "3",
                Name = "R2-D2",
                Friends = new[] { "1", "4" },
                AppearsIn = new[] { 4, 5, 6 },
                PrimaryFunction = "Astromech"
            });
            _droids.Add(new Droid
            {
                Id = "4",
                Name = "C-3PO",
                AppearsIn = new[] { 4, 5, 6 },
                PrimaryFunction = "Protocol"
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
                friends.AddRange(_humans.Where(h => lookup.Contains(h.Id)));
                friends.AddRange(_droids.Where(d => lookup.Contains(d.Id))); 
            }
            return friends;
        }

        public Task<Human> GetHumanByIdAsync(string id)
        {
            return Task.FromResult(_humans.FirstOrDefault(h => h.Id == id));
        }

        public Task<Droid> GetDroidByIdAsync(string id)
        {
            return Task.FromResult(_droids.FirstOrDefault(h => h.Id == id));
        }
    }
}
