using BusinessServiceLayer.DTOs;
using PresentationLayer.Models;
using PresentationLayer.ViewModels;
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

namespace PresentationLayer.Views
{
    /// <summary>
    /// Interaction logic for MakeReservationView.xaml
    /// </summary>
    public partial class MakeReservationView : UserControl
    {
        public MakeReservationView()
        {
            InitializeComponent();
        }

        private void Remove_Item(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                BasketItem basketItem = (BasketItem) element.DataContext;
                BasketManager.DeleteBasketItem(basketItem.RoomId);                                     
            }
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RoomList.Items.Filter = FilterCombinedMethod;
        }

        private void FilterDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoomList.Items.Filter = FilterCombinedMethod;
        }

        private bool FilterCombinedMethod(object obj)
        {
            var room = (MakeReservationRoomItemViewModel)obj;

            // Check if any filter is active (TextBox or DropdownComboBox)
            bool filterTextBoxActive = !string.IsNullOrEmpty(FilterTextBox.Text);
            bool filterDropdownActive = FilterComboBox.SelectedItem != null;

            bool roomNumberMatch = filterTextBoxActive && room.RoomNumber.Contains(FilterTextBox.Text)
                    || room.RoomMaxCapacity.Equals(FilterTextBox.Text);

            bool roomTypeMatch = filterDropdownActive && room.RoomTypeId == ((RoomTypeDTO)FilterComboBox.SelectedItem).Id;

            // Apply filters based on active conditions
            if (!filterTextBoxActive && !filterDropdownActive)
            {
                // No filters active, show all rooms
                return true;
            }
            else if (filterTextBoxActive && !filterDropdownActive)
            {
                // Only text filter
                return roomNumberMatch;
            }
            else if (!filterTextBoxActive && filterDropdownActive)
            {
                // Only dropdown filter
                return roomTypeMatch;
            }
            else
            {
                // Both text and dropdown filter
                return roomNumberMatch && roomTypeMatch;
            }
        }

        private void Button_Reset(object sender, RoutedEventArgs e)
        {
            RoomList.Items.Filter = ResetFilter;
        }

        private bool ResetFilter(object obj)
        {
            FilterTextBox.Text = string.Empty;
            FilterComboBox.SelectedItem = null;
            return true;
        }
    }
}
