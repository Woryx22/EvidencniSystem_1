using EvidencniSystem.Data;
using EvidencniSystem.Models;
using System;
using Microsoft.Maui.Controls;
using EvidencniSystem;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using System.Diagnostics;

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
        forDodavatel.ItemsSource = _context.Odberatele.ToList();
    }

    private void SavePrijateFaktury(object sender, EventArgs e)
    {
        // Získání vybraného objektu typu Odberatel
        Odberatel selectedOdberatel = forOdberatel.SelectedItem as Odberatel;
        Odberatel selectedDodavatel = forDodavatel.SelectedItem as Odberatel;

        // Pokud je vybrán nìjaký objekt
        if (selectedOdberatel != null)
        {
            PrijateFaktury newPrijateFaktury = new PrijateFaktury
            {
                Odberatel = selectedOdberatel,
                Dodavatel = selectedDodavatel,
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
        DetailPrijateFakturyPage dp = new(id, _context);
        await Navigation.PushAsync(dp);
    }

    void refresh()
    {
        lst3.ItemsSource = null;
        lst3.ItemsSource = _context.PrijateFaktury_.ToList();
    }

    private void GeneratePDF(object sender, EventArgs e)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "demo.pdf");
        string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ahoj.jpg");
        PdfWriter writer = new PdfWriter(filePath);
        PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf);
        Paragraph header = new Paragraph("Faktura - daòový doklad")
           .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
           .SetFontSize(20);


        //iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory
        //.Create(dataPath))
        //.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
        //document.Add(img);


        document.Add(header);
        document.Close();
        Process.Start(new ProcessStartInfo
        {
            FileName = filePath,
            UseShellExecute = true
        });
    }
}