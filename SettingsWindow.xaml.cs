using System;
using System.Configuration;
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
            // Загрузка остальных настроек...

            // Загрузка цветового режима печати
            string colorMode = ConfigurationManager.AppSettings["ColorMode"];
            if (!string.IsNullOrEmpty(colorMode))
            {
                foreach (ComboBoxItem item in ColorModeComboBox.Items)
                {
                    if (item.Content.ToString() == colorMode)
                    {
                        ColorModeComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // Загрузка источника бумаги
            string paperSource = ConfigurationManager.AppSettings["PaperSource"];
            if (!string.IsNullOrEmpty(paperSource))
            {
                foreach (ComboBoxItem item in PaperSourceComboBox.Items)
                {
                    if (item.Content.ToString() == paperSource)
                    {
                        PaperSourceComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            // Загрузка количества копий (необходимо преобразование типов)
            int copies;
            if (int.TryParse(ConfigurationManager.AppSettings["Copies"], out copies))
            {
                CopiesTextBox.Text = copies.ToString();
            }
        }

        private void SaveSettings()
        {
            // Сохранение остальных настроек...

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Сохранение цветового режима печати
            string selectedColorMode = ((ComboBoxItem)ColorModeComboBox.SelectedItem).Content.ToString();
            config.AppSettings.Settings.Remove("ColorMode");
            config.AppSettings.Settings.Add("ColorMode", selectedColorMode);

            // Сохранение формата бумаги
            string selectedFormat = ((ComboBoxItem)FormatComboBox.SelectedItem).Content.ToString();
            config.AppSettings.Settings.Remove("Format");
            config.AppSettings.Settings.Add("Format", selectedFormat);

            // Сохранение значения флажка Double-sided printing
            bool doubleSidedPrinting = DoubleSidedCheckBox.IsChecked ?? false;
            config.AppSettings.Settings.Remove("DoubleSidedPrinting");
            config.AppSettings.Settings.Add("DoubleSidedPrinting", doubleSidedPrinting.ToString());

            // Сохранение источника бумаги
            string selectedPaperSource = ((ComboBoxItem)PaperSourceComboBox.SelectedItem).Content.ToString();
            config.AppSettings.Settings.Remove("PaperSource");
            config.AppSettings.Settings.Add("PaperSource", selectedPaperSource);

            // Сохранение количества копий
            int copies;
            if (int.TryParse(CopiesTextBox.Text, out copies))
            {
                config.AppSettings.Settings.Remove("Copies");
                config.AppSettings.Settings.Add("Copies", copies.ToString());
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        // Обработчик события закрытия окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Сохранение настроек перед закрытием окна
            SaveSettings();
        }
    }
}
