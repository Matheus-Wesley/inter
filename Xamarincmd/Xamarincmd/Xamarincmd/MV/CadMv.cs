using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarincmd.Models;
using Xamarincmd.Service;

namespace Xamarincmd.MV
{
    class CadMv
    {
        private Cad _page;
        private DataService _dataService;
        public CadMv(Cad pg)
        {
            _page = pg;
            _dataService = new DataService();
        }
        public async void Cad(Commander command)
        {
            var er = new List<ValidationResult>();
            var ctxt = new ValidationContext(command);
            bool isValid = Validator.TryValidateObject(command, ctxt, er, true);
            var prop = GetValidatablePropertyNames(command);
            foreach (var proName in prop)
            {
                setVisibilidadeErros(proName.ToString().Replace(".", ""), false);
            }
            foreach (var erro in er)
            {
                var en = erro.MemberNames.GetEnumerator();
                while (en.MoveNext())
                {
                    setVisibilidadeErros(en.Current, true);
                }
            }
            
            if (isValid)
            {
                try
                {
                    if (await _dataService.AddCommandAsync(command))
                    {
                        _ = _page.DisplayAlert("Al", "Cadastro realizado com sucesso", "OK");
                        foreach (var proName in prop)
                        {
                            setVisibilidadeErros(proName, false);
                            ApagaTexto(proName);

                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    _ = _page.DisplayAlert("Alert", "Erro: " + ex.Message, "OK");
                }
            }
            
            else
            {
                _ = _page.DisplayAlert("Alert", "Erro", "OK");
            }
            
        }
                //catch/else/errormsg
                private void setVisibilidadeErros(String campo, bool visibilidade)//v
            {
                var control = _page.FindByName<Label>("lb_" + campo);
                if (control != null)
                {
                control.IsVisible = visibilidade;
                }
            }
            private void ApagaTexto(String campo)
            {
                var control = _page.FindByName<Entry>("et_" + campo);
                if (control != null)
                {
                    control.Text = "";
                }
            }
            private static IEnumerable<string> GetValidatablePropertyNames(object model)
            {
                var validatableProperties = new List<string>();
                var properties = GetValidatableProperties(model);
                foreach (var propertyInfo in properties)
                {
                    var errorControlName = propertyInfo.Name;
                    validatableProperties.Add(errorControlName);
                }
                return validatableProperties;
            }
            private static List<PropertyInfo> GetValidatableProperties(object model)
            {
                var properties = model.GetType().GetProperties().Where(prop => prop.CanRead

                    && prop.GetIndexParameters().Length == 0).ToList();
                return properties;
            }

        }

    }

   
