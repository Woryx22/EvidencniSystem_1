using EvidencniSystem.Data;
using EvidencniSystem.Models;
using System;
using Microsoft.Maui.Controls;
using EvidencniSystem;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Diagnostics;
using iText.Kernel.Pdf.Canvas.Draw;
using QRCoder;
using System.Text;

namespace EvidencniSystem;

public partial class VydaneFaktury_Page : ContentPage
{
    MyContext _context;
    public VydaneFaktury_Page()
    {
        _context = new();
        InitializeComponent();
        lst2.ItemsSource = _context.VydaneFaktury_.ToList(); // pøipojení zdroje dat k ListView
        forOdberatel.ItemsSource = _context.Odberatele.ToList();
        forDodavatel.ItemsSource = _context.Odberatele.ToList();
    }

    private void SaveVydaneFaktury(object sender, EventArgs e)
    {
        // Získání vybraného objektu typu Odberatel
        Odberatel selectedOdberatel = forOdberatel.SelectedItem as Odberatel;
        Odberatel selectedDodavatel = forDodavatel.SelectedItem as Odberatel;

        // Pokud je vybrán nìjaký objekt
        if (selectedOdberatel != null)
        {
            VydaneFaktury newVydaneFaktury = new VydaneFaktury
            {
                Dodavatel = selectedDodavatel,
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

            _context.Add(newVydaneFaktury);
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
        VydaneFaktury keSmazani = lst2.SelectedItem as VydaneFaktury;
        if (keSmazani != null)
        {
            _context.VydaneFaktury_.Remove(keSmazani); // odebrání VydaneFakturya z data setu
            _context.SaveChanges(); // uloží zmìny do databáze
            refresh();
        }
    }

    private async void Detajly(object sender, EventArgs e)
    {
        int id = (lst2.SelectedItem as VydaneFaktury).Id;
        DetailFakturyPage dp = new(id, _context);
        await Navigation.PushAsync(dp);
    }

    void refresh()
    {
        lst2.ItemsSource = null;
        lst2.ItemsSource = _context.VydaneFaktury_.ToList();
    }
    private void GeneratePDF(object sender, EventArgs e)
    {
        //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //string filePath = Path.Combine(desktopPath, "demo.pdf");
        //string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ahoj.jpg");
        //PdfWriter writer = new PdfWriter(filePath);
        //PdfDocument pdf = new PdfDocument(writer);
        //Document document = new Document(pdf);
        //Paragraph header = new Paragraph("Faktura - daòový doklad")
        //   .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
        //   .SetFontSize(20);


        ////iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory
        ////.Create(dataPath))
        ////.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
        ////document.Add(img);

        //Paragraph subheader = new Paragraph("PDF CREATED USING ASP.NET C# WITH iTExT7 LIBRARY").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(10);
        //document.Add(subheader);

        //LineSeparator ls = new LineSeparator(new SolidLine());
        //document.Add(ls);

        //Paragraph sellerHeader = new Paragraph("Sold by:").SetBold().SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
        //Paragraph sellerDetail = new Paragraph("Seller Company").SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
        //Paragraph sellerAddress = new Paragraph("Mumbai, Maharashtra India").SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
        //Paragraph sellerContact = new Paragraph("+91 1000000000").SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);

        //document.Add(sellerHeader);
        //document.Add(sellerDetail);
        //document.Add(sellerAddress);
        //document.Add(sellerContact);

        //Paragraph customerHeader = new Paragraph("Customer details:").SetBold().SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
        //Paragraph customerDetail = new Paragraph("Customer ABC").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
        //Paragraph customerAddress1 = new Paragraph("R783, Rose Apartments, Santacruz (E)").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
        //Paragraph customerAddress2 = new Paragraph("Mumbai 400054, Maharashtra India").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

        //Paragraph customerContact = new Paragraph("+91 0000000000").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

        //document.Add(customerHeader);
        //document.Add(customerDetail);
        //document.Add(customerAddress1);
        //document.Add(customerAddress2);
        //document.Add(customerContact);

        //Paragraph orderNo = new Paragraph("Order No:15484659").SetBold().SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
        //Paragraph invoiceNo = new Paragraph("Invoice No:MH-MU-1077").SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
        //Paragraph invoiceTimestamp = new Paragraph("Date: 30/05/2021 04:25:37 PM").SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);

        //document.Add(orderNo);
        //document.Add(invoiceNo);
        //document.Add(invoiceTimestamp);


        //document.Add(header);
        //document.Close();
        //Process.Start(new ProcessStartInfo
        //{
        //    FileName = filePath,
        //    UseShellExecute = true
        //});


        // Získání vybrané faktury
        VydaneFaktury selectedFaktura = lst2.SelectedItem as VydaneFaktury;

        if (selectedFaktura != null)
        {
            // Get the account number from the input field
            string accountNumber = selectedFaktura.Odberatel.Cislouctu;

            // Construct the payment string (replace with your payment format)
            string paymentString = $"SPD*1.0*ACC:{accountNumber}";

            // Generate the QR code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(paymentString, QRCodeGenerator.ECCLevel.L);

            // Convert QR code to PNG bytes
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);

            // Save the QR code as a file to Local Application Data folder
            string imageFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "qrcode.png");
            File.WriteAllBytes(imageFilePath, qrCodeBytes);

            // Display the QR code
            //QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));



            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, $"faktura{selectedFaktura.Id}.pdf");
            string dataImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "qrcode.png");
            PdfWriter writer = new PdfWriter(filePath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Faktura - daòový doklad")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(20);
            document.Add(header);
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            // Pøidání informací o prodejci
            Paragraph sellerHeader = new Paragraph("Dodavatel:").SetBold().SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph sellerDetail = new Paragraph(selectedFaktura.Dodavatel.Name).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph sellerAddress = new Paragraph(selectedFaktura.Dodavatel.State).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph sellerContact = new Paragraph($"{selectedFaktura.Dodavatel.Street}, {selectedFaktura.Dodavatel.PSC}, {selectedFaktura.Dodavatel.City}").SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);

