using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarincmd.Models;
using Xamarincmd.Service;


namespace Xamarincmd
{
    public partial class MainPage : ContentPage
    {
        DataService dataService;
        List<Commander> commands;

        public MainPage()
        {
            InitializeComponent();
            dataService = new DataService();
            AttDados();


        }
       
        private async void AttDados()
        {
            commands = await dataService.GetCommandAsync();
            lista.ItemsSource = commands;
            
        }
        private async void Op_Cadastro(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cad(Navigation));
        }
        override
        protected void OnAppearing()
        {
            AttDados();
        }
    }
}
