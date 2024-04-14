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
        //Odberatel s = _context.Odberatele.FirstOrDefault(x => x.Id == id); // lambda varianta

        VydaneFaktury f = (from VydaneFaktury in context.VydaneFaktury_
                       where VydaneFaktury.Id == id
                       select VydaneFaktury).FirstOrDefault();

        if (f != null)
        {
            Vystup = $"Odbìratel:  {f.Odberatel.Name}\n" +
                     $"IÈ:  {f.Odberatel.IC}\n" +
                     $"DIÈ:  {f.Odberatel.DIC}\n" +
                     $"Stát:  {f.Odberatel.State}\n" +
                     $"Sídlo:  {f.Odberatel.Street}, {f.Odberatel.PSC}, {f.Odberatel.City}\n" +
                     $"Èíslo objednávky:  {f.CisloObjednavky}\n" +
                     $"Popis:  {f.Popis}\n\n" +
                     $"Vystaveno:  {f.Vystaveno}\n" +
                     $"Splatnost:  {f.Splatnost}\n\n" +
                     $"Položky:  {f.Polozky}, Množství:  {f.Mnozstvi}, Celková cena:  {f.Celkovacena}\n" +
                     $"Zpùson úhrady:  {f.ZpusobUhrady}";

        }
        InitializeComponent();
        BindingContext = this;


    }
}