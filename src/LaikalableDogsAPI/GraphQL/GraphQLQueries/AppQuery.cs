using GraphQL.Types;
using LaikableDogsAPI.DataAccess.Interfaces;
using LaikableDogsAPI.GraphQL.GraphQLTypes;

namespace LaikableDogsAPI.GraphQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IDogProvider provider)
        {
            Field<ListGraphType<DogType>>(
                "dogs",
                resolve: context => provider.GetAllDogs()
            );
        }
    }
}
