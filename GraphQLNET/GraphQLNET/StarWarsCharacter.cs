using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLNET
{
    #region Dominio

    public abstract class StarWarsCharacter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Friends { get; set; }
        public int[] AppearsIn { get; set; }
    }

    public class Human : StarWarsCharacter
    {
        public string HomePlanet { get; set; }
    }

    public class Droid : StarWarsCharacter
    {
        public string PrimaryFunction { get; set; }
    }

    public class EpisodeEnum : EnumerationGraphType
    {
        public EpisodeEnum()
        {
            Name = "Episode";
            Description = "One of the films in the Star Wars Trilogy.";
            AddValue("NEWHOPE", "Released in 1977.", 4);
            AddValue("EMPIRE", "Released in 1980.", 5);
            AddValue("JEDI", "Released in 1983.", 6);
        }
    }
    
    public enum Episodes
    {
        NEWHOPE = 4,
        EMPIRE = 5,
        JEDI = 6
    }
    #endregion

    
    public class CharacterInterface : InterfaceGraphType<StarWarsCharacter>
    {
        public CharacterInterface()
        {
            Name = "Character";

            Field(d => d.Id).Description("Id do Personagem");
            Field(d => d.Name, nullable: true).Description("Nome do Personagem");

            Field<ListGraphType<CharacterInterface>>("friends");
            Field<ListGraphType<EpisodeEnum>>("appearsIn", "Qual filme apareceu");
        }
    }
}
