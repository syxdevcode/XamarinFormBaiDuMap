using Android.App;
using Android.Widget;
using Android.OS;
using Com.Baidu.Mapapi;
using Com.Baidu.Mapapi.Map;

namespace XamarinBaiDuMap.Android
{
    [Activity(Label = "XamarinBaiDuMap.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        MapView mMapView = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SDKInitializer.Initialize(ApplicationContext);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mMapView = FindViewById<MapView>(Resource.Id.bmapView);
            var s = mMapView.Map;
            s.MapType = BaiduMap.MapTypeSatellite;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mMapView.OnDestroy();
        }

        protected override void OnResume()
        {
            base.OnResume();
            mMapView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();

            mMapView.OnPause();
        }
    }
}

