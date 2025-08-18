# How to display unsorted icons in tristate sorting in .NET MAUI DataGrid?.

This article demonstrates how to display unsorted icons in tristate sorting in [.NET MAUI DataGrid](https://www.syncfusion.com/maui-controls/maui-datagrid).

To show an icon for unsorted columns in the SfDataGrid, you can use the [HeaderTemplate](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.DataGrid.DataGridColumn.html#Syncfusion_Maui_DataGrid_DataGridColumn_HeaderTemplate) of each column. This template allows you to define a custom icon that appears only when the column is not sorted. The visibility of this icon is controlled via a ViewModel and a Boolean converter. When a column is sorted, the built-in SortIconTemplate of the DataGrid automatically displays the appropriate ascending or descending icon.

## Xaml:

```Xml
<syncfusion:DataGridNumericColumn HeaderText="Order ID" Format="0"
                                MappingName="OrderID" Width="150">
    <syncfusion:DataGridNumericColumn.HeaderTemplate>
        <DataTemplate>
            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="*" />
                    <ColumnDefinition Width="Auto" />
                </Grid.ColumnDefinitions>
                
                <Label Grid.Column="0" 
                       Text="Order ID"
                       VerticalOptions="Center"
                       HorizontalOptions="Center" />
                
                <Label Grid.Column="1"
                       Text="↕"
                       TextColor="Gray"
                       FontSize="16"
                       VerticalOptions="Center"
                       HorizontalOptions="End"
                       Margin="0,0,5,0"
                       IsVisible="{Binding Source={StaticResource SortStateViewModel}, Path=IsOrderIDSorted, Converter={StaticResource InvertBooleanConverter},ConverterParameter={x:Reference dataGrid}}"/>
            </Grid>
        </DataTemplate>
    </syncfusion:DataGridNumericColumn.HeaderTemplate>
</syncfusion:DataGridNumericColumn>
```

## C#

```C#
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
```

You can download this example on [GitHub](https://github.com/SyncfusionExamples/How-to-display-the-unsorted-icon-in-tristate-sorting-in-.NET-MAUI-DataGrid).