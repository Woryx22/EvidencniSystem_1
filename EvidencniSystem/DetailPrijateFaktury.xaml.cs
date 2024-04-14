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
            Vystup = $"Odb�ratel:  {pf.Odberatel.Name}\n" +
                     $"I�:  {pf.Odberatel.IC}\n" +
                     $"DI�:  {pf.Odberatel.DIC}\n" +
                     $"St�t:  {pf.Odberatel.State}\n" +
                     $"S�dlo:  {pf.Odberatel.Street}, {pf.Odberatel.PSC}, {pf.Odberatel.City}\n" +
                     $"��slo objedn�vky:  {pf.CisloObjednavky}\n" +
                     $"Popis:  {pf.Popis}\n\n" +
                     $"Vystaveno:  {pf.Vystaveno}\n" +
                     $"Splatnost:  {pf.Splatnost}\n\n" +
                     $"Polo�ky:  {pf.Polozky}, Mno�stv�:  {pf.Mnozstvi}, Celkov� cena:  {pf.Celkovacena}\n" +
                     $"Zp�son �hrady:  {pf.ZpusobUhrady}";

        }
        InitializeComponent();
        BindingContext = this;


    }
}