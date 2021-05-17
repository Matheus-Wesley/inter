using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarincmd.Models;
using Xamarincmd.MV;

namespace Xamarincmd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cad : ContentPage
    {
        CadMv mView;
        INavigation navigation { get; }
        public Cad(INavigation na)
        {
            InitializeComponent();
            navigation = na;
            mView = new CadMv(this);
        }
        private void Button_Cad(object sender, EventArgs e)
        {
            Commander command = new Commander();
            command.firstName = et_firstName.Text;
            command.surname = et_surname.Text;
            command.age = 0;
            if (et_age.Text != null && et_age.Text != "")
            {
                command.age = Int16.Parse(et_age.Text);
            }
            mView.Cad(command);
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            if (!Int16.TryParse(e.NewTextValue, out Int16 value))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }

    }
}