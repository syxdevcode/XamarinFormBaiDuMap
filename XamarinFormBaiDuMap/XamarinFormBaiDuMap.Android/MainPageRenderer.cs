﻿using System;
using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.Views;
using XamarinFormBaiDuMap.Views;
using Xamarin.Forms;
using BottomNavigationBar.Listeners;
using Xamarin.Forms.Platform.Android;
using XamarinFormBaiDuMap.Forms;
using BottomNavigationBar;
using Color = Android.Graphics.Color;
using XamarinFormBaiDuMap.Droid;

[assembly: ExportRenderer(typeof(MainPage), typeof(MainPageRenderer))]
namespace XamarinFormBaiDuMap.Droid
{
    class MainPageRenderer : VisualElementRenderer<MainPage>, IOnTabClickListener
    {

        private BottomBar _bottomBar;

        private Page _currentPage;

        private int _lastSelectedTabIndex = -1;

        public MainPageRenderer()
        {
            // Required to say packager to not to add child pages automatically
            AutoPackage = false;
        }

        /// <summary>
        /// 选中后,加载新的页面内容
        /// </summary>
        /// <param name="position"></param>
        public void OnTabSelected(int position)
        {
            LoadPageContent(position);
        }

        public void OnTabReSelected(int position)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<MainPage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                ClearElement(e.OldElement);
            }

            if (e.NewElement != null)
            {
                InitializeElement(e.NewElement);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearElement(Element);
            }

