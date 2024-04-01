using System;
using Microsoft.Maui.Controls;
using EvidencniSystem;

namespace EvidencniSystem;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void Odberatele_Page_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Odberatel_Page());
    }
}