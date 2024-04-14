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
            Vystup = $"Faktura s Id {f.Id}\n" +
                     $"Jménem {f.Odberatel} ";
        }
        InitializeComponent();
        BindingContext = this;


    }
}