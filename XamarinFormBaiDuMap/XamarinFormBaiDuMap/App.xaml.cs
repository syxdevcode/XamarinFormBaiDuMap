using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinFormBaiDuMap.Views;

namespace XamarinFormBaiDuMap
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }
        public static void SetMainPage()
        {
            Current.MainPage = new MasterDetailPage
            {
                Master = new NavigationPage(new UserPage())
                {
                    Title = "用户信息"
                    ,Icon = Device.OnPlatform("tab_about.png", null, null)
                },
                Detail = new NavigationPage(new MainPage())
                {
                    Title = "列表"
                    ,Icon =Device.OnPlatform("tab_feed.png", null, null)
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
