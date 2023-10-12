using ClientAPI.Models.Enums;

namespace ClientAPI.Models
{
    public class Dog
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Size { get; set; }
        public int Color { get; set; }
        public IEnumerable<Guid> DogFriends { get; set; } = null!;
        public Breed Breed { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}
