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
        //Odberatel s = _context.Odberatele.FirstOrDefault(x => x.Id == id); // lambda varianta

        PrijateFaktury pf = (from PrijateFaktury in context.PrijateFaktury_
                            where PrijateFaktury.Id == id
                           select PrijateFaktury).FirstOrDefault();

        if (pf != null)
        {
            Vystup = $"Odbìratel:  {pf.Odberatel.Name}\n" +
                     $"IÈ:  {pf.Odberatel.IC}\n" +
                     $"DIÈ:  {pf.Odberatel.DIC}\n" +
                     $"Stát:  {pf.Odberatel.State}\n" +
                     $"Sídlo:  {pf.Odberatel.Street}, {pf.Odberatel.PSC}, {pf.Odberatel.City}\n" +
                     $"Èíslo objednávky:  {pf.CisloObjednavky}\n" +
                     $"Popis:  {pf.Popis}\n\n" +
                     $"Vystaveno:  {pf.Vystaveno}\n" +
                     $"Splatnost:  {pf.Splatnost}\n\n" +
                     $"Položky:  {pf.Polozky}, Množství:  {pf.Mnozstvi}, Celková cena:  {pf.Celkovacena}\n" +
                     $"Zpùson úhrady:  {pf.ZpusobUhrady}";

        }
        InitializeComponent();
        BindingContext = this;


    }
}