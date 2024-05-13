using System;
using System.Configuration;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;

namespace Print_Server
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            // Загрузка сохраненных настроек (если они есть)
            LoadSettings();
        }

        private void LoadSettings()
        {
       

            // Загрузка формата бумаги
            string paperFormat = ConfigurationManager.AppSettings["PaperFormat"];
            if (!string.IsNullOrEmpty(paperFormat))
            {
                foreach (ComboBoxItem item in FormatComboBox.Items)
                {
                    if (item.Content.ToString() == paperFormat)
                    {
                        FormatComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // Загрузка двусторонней печати
            string isDoubleSidedStr = ConfigurationManager.AppSettings["IsDoubleSided"];
            bool isDoubleSided;
            if (bool.TryParse(isDoubleSidedStr, out isDoubleSided))
            {
                DoubleSidedCheckBox.IsChecked = isDoubleSided;
            }
        }


        private void SaveSettings()
        {
            // Сохранение качества печати
        

            // Сохранение формата бумаги
            string selectedPaperFormat = FormatComboBox.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedPaperFormat))
            {
                ConfigurationManager.AppSettings["PaperFormat"] = selectedPaperFormat;
            }

            // Сохранение двусторонней печати
            ConfigurationManager.AppSettings["IsDoubleSided"] = (DoubleSidedCheckBox.IsChecked ?? false).ToString();
        }
        
        // Обработчик события закрытия окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Сохранение настроек перед закрытием окна
            SaveSettings();
        }
    }
}
