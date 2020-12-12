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
using System.IO;
using System.Windows.Forms;
namespace ApplicationCipher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string key;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(Input.Text))
            { 
                Output.Text = "";
                KeyForm form1 = new KeyForm();
                form1.ShowDialog();
                Output.Text = Encrypt(Input.Text.ToUpper(), key);
            }
            else System.Windows.MessageBox.Show("Input is empty");
        }
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(Input.Text))
            {
                Output.Text = "";
                KeyForm form2 = new KeyForm();
                form2.ShowDialog();
                Output.Text = Decrypt(Input.Text.ToUpper(), key);
            }
            else System.Windows.MessageBox.Show("Input is empty");
        }
        private void Clear1_Click(object sender, RoutedEventArgs e)
        {
            Input.Clear();

        }
        private void Clear2_Click(object sender, RoutedEventArgs e)
        {
            Output.Clear();

        }
        private void Upload_Click(object sender, RoutedEventArgs e)
        {
         
            UploadWindow uploadWindow = new UploadWindow();
            uploadWindow.ShowDialog();
            Output.Text = uploadWindow.result;
           
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";

                if (saveFileDialog.ShowDialog() ==System.Windows.Forms.DialogResult.OK)
                {
                    StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                    streamWriter.WriteLine(Output.Text);
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error" + ex);
            }
        }

         public static string GetRepeatKey(string s1, string text)
          {
            string s = s1.ToLower(); 
            string letters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщьыъэюя";
              string str = "";
              int count = 0;
              for (int i = 0; i < text.Length; i++)
              {
                  if (letters.Contains(text[i]))
                  {
                      str += s[count];
                      count++;
                  }
                  else str += text[i];
                  if (count == s.Length) count = 0;
              }
           
              return str;
          }
   
        public static string Decrypt(string text1, string password)
        {
            var text = text1.ToUpper();
            string letters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            var gamma = (GetRepeatKey(password, text)).ToUpper();
            var retValue = "";
            var q = letters.Length;

            for (int i = 0; i < text.Length; i++)
            {
                var letterIndex = letters.IndexOf(text[i]);
                var codeIndex = letters.IndexOf(gamma[i]);
                if (!letters.Contains(text[i]))
                {
                    retValue += text[i].ToString();
                }
                else
                {
                   
                    retValue += letters[((q + letterIndex + (-1) * codeIndex)) % q].ToString();
                }
            }

            return retValue;
        }
       public static string Encrypt(string text1, string password)
        {
            var text = text1.ToUpper();
            string letters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            var gamma = (GetRepeatKey(password, text)).ToUpper();
            var retValue = "";
            var q = letters.Length;

            for (int i = 0; i < text.Length; i++)
            {
                var letterIndex = letters.IndexOf(text[i]);
                var codeIndex = letters.IndexOf(gamma[i]);
                if (!letters.Contains(text[i]))
                {
                    retValue += text[i].ToString();
                }
                else
                {
                    retValue += letters[((q + letterIndex + (1) * codeIndex)) % q].ToString();
                }
            }
            return retValue.ToLower();
        }



    } 
    }

