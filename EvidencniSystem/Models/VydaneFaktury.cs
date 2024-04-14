﻿

namespace EvidencniSystem.Models
{
    public class VydaneFaktury
    {
        public int Id { get; set; }

        public string Odberatel { get; set; }

        public string CisloObjednavky { get; set; }

        public string Popis { get; set; }

        public string Vystaveno { get; set; }

        public string Splatnost { get; set; }

        public string ZpusobUhrady { get; set; }

        public override string ToString()
        {
            return $"{Odberatel} {CisloObjednavky} {Popis} {Vystaveno} {Splatnost}  {ZpusobUhrady} " ;
        }
    }
}