            base.Dispose(disposing);
        }

        /// <summary>
        ///  重写布局的方法
        /// </summary>
        /// <param name="changed"></param>
        /// <param name="l"></param>
        /// <param name="t"></param>
        /// <param name="r"></param>
        /// <param name="b"></param>
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            if (Element == null)
            {
                return;
            }

            int width = r - l;
            int height = b - t;

            _bottomBar.Measure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.AtMost));

            // We need to call measure one more time with measured sizes 
            // in order to layout the bottom bar properly
            ////这里需要重新测量位置和尺寸,为了重新布置tab菜单的位置 
            _bottomBar.Measure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(_bottomBar.ItemContainer.MeasuredHeight, MeasureSpecMode.Exactly));

            int barHeight = _bottomBar.ItemContainer.MeasuredHeight;

            _bottomBar.Layout(0, b - barHeight, width, b);

            float density = Resources.DisplayMetrics.Density;

            double contentWidthConstraint = width / density;
            double contentHeightConstraint = (height - barHeight) / density;

            if (_currentPage != null)
            {
                var renderer = Platform.GetRenderer(_currentPage);

                renderer.Element.Measure(contentWidthConstraint, contentHeightConstraint);
                renderer.Element.Layout(new Rectangle(0, 0, contentWidthConstraint, contentHeightConstraint));

                renderer.UpdateLayout();
            }
        }
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="element"></param>
        private void InitializeElement(MainPage element)
        {
            PopulateChildren(element);
        }
        /// <summary>
        /// 生成新的底部控件
        /// </summary>
        /// <param name="element"></param>
        private void PopulateChildren(MainPage element)
        {
            // Unfortunately bottom bar can not be reused so we have to
            // remove it and create the new instance
            // 我们需要删除原有的底部控件,然后添加新的
            _bottomBar?.RemoveFromParent();

            _bottomBar = CreateBottomBar(element);
            AddView(_bottomBar);

            LoadPageContent(0);
        }
        /// <summary>
        ///  清除旧的底部控件
        /// </summary>
        /// <param name="element"></param>
        private void ClearElement(MainPage element)
        {
            if (_currentPage != null)
            {
                IVisualElementRenderer renderer = Platform.GetRenderer(_currentPage);

                if (renderer != null)
                {
                    renderer.ViewGroup.RemoveFromParent();
                    renderer.ViewGroup.Dispose();
                    renderer.Dispose();

                    _currentPage = null;
                }

                if (_bottomBar != null)
                {
                    _bottomBar.RemoveFromParent();
                    _bottomBar.Dispose();
                    _bottomBar = null;
                }
            }
        }
        /// <summary>
        /// 创建新的底部控件
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private BottomBar CreateBottomBar(MainPage element)
        {
            var bar = new BottomBar(Context);

            // TODO: Configure the bottom bar here according to your needs

            bar.SetOnTabClickListener(this);
            bar.UseFixedMode();

            PopulateBottomBarItems(bar, element.Children);
            var barcolor = element.BarBackgroundColor;
            // Color a = new Color(Convert.ToByte(barcolor.), Convert.ToByte(barcolor.G), Convert.ToByte(barcolor.B), Convert.ToByte(barcolor.A));


            bar.ItemContainer.SetBackgroundColor(barcolor.ToAndroid());
            bar.SetActiveTabColor(Color.White);
            //bar.ItemContainer.
            //bar.ItemContainer.SetBackgroundColor(Color.Red);

            return bar;
        }
        /// <summary>
        /// 查询原来底部的菜单,并添加到新的控件
        /// </summary>
        /// <param name="bar"></param>
        /// <param name="pages"></param>
        private void PopulateBottomBarItems(BottomBar bar, IEnumerable<Page> pages)
        {

            var barItems = pages.Select(x => new BottomBarTab(Context.Resources.GetDrawable(x.Icon), x.Title));

            bar.SetItems(barItems.ToArray());
        }

        private void LoadPageContent(int position)
        {
            ShowPage(position);
        }
        /// <summary>
        /// 显示Page的方法
        /// </summary>
        /// <param name="position"></param>
        private void ShowPage(int position)
        {
            if (position != _lastSelectedTabIndex)
            {
                Element.CurrentPage = Element.Children[position];

                if (Element.CurrentPage != null)
                {
                    LoadPageContent(Element.CurrentPage);
                }
            }

            _lastSelectedTabIndex = position;
        }
        /// <summary>
        /// 加载方法
        /// </summary>
        /// <param name="page"></param>
        private void LoadPageContent(Page page)
        {
            UnloadCurrentPage();

            _currentPage = page;

            LoadCurrentPage();

            Element.CurrentPage = _currentPage;
        }
        /// <summary>
        /// 加载当前Page
        /// </summary>
        private void LoadCurrentPage()
        {
            var renderer = Platform.GetRenderer(_currentPage);

            if (renderer == null)
            {
                renderer = Platform.CreateRenderer(_currentPage);
                Platform.SetRenderer(_currentPage, renderer);


            }
            else
            {
                // As we show and hide pages manually OnAppearing and OnDisappearing
                // workflow methods won't be called by the framework. Calling them manually...
                var basePage = _currentPage as BaseContentPage;
                basePage?.SendAppearing();
            }

            AddView(renderer.ViewGroup);
            renderer.ViewGroup.Visibility = ViewStates.Visible;

        }
        /// <summary>
        /// 释放上一个Page
        /// </summary>
        private void UnloadCurrentPage()
        {
            if (_currentPage != null)
            {
                var basePage = _currentPage as BaseContentPage;
                basePage?.SendDisappearing();
                var renderer = Platform.GetRenderer(_currentPage);

                if (renderer != null)
                {
                    renderer.ViewGroup.Visibility = ViewStates.Invisible;
                    RemoveView(renderer.ViewGroup);
                    //for (int i = 0; i < renderer.ViewGroup.ChildCount; i++)
                    //{
                    //    /**让viewgroup隐藏还不行，子view还是占用了地方， 
                    //     * 点击焦点还是会选中，得一口气把所以子view 
                    //     * 包括 viewgroup 都隐藏掉**/
                    //    renderer.ViewGroup.GetChildAt(i).Visibility = ViewStates.Invisible;
                    //}
                    // renderer.ViewGroup.views
                    //renderer.Dispose();
                    //renderer.ViewGroup.Dispose();
                }

            }
        }
    }
}