using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace WPFMonitor
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //ResourceDictionary rd = new ResourceDictionary();
            //rd.MergedDictionaries.Add(Application.LoadComponent(new Uri(@"Skins\Black\BlackResources.xaml", UriKind.Relative)) as ResourceDictionary);
            //Application.Current.Resources = rd;
            base.OnStartup(e);
        }
    }
}
