using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormBaiDuMap.Forms
{
    public class BaseContentPage : ContentPage
    {
        public void SendAppearing()
        {
            OnAppearing();
        }

        public void SendDisappearing()
        {
            OnDisappearing();
        }
    }
}
