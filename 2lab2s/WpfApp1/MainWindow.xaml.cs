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
using Microsoft.Win32;
using System.Threading;

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

        string text;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                text = File.ReadAllText(openFileDialog.FileName);
                TextFile.Text = text;
            }
            if (String.IsNullOrEmpty(text))
                MessageBox.Show("Файл пуст");
        }
        
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string alphabet2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private void Encrypt_Cesare(object sender, RoutedEventArgs e)
        {
            Encrypt.Text = null;
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string key = null;
                key=Key.Text.ToUpper();
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (key[i].Equals(alphabet[j]))
                    {
                        alphabet = alphabet.Remove(j, 1);
                    }
                }
            }
            string newAlphabet = (key + alphabet).ToUpper();
            newAlphabet = new string(newAlphabet.Distinct().ToArray());


            string text = TextFile.Text.ToUpper();//.Replace(" ","");
            for (int i = 0; i < text.Length; i++)
            {
                if (newAlphabet.Contains(text[i])) 
                {
                    Encrypt.Text += newAlphabet[alphabet2.IndexOf(text[i])];
                }
                else Encrypt.Text += text[i];
            }
        }

        private void Decrypt_Cesare(object sender, RoutedEventArgs e)
        {
            Decrypt.Text = null;
            string key = Key.Text.ToUpper();
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (key[i].Equals(alphabet[j]))
                    {
                        alphabet = alphabet.Remove(j, 1);
                    }
                }
            }
            string newAlphabet = (key + alphabet).ToUpper();
            newAlphabet = new string(newAlphabet.Distinct().ToArray());


            string text = Encrypt.Text.ToUpper();//.Replace(" ", "");
            for (int i = 0; i < text.Length; i++)
            {
                //Decrypt.Text += alphabet2[newAlphabet.IndexOf(text[i])];

                if (newAlphabet.Contains(text[i]))
                {
                    Decrypt.Text += alphabet2[newAlphabet.IndexOf(text[i])];
                }
                else Decrypt.Text += text[i];

            }
        }

        private void Encrypt_Trismus(object sender, RoutedEventArgs e)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Encrypt.Text = null;
            string key = null;
            key = Key.Text.ToUpper();
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (key[i].Equals(alphabet[j]))
                    {
                        alphabet = alphabet.Remove(j, 1);
                    }
                }
            }
            string newAlphabet = (key + alphabet).ToUpper();
            string text = TextFile.Text.ToUpper();//.Replace(" ", "");
            newAlphabet = new string(newAlphabet.Distinct().ToArray());


            char[,] newTable = new char[2, 13];

            int k = 0;
            for (int i = 0; i < 2; i++)
            { 
                for (int j = 0; j < 13; j++)
                {
                    newTable[i, j] = newAlphabet[k++];
                }
            }
            
            for(int ix = 0; ix < text.Length; ix++)
            {
                if (newAlphabet.Contains(text[ix]))
                {
                    
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        if (text[ix] == newTable[i, j] && i == 0)
                            Encrypt.Text += newTable[1, j];
                        else if (text[ix] == newTable[i, j] && i == 1)
                            Encrypt.Text += newTable[0, j];
                    }
                }
                }
                else Encrypt.Text += text[ix];
            }           
        }

        private void Decrypt_Trismus(object sender, RoutedEventArgs e)
        {
            Decrypt.Text = null;
            string key = Key.Text.ToUpper();
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (key[i].Equals(alphabet[j]))
                    {
                        alphabet = alphabet.Remove(j, 1);
                    }
                }
            }
            string newAlphabet = (key + alphabet).ToUpper();
            newAlphabet = new string(newAlphabet.Distinct().ToArray());

            string text = Encrypt.Text.ToUpper();//.Replace(" ", "");

            char[,] newTable = new char[2, 13];

            int k = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    newTable[i, j] = newAlphabet[k++];
                }
            }

            for (int ix = 0; ix < text.Length; ix++)
            {
                if (newAlphabet.Contains(text[ix]))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 13; j++)
                        {
                            if (text[ix] == newTable[i, j] && i == 0)
                                Decrypt.Text += newTable[1, j];
                            else if (text[ix] == newTable[i, j] && i == 1)
                                Decrypt.Text += newTable[0, j];
                        }
                    }
                }
                else Decrypt.Text += text[ix];

            }
        }




        private void Clear_ALL(object sender, RoutedEventArgs e)
        {
            Encrypt.Text = Decrypt.Text = null;
        }
    }
}
