using EvidencniSystem.Data;
using EvidencniSystem.Models;
using System;
using Microsoft.Maui.Controls;
using EvidencniSystem;

namespace EvidencniSystem;

public partial class PrijateFaktury_Page : ContentPage
{
    MyContext _context;
    public PrijateFaktury_Page()
    {
        _context = new();
        InitializeComponent();
        lst3.ItemsSource = _context.PrijateFaktury_.ToList(); // pøipojení zdroje dat k ListView
        forOdberatel.ItemsSource = _context.Odberatele.ToList();
    }

    private void SavePrijateFaktury(object sender, EventArgs e)
    {
        // Získání vybraného objektu typu Odberatel
        Odberatel selectedOdberatel = forOdberatel.SelectedItem as Odberatel;

        // Pokud je vybrán nìjaký objekt
        if (selectedOdberatel != null)
        {
            PrijateFaktury newPrijateFaktury = new PrijateFaktury
            {
                Odberatel = selectedOdberatel,
                CisloObjednavky = forCisloObjednavky.Text,
                Popis = forPopis.Text,
                Vystaveno = forVystaveno.Date.ToShortDateString(),
                Splatnost = forSplatnost.Date.ToShortDateString(),
                ZpusobUhrady = (string)forZpusobUhrady.SelectedItem,
                Polozky = forPolozky.Text,
                Mnozstvi = forMnozstvi.Text,
                Celkovacena = forCelkovacena.Text
            };

            _context.Add(newPrijateFaktury);
            _context.SaveChanges();
            refresh();
        }
        else
        {
            //bleeh
        }
    }


    private void Smazat(object sender, EventArgs e)
    {
        PrijateFaktury keSmazani = lst3.SelectedItem as PrijateFaktury;
        if (keSmazani != null)
        {
            _context.PrijateFaktury_.Remove(keSmazani); // odebrání PrijateFakturya z data setu
            _context.SaveChanges(); // uloží zmìny do databáze
            refresh();
        }
    }

    private async void Detajly(object sender, EventArgs e)
    {
        int id = (lst3.SelectedItem as PrijateFaktury).Id;
        DetailFakturyPage dp = new(id, _context);
        await Navigation.PushAsync(dp);
    }

    void refresh()
    {
        lst3.ItemsSource = null;
        lst3.ItemsSource = _context.PrijateFaktury_.ToList();
    }
}