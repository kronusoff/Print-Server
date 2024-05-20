using System;
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
            LoadPrinters();
        }

        private void LoadPrinters()
        {
            try
            {
                LocalPrintServer printServer = new LocalPrintServer();
                foreach (var printQueue in printServer.GetPrintQueues())
                {
                    // Проверяем доступность принтера перед добавлением в список
                    if (!IsPrinterUnavailable(printQueue))
                    {
                        PrintersComboBox.Items.Add(printQueue.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading printers: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    selectedPrintQueue.Refresh();
                    if (IsPrinterUnavailable(selectedPrintQueue))
                    {
                        MessageBox.Show("Selected printer is offline or not ready and cannot be added.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private bool IsPrinterUnavailable(PrintQueue printQueue)
        {
            if (printQueue == null)
                throw new ArgumentNullException(nameof(printQueue));

            printQueue.Refresh();

            // Combine multiple statuses that indicate the printer is unavailable
            if ((printQueue.QueueStatus & PrintQueueStatus.Offline) == PrintQueueStatus.Offline ||
                (printQueue.QueueStatus & PrintQueueStatus.PaperOut) == PrintQueueStatus.PaperOut ||
                (printQueue.QueueStatus & PrintQueueStatus.DoorOpen) == PrintQueueStatus.DoorOpen ||
                (printQueue.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error ||
                (printQueue.QueueStatus & PrintQueueStatus.NotAvailable) == PrintQueueStatus.NotAvailable ||
                (printQueue.QueueStatus & PrintQueueStatus.NoToner) == PrintQueueStatus.NoToner ||
                (printQueue.QueueStatus & PrintQueueStatus.OutOfMemory) == PrintQueueStatus.OutOfMemory ||
                (printQueue.QueueStatus & PrintQueueStatus.PaperJam) == PrintQueueStatus.PaperJam ||
                (printQueue.QueueStatus & PrintQueueStatus.OutputBinFull) == PrintQueueStatus.OutputBinFull ||
                (printQueue.QueueStatus & PrintQueueStatus.PaperProblem) == PrintQueueStatus.PaperProblem ||
                (printQueue.QueueStatus & PrintQueueStatus.UserIntervention) == PrintQueueStatus.UserIntervention)
            {
                return true;
            }

            return false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
