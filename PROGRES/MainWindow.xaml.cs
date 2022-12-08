using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PROGRES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            
            InitializeComponent();
            MainFrame.Navigate(new GreetingPage());
            Manager.MainFrame = MainFrame;

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { txtTime.Text = "Час: " + DateTime.Now.ToLongTimeString(); };

            timer.Start();

            var timer1 = new System.Windows.Threading.DispatcherTimer();
            timer1.Interval = new TimeSpan(0, 0, 0);
            timer1.IsEnabled = true;
            timer1.Tick += (o, t) => { txtDate.Text = "Дата: " + DateTime.Now.ToShortDateString(); };

            timer1.Start();
        }

       

        private void Btn_Catalogue_Prod_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CataloguePage());
            Manager.MainFrame = MainFrame;
            //Btn_Catalogue_Prod.IsEnabled = false;

            //Btn_Order.IsEnabled = false;
            //Btn_Order_History.IsEnabled = false;
            
            txtDate.Visibility = Visibility.Hidden;
            txtTime.Visibility = Visibility.Hidden;
        }

        private void Btn_Order_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrdersPage(null));
            Manager.MainFrame = MainFrame;
            //Btn_Catalogue_Prod.IsEnabled = false;
           // Btn_Order.IsEnabled = false;
            //Btn_Order_History.IsEnabled = false;
        }

        private void Btn_Order_History_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrderHistoryPage());
            Manager.MainFrame = MainFrame;
           // Btn_Catalogue_Prod.IsEnabled = false;
            //Btn_Order.IsEnabled = false;
            //Btn_Order_History.IsEnabled = false;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
           // Btn_Catalogue_Prod.IsEnabled = true;
           // Btn_Order.IsEnabled = true;
            //Btn_Order_History.IsEnabled = true;
            RBtnCatalogue.IsChecked= false;
            RBtnCatalogue.IsEnabled = true;

            RBtnOrder.IsEnabled = true;
            RBtnOrder.IsChecked = false;

            RBtnHistory.IsEnabled = true;
            RBtnHistory.IsChecked = false;

            txtTitle.Text = "Головна сторінка";
            txtTitle.Margin = new System.Windows.Thickness(200, 8, 0, 0);

            txtDate.Visibility = Visibility.Visible;
            txtTime.Visibility = Visibility.Visible;

            mainImage.Source = new BitmapImage(new Uri(@"/PROGRES;component/Images/MainPage.png", UriKind.Relative));
            mainImage.Height = 50;
            mainImage.Width = 50;
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack) { BtnBack.Visibility = Visibility.Visible; }
            else { BtnBack.Visibility = Visibility.Hidden; }
        }

        private void RBtnCatalogue_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CataloguePage());
            Manager.MainFrame = MainFrame;
            mainImage.Source = new BitmapImage(new Uri(@"/PROGRES;component/Images/Catalogue.png", UriKind.Relative));
            mainImage.Height = 70;
            mainImage.Width = 70;
            RBtnCatalogue.IsEnabled = false;
            RBtnOrder.IsEnabled = false;
            RBtnHistory.IsEnabled = false;
            txtTitle.Text = "Довідник продукції";
            txtTitle.Margin = new System.Windows.Thickness(150, 8, 0, 0);
            //Btn_Order.IsEnabled = false;
            //Btn_Order_History.IsEnabled = false;

            txtDate.Visibility = Visibility.Hidden;
            txtTime.Visibility = Visibility.Hidden;
        }

        private void RBtnOrder_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrdersPage(null));
            Manager.MainFrame = MainFrame;
            RBtnOrder.IsEnabled = false;
            RBtnCatalogue.IsEnabled = false;
            RBtnHistory.IsEnabled = false;
            mainImage.Source = new BitmapImage(new Uri(@"/PROGRES;component/Images/Order.png", UriKind.Relative));
            mainImage.Height = 50;
            mainImage.Width = 50;
            txtTitle.Text = "Оформлення замовлення";
            //Btn_Order.IsEnabled = false;
            //Btn_Order_History.IsEnabled = false;

            txtDate.Visibility = Visibility.Hidden;
            txtTime.Visibility = Visibility.Hidden;

            txtTitle.Margin = new System.Windows.Thickness(120, 8, 0, 0);
        }

        private void RBtnHistory_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrderListPage(null));
            Manager.MainFrame = MainFrame;
            RBtnOrder.IsEnabled = false;
            RBtnCatalogue.IsEnabled = false;
            RBtnHistory.IsEnabled = false;
            mainImage.Source = new BitmapImage(new Uri(@"/PROGRES;component/Images/OrderHistory.png", UriKind.Relative));
            mainImage.Height = 50;
            mainImage.Width = 50;
            txtTitle.Text = "Історія замовлень";
            //Btn_Order.IsEnabled = false;
            //Btn_Order_History.IsEnabled = false;

            txtDate.Visibility = Visibility.Hidden;
            txtTime.Visibility = Visibility.Hidden;

            txtTitle.Margin = new System.Windows.Thickness(170, 8, 0, 0);
        }

        private void RBtnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
