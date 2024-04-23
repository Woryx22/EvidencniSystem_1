using EvidencniSystem.Data;
using EvidencniSystem.Models;

namespace EvidencniSystem;

public partial class DetailPrijateFakturyPage : ContentPage
{
    public string Vystup { get; set; }
    MyContext _context;
    public DetailPrijateFakturyPage(int id, MyContext context)
    {
        _context = context;

        PrijateFaktury pf = (from PrijateFaktury in context.PrijateFaktury_
                            where PrijateFaktury.Id == id
                           select PrijateFaktury).FirstOrDefault();

        if (pf != null)
        {
            Vystup = $"Dodavatel:  {pf.Dodavatel.Name}\n" +
                     $"I�:  { pf.Dodavatel.IC}\n" +
                     $"DI�:  {pf.Dodavatel.DIC}\n" +
                     $"St�t:  {pf.Dodavatel.State}\n" +
                     $"S�dlo:  {pf.Dodavatel.Street}, {pf.Dodavatel.PSC}, {pf.Dodavatel.City}\n" +
                     $"Odb�ratel:  {pf.Odberatel.Name}\n" +
                     $"I�:  {pf.Odberatel.IC}\n" +
                     $"DI�:  {pf.Odberatel.DIC}\n" +
                     $"St�t:  {pf.Odberatel.State}\n" +
                     $"S�dlo:  {pf.Odberatel.Street}, {pf.Odberatel.PSC}, {pf.Odberatel.City}\n" +
                     $"��slo objedn�vky:  {pf.CisloObjednavky}\n" +
                     $"Popis:  {pf.Popis}\n" +
                     $"Vystaveno:  {pf.Vystaveno}\n" +
                     $"Splatnost:  {pf.Splatnost}\n\n" +
                     $"Polo�ky:  {pf.Polozky}, Mno�stv�:  {pf.Mnozstvi}\n" +
                     $"Celkov� cena:  {pf.Celkovacena}, Zp�son �hrady:  {pf.ZpusobUhrady}";

        }
        InitializeComponent();
        BindingContext = this;


    }
}