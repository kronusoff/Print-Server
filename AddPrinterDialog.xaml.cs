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
                string selectedPrinterName = PrintersComboBox.SelectedItem as string;
                LocalPrintServer printServer = new LocalPrintServer();
                PrintQueue selectedPrintQueue = printServer.GetPrintQueue(selectedPrinterName);

                if (selectedPrintQueue != null)
                {
                    if ((selectedPrintQueue.QueueStatus & PrintQueueStatus.Offline) == PrintQueueStatus.Offline)
                    {
                        MessageBox.Show("Selected printer is offline and cannot be added.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        SelectedPrinter = selectedPrinterName;
                        DialogResult = true;
                    }
                }
                else
                {
                    MessageBox.Show("Error retrieving information for the selected printer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Select a printer from the list.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
