using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project2
{
    /// <summary>
    /// Interaction logic for Datas.xaml
    /// </summary>
    public partial class Datas : Window
    {
        //Criação do cronometro para executar processos "thread"
        DispatcherTimer th = new DispatcherTimer();
        DispatcherTimer th1 = new DispatcherTimer();
        DateTime dt;
        int i = 0;// Contador que será usado no cronometro

        public Datas()
        {
            InitializeComponent();
            //DataHoraAtual.Content = DateTime.Now.ToString();

            // th_thick é o método que será executado pela thread, incrementando pelo tempo definido abaixo
            th.Interval = new TimeSpan(0, 0, 0, 1);
            th.Tick += new EventHandler(th_tick);
            th.Start();

            dt = DateTime.Now;

            /**************** Thread do Cronometro ******************/
            th1.Tick += new EventHandler(th1_tick);
            th1.Start();
            th.Interval = new TimeSpan( 0, 0, 0, 1);
        }

        private void Calendario_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Dia.Content = "Dia " + Calendario.SelectedDate.Value.Day.ToString();
            Mes.Content = "Mes " + Calendario.SelectedDate.Value.Month.ToString() + " - " + Calendario.SelectedDate.Value.Date.ToString("MMMM");

            Ano.Content = "Ano " + Calendario.SelectedDate.Value.Year.ToString();

            DiaSemana.Content = Calendario.SelectedDate.Value.Date.ToString("dddd");
            DataLonga.Content = Calendario.SelectedDate.Value.ToLongDateString();

            DateTimeUniversal.Content = Calendario.SelectedDate.Value.ToUniversalTime();


            // EXIBE DATAS FORMATADAS PARA OUTRAS CULTURAS
            String[] cultureNames = { "pt-BR", "en-US", "en-GB", "fr-FR", "de-DE", "ru-RU", "ja-JP" };

            CultureDate.Content = "";

            foreach (var cultureName in cultureNames)
            {
                var culture = new CultureInfo(cultureName);
                CultureDate.Content += string.Format("{0}: {1}\n", cultureName, Calendario.SelectedDate.Value.ToString(culture) );
            }


        }

        private void th_tick(object send, EventArgs e)
        {
            dt = DateTime.Now;
            DataHoraAtual.Content = dt.ToString();
        }

        private void th1_tick(object send, EventArgs e)
        {
            Cronometro.Content = i += 1;
        }

        private void StopTreading_Click(object sender, RoutedEventArgs e)
        {
            th.Stop();
            StopTreading.IsEnabled = false;
            StartTreading.IsEnabled = true;
        }

        private void StartTreading_Click(object sender, RoutedEventArgs e)
        {
            th.Start();
            StopTreading.IsEnabled = true;
            StartTreading.IsEnabled = false;
        }
    }
}
