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
using iText.Kernel.Pdf.Canvas.Draw;
using QRCoder;
using System.Globalization;
using System.Text;

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
        if (selectedOdberatel != null && selectedDodavatel != null)
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
            DisplayAlert("Chybka", "Vyplòte povinná pole", "Ok");
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
        // Získání vybrané faktury
        PrijateFaktury selectedFaktura = lst3.SelectedItem as PrijateFaktury;

        if (selectedFaktura != null)
        {
            string accountNumber = selectedFaktura.Dodavatel.Cislouctu;

            string paymentString = $"SPD*1.0*ACC:{accountNumber}*AM:{selectedFaktura.Celkovacena}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(paymentString, QRCodeGenerator.ECCLevel.L);

            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);

            string imageFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "qrcode.png");
            File.WriteAllBytes(imageFilePath, qrCodeBytes);

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, $"faktura{selectedFaktura.Id}.pdf");
            string dataImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "qrcode.png");
            PdfWriter writer = new PdfWriter(filePath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Faktura - doklad")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(20);
            document.Add(header);
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            // Pøidání informací o Dodavateli
            Paragraph sellerHeader = new Paragraph("Dodavatel:").SetBold().SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph sellerDetail = new Paragraph(RemoveDiacritics(selectedFaktura.Dodavatel.Name + " ")).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph sellerAddress = new Paragraph(RemoveDiacritics(selectedFaktura.Dodavatel.State + " ")).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph sellerContact = new Paragraph(RemoveDiacritics($"{selectedFaktura.Dodavatel.Street}, {selectedFaktura.Dodavatel.PSC}, {selectedFaktura.Dodavatel.City}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);

            document.Add(sellerHeader);
            document.Add(sellerDetail);
            document.Add(sellerAddress);
            document.Add(sellerContact);

            // Pøidání informací o Odbìrateli

            Paragraph customerHeader = new Paragraph("Odberatel:").SetBold().SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Paragraph customerDetail = new Paragraph(RemoveDiacritics(selectedFaktura.Odberatel.Name)).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Paragraph customerAddress1 = new Paragraph(RemoveDiacritics($"Stat:{selectedFaktura.Odberatel.State}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Paragraph customerAddress2 = new Paragraph(RemoveDiacritics($"{selectedFaktura.Odberatel.Street}, {selectedFaktura.Odberatel.PSC}, {selectedFaktura.Odberatel.City}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Paragraph customerContact = new Paragraph(RemoveDiacritics($"IC:{selectedFaktura.Odberatel.IC}, DIC:{selectedFaktura.Odberatel.DIC}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

            document.Add(customerHeader);
            document.Add(customerDetail);
            document.Add(customerAddress1);
            document.Add(customerAddress2);
            document.Add(customerContact);

            // Pøidání informací o faktuøe
            Paragraph orderNo = new Paragraph(RemoveDiacritics($"Cislo objednavky: {selectedFaktura.CisloObjednavky}")).SetBold().SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph invoiceNo = new Paragraph(RemoveDiacritics($"Vystaveno: {selectedFaktura.Vystaveno}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph invoiceTimestamp = new Paragraph(RemoveDiacritics($"Splatnost: {selectedFaktura.Splatnost}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph popis = new Paragraph(RemoveDiacritics($"Popis: {selectedFaktura.Popis}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph polozky = new Paragraph(RemoveDiacritics($"Polozky: {selectedFaktura.Polozky}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph mnozstvi = new Paragraph(RemoveDiacritics($"Mnozstvi: {selectedFaktura.Mnozstvi}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph cena = new Paragraph(RemoveDiacritics($"Cena: {selectedFaktura.Celkovacena}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph zpusobuhrady = new Paragraph(RemoveDiacritics($"Zpusob uhrady: {selectedFaktura.ZpusobUhrady}")).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);

            document.Add(orderNo);
            document.Add(invoiceNo);
            document.Add(invoiceTimestamp);
            document.Add(popis);
            document.Add(polozky);
            document.Add(mnozstvi);
            document.Add(cena);
            document.Add(zpusobuhrady);

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(iText.IO.Image.ImageDataFactory
            .Create(dataImagePath))
            .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT)
            .SetHeight(200)
            .SetWidth(200);
            document.Add(img);


            document.Close();
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
        else
        {
            DisplayAlert("Chyba", "Není vybrána faktura", "Ok");
        }
    }

    static string RemoveDiacritics(string input)
    {
        string normalizedString = input.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char c in normalizedString)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}