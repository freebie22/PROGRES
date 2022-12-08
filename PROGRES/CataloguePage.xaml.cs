using System;
using System.Collections.Generic;
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
    /// Interaction logic for CataloguePage.xaml
    /// </summary>
    public partial class CataloguePage : Page
    {
        public CataloguePage()
        {
            InitializeComponent();
            UpdateOutfits();
            var currentOutfits = ProgresDataBaseEntities.GetContext().Outfit.ToList();
            LViewOutfits.ItemsSource = currentOutfits;
          
            var allTypes = ProgresDataBaseEntities.GetContext().Type.ToList();
            allTypes.Insert(0, new Type { Name = "Всі типи"});
            ComboType.ItemsSource = allTypes;
            CheckActual.IsChecked = true;
            ComboType.SelectedIndex = 0;
        }

        private void UpdateOutfits()
        {
            var currentOutfits = ProgresDataBaseEntities.GetContext().Outfit.ToList();

            if(ComboType.SelectedIndex > 0)
            {
                currentOutfits = currentOutfits.Where(p => p.Type.Contains(ComboType.SelectedItem as Type)).ToList();
            }

            currentOutfits = currentOutfits.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (CheckActual.IsChecked.Value)
                currentOutfits = currentOutfits.Where(p => p.IsActual).ToList();
            LViewOutfits.ItemsSource = currentOutfits.OrderBy(p => p.CountAvailiable).ToList();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateOutfits();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateOutfits();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOutfits();
        }
    }
}
