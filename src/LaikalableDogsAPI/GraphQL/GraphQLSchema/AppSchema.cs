using GraphQL.Types;

namespace LaikableDogsAPI.GraphQL.GraphQLSchema
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider) :base(provider)
        {

        }
    }
}
