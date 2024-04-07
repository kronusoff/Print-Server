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

            // Сохраняем выбранный принтер
            selectedPrinter = printer;

            // Добавляем обработчики событий DragOver и Drop к окну
            this.AllowDrop = true;
            this.Drop += PrintDocumentWindow_Drop;
            this.DragOver += PrintDocumentWindow_DragOver;
        }

        private void PrintDocumentWindow_Drop(object sender, DragEventArgs e)
        {
            // Получаем путь к перетаскиваемому файлу
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                // Отображаем путь к выбранному файлу в TextBox
                FilePathTextBox.Text = files[0];
            }
        }

        private void PrintDocumentWindow_DragOver(object sender, DragEventArgs e)
        {
            // Указываем, что перетаскиваемые данные являются файлами
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
            // Открываем диалоговое окно для выбора файла для печати
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // Отображаем путь к выбранному файлу в TextBox
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем путь к выбранному файлу
            string filePath = FilePathTextBox.Text;

            // Проверяем, существует ли файл
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не найден.");
                return;
            }

            // Проверяем расширение файла
            string extension = Path.GetExtension(filePath);
            if (!IsSupportedFileExtension(extension))
            {
                MessageBox.Show("Неподдерживаемое расширение файла.");
                return;
            }

            // Печатаем файл на выбранном принтере
            PrintFile(filePath, selectedPrinter);
        }

        // Метод для проверки поддерживаемого расширения файла
        private bool IsSupportedFileExtension(string extension)
        {
            string[] supportedExtensions = { ".txt", ".doc", ".docx", ".pdf" }; // Укажите поддерживаемые расширения
            return supportedExtensions.Contains(extension.ToLower());
        }

        private void PrintFile(string filePath, string printerName)
        {
            try
            {
                // Получаем принтер по его имени
                System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
                printDoc.PrinterSettings.PrinterName = printerName;

                // Печатаем файл
                printDoc.Print();
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при печати файла: {ex.Message}");
            }
        }
    }
}
