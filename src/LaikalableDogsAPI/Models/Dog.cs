using LaikableDogsAPI.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LaikalableDogsAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Dog
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Size { get; set; }
        public int Color { get; set; }
        public List<Guid> DogFriends { get; set; } = null!;
        [BsonRepresentation(BsonType.String)]
        public Breed Breed { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}
