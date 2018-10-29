using System.Windows;

namespace Project2{
    public partial class MainWindow : Window{
        public MainWindow(){
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controls c = new Controls();

            c.ShowDialog();            
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Datas datas = new Datas();

            datas.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Arquivo arquivo = new Arquivo();
            arquivo.ShowDialog();
        }
    }
}