using System.Windows;
using System.Printing; 

namespace Print_Server
{
    public partial class AddPrinterDialog : Window
    {
        public string SelectedPrinter { get; private set; }

        public AddPrinterDialog()
        {
            InitializeComponent(); 
            LocalPrintServer printServer = new LocalPrintServer();
            foreach (var printQueue in printServer.GetPrintQueues())
            {
                PrintersComboBox.Items.Add(printQueue.Name);
            }
        }
        private void AddPrinterButton_Click(object sender, RoutedEventArgs e)
        {
      
            if (PrintersComboBox.SelectedItem != null)
            {
                SelectedPrinter = PrintersComboBox.SelectedItem as string;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Выберите принтер из списка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
