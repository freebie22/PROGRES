using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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



namespace PROGRES
{
    /// <summary>
    /// Interaction logic for OrderListPage.xaml
    /// </summary>
    /// 

    public partial class OrderListPage : Page
    {
        


        public OrderListPage(OrderList selectedOrderList)
        {
            InitializeComponent();

            
        }

        
   
       

       
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ProgresDataBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridOrders.ItemsSource = ProgresDataBaseEntities.GetContext().Order.ToList();
            }
        }
    }
}
