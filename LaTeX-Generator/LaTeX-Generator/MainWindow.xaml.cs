using Microsoft.Win32;
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
using System.Diagnostics;

namespace LaTeX_Generator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool cb1 = false;
        public MainWindow()
        {
           
            InitializeComponent();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG|*.png|JPG|*.jpg|JPEG|*.jpeg";
            ofd.Multiselect = true;
            ofd.InitialDirectory = @"C:\temp\blabla";
            
            if (ofd.ShowDialog() == true)
            {
                if(ofd.FileNames.Length != 0)
                {
                    generateFile(ofd.FileNames);
                }
            }
            txtCaption.Text = "";
            txtLatexPath.Text = "";
            txtFigure.Text = "";
        }
        private void generateFile(string[] fn)
        {
            string latexpath = txtLatexPath.Text;
            string caption = txtCaption.Text;
            string figure = txtFigure.Text;
            List<string> files = new List<string>();
            int lastindex = fn[0].LastIndexOf('\\');
            string outputpath = fn[0].Substring(0, lastindex);
            string temp;
            string temp2 = "";
            int index = 1;
            string addofn = "\n";

            foreach (var filename in fn)
            {
                temp = filename.Split('\\')[filename.Split('\\').Length - 1];
                files.Add(temp);
            }
            files.Sort();
            foreach(var filename in files)
            {
                temp = "\\begin figure[H]\n" +
                       "    \\centering\n" +
                       "    \\includegraphics[width=0.9\\linewidth]{" + latexpath + "/" + filename + "}\n" +
                       "    \\caption{" + caption + " - " + index + "}\n" +
                       "    \\label{fig:" + figure + "_" + index + "}\n" +
                       "\\end{figure}\n";
                if (chb_NewLine.IsChecked == true)
                {
                    temp = temp + addofn;
                }
                
                index++;
                temp2 += temp;
            }
            File.WriteAllText(outputpath + "\\output.txt", temp2);
            Process.Start(outputpath);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
