using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        public ListaProduto()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista; 
        }

        protected async override void OnAppearing()
        {
            
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));
        }

        
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new Views.NovoProduto());
            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        
        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = e.NewTextValue; 
            lista.Clear(); 
            
            List<Produto> tmp = string.IsNullOrEmpty(query)
                ? await App.Db.GetAll() 
                : await App.Db.Search(query); 

            tmp.ForEach(i => lista.Add(i)); 
        }

        
        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            double soma = lista.Sum(i => i.Total); 
            string msg = $"O total � {soma:C}";
            DisplayAlert("Total dos Produtos", msg, "OK");
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}
