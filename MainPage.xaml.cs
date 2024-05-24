using MauiMyApp.Model;

namespace MauiMyApp
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private void KayitLoginEkraniGoster(object sender, EventArgs e)
        {
            if (kayitEkran.IsVisible)
            {
                kayitEkran.IsVisible = false;
                loginEkran.IsVisible = true;
            }else
            {
                loginEkran.IsVisible = false;
                kayitEkran.IsVisible = true;
            }
        }

        private async void RegisterClicked(object sender, EventArgs e)
        {
            var user = await Model.FireBaseServices.Register(txtName.Text, txtREmail.Text, txtRPassword.Text );
            if (user!=null)
            {
                // istenen sayfaya git
            }
            else
            {
                await DisplayAlert("Hata", "Kayıt başarısız", "OK");
            }
        }

        private async void LoginInClicked(object sender, EventArgs e)
        {
            //var data = NewsServices.GetNews("https://www.trthaber.com/manset_articles.rss");
            //await Console.Out.WriteLineAsync(data.Result);
            //return;

            var user = await Model.FireBaseServices.Login(txtEmail.Text, txtPassword.Text);
            if (user!=null)
            {
                // istenen sayfaya git
                await DisplayAlert($"Hoşgeldin! {user.Info.DisplayName}", "Giriş başarılı", "OK");
                
                await Navigation.PushAsync(new NewsPage() { Title = "Haberler" });

            }
            else
            {
                await DisplayAlert("Hata", "Kullanıcı adı veya şifre hatalı", "OK");
            }
        }
    }

}
