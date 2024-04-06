using EvidencniSystem.Data;
using EvidencniSystem.Models;
using System;
using Microsoft.Maui.Controls;
using EvidencniSystem;

namespace EvidencniSystem;

public partial class VydaneFaktury_Page : ContentPage
{
    MyContext _context;
    public VydaneFaktury_Page()
    {
        _context = new();
        InitializeComponent();
        lst2.ItemsSource = _context.VydaneFaktury_.ToList(); // p�ipojen� zdroje dat k ListView
    }

    private void SaveVydaneFaktury(object sender, EventArgs e)
    {
        VydaneFaktury newVydaneFaktury = new()
        {
            Name = forName.Text
        };

        _context.Add(newVydaneFaktury); // p�id� z�znam do Data Setu
        _context.SaveChanges(); // ulo�� zm�ny do datab�ze !!!!!!
        refresh();
    }

    private void Smazat(object sender, EventArgs e)
    {
        VydaneFaktury keSmazani = lst2.SelectedItem as VydaneFaktury;
        if (keSmazani != null)
        {
            _context.VydaneFaktury_.Remove(keSmazani); // odebr�n� VydaneFakturya z data setu
            _context.SaveChanges(); // ulo�� zm�ny do datab�ze
            refresh();
        }
    }

    private async void Detajly(object sender, EventArgs e)
    {
        int id = (lst2.SelectedItem as VydaneFaktury).Id;
        DetailPage dp = new(id, _context);
        await Navigation.PushAsync(dp);
    }

    void refresh()
    {
        lst2.ItemsSource = null;
        lst2.ItemsSource = _context.VydaneFaktury_.ToList();
    }
}