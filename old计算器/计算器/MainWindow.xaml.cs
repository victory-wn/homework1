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


namespace 计算器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string Number1="",Number2="",Flag=null;
        List<string> expressions = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_number_Click(object sender, RoutedEventArgs e)
        {
            Numberinput(Convert.ToString((sender as Button).Content));
        }
        private void Button_flag_Click(object sender, RoutedEventArgs e)
        {
            Flaginput(Convert.ToString((sender as Button).Content));
        }
        private void Button_equ_Click(object sender, RoutedEventArgs e)
        {
            if(Number1 == ""|| Number2 == "")
            {
                Number1 = "";Number2 = "";Flag = null;return;
            }
            switch(Flag)
            {
                case "+": Label1.Content = Convert.ToString(Convert.ToDouble(Number1) + Convert.ToDouble(Number2));
                    expressions.Add(Number1 + "+" + Number2 + "=" + Label1.Content);
                    break;
                case "-": Label1.Content = Convert.ToString(Convert.ToDouble(Number1) - Convert.ToDouble(Number2));
                    expressions.Add(Number1 + "-" + Number2 + "=" + Label1.Content);
                    break;
                case "*": Label1.Content = Convert.ToString(Convert.ToDouble(Number1) * Convert.ToDouble(Number2));
                    expressions.Add(Number1 + "*" + Number2 + "=" + Label1.Content);
                    break;
                case "/": Label1.Content = Convert.ToString(Convert.ToDouble(Number1) / Convert.ToDouble(Number2));
                    expressions.Add(Number1 + "/" + Number2 + "=" + Label1.Content);
                    break;
            }
            FileStream resultfile = new FileStream("result.txt", FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(resultfile);
            foreach (string a in expressions)
            {
                streamWriter.WriteLine(a);
            }
            streamWriter.Close();
            Number1 = ""; Number2 = "";Flag = null;
        }
        private void MenuItem_save_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("result.txt");
        }
        private void Button_clean_Click(object sender, RoutedEventArgs e)
        {
            Number1 = "";Number2 = "";Flag = null;
            Label1.Content = "0";
        }
        private void Button_delete_Click(object sender, RoutedEventArgs e)
        {
            if (Flag == null)
            {
                if(Number1.Length > 0)
                {
                    Number1 = Number1.Remove(Number1.Length - 1);
                    Label1.Content = Number1;
                }

            }
            else
            {
                if (Number2.Length > 0)
                {
                    Number2 = Number2.Remove(Number2.Length - 1);
                    Label1.Content = Number1 + Flag + Number2;
                }
          
            }
        }
        private void Button_opp_Click(object sender, RoutedEventArgs e)
        {
            if (Number1 != "")
            {

                if (Flag == null)
                {
                    Number1 = Convert.ToString(Convert.ToDouble(Number1) * (-1));
                    Label1.Content = Number1;
                }
                else
                {
                    if (Number2 != "")
                    {
                        Number2 = Convert.ToString(Convert.ToDouble(Number2) * (-1));
                        Label1.Content = Number1 + Flag + Number2;
                    }
                }
            }
        }
        private void Button_left_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Button_right_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Numberinput(string Content)
        {
            if (Flag == null)
            {
                if(Number1.Contains(".") && Content == ".")
                {
                    return;
                }
                Number1 = Number1 + Content;
                Label1.Content = Number1;
            }
            else
            {
                if (Number2.Contains(".") && Content == ".")
                {
                    return;
                }
                Number2 = Number2 + Content;
                Label1.Content = Number1 + Flag + Number2;
            }
        }
        private void Flaginput(string Content)
        {
            Flag = Content;
            Label1.Content = Number1 + Flag;
        }
    }
}
