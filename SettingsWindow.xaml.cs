using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Print_Server
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public string PrintQuality { get; set; }
        public string PaperFormat { get; set; }
        public bool IsDoubleSided { get; set; }

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void OnPrintSettingsChanged(object sender, SelectionChangedEventArgs e)
        {
            // Здесь вы можете добавить логику для сохранения настроек печати
            // Например, вы можете сохранить их в файл или в базу данных
        }
    }
}
