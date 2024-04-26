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
                MessageBox.Show("The printer is not selected!\r\n");
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
                MessageBox.Show("Select the printer from the list to delete.\r\n");
            }
        }


        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр окна Settings
            SettingsWindow settingsWindow = new SettingsWindow();

            // Отображаем окно как модальное (ShowDialog())
            settingsWindow.ShowDialog();

            // Если окно Settings закрывается, программа будет продолжена с этой строки
            // Если вам нужно выполнять какие-либо действия после закрытия окна Settings,
            // поместите их здесь
        }

    }
}
