using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MachathonApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = serviceType.SelectedIndex;
            if (selectedIndex > 0) {
                if (selectedIndex == 1) {
                    MTGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    DBGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                if (selectedIndex == 2)
                {
                    DBGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    MTGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                First.Text = "";
                Second.Text = "";
                Third.Text = "";
                Fourth.Text = "";
            }
        }

        private async void DBbtn_Click(object sender, RoutedEventArgs e)
        {
            DBClass dbObject = new DBClass();
            dbObject.dataType = dataType.SelectedValue.ToString();
            dbObject.geoReplication = geo1.SelectedValue.ToString();
            dbObject.queryCapability = queryCapability.SelectedValue.ToString();
            dbObject.scaleOut = scaleOut.SelectedValue.ToString();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"uri" + dbObject);

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            List<string> retrnentiy = response.Content.ReadAsAsync<List<string>>().Result;
            First.Text = retrnentiy[0] + System.Environment.NewLine;
            Second.Text = retrnentiy[1] + System.Environment.NewLine;
            Third.Text = retrnentiy[2] + System.Environment.NewLine;
            Fourth.Text = retrnentiy[3] + System.Environment.NewLine;
        }

        private async void MTbtn_Click(object sender, RoutedEventArgs e)
        {
            MTClass mtObject = new MTClass();
            mtObject.deployTime = deployTime.SelectedValue.ToString();
            mtObject.deviceType = deviceType.SelectedValue.ToString();
            mtObject.domain = domain.SelectedValue.ToString();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://machathon.azurewebsites.net/api/middle/" + mtObject);

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            List<Returnentity> retrnentiy = response.Content.ReadAsAsync<List<Returnentity>>().Result;
            First.Text = retrnentiy[0] + System.Environment.NewLine;
            Second.Text = retrnentiy[1] + System.Environment.NewLine;
            Third.Text = retrnentiy[2] + System.Environment.NewLine;
            Fourth.Text = "";
        }
    }
}
