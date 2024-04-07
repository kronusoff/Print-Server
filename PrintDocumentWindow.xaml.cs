using System;
using System.Windows;
using Microsoft.Win32;
using System.Printing;
using System.Linq;
using System.IO;

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
                MessageBox.Show("Файл не найден.");
                return;
            }

            string extension = Path.GetExtension(filePath);
            if (!IsSupportedFileExtension(extension))
            {
                MessageBox.Show("Неподдерживаемое расширение файла.");
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
                System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
                printDoc.PrinterSettings.PrinterName = printerName;

                printDoc.Print();
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при печати файла: {ex.Message}");
            }
        }
    }
}
