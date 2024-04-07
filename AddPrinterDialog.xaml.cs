using System.Windows;
using System.Printing; // Добавьте эту директиву

namespace Print_Server
{
    public partial class AddPrinterDialog : Window
    {
        public string SelectedPrinter { get; private set; }

        public AddPrinterDialog()
        {
            InitializeComponent();

            // Заполняем ComboBox списком доступных принтеров при загрузке окна
            LocalPrintServer printServer = new LocalPrintServer();
            foreach (var printQueue in printServer.GetPrintQueues())
            {
                PrintersComboBox.Items.Add(printQueue.Name);
            }
        }

        private void AddPrinterButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли какой-то принтер
            if (PrintersComboBox.SelectedItem != null)
            {
                // Получаем выбранный принтер из ComboBox
                SelectedPrinter = PrintersComboBox.SelectedItem as string;

                // Закрываем окно с результатом DialogResult = true
                DialogResult = true;
            }
            else
            {
                // Если принтер не выбран, выводим сообщение об ошибке
                MessageBox.Show("Выберите принтер из списка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем окно с результатом DialogResult = false
            DialogResult = false;
        }
    }
}
