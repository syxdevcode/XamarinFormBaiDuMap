﻿using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinFormBaiDuMap.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "关于";

            //OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://www.cnblogs.com/GuZhenYin/")));
            //OpenWebCommand = new Command(() => {

                

            //});
        }

        /// <summary>
        /// Command to open browser to xamarin.com
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }
}
