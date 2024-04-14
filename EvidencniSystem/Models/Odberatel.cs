namespace EvidencniSystem.Models
{
    public class Odberatel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string State { get; set; }

        public string IC { get; set; }

        public string DIC { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PSC { get; set; }

        public string Cislouctu { get; set; }

        public override string ToString()
        {
            return $"Jméno/Firma:{Name}, Stát:{State}, IČ:{IC}, DIČ:{DIC}, Sídlo:{Street}, {PSC}, {City}, Číslo účtu:{Cislouctu}";
        }
    }
}