            document.Add(sellerHeader);
            document.Add(sellerDetail);
            document.Add(sellerAddress);
            document.Add(sellerContact);

            // Pøidání informací o zákazníkovi

            Paragraph customerHeader = new Paragraph("Odbìratel:").SetBold().SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Paragraph customerDetail = new Paragraph(selectedFaktura.Odberatel.Name).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Paragraph customerAddress1 = new Paragraph($"Stát:{selectedFaktura.Odberatel.State}").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Paragraph customerAddress2 = new Paragraph($"{selectedFaktura.Odberatel.Street}, {selectedFaktura.Odberatel.PSC}, {selectedFaktura.Odberatel.City}").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Paragraph customerContact = new Paragraph($"IÈ:{selectedFaktura.Odberatel.IC}, DIÈ:{selectedFaktura.Odberatel.DIC}").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

            document.Add(customerHeader);
            document.Add(customerDetail);
            document.Add(customerAddress1);
            document.Add(customerAddress2);
            document.Add(customerContact);

            // Pøidání informací o faktuøe
            Paragraph orderNo = new Paragraph($"Order No: {selectedFaktura.CisloObjednavky}").SetBold().SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph invoiceNo = new Paragraph($"Invoice No: {selectedFaktura.Id}").SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Paragraph invoiceTimestamp = new Paragraph($"Date: {selectedFaktura.Vystaveno}").SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);

            document.Add(orderNo);
            document.Add(invoiceNo);
            document.Add(invoiceTimestamp);

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

    //static string RemoveDiacritics(string input)
    //{
    //    string normalizedString = input.Normalize(NormalizationForm.FormD);
    //    StringBuilder stringBuilder = new StringBuilder();

    //    foreach (char c in normalizedString)
    //    {
    //        if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
    //        {
    //            stringBuilder.Append(c);
    //        }
    //    }

    //    return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    //}
}