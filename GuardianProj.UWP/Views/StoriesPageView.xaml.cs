﻿using GuardianProj.Core.Models;
using GuardianProj.Core.ViewModels;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GuardianProj.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxRegionPresentation(regionName: "ContentFrame")]
    public sealed partial class StoriesPageView : MvxWindowsPage
    {
        public StoriesPageView()
        {
            this.InitializeComponent();
        }

        private void storiesListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var context = DataContext as StoriesPageViewModel;
            StoryHeader item = e.ClickedItem as StoryHeader;
            if (item != null)
            {
                context.GoToSingleStoryPage.Execute(item.WebUrl);
            }

        }

    }
}
