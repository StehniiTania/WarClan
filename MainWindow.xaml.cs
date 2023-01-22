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
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace WarClan
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        

        public MainWindow()
        {
            InitializeComponent();        
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            int scale = 15;
            if ((sender as Button).Name == "Start_Small")
                scale = 21;
            if ((sender as Button).Name == "Start_Normal")
                scale = 15;
            if ((sender as Button).Name == "Start_Big")
                scale = 9;

            Field1 window = new Field1(scale);
            window.Show();          
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
