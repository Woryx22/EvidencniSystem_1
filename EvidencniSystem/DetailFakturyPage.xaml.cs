using EvidencniSystem.Data;
using EvidencniSystem.Models;

namespace EvidencniSystem;

public partial class DetailFakturyPage : ContentPage
{
    public string Vystup { get; set; }
    MyContext _context;
    public DetailFakturyPage(int id, MyContext context)
    {
        _context = context;

        VydaneFaktury f = (from VydaneFaktury in context.VydaneFaktury_
                       where VydaneFaktury.Id == id
                       select VydaneFaktury).FirstOrDefault();

        if (f != null)
        {
            Vystup = $"Dodavatel:  {f.Dodavatel.Name}\n" +
                     $"IÈ:  {f.Dodavatel.IC}\n" +
                     $"DIÈ:  {f.Dodavatel.DIC}\n" +
                     $"Stát:  {f.Dodavatel.State}\n" +
                     $"Sídlo:  {f.Dodavatel.Street}, {f.Dodavatel.PSC}, {f.Dodavatel.City}\n\n" +
                     $"Odbìratel:  {f.Odberatel.Name}\n" +
                     $"IÈ:  {f.Odberatel.IC}\n" +
                     $"DIÈ:  {f.Odberatel.DIC}\n" +
                     $"Stát:  {f.Odberatel.State}\n" +
                     $"Sídlo:  {f.Odberatel.Street}, {f.Odberatel.PSC}, {f.Odberatel.City}\n\n" +
                     $"Èíslo objednávky:  {f.CisloObjednavky}\n" +
                     $"Popis:  {f.Popis}\n" +
                     $"Vystaveno:  {f.Vystaveno}\n" +
                     $"Splatnost:  {f.Splatnost}\n\n" +
                     $"Položky:  {f.Polozky}, Množství:  {f.Mnozstvi}\n" +
                     $"Celková cena:  {f.Celkovacena}, Zpùson úhrady:  {f.ZpusobUhrady}";

        }
        InitializeComponent();
        BindingContext = this;


    }
}