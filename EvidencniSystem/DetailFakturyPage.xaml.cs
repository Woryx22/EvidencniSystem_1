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
                     $"I�:  {f.Dodavatel.IC}\n" +
                     $"DI�:  {f.Dodavatel.DIC}\n" +
                     $"St�t:  {f.Dodavatel.State}\n" +
                     $"S�dlo:  {f.Dodavatel.Street}, {f.Dodavatel.PSC}, {f.Dodavatel.City}\n\n" +
                     $"Odb�ratel:  {f.Odberatel.Name}\n" +
                     $"I�:  {f.Odberatel.IC}\n" +
                     $"DI�:  {f.Odberatel.DIC}\n" +
                     $"St�t:  {f.Odberatel.State}\n" +
                     $"S�dlo:  {f.Odberatel.Street}, {f.Odberatel.PSC}, {f.Odberatel.City}\n\n" +
                     $"��slo objedn�vky:  {f.CisloObjednavky}\n" +
                     $"Popis:  {f.Popis}\n" +
                     $"Vystaveno:  {f.Vystaveno}\n" +
                     $"Splatnost:  {f.Splatnost}\n\n" +
                     $"Polo�ky:  {f.Polozky}, Mno�stv�:  {f.Mnozstvi}\n" +
                     $"Celkov� cena:  {f.Celkovacena}, Zp�son �hrady:  {f.ZpusobUhrady}";

        }
        InitializeComponent();
        BindingContext = this;


    }
}