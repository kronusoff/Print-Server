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

        // Обработчик события для кнопки "Добавить принтер"
        private void AddPrinter_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и открываем диалоговое окно для добавления принтера
            AddPrinterDialog addPrinterDialog = new AddPrinterDialog();
            if (addPrinterDialog.ShowDialog() == true)
            {
                // Если диалоговое окно закрыто с результатом "OK", получаем имя выбранного принтера из диалога
                string selectedPrinter = addPrinterDialog.SelectedPrinter;

                // Проверяем, был ли выбран принтер
                if (!string.IsNullOrEmpty(selectedPrinter))
                {
                    // Добавляем новый принтер в ListBox
                    ListBoxItem newItem = new ListBoxItem();
                    newItem.Content = selectedPrinter;
                    PrintersListBox.Items.Add(newItem);

                    // Опционально: Вызываем метод для получения списка принтеров
                    // и обновляем ListBox
                    // GetPrinters();
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





        // Обработчик события для кнопки "Удалить принтер"
        private void RemovePrinter_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли какой-то элемент в списке
            if (PrintersListBox.SelectedItem != null)
            {
                // Получаем выбранный принтер
                ListBoxItem selectedPrinterItem = PrintersListBox.SelectedItem as ListBoxItem;
                // Удаляем выбранный принтер из списка
                PrintersListBox.Items.Remove(selectedPrinterItem);
                // Опционально: Добавьте здесь логику для удаления принтера из вашей системы управления принтерами
            }
            else
            {
                // Если ничего не выбрано, выводим сообщение об ошибке
                MessageBox.Show("Выберите принтер из списка для удаления.");
            }
        }


        // Обработчик события для кнопки "Настройки"
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            // Добавить логику для настроек
            // Например, открыть окно с настройками
        }
    }
}
