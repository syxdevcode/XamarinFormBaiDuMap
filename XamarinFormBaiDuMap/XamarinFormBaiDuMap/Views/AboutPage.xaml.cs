using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormBaiDuMap.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new WebViewPage("http://www.cnblogs.com/GuZhenYin/"));
        }
    }
}