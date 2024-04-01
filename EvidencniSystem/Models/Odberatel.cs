namespace EvidencniSystem.Models
{
    public class Odberatel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}
