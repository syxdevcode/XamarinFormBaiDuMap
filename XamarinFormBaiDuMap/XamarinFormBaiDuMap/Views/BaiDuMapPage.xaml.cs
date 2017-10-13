﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormBaiDuMap.BaiDuMapClass;
using XamarinFormBaiDuMap.Forms;

namespace XamarinFormBaiDuMap.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaiDuMapPage : BaseContentPage
    {
        public BaiDuMapPage()
        {
            InitializeComponent();
            map.ShowCompass = true;
            map.IsVisible = true;
            map.Loaded += Map_Loaded;
        }
        private void Map_Loaded(object sender, EventArgs e)
        {

            InitLocationService();
        }

        protected override void OnDisappearing()
        {
            //= false;
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            //map.IsVisible = true ;
            base.OnAppearing();
        }

        public void InitLocationService()
        {
            //map.LocationService.LocationUpdated += (_, e) => {
            //    Debug.WriteLine("LocationUpdated: " + e.Direction);
            //    //if (!moved)
            //    //{
            //    //    map.Center = e.Coordinate;
            //    //    moved = true;
            //    //}
            //};

            //map.LocationService.Failed += (_, e) => {
            //    Debug.WriteLine("Location failed: " + e.Message);
            //};

            map.LocationService.Start();
        }
        private void btnTrack_Clicked(object sender, EventArgs e)
        {
            map.CompassPosition = new Point(10, 10);
            if (map.ShowUserLocation)
            {
                map.UserTrackingMode = UserTrackingMode.None;
                map.ShowUserLocation = false;
            }
            else
            {
                map.UserTrackingMode = UserTrackingMode.Follow;
                map.ShowUserLocation = true;
            }
            //map.Width  = "1545"
        }
    }
}