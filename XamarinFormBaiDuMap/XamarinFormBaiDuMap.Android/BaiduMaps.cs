using System;
using System.Collections.Generic;
using Com.Baidu.Mapapi;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinFormBaiDuMap.Droid
{
    public class BaiduMaps
    {
        public static void Init(string APIKey)
        {
            SDKInitializer.Initialize(Application.Context);
        }
    }
}