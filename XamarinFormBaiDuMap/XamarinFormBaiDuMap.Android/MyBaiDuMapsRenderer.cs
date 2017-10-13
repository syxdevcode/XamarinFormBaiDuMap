﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using AG = Android.Graphics;
using XamarinFormBaiDuMap.Forms;
using XamarinFormBaiDuMap.Droid;
using Com.Baidu.Mapapi.Map;
using XamarinFormBaiDuMap.Droid.BaiDuMapClass;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(MyBaiDuMaps), typeof(MyBaiDuMapsRenderer))]
namespace XamarinFormBaiDuMap.Droid
{
    public class MyBaiDuMapsRenderer : ViewRenderer<MyBaiDuMaps, MapView>, BaiduMap.IOnMapLoadedCallback
    {
        protected MapView NativeMap => Control;
        protected MyBaiDuMaps Map => Element;



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != Element)
                {
                    //Map.Pins.Clear();
                    ((LocationServiceImpl)Map.LocationService).Unregister();
                }

                //pinImpl.Unregister(Map);
                //polylineImpl.Unregister(Map);
                //polygonImpl.Unregister(Map);
                //circleImpl.Unregister(Map);

                //NativeMap.Map.Clear();
                //NativeMap.Map.MapClick -= OnMapClick;
                //NativeMap.Map.MapPoiClick -= OnMapPoiClick;
                //NativeMap.Map.MapDoubleClick -= OnMapDoubleClick;
                //NativeMap.Map.MapLongClick -= OnMapLongClick;
                //NativeMap.Map.MarkerClick -= OnMarkerClick;
                //NativeMap.Map.MarkerDragStart -= OnMarkerDragStart;
                //NativeMap.Map.MarkerDrag -= OnMarkerDrag;
                //NativeMap.Map.MarkerDragEnd -= OnMarkerDragEnd;
                //NativeMap.Map.MapStatusChangeFinish -= MapStatusChangeFinish;
                NativeMap.Map.SetOnMapLoadedCallback(null);

