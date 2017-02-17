using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLNET
{
    public class StarWarsSchema : Schema
    {
        public StarWarsSchema(Func<Type, GraphType> resolveType)
            : base(resolveType)
        {
            Query = (StarWarsQuery)resolveType(typeof(StarWarsQuery));
        }
    }
}
