using System.Windows;
using System.Windows.Controls;

namespace Project2{
    public partial class Controls : Window{
        public Controls(){
            InitializeComponent();
        }

        private void Send_info_Click(object sender, RoutedEventArgs e){
            label_result.Content = textBox_input.Text;
        }

        private void checkBox_yellow_Checked(object sender, RoutedEventArgs e){
            label_result.Foreground = System.Windows.Media.Brushes.Yellow;
            checkBox_blue.IsChecked = false;
            checkBox_red.IsChecked = false;
        }

        private void checkBox_red_Checked(object sender, RoutedEventArgs e){
            label_result.Foreground = System.Windows.Media.Brushes.Red;
            checkBox_blue.IsChecked = false;
            checkBox_yellow.IsChecked = false;
        }

        private void checkBox_blue_Checked(object sender, RoutedEventArgs e){
            label_result.Foreground = System.Windows.Media.Brushes.Blue;
            checkBox_yellow.IsChecked = false;
            checkBox_red.IsChecked = false;
        }

        private void radioButton_mystic_Checked(object sender, RoutedEventArgs e){
            label_result.Content = radioButton_mystic.Content;
            label_result.Foreground = radioButton_mystic.Foreground;
        }

        private void radioButton1_valor_Checked(object sender, RoutedEventArgs e){
            label_result.Content = radioButton1_valor.Content;
            label_result.Foreground = radioButton1_valor.Foreground;
        }

        private void radioButton2_yellow_Checked(object sender, RoutedEventArgs e){
            label_result.Content = radioButton2_instinct.Content;
            label_result.Foreground = radioButton2_instinct.Foreground;
        }

        private void Paises_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)Paises.SelectedItem;
            label_result.Content = item.Content;
            label_result.Foreground = item.Foreground;

        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

            MessageBoxResult resposta = MessageBox.Show("O arquivo já existe, deseja regravar?","REGRAVAR",MessageBoxButton.YesNoCancel);

            if(resposta == MessageBoxResult.Cancel)
            {
                label_result.Content = "CANCELOU";
            }else if (resposta == MessageBoxResult.Yes) {
                label_result.Content = "SIM";
            }else if(resposta == MessageBoxResult.No){
                label_result.Content = "NÃO";
            }

        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
                this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult resposta = MessageBox.Show("Deseja sair?", "Sair", MessageBoxButton.YesNo);

            if (resposta == MessageBoxResult.No)
            {
                e.Cancel = true;
                label_result.Content = "Continue";
            }
        }
        
    }
}