                NativeMap.OnDestroy();
            }

            System.Diagnostics.Debug.WriteLine("Disposing: " + disposing);
            base.Dispose(disposing);
        }

        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            return new SizeRequest(new Size(Context.ToPixels(0), Context.ToPixels(0)));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<MyBaiDuMaps> e)
        {
            base.OnElementChanged(e);

            if (null == Control)
            {
                SetNativeControl(new MapView(Context));
            }

            if (null != e.OldElement)
            {
                var oldMap = e.OldElement;
                // oldMap.Pins.Clear();
                ((LocationServiceImpl)oldMap.LocationService).Unregister();

                MapView oldMapView = Control;
                oldMapView.Map.Clear();
                //oldMapView.Map.MapClick -= OnMapClick;
                //oldMapView.Map.MapPoiClick -= OnMapPoiClick;
                //oldMapView.Map.MapDoubleClick -= OnMapDoubleClick;
                //oldMapView.Map.MapLongClick -= OnMapLongClick;
                //oldMapView.Map.MarkerClick -= OnMarkerClick;
                //oldMapView.Map.MarkerDragStart -= OnMarkerDragStart;
                //oldMapView.Map.MarkerDrag -= OnMarkerDrag;
                //oldMapView.Map.MarkerDragEnd -= OnMarkerDragEnd;
                //oldMapView.Map.MapStatusChangeFinish -= MapStatusChangeFinish;
                oldMapView.Map.SetOnMapLoadedCallback(null);

                oldMapView.OnDestroy();
            }

            if (null != e.NewElement)
            {
                Map.LocationService = new LocationServiceImpl(NativeMap, Context);
                // NativeMap.OnResume
                //NativeMap.Map.MapClick += OnMapClick;
                //NativeMap.Map.MapPoiClick += OnMapPoiClick;
                //NativeMap.Map.MapDoubleClick += OnMapDoubleClick;
                //NativeMap.Map.MapLongClick += OnMapLongClick;
                //NativeMap.Map.MarkerClick += OnMarkerClick;
                //NativeMap.Map.MarkerDragStart += OnMarkerDragStart;
                //NativeMap.Map.MarkerDrag += OnMarkerDrag;
                //NativeMap.Map.MarkerDragEnd += OnMarkerDragEnd;
                //NativeMap.Map.MapStatusChangeFinish += MapStatusChangeFinish;
                NativeMap.Map.SetOnMapLoadedCallback(this);

                NativeMap.ShowZoomControls(false);

                UpdateMapType();
                UpdateUserTrackingMode();
                UpdateShowUserLocation();

                UpdateShowCompass();
                UpdateCompassPosition();

                UpdateZoomLevel();
                UpdateMinZoomLevel();
                UpdateMaxZoomLevel();

                //UpdateCenter();
                UpdateShowScaleBar();
                UpdateShowZoomControl();

                //pinImpl.Unregister(e.OldElement);
                //pinImpl.Register(Map, NativeMap);

                //polylineImpl.Unregister(e.OldElement);
                //polylineImpl.Register(Map, NativeMap);

                //polygonImpl.Unregister(e.OldElement);
                //polygonImpl.Register(Map, NativeMap);

                //circleImpl.Unregister(e.OldElement);
                //circleImpl.Register(Map, NativeMap);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ("Height" == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("Height = " + Map.Height);
                return;
            }

            if ("Width" == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("Width = " + Map.Width);
                return;
            }

            if (MyBaiDuMaps.MapTypeProperty.PropertyName == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("MapType = " + Map.MapType);
                UpdateMapType();
                return;
            }

            if (MyBaiDuMaps.UserTrackingModeProperty.PropertyName == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("UserTrackingMode = " + Map.UserTrackingMode);
                UpdateUserTrackingMode();
                return;
            }

            if (MyBaiDuMaps.ShowUserLocationProperty.PropertyName == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("ShowUserLocation = " + Map.ShowUserLocation);
                UpdateShowUserLocation();
                return;
            }

            if (MyBaiDuMaps.ShowCompassProperty.PropertyName == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("ShowCompass = " + Map.ShowCompass);
                UpdateShowCompass();
                return;
            }

            if (MyBaiDuMaps.CompassPositionProperty.PropertyName == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("CompassPosition = " + Map.CompassPosition);
                UpdateCompassPosition();
                return;
            }

            if (MyBaiDuMaps.ZoomLevelProperty.PropertyName == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("ZoomLevel = " + Map.ZoomLevel);
                UpdateZoomLevel();
                return;
            }

            if (MyBaiDuMaps.MinZoomLevelProperty.PropertyName == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("MinZoomLevel = " + Map.MinZoomLevel);
                UpdateMinZoomLevel();
                return;
            }

            if (MyBaiDuMaps.MaxZoomLevelProperty.PropertyName == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("MaxZoomLevel = " + Map.MaxZoomLevel);
                UpdateMaxZoomLevel();
                return;
            }

            //if (MyBaiDuMaps.CenterProperty.PropertyName == e.PropertyName)
            //{
            //    System.Diagnostics.Debug.WriteLine("Center = " + Map.Center);
            //    UpdateCenter();
            //    return;
            //}

            if (MyBaiDuMaps.ShowScaleBarProperty.PropertyName == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("ShowScaleBar = " + Map.ShowScaleBar);
                UpdateShowScaleBar();
                return;
            }

            if (MyBaiDuMaps.ShowZoomControlProperty.PropertyName == e.PropertyName)
            {
                System.Diagnostics.Debug.WriteLine("ShowZoomControl = " + Map.ShowZoomControl);
                UpdateShowZoomControl();
                return;
            }

            System.Diagnostics.Debug.WriteLine("OnElementPropertyChanged: " + e.PropertyName);
            base.OnElementPropertyChanged(sender, e);
        }

        void UpdateMapType()
        {
            switch (Map.MapType)
            {
                case MapType.None:
                    NativeMap.Map.MapType = 0;
                    break;

                case MapType.Standard:
                    NativeMap.Map.MapType = 1;
                    break;

                case MapType.Satellite:
                    NativeMap.Map.MapType = 2;
                    break;
            }
        }

        void UpdateUserTrackingMode()
        {
            MyLocationConfiguration.LocationMode mode;

            switch (Map.UserTrackingMode)
            {
                default:
                case UserTrackingMode.None:
                    mode = MyLocationConfiguration.LocationMode.Normal;
                    break;

                case UserTrackingMode.Follow:
                    mode = MyLocationConfiguration.LocationMode.Following;
                    break;

                case UserTrackingMode.FollowWithCompass:
                    mode = MyLocationConfiguration.LocationMode.Compass;
                    break;
            }

            NativeMap.Map.SetMyLocationConfigeration(
                new MyLocationConfiguration(mode, true, null)
            );

            if (UserTrackingMode.FollowWithCompass != Map.UserTrackingMode)
            {
                // 恢复俯视角
                MapStatusUpdate overlook = MapStatusUpdateFactory.NewMapStatus(
                    new MapStatus.Builder(NativeMap.Map.MapStatus).Rotate(0).Overlook(0).Build()
                );

                NativeMap.Map.AnimateMapStatus(overlook);
            }
        }

        void UpdateShowUserLocation()
        {
            NativeMap.Map.MyLocationEnabled = Map.ShowUserLocation;
        }

        void UpdateShowCompass()
        {
            NativeMap.Map.UiSettings.CompassEnabled = Map.ShowCompass;
        }

        void UpdateCompassPosition()
        {
            NativeMap.Map.CompassPosition = new AG.Point
            {
                X = (int)Map.CompassPosition.X,
                Y = (int)Map.CompassPosition.Y
            };
        }

        void UpdateZoomLevel()
        {
            NativeMap.Map.AnimateMapStatus(
                MapStatusUpdateFactory.ZoomTo(Map.ZoomLevel)
            );
        }

        void UpdateMinZoomLevel()
        {
            NativeMap.Map.SetMaxAndMinZoomLevel(Map.MaxZoomLevel, Map.MinZoomLevel);
        }

        void UpdateMaxZoomLevel()
        {
            NativeMap.Map.SetMaxAndMinZoomLevel(Map.MaxZoomLevel, Map.MinZoomLevel);
        }



        void UpdateShowScaleBar()
        {
            NativeMap.ShowScaleControl(Map.ShowScaleBar);
        }

        void UpdateShowZoomControl()
        {
            NativeMap.ShowZoomControls(Map.ShowZoomControl);

        }

        public void OnMapLoaded()
        {
            // Map.Projection = new ProjectionImpl(NativeMap);
            NativeMap.OnResume();
            Map.SendLoaded();
        }

        protected override void OnAttachedToWindow()
        {
            NativeMap.Visibility = ViewStates.Visible;
            NativeMap.OnResume();
            base.OnAttachedToWindow();
        }
        protected override void OnDetachedFromWindow()
        {
            NativeMap.Visibility = ViewStates.Invisible;
            base.OnDetachedFromWindow();
        }
    }
}