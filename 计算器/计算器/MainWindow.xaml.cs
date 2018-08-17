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
        string Number = "",Answer = "";
        List<string> expressions = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_number_Click(object sender, RoutedEventArgs e)
        {
            Numberinput(Convert.ToString((sender as Button).Content));
        }   
        private void Button_equ_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = Result(Number);
            Answer = Result(Number);
            expressions.Add(Number + "=" + Result(Number));
            FileStream resultfile = new FileStream("result.txt", FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(resultfile);
            foreach (string a in expressions)
            {
                streamWriter.WriteLine(a);
            }
            streamWriter.Close();
            Number = "";
        }
        private void MenuItem_save_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("result.txt");
        }
        private void Button_clean_Click(object sender, RoutedEventArgs e)
        {
            Number = "";
            Label1.Content = "0";
        }
        private void Button_delete_Click(object sender, RoutedEventArgs e)
        {
            if(Number.Length > 0)
            {
                Number = Number.Remove(Number.Length - 1);
                Label1.Content = Number;
            }
        }
        private void Button_ans_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = Answer;
            Number = Answer;
        }
        private void Numberinput(string Content)
        {
            if(Number.Length > 0)
            {
                if (Number.Substring(Number.Length - 1, 1) == "." && Content == ".")
                {
                    return;
                }
            }
            Number = Number + Content;
            Label1.Content = Number;
        }
        static char[] Operators = new char[] { '+', '-', '*', '/', '(', ')' };
        static string Result(string args)
        {
            string Number = "";
            float a = EvaluateExpression(args);
            Number = Convert.ToString(a);
            return Number;
        }
        /// <summary>
        /// 初始化运算符优先级
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static char InitPriorities(char a, char b)
        {
            int aIndex = -1;
            int bIndex = -1;
            for (int i = 0; i < Operators.Length; i++)
            {
                if (Operators[i] == a)
                    aIndex = i;
                if (Operators[i] == b)
                    bIndex = i;

            }
            char[,] Priorities = new char[6, 6] {{'>','>','<','<','<','>'},
                                                 {'>','>','<','<','<','>'},
                                                 {'>','>','>','>','<','>'},
                                                 {'>','>','>','>','<','>'},
                                                 {'<','<','<','<','<','='},
                                                 {'?','?','?','?','?','?'}};
            return Priorities[aIndex, bIndex];
        }
        static float Calculate(float Operand1, float Operand2, char Operator)
        {
            float Ret = 0;
            if (Operator == '+')
            {
                Ret = Operand1 + Operand2;
            }
            else if (Operator == '-')
            {
                Ret = Operand1 - Operand2;
            }
            else if (Operator == '*')
            {
                Ret = Operand1 * Operand2;
            }
            else if (Operator == '/')
            {
                Ret = Operand1 / Operand2;
            }
            return Ret;
        }
        static float EvaluateExpression(string str)
        {
            Stack<float> OperandStack = new Stack<float>();
            Stack<char> OperatorStack = new Stack<char>(); 
            float OperandTemp = 0;
            int key = 0,j = 0,k = 0;
            char LastOperator = '0';
            for (int i = 0, size = str.Length; i < size; ++i)
            {
                char ch = str[i];
                if (ch == '.')
                {
                    key = 1;
                }
                else if ('0' <= ch && ch <= '9')
                {   
                    if (key == 0)
                    { OperandTemp = OperandTemp * 10 + ch - '0'; }
                    else if (key == 1)
                    {
                        float s = 1;
                        j++;
                        for (k = j; k > 0; k--)
                        {
                            s = s * 10;
                        }
                        OperandTemp = OperandTemp + (ch - '0') / s; 
                    }
                }
                else if (ch == '+' || ch == '-' || ch == '*' || ch == '/' ||
                    ch == '(' || ch == ')')
                {
                    key = 0;j = 0;
                    if (ch != '(' && LastOperator != ')')
                    {
                        OperandStack.Push(OperandTemp);
                        OperandTemp = 0;
                    }
                    char Opt2 = ch;
                    for (; OperatorStack.Count > 0;)
                    {
                        char Opt1 = OperatorStack.Peek();
                        char CompareRet = InitPriorities(Opt1, Opt2);
                        if (CompareRet == '>')
                        {   
                            float Operand2 = OperandStack.Pop();
                            float Operand1 = OperandStack.Pop();
                            OperatorStack.Pop();
                            float Ret = Calculate(Operand1, Operand2, Opt1);
                            OperandStack.Push(Ret);
                        }
                        else if (CompareRet == '<')
                        {   
                            break;
                        }
                        else if (CompareRet == '=')
                        {
                            OperatorStack.Pop();
                            break;
                        }
                    }   
                    if (Opt2 != ')')
                    {
                        OperatorStack.Push(Opt2);
                    }
                    LastOperator = Opt2;
                }
            } 
            if (LastOperator != ')')
            {
                OperandStack.Push(OperandTemp);
            }
            for (; OperatorStack.Count > 0;)
            {
                float Operand2 = OperandStack.Pop();
                float Operand1 = OperandStack.Pop();
                char Opt = OperatorStack.Pop();
                float Ret = Calculate(Operand1, Operand2, Opt);
                OperandStack.Push(Ret);
            }
            return OperandStack.Peek();
        }
    }
}
