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
using System.Windows.Shapes;

namespace WpfApp1
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NumberA.Text == "2")
            {
                Result.Text = null;
                int a = NumberA.Text == null ? 0 : int.Parse(NumberA.Text);
                int b = NumberB.Text == null ? 0 : int.Parse(NumberB.Text);

                List<int> num = new List<int> { };
                for (int i = 2; i <= b; i++)
                {
                    num.Add(i);
                }

                for (int i = 0; i < num.Count; i++)
                {
                    for (int j = 2; j < b; j++)
                        num.Remove(num[i] * j);
                }

                if (num.Count != 0)
                {
                    Result.Text = "Простые числа от 2 до " + b + ": ";
                    foreach (int w in num)
                    {
                        Result.Text += w + ", "; ;
                    }
                    Result.Text += "\n Количесвто простых чисел: " + num.Count;
                }
                else
                    Result.Text = "Простых чисел в данном диапозоне нет";
            }
            else
            {
                Result.Text = null;
                int a = NumberA.Text == null ? 0 : int.Parse(NumberA.Text);
                int b = NumberB.Text == null ? 0 : int.Parse(NumberB.Text);

                List<int> numb = new List<int> { };
                for (int i = 2; i <= b; i++)
                {
                    numb.Add(i);
                }

                for (int i = 0; i < numb.Count; i++)
                {
                    for (int j = 2; j < b; j++)
                        numb.Remove(numb[i] * j);
                }
                if (numb.Count != 0)
                {
                    Result.Text = "Простые числа от " + a + " до " + b +": ";
                    foreach (int w in numb)
                    {
                        if (w > a)
                            Result.Text += w + ", ";
                    }
                }
                else
                    Result.Text = "Простых чисел в данном диапозоне нет";
            }

        }

        private void NOD(object sender, RoutedEventArgs e)
        {
            Result.Text = null;
            int a = NumberA.Text == null ? 0 : int.Parse(NumberA.Text);
            int b = NumberB.Text == null ? 0 : int.Parse(NumberB.Text);
            int c = NumberC.Text == null ? 0 : int.Parse(NumberC.Text);
            NodD(a, b);
            if (c!=0)
            NodD(cc, c);
            Result.Text = "НОД: " +cc;

        }
        public static int cc;

        static void NodD(int a, int b)
        {
            if (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a = a % b;
                }
                else
                {
                    b = b % a;
                }
                NodD(a, b);
            }
            else
                cc = a + b;
        }
    }
}
