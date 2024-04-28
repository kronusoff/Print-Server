using System;
using System.Windows;
using Microsoft.Win32;
using System.Printing;
using System.Linq;
using System.IO;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace Print_Server
{
    public partial class PrintDocumentWindow : Window
    {
        private string selectedPrinter;

        public PrintDocumentWindow(string printer)
        {
            InitializeComponent();

            selectedPrinter = printer;

            this.AllowDrop = true;
            this.Drop += PrintDocumentWindow_Drop;
            this.DragOver += PrintDocumentWindow_DragOver;
        }

        private void PrintDocumentWindow_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                FilePathTextBox.Text = files[0];
            }
        }

        private void PrintDocumentWindow_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;

            if (!File.Exists(filePath))
            {
                MessageBox.Show("The file was not found.\r\n");
                return;
            }

            string extension = Path.GetExtension(filePath);
            if (!IsSupportedFileExtension(extension))
            {
                MessageBox.Show("Unsupported file extension.\r\n");
                return;
            }

            PrintFile(filePath, selectedPrinter);
        }

        private bool IsSupportedFileExtension(string extension)
        {
            string[] supportedExtensions = { ".txt", ".doc", ".docx", ".pdf" }; 
            return supportedExtensions.Contains(extension.ToLower());
        }

        private void PrintFile(string filePath, string printerName)
        {
            try
            {
                PrintQueue printQueue = new PrintServer().GetPrintQueue(printerName);
                if (printQueue == null)
                {
                    MessageBox.Show("Printer not found.");
                    return;
                }

                // Открываем и читаем содержимое файла
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    // Создаем задание на печать
                    PrintSystemJobInfo printJob = printQueue.AddJob();

                    // Получаем поток для записи данных в задание на печать
                    Stream printStream = printJob.JobStream;

                    // Копируем содержимое файла в поток задания на печать
                    fileStream.CopyTo(printStream);

                    // Закрываем поток задания на печать
                    printStream.Close();
                }

                MessageBox.Show("File added to print queue successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding file to print queue: {ex.Message}");
            }
        }

    }
}

