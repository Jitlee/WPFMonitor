//using System;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Ink;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;
//using System.Windows.Resources;
//using System.IO;
//using System.Collections.Generic;
//using System.Linq;
//using System.Xml.Linq;

//namespace MonitorSystem.GetData
//{
//    public class TemplateManager
//    {
//        public static Dictionary<string, string> DataTemplates;

//        public static void LoadTemplate(string path)
//        {
//            WebClient wc = new WebClient();
//            wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
//            wc.OpenReadAsync(new Uri(App.Current.Host.Source, "../Template/" + path)); //DataGrid/Content.zip			
//        }

//        static void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
//        {
//            if (e.Error == null)
//            {
//                StreamResourceInfo zipInfo = new StreamResourceInfo(e.Result, null);
//                // Read manifest from zip file  StreamResourceInfo manifestInfo = Application.GetResourceStream(_zipInfo, new Uri("content.xml", UriKind.Relative)); StreamReader reader = new StreamReader(manifestInfo.Stream); XDocument document = XDocument.Load(reader); var mediaFiles = from m in document.Descendants("mediafile") select new MediaInfo  { Type = (MediaType) Enum.Parse(typeof(MediaType), m.Attribute("type").Value, true), Name = m.Attribute("name").Value }; _mediaInfos = new List<MediaInfo>(); _mediaInfos.AddRange(mediaFiles); ProgressTextBlock.Visibility = Visibility.Collapsed; PrevButton.IsEnabled = true; NextButton.IsEnabled = true; DisplayMedia(_mediaInfos[0]);
//                StreamResourceInfo manifestInfo = Application.GetResourceStream(zipInfo, new Uri("content.xml", UriKind.Relative));
//                StreamReader reader = new StreamReader(manifestInfo.Stream);
//                XDocument xd = XDocument.Load(reader);
//                reader.Close();
//                var files = from m in xd.Descendants("templatefile")
//                            select m.Attribute("name").Value;

//                if (DataTemplates == null)
//                    DataTemplates = new Dictionary<string, string>();
//                foreach (var f in files)
//                {
//                    string filename = f + ".xaml";
//                    StreamResourceInfo streamInfo = Application.GetResourceStream(zipInfo, new Uri(filename, UriKind.Relative));
//                    using (StreamReader r = new StreamReader(streamInfo.Stream))
//                    {
//                        string s = r.ReadToEnd();
//                        if (!DataTemplates.ContainsKey(f))
//                            DataTemplates.Add(f, s);
//                    }
//                }
//            }
//        }
//    }
//}
