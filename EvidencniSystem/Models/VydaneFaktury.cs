namespace EvidencniSystem.Models
{
    public class VydaneFaktury
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
