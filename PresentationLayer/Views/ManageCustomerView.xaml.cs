using PresentationLayer.ViewModels;
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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class ManageCustomerView : UserControl
    {
        public ManageCustomerView()
        {
            InitializeComponent();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomerList.Items.Filter = FilterMethod;
        }

        // Filter customer based on Name, Phone, Email
        private bool FilterMethod(object obj)
        {
            var customer = (CustomerItemsViewModel)obj;
            return customer.CustomerFullName.ToLowerInvariant().Contains(FilterTextBox.Text) ||
                customer.Telephone.ToLowerInvariant().Contains(FilterTextBox.Text) ||
                customer.EmailAddress.ToLowerInvariant().Contains(FilterTextBox.Text);
        }
    }
}
