using EvidencniSystem.Data;
using EvidencniSystem.Models;
using System;
using Microsoft.Maui.Controls;
using EvidencniSystem;

namespace EvidencniSystem;

public partial class Odberatel_Page : ContentPage
{
    MyContext _context;
    public Odberatel_Page()
    {
        _context = new();
        InitializeComponent();
        lst.ItemsSource = _context.Odberatele.ToList(); // pøipojení zdroje dat k ListView
        forName.Text = "";
    }

    private void SaveOdberatel(object sender, EventArgs e)
    {
        if (forName.Text == "")
        {
            DisplayAlert("Chyba", "Musíte zadat jméno", "Ok");
        }
        else
            {
            Odberatel newOdberatel = new()
            {
                Name = forName.Text,
                State = forState.Text,
                IC = forIC.Text,
                DIC = forDIC.Text,
                Street = forStreet.Text,
                City = forCity.Text,
                PSC = forPSC.Text,
                Cislouctu = forCislouctu.Text
            };

            _context.Add(newOdberatel); // pøidá záznam do Data Setu
            _context.SaveChanges(); // uloží zmìny do databáze !!!!!!
            refresh();
        }
    }

    private void Smazat(object sender, EventArgs e)
    {
        Odberatel keSmazani = lst.SelectedItem as Odberatel;
        if (keSmazani != null)
        {
            _context.Odberatele.Remove(keSmazani); // odebrání Odberatela z data setu
            _context.SaveChanges(); // uloží zmìny do databáze
            refresh();
        }
    }
    void refresh()
    {
        lst.ItemsSource = null;
        lst.ItemsSource = _context.Odberatele.ToList();
    }
}