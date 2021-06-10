using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Lab2
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

        string text;
        double Entropy;
        private void TextFileOpen(object sender, RoutedEventArgs e)
        {
            text = "";
            Entropy = 0;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
                text = File.ReadAllText(openFileDialog.FileName);
            if (text == "") MessageBox.Show("Файл пуст");
            else
            {
                foreach (float letterProbability in GetEntropy(text))
                {
                    Entropy -= letterProbability * Math.Log(letterProbability, 2);
                }

                MessageBox.Show("Количество бит информации, приходящихся на 1 символ: " + Entropy.ToString());
                TextFileEntropy.Text = Entropy.ToString();
            }
        }

        private void BinaryFileOpen(object sender, RoutedEventArgs e)
        {
            text = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
                text = File.ReadAllText(openFileDialog.FileName);
            if (text == "") MessageBox.Show("Файл пуст");
            else
            {
                    Entropy = Math.Log(2, 2);

                MessageBox.Show("Количество бит информации, приходящихся на 1 символ: " + Entropy.ToString());
                BinaryFileEntropy.Text = Entropy.ToString();
            }
        }

        List<float>GetEntropy(string text)
        {
            List<string> letters = new List<string>();
            List<float> probability = new List<float>();

            for (int i = 0; i < text.Length; i++)
            {
                if(!letters.Contains(text[i].ToString()))
                {
                    letters.Add(text[i].ToString());
                }
            }

            foreach(string letter in letters)
            {
                float lettersCount=0;
                for (int j = 0; j < text.Length; j++)
                {
                    if (letter == text[j].ToString())
                        lettersCount++;
                }
                probability.Add(lettersCount / text.Length);
            }
            return probability;
        }

        private void TextInfoAmount(object sender, RoutedEventArgs e)
        {
            string text = TextName.Text;
            text = text.Replace(" ", "");
            float p = 0;
            double conditionalEntropy = 0;
            double realEntropy = 0;
            double Entropy = Convert.ToDouble(TextFileEntropy.Text);
            double amount = 0;
            if (String.IsNullOrEmpty(TextFileEntropy.Text))
            {
                MessageBox.Show("Сначала рассчитайте энтропию алфавита");
            }
            else
            {
                if (R11.IsChecked == true)
                {
                    p = 0.1f;
                    conditionalEntropy = -0.1f * Math.Log(0.1f, 2) - 0.9f * Math.Log(0.9f, 2);
                }
                else if (R12.IsChecked == true)
                {
                    p = 0.5f;
                    conditionalEntropy = -0.5f * Math.Log(0.5f, 2) - 0.5f * Math.Log(0.5f, 2);
                }
                else if (R13.IsChecked == true)
                {
                    p = 1.0f;
                    conditionalEntropy = 0;
                }
                realEntropy = Entropy - conditionalEntropy;
                amount = realEntropy * text.Length;
                MessageBox.Show("Количество информации в сообщении \"..." + text + "\"... где p= " + p + ":" + amount.ToString());
            }
        }

        private void BinaryInfoAmount(object sender, RoutedEventArgs e)
        {
            string code = BinaryName.Text;
            code = code.Replace(" ", "");
            float p = 0;
            double conditionalEntropy = 0;
            double realEntropy = 0;
            double Entropy = Convert.ToDouble(BinaryFileEntropy.Text);
            double amount = 0;
            if (String.IsNullOrEmpty(BinaryFileEntropy.Text))
            {
                MessageBox.Show("Сначала рассчитайте энтропию алфавита");
            }
            else
            {
                if (R21.IsChecked == true)
                {
                    p = 0.1f;
                    conditionalEntropy = -0.1f * Math.Log(0.1f, 2) - 0.9f * Math.Log(0.9f, 2);
                }
                else if (R22.IsChecked == true)
                {
                    p = 0.5f;
                    conditionalEntropy = -0.5f * Math.Log(0.5f, 2) - 0.5f * Math.Log(0.5f, 2);
                }
                else if (R23.IsChecked == true)
                {
                    p = 1.0f;
                    conditionalEntropy = 0;
                }
                realEntropy = Entropy - conditionalEntropy;
                amount = realEntropy * code.Length;
                MessageBox.Show("Количество информации в сообщении \"..." + code + "\"... где p= " + p + ":" + amount.ToString());
            }
        }
    }
}
