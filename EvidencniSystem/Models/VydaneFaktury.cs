﻿namespace EvidencniSystem.Models
{
    public class VydaneFaktury
    {
        public int Id { get; set; }

        public Odberatel Dodavatel { get; set; }

        public Odberatel Odberatel { get; set; }

        public string CisloObjednavky { get; set; }

        public string Popis { get; set; }

        public string Vystaveno { get; set; }

        public string Splatnost { get; set; }

        public string ZpusobUhrady { get; set; }

        public string Polozky { get; set; }

        public string Mnozstvi { get; set; }

        public string Celkovacena { get; set; }

        public override string ToString()
        {
            return $"{Dodavatel.Name} {Odberatel.Name} {CisloObjednavky} {Popis} {Vystaveno} {Splatnost}  {ZpusobUhrady} {Polozky} {Mnozstvi} {Celkovacena} " ;
        }
    }
}
