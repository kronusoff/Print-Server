using System;
using System.Windows;
using System.Windows.Controls;

namespace Print_Server
{
    public partial class SettingsWindow : Window
    {
        public string PrintQuality { get; set; }
        public string PaperFormat { get; set; }
        public bool IsDoubleSided { get; set; }

        public SettingsWindow()
        {
            InitializeComponent();
            // Подписываемся на события изменения настроек
            QualityComboBox.SelectionChanged += OnPrintSettingsChanged;
            FormatComboBox.SelectionChanged += OnPrintSettingsChanged;
            DoubleSidedCheckBox.Checked += OnPrintSettingsChanged;
            DoubleSidedCheckBox.Unchecked += OnPrintSettingsChanged;
        }

        private void OnPrintSettingsChanged(object sender, EventArgs e)
        {
            // Сохранение настроек печати
            ComboBoxItem qualityItem = (ComboBoxItem)QualityComboBox.SelectedItem;
            PrintQuality = qualityItem.Content.ToString();

            ComboBoxItem formatItem = (ComboBoxItem)FormatComboBox.SelectedItem;
            PaperFormat = formatItem.Content.ToString();

            IsDoubleSided = DoubleSidedCheckBox.IsChecked ?? false;
        }

        // Метод для получения сохраненных настроек печати
        public PrintSettings GetPrintSettings()
        {
            return new PrintSettings
            {
                Quality = PrintQuality,
                Format = PaperFormat,
                IsDoubleSided = IsDoubleSided
            };
        }
    }

    // Класс для хранения настроек печати
    public class PrintSettings
    {
        public string Quality { get; set; }
        public string Format { get; set; }
        public bool IsDoubleSided { get; set; }
    }
}
