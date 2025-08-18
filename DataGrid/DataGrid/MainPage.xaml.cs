using System.Collections.ObjectModel;
using Syncfusion.Maui.DataGrid;
using System.Drawing;
using System.Globalization;
using Syncfusion.Maui.DataGrid.Helper;
using Microsoft.Maui.Controls;

namespace DataGrid
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            dataGrid.SortColumnsChanged += DataGrid_SortColumnsChanged;
        }

        private void DataGrid_SortColumnsChanged(object sender, DataGridSortColumnsChangedEventArgs e)
        {
            var sortStateViewModel = Resources["SortStateViewModel"] as ColumnSortStateViewModel;
            if (sortStateViewModel != null)
            {
                var sortedColumns = dataGrid.SortColumnDescriptions.Select(s => s.ColumnName).ToList();

                sortStateViewModel.IsOrderIDSorted = sortedColumns.Contains("OrderID");
                sortStateViewModel.IsCustomerIDSorted = sortedColumns.Contains("CustomerID");
                sortStateViewModel.IsShipCountrySorted = sortedColumns.Contains("ShipCountry");
            }
        }

    }

    public class ColumnSortStateViewModel : BindableObject
    {
        public static readonly BindableProperty OrderIDSortedProperty =
            BindableProperty.Create(nameof(IsOrderIDSorted), typeof(bool), typeof(ColumnSortStateViewModel), false);

        public static readonly BindableProperty CustomerIDSortedProperty =
            BindableProperty.Create(nameof(IsCustomerIDSorted), typeof(bool), typeof(ColumnSortStateViewModel), false);

        public static readonly BindableProperty ShipCountrySortedProperty =
            BindableProperty.Create(nameof(IsShipCountrySorted), typeof(bool), typeof(ColumnSortStateViewModel), false);

        public bool IsOrderIDSorted
        {
            get => (bool)GetValue(OrderIDSortedProperty);
            set => SetValue(OrderIDSortedProperty, value);
        }

        public bool IsCustomerIDSorted
        {
            get => (bool)GetValue(CustomerIDSortedProperty);
            set => SetValue(CustomerIDSortedProperty, value);
        }

        public bool IsShipCountrySorted
        {
            get => (bool)GetValue(ShipCountrySortedProperty);
            set => SetValue(ShipCountrySortedProperty, value);
        }
    }

    public class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var dataGrid = parameter as SfDataGrid;
            if (dataGrid != null && !dataGrid.AllowTriStateSorting) {
                return false;
            }
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return value;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return value;
        }
    }


    public class SortIconTemplate : DataTemplateSelector
    {
        public DataTemplate AscendingTemplate { get; set; }

        public DataTemplate DescendingTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var description = item as SortColumnDescription;
            if (description == null)
            {
                return null;
            }

            if (description.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
            {
                return AscendingTemplate;
            }
            else
            {
                return DescendingTemplate;
            }
        }
    }
    public class OrderInfo
    {
        private int orderID;
        private string customerID;
        private string customer;
        private string shipCity;
        private string shipCountry;

        public int OrderID
        {
            get { return orderID; }
            set { this.orderID = value;}
        }

        public string CustomerID
        {
            get { return customerID; }
            set { this.customerID = value; }
        }

        public string ShipCountry
        {
            get { return shipCountry; }
            set { this.shipCountry = value; }
        }

        public string Customer
        {
            get { return this.customer; }
            set { this.customer = value; }
        }

        public string ShipCity
        {
            get { return shipCity; }
            set { this.shipCity = value; }
        }

        public OrderInfo(int orderId, string customerId, string country, string customer, string shipCity)
        {
            this.OrderID = orderId;
            this.CustomerID = customerId;
            this.Customer = customer;
            this.ShipCountry = country;
            this.ShipCity = shipCity;
        }
    }

    public class OrderInfoRepository
    {
        private ObservableCollection<OrderInfo> orderInfo;
        public ObservableCollection<OrderInfo> OrderInfoCollection
        {
            get { return orderInfo; }
            set { this.orderInfo = value; }
        }

        public OrderInfoRepository()
        {
            orderInfo = new ObservableCollection<OrderInfo>();
            this.GenerateOrders();
        }

        public void GenerateOrders()
        {
            orderInfo.Add(new OrderInfo(1001, "Maria Anders", "Germany", "ALFKI", "Berlin"));
            orderInfo.Add(new OrderInfo(1002, "Ana Trujillo", "Mexico", "ANATR", "Mexico D.F."));
            orderInfo.Add(new OrderInfo(1003, "Ant Fuller", "Mexico", "ANTON", "Mexico D.F."));
            orderInfo.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", "London"));
            orderInfo.Add(new OrderInfo(1005, "Tim Adams", "Sweden", "BERGS", "London"));
            orderInfo.Add(new OrderInfo(1006, "Hanna Moos", "Germany", "BLAUS", "Mannheim"));
            orderInfo.Add(new OrderInfo(1007, "Andrew Fuller", "France", "BLONP", "Strasbourg"));
            orderInfo.Add(new OrderInfo(1008, "Martin King", "Spain", "BOLID", "Madrid"));
            orderInfo.Add(new OrderInfo(1009, "Lenny Lin", "France", "BONAP", "Marsiella"));
            orderInfo.Add(new OrderInfo(1010, "John Carter", "Canada", "BOTTM", "Lenny Lin"));
            orderInfo.Add(new OrderInfo(1011, "Laura King", "UK", "AROUT", "London"));
            orderInfo.Add(new OrderInfo(1012, "Anne Wilson", "Germany", "BLAUS", "Mannheim"));
            orderInfo.Add(new OrderInfo(1013, "Martin King", "France", "BLONP", "Strasbourg"));
            orderInfo.Add(new OrderInfo(1014, "Gina Irene", "UK", "AROUT", "London"));
        }
    }

}
