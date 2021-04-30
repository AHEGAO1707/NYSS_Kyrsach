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

namespace NYSS_Kyrsach
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Clear();
            Import();
            TextBox.Text = text;
        }

        public static string text = "";
        public static void Import()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();

            string s = "";
            if (openFile.FileName != "")
            {
                Encoding win1251 = Encoding.GetEncoding("windows-1251");

                StreamReader streamRead = new StreamReader(openFile.FileName, win1251);
                while (!streamRead.EndOfStream)
                {
                    s = streamRead.ReadToEnd();
                    text = Decoding(s, "скорпион");
                }
            }
            else MessageBox.Show("Вы не выбрали файл!");
        }

        public static char[] alphavit = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я' };


        public static int alphavitPower = alphavit.Length;

        public static string Decoding(string s, string key)
        {
            string resultString = "";

            int keywordInd = 0;

            int character = 0;
            bool isHave = false;
            foreach (char symbol in s)
            {
                foreach (char letter in alphavit)
                {
                    if (symbol.ToString() == letter.ToString())
                    {
                        character = (Array.IndexOf(alphavit, symbol) + alphavitPower - Array.IndexOf(alphavit, key[keywordInd])) % alphavitPower;
                        isHave = true;
                        break;
                    }
                }
                if (isHave)
                {

                    resultString += alphavit[character];
                    keywordInd++;

                    if (keywordInd == key.Length)
                        keywordInd = 0;
                }
                else
                {
                    resultString += symbol;
                }


                isHave = false;
            }

            return resultString;
        }

        public static string Enkoding(string s, string key)
        {
            string resultString = "";

            int keywordInd = 0;

            int character = 0;
            bool isHave = false;
            foreach (char symbol in s)
            {
                foreach (char letter in alphavit)
                {
                    if (symbol.ToString() == letter.ToString())
                    {
                        character = (Array.IndexOf(alphavit, symbol) + Array.IndexOf(alphavit, key[keywordInd])) % alphavitPower;
                        isHave = true;
                        break;
                    }
                }
                if (isHave)
                {

                    resultString += alphavit[character];
                    keywordInd++;

                    if (keywordInd == key.Length)
                        keywordInd = 0;
                }
                else
                {
                    resultString += symbol;
                }

                isHave = false;
            }

            return resultString;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Текстовый документ (*.txt)|*.txt";
                saveFile.ShowDialog();

                StreamWriter streamWrite = new StreamWriter(saveFile.FileName);
                streamWrite.Write(TextBox.Text);
                streamWrite.Close();
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string key = Key.Text;
            TextBox.Text = Enkoding(TextBox.Text, key);
        }
    }
}
