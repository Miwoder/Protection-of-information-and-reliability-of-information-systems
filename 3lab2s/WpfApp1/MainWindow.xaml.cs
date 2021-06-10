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
            string key = Key.Text;
            string text = key + TextFile.Text;
            char[,] tabl = new char[key.Length, text.Length / key.Length];
            int k = 0;
            
            for(int i = 0; i<key.Length; i++)
            {
                for(int j = 0; j < text.Length / key.Length; j++)
                {
                    tabl[i, j] += text[k++];
                }
            }

            string result = "";
            for (int i = 0; i <= (text.Length / key.Length) - 1; i++)
            {
                if (i == 0 || i % 2 == 0)
                {
                    for (int j = key.Length - 1; j >= 0; j--)
                    {
                        result += tabl[j, i];
                    }
                }
                else
                {
                    for (int j = 0; j <= key.Length - 1; j++)
                    {
                        result += tabl[j, i];
                    }
                }
            }            
            Encrypt.Text += result;
            
        }

        private void Decrypt_Cesare(object sender, RoutedEventArgs e)
        {
            Decrypt.Text = null;
            string key = Key.Text;
            string encrypt = Encrypt.Text;
            char[,] tabl = new char[key.Length, encrypt.Length / key.Length];
            

            int k = 0;
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < encrypt.Length / key.Length; j++)
                {
                    tabl[i, j] += encrypt[k++];
                    //Decrypt.Text += tabl[i, j];
                }
            }


            string result = "";
            for (int i = (text.Length / key.Length) - 1; i >= 0; i--)
            {
                if (i == 0 || i % 2 == 0)
                {
                    for (int j = key.Length - 1; j >= 0; j--)
                    {
                        result += tabl[j, i];
                    }
                }
                else
                {
                    for (int j = 0; j <= key.Length - 1; j++)
                    {
                        result += tabl[j, i];
                    }
                }
            }
            Decrypt.Text += TextFile.Text;

        }



        List<CharNum> listCharNumFirst;

        List<CharNum> listCharNumSecond;
        private void Encrypt_Trismus(object sender, RoutedEventArgs e)
        {

            string firstKey = "Vadim";
            // Второй ключ, количество строк
            string secondKey = "Govrnk";
            // Предложение которое шифруем
            string stringUser = TextFile.Text;

            string fill = " ";

            for (int i = stringUser.Length; i < firstKey.Length * secondKey.Length; i++)
            {
                stringUser += fill;
            }
            string newText = "";
            // Матрица в которой производим шифрование
            char[,] matrix = new char[secondKey.Length, firstKey.Length];

            // Счетчик символов в строке
            int countSymbols = 0;

            // Переводим строки в массивы типа char
            char[] charsFirstKey = firstKey.ToCharArray();
            char[] charsSecondKey = secondKey.ToCharArray();
            char[] charStringUser = stringUser.ToCharArray();

            // Создаем списки в которых будут храниться символы и порядковые номера символов
            listCharNumFirst =
                new List<CharNum>(firstKey.Length);

            listCharNumSecond =
                new List<CharNum>(secondKey.Length);

            // Заполняем символами из ключей
            listCharNumFirst = FillListKey(charsFirstKey);
            listCharNumSecond = FillListKey(charsSecondKey);

            // Заполняем порядковыми номерами
            listCharNumFirst = FillingSerialsNumber(listCharNumFirst);
            listCharNumSecond = FillingSerialsNumber(listCharNumSecond);

            // Заполнение матрицы строкой пользователя
            for (int i = 0; i < listCharNumSecond.Count; i++)
            {
                for (int j = 0; j < listCharNumFirst.Count; j++)
                {
                    matrix[i, j] = charStringUser[countSymbols++];
                }
            }


            countSymbols = 0;
            // Заполнение матрицы с учетом шифрования. 
            // Переставляем столбцы по порядку следования в первом ключе. 
            // Затем переставляем строки по порядку следования во втором ключа. 
            for (int i = 0; i < listCharNumSecond.Count; i++)
            {
                for (int j = 0; j < listCharNumFirst.Count; j++)
                {
                    matrix[listCharNumSecond[i].NumberInWord,
                       listCharNumFirst[j].NumberInWord] = charStringUser[countSymbols++];
                }
            }

            for (int i = 0; i < listCharNumFirst.Count; i++)
            {
                for (int j = 0; j < listCharNumSecond.Count; j++)
                {
                    newText += matrix[j, i];
                }
            }
            //return newText;
            Encrypt.Text = newText;
        }

        private void Decrypt_Trismus(object sender, RoutedEventArgs e)
        {
            string firstKey = "Vadim";
            // Второй ключ, количество строк
            string secondKey = "Govrnk";
            text = Encrypt.Text;
            char[,] newMatrix = new char[secondKey.Length, firstKey.Length];
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            foreach (CharNum index in listCharNumFirst)
            {
                list1.Add(index.NumberInWord);
            }
            foreach (CharNum index in listCharNumSecond)
            {
                list2.Add(index.NumberInWord);
            }
            int k = 0;

            for (int i = 0; i < firstKey.Length; i++)
            {
                int x = list1.IndexOf(i);
                for (int j = 0; j < secondKey.Length; j++)
                {
                    int y = list2.IndexOf(j);
                    //MessageBox.Show(text[k].ToString());
                    newMatrix[y, x] = text[k];
                    k++;
                }
            }

            string oldText = "";

            for (int i = 0; i < secondKey.Length; i++)
            {
                for (int j = 0; j < firstKey.Length; j++)
                {
                    oldText += newMatrix[i, j];
                }
            }
            Decrypt.Text = oldText;

        }

        public static int GetNumberInThealphabet(char s)
        {
            string str = @"AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";

            int number = str.IndexOf(s) / 2;

            return number;
        }

        /// <summary>
        /// Заполнение символами списка с ключом.
        /// </summary>
        /// <param name="chars">массив символов.</param>
        /// <returns>Список символов.</returns>
        private static List<CharNum> FillListKey(char[] chars)
        {
            List<CharNum> listKey = new List<CharNum>(chars.Length);

            for (int i = 0; i < chars.Length; i++)
            {
                CharNum charNum = new CharNum()
                {
                    Ch = chars[i],
                    NumberInWord = GetNumberInThealphabet(chars[i])
                };

                listKey.Add(charNum);
            }
            return listKey;
        }
        /// <summary>
        /// Отображение ключа.
        /// </summary>
        /// <param name="listCharNum">Список в котором содержатся символы с порядковыми номерами.</param>
        private static void ShowKey(List<CharNum> listCharNum, string message)
        {
            Console.WriteLine(message);

            foreach (var i in listCharNum)
            {
                Console.Write(i.Ch + " ");
            }
            Console.WriteLine();

            foreach (var i in listCharNum)
            {
                Console.Write(i.NumberInWord + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        /// <summary>
        /// Заполнение символов ключей, порядковыми номерами.
        /// </summary>
        /// <param name="listCharNum"></param>
        /// <returns></returns>
        private static List<CharNum> FillingSerialsNumber(List<CharNum> listCharNum)
        {
            int count = 0;

            var result = listCharNum.OrderBy(a =>
                a.NumberInWord);

            foreach (var i in result)
            {
                i.NumberInWord = count++;
            }

            return listCharNum;
        }
        class CharNum
        {
            #region Fields
            /// <summary>
            /// Символ.
            /// </summary>
            private char _ch;
            /// <summary>
            /// Порядковый номер зависящий от алфавита.
            /// </summary>
            private int _numberInWord;
            #endregion Fieds

            #region Properties
            /// <summary>
            /// Символ.
            /// </summary>
            public char Ch
            {
                get { return _ch; }
                set
                {
                    if (_ch == value)
                        return;
                    _ch = value;
                }
            }
            /// <summary>
            /// Порядковый номер в строке, зависящий от алфавита.
            /// </summary>
            public int NumberInWord
            {
                get { return _numberInWord; }
                set
                {
                    if (_numberInWord == value)
                        return;
                    _numberInWord = value;
                }
            }
            #endregion Properties
        }



        private void Clear_ALL(object sender, RoutedEventArgs e)
        {
            Encrypt.Text = Decrypt.Text = null;
        }
    }








}
