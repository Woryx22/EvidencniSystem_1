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
                     $"IÈ:  { pf.Dodavatel.IC}\n" +
                     $"DIÈ:  {pf.Dodavatel.DIC}\n" +
                     $"Stát:  {pf.Dodavatel.State}\n" +
                     $"Sídlo:  {pf.Dodavatel.Street}, {pf.Dodavatel.PSC}, {pf.Dodavatel.City}\n" +
                     $"Odbìratel:  {pf.Odberatel.Name}\n" +
                     $"IÈ:  {pf.Odberatel.IC}\n" +
                     $"DIÈ:  {pf.Odberatel.DIC}\n" +
                     $"Stát:  {pf.Odberatel.State}\n" +
                     $"Sídlo:  {pf.Odberatel.Street}, {pf.Odberatel.PSC}, {pf.Odberatel.City}\n" +
                     $"Èíslo objednávky:  {pf.CisloObjednavky}\n" +
                     $"Popis:  {pf.Popis}\n" +
                     $"Vystaveno:  {pf.Vystaveno}\n" +
                     $"Splatnost:  {pf.Splatnost}\n\n" +
                     $"Položky:  {pf.Polozky}, Množství:  {pf.Mnozstvi}\n" +
                     $"Celková cena:  {pf.Celkovacena}, Zpùson úhrady:  {pf.ZpusobUhrady}";

        }
        InitializeComponent();
        BindingContext = this;


    }
}