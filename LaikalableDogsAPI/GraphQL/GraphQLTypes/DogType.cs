using GraphQL.Types;
using LaikalableDogsAPI.Models;

namespace LaikableDogsAPI.GraphQL.GraphQLTypes
{
    public class DogType : ObjectGraphType<Dog>
    {
        public DogType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object.");
        }
    }
}
