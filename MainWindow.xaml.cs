using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Printing;

namespace Print_Server
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddPrinter_Click(object sender, RoutedEventArgs e)
        {
            AddPrinterDialog addPrinterDialog = new AddPrinterDialog();
            if (addPrinterDialog.ShowDialog() == true)
            {
                string selectedPrinter = addPrinterDialog.SelectedPrinter;
                if (!string.IsNullOrEmpty(selectedPrinter))
                {
                    ListBoxItem newItem = new ListBoxItem();
                    newItem.Content = selectedPrinter;
                    PrintersListBox.Items.Add(newItem);
                }
            }
        }

        private void PrintersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Получаем выбранный принтер из ListBox
            ListBoxItem selectedItem = PrintersListBox.SelectedItem as ListBoxItem;

            if (selectedItem != null)
            {
                string selectedPrinter = selectedItem.Content.ToString();

                // Создаем новое окно для загрузки документа для печати
                PrintDocumentWindow printDocumentWindow = new PrintDocumentWindow(selectedPrinter);
                printDocumentWindow.ShowDialog(); // Отображаем окно
            }
            else
            {
                MessageBox.Show("Не выбран принтер!");
            }
        }
 private void RemovePrinter_Click(object sender, RoutedEventArgs e)
        {
            if (PrintersListBox.SelectedItem != null)
            {
                ListBoxItem selectedPrinterItem = PrintersListBox.SelectedItem as ListBoxItem;
                PrintersListBox.Items.Remove(selectedPrinterItem);
            }
            else
            {
                MessageBox.Show("Выберите принтер из списка для удаления.");
            }
        }


        private void Settings_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
