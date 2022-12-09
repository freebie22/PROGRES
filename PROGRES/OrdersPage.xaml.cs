using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

namespace PROGRES
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public Guid identificator;
        public DateTime dt;
        private Order _currentOrder = new Order();

        List<Outfit> outfits;
        List<Outfit> cartItems;

        public double cartTotal = 0;



        public OrdersPage(Order selectedOrder)
        {
            InitializeComponent();
            identificator = Guid.NewGuid();

          
            if (selectedOrder != null)
                _currentOrder = selectedOrder;
            DataContext = _currentOrder;

            outfits = ProgresDataBaseEntities.GetContext().Outfit.Where(p => p.CountAvailiable > 0).ToList();
            cartItems = new List<Outfit>();

            GetItems();


            ComboPay.Items.Add("Передоплата картою");
            ComboPay.Items.Add("При отриманні");
        }

        private void GetItems()
        {
            txtOutfit.ItemsSource = outfits;
            txtCartList.ItemsSource = cartItems;
        }

        

        private void RBtnMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            string cost;

            cost = GetTotal();

            StringBuilder errors = new StringBuilder();
            if (txtCartList.Items.Count == 0)
                errors.AppendLine("Оберіть товар зі списку");
            if (_currentOrder.Outfit_Count < 0)
                errors.AppendLine("Вкажіть правильну кількість товару");
            if(string.IsNullOrWhiteSpace(_currentOrder.Customer_Surname))
                errors.AppendLine("Вкажіть прізвище замовника");
            if (string.IsNullOrWhiteSpace(_currentOrder.Customer_Name))
                errors.AppendLine("Вкажіть ім'я замовника");
            if (string.IsNullOrWhiteSpace(_currentOrder.Customer_Midname))
                errors.AppendLine("Вкажіть по-батькові замовника");
            if (string.IsNullOrWhiteSpace(_currentOrder.Customer_Email))
                errors.AppendLine("Вкажіть електронну пошту замовника");
            if (string.IsNullOrWhiteSpace(_currentOrder.Customer_Phone))
                errors.AppendLine("Вкажіть телефон замовника");
            if (string.IsNullOrWhiteSpace(_currentOrder.Adress_Info))
                errors.AppendLine("Вкажіть адресу замовника");
            if (string.IsNullOrWhiteSpace(_currentOrder.Pay_Option))
                errors.AppendLine("Вкажіть тип оплати замовника");
            if (_currentOrder.TotalSum == 0)
                errors.AppendLine("Вкажіть ціну до сплати");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(),"Заповніть, будь-ласка, інформацію про замовлення",MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string message = $"Кількість товарів у кошику: {cartItems.Count}" + Environment.NewLine +
                                   $"Ціна до сплати: {cost}" + " грн." + Environment.NewLine +
                                   Environment.NewLine +
                                   $"Оформляємо замовлення?";


            string caption = "Закриваємо сторінку";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                e.Handled = true;

            if (result == MessageBoxResult.Yes)
            {
                if (_currentOrder.ID == 0)
                {
                    ProgresDataBaseEntities.GetContext().Order.Add(_currentOrder);
                    _currentOrder.Identificator = identificator.ToString();
                    _currentOrder.CreateDate = DateTime.Now.Date;
                }

                try
                {
                    ProgresDataBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Замовлення сформовано");
                    cartItems.Clear();

                    txtSurname.Text = "";
                    txtName.Text = "";
                    txtMidname.Text = "";
                    txtAdress.Text = "";
                    txtEmail.Text = "";
                    txtPhone.Text = "";
                    txtSum.Text = "0";
                    CartCount.Text = "0";
                  
                }

                catch (DbEntityValidationException er)
                {
                    foreach (var eve in er.EntityValidationErrors)
                    {
                        MessageBox.Show($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error:");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            MessageBox.Show($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                    throw;
                }
            }
            
        }

        private void txtOutfit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Outfit selectedOutfit = (Outfit)((ListBox)sender).SelectedItem;
            if (selectedOutfit == null)
            {
                return;
            }

            cartItems.Add(selectedOutfit);
            var itemNames = string.Join(", ", txtCartList.Items.Cast<Outfit>().Select(item => item.Name));
            _currentOrder.Shopping_Info = itemNames;
            CartCount.Text = cartItems.Count.ToString();
            txtSum.Text = GetTotal();
            _currentOrder.TotalSum = decimal.Parse(txtSum.Text);
            _currentOrder.Outfit_Count = int.Parse(CartCount.Text);
            txtOutfit.SelectedItem = null;
        }

        private string GetTotal()
        {
            cartTotal = 0;

            foreach (var item in cartItems)
                cartTotal += (int)item.Price;

            return cartTotal.ToString();
        }


    }
}
