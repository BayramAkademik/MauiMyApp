using MauiMyApp.Model;
using System.Text.Json;

namespace MauiMyApp;

public partial class NewsPage : ContentPage
{
	public NewsPage()
	{
		InitializeComponent();
        lstKategori.ItemsSource = haberKategoriList;

        selectedCategory = haberKategoriList[0];
	}

    List<HaberKategori> haberKategoriList = new List<HaberKategori>()
    {
         new HaberKategori(){Baslik = "Manþet", Link = "https://www.trthaber.com/manset_articles.rss"},
         new HaberKategori(){Baslik = "Son Dakika", Link = "https://www.trthaber.com/sondakika_articles.rss"},
         new HaberKategori(){Baslik = "Spor", Link = "https://www.trthaber.com/spor_articles.rss"},
         new HaberKategori(){Baslik = "Ekonomi", Link = "https://www.trthaber.com/ekonomi_articles.rss"},
         new HaberKategori(){Baslik = "Bilim Teknoloji", Link = "https://www.trthaber.com/bilim_teknoloji_articles.rss"},
    };

    HaberKategori selectedCategory = null;
    private async void LoadHaberler(object sender, EventArgs e)
    {
        //webHttp.Source = "https://www.trthaber.com/";

        Load();
        refreshView.IsRefreshing = false;

    }

    async Task Load()
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        string jsondata = await NewsServices.GetNews(selectedCategory.Link);
        if (!string.IsNullOrEmpty(jsondata))
        {
            var haberler = JsonSerializer.Deserialize<HaberRoot>(jsondata);
            lstHaberler.ItemsSource = haberler.items;
        }
    }

    private async void lstKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedCategory = lstKategori.SelectedItem as HaberKategori;
        await Load();
    }

    private void lstHaberler_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var url = (lstHaberler.SelectedItem as Item).link;
        NewsDetailPage page = new NewsDetailPage(url);
        Navigation.PushAsync(page);
    }
}

public class HaberKategori
{
    public string Baslik {  get; set; }
    public string Link { get; set; }
}