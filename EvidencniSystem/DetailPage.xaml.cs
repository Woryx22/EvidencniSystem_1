using EvidencniSystem.Data;
using EvidencniSystem.Models;

namespace EvidencniSystem;

public partial class DetailPage : ContentPage
{
    public string Vystup { get; set; }
    MyContext _context;
    public DetailPage(int id, MyContext context)
    {
        _context = context;
        //Odberatel s = _context.Odberatele.FirstOrDefault(x => x.Id == id); // lambda varianta

        Odberatel s = (from Odberatel in context.Odberatele
                       where Odberatel.Id == id
                       select Odberatel).FirstOrDefault();

        if (s != null)
        {
            Vystup = $"Odberatel s Id {s.Id} jménem {s.Name} ";
        }
        InitializeComponent();
        BindingContext = this;


    }
}