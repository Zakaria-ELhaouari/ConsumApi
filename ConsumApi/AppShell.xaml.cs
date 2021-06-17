using ConsumApi.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ConsumApi
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Prducts), typeof(Prducts));
        }


    }
}
