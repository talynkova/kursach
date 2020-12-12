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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using static System.Net.WebRequest;
using System.Net;

namespace ApplicationCipher
{
    /// <summary>
    /// Логика взаимодействия для UploadWindow.xaml
    /// </summary>
    public partial class UploadWindow : Window
    {

        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\test.txt";
        private string fileName;
        private string fileContent;
        private string link;
        public string result;

        public UploadWindow()
        {
            InitializeComponent();
        }
        public void Upload_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                fileContent = null;
                OpenFileDialog OPF = new OpenFileDialog();
                OPF.ShowDialog();              
                OPF.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                fileName = OPF.FileName;
                Encoding win1251 = Encoding.GetEncoding("windows-1251");
                fileContent = File.ReadAllText(fileName, win1251);
              
                if (String.IsNullOrWhiteSpace(fileContent)) MessageBox.Show("This file is empty");
                else
                {
                    KeyForm form3 = new KeyForm();
                    form3.ShowDialog();
                    result = MainWindow.Decrypt(fileContent, MainWindow.key);
                    Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void Download_Click(object sender, RoutedEventArgs args)
        {
           
            fileContent = null;
            LinkForm linkForm = new LinkForm();
            linkForm.ShowDialog();
            link = linkForm.link;
            try
            {
               if (File.Exists(path)) File.Delete(path);
                WebClient wc = new WebClient();
                wc.DownloadFileAsync(new Uri(link), path);
                if (File.Exists(path) && new FileInfo(path).Length != 0)
                {
                    Encoding win1251 = Encoding.GetEncoding("windows-1251");
                    fileContent = File.ReadAllText(path, win1251);

                    KeyForm form4 = new KeyForm();
                    form4.ShowDialog();
                    result = MainWindow.Decrypt(fileContent, MainWindow.key);
                    Close();
                }

                if (String.IsNullOrWhiteSpace(fileContent)) MessageBox.Show("This file is empty");
              
            }
            catch (WebException)
            {
                MessageBox.Show("Please, check your connection");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message);
                Close();
            }

        }
    }
}
