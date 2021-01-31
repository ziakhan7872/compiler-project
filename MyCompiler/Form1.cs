using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;

namespace MyCompiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string text = "";

        public static int evaluate(string expression)
        {
            char[] tokens = expression.ToCharArray();
            Stack<int> values = new Stack<int>();
            Stack<char> ops = new Stack<char>();
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == ' ')
                {
                    continue;
                }
                if (tokens[i] >= '0' && tokens[i] <= '9')
                {
                    StringBuilder sbuf = new StringBuilder();
                    while (i < tokens.Length && tokens[i] >= '0' && tokens[i] <= '9')
                    {
                        sbuf.Append(tokens[i++]);
                    }
                    values.Push(int.Parse(sbuf.ToString()));
                }
                else if (tokens[i] == '(')
                {
                    ops.Push(tokens[i]);
                }
                else if (tokens[i] == ')')
                {
                    while (ops.Peek() != '(')
                    {
                        values.Push(applyOp(ops.Pop(), values.Pop(), values.Pop()));
                    }
                    ops.Pop();
                }
                else if (tokens[i] == '+' || tokens[i] == '-' || tokens[i] == '*' || tokens[i] == '/')
                {
                    while (ops.Count > 0 && hasPrecedence(tokens[i], ops.Peek()))
                    {
                        values.Push(applyOp(ops.Pop(), values.Pop(), values.Pop()));
                    }
                    ops.Push(tokens[i]);
                }
            }
            while (ops.Count > 0)
            {
                values.Push(applyOp(ops.Pop(), values.Pop(), values.Pop()));
            }
            return values.Pop();
        }

        public static bool hasPrecedence(char op1, char op2)
        {
            if (op2 == '(' || op2 == ')')
            {
                return false;
            }
            if ((op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int applyOp(char op, int b, int a)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    if (b == 0)
                    {
                        throw new System.NotSupportedException("Cannot divide by zero");
                    }
                    return a / b;
            }
            return 0;
        }

        public string Spacing(string exp)
        {
            string newexp = "";
            foreach (char c in exp)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '(' || c == ')')
                {
                    newexp = newexp + " " + c;
                }
                else
                    newexp += c;
            }
            return newexp;
        }

        public void quadRupples(string code)
        {
            string leftside = code.Split('=')[0];
            string rightside = code.Split('=')[1];
            char[] arr = rightside.ToCharArray();

            dataGridView1.Rows.Clear();
            if (arr.Length == 3)
            {
                switch (arr[1])
                {
                    case '+':
                        string left = rightside.Split('+')[0];
                        string right = rightside.Split('+')[1];
                        dataGridView1.Rows.Add('+', left, right, leftside);
                        break;
                    case '-':
                        left = rightside.Split('-')[0];
                        right = rightside.Split('-')[1];
                        dataGridView1.Rows.Add('-', left, right, leftside);
                        break;
                    case '/':
                        left = rightside.Split('/')[0];
                        right = rightside.Split('/')[1];
                        dataGridView1.Rows.Add('/', left, right, leftside);
                        break;
                    case '*':
                        left = rightside.Split('*')[0];
                        right = rightside.Split('*')[1];
                        dataGridView1.Rows.Add('*', left, right, leftside);
                        break;
                    default:
                        break;
                }
            }
            if (arr.Length == 2)
            {
                switch (arr[0])
                {
                    case '-':
                        string right = rightside.Split('-')[1];
                        dataGridView1.Rows.Add('-', right, "", leftside);
                        break;
                    default:
                        break;
                }
            }
            if (arr.Length == 1)
            {
                dataGridView1.Rows.Add('=', rightside, "", leftside);
            }
        }

        public void tripRupples(string code)
        {
            string leftside = code.Split('=')[0];
            string rightside = code.Split('=')[1];

            char[] arr = rightside.ToCharArray();
            int i = 1;
            dataGridView2.Rows.Clear();
            if (arr.Length == 3)
            {
                switch (arr[1])
                {
                    case '+':
                        string left = rightside.Split('+')[0];
                        string right = rightside.Split('+')[1];
                        dataGridView2.Rows.Add(i, '+', left, right);
                        i++;
                        dataGridView2.Rows.Add(i, '=', leftside, --i);
                        break;
                    case '-':
                        left = rightside.Split('-')[0];
                        right = rightside.Split('-')[1];
                        dataGridView2.Rows.Add(i, '-', left, right);
                        i++;
                        dataGridView2.Rows.Add(i, '=', leftside, --i);
                        break;
                    case '/':
                        left = rightside.Split('/')[0];
                        right = rightside.Split('/')[1];
                        dataGridView2.Rows.Add(i, '/', left, right);
                        i++;
                        dataGridView2.Rows.Add(i, '=', leftside, --i);
                        break;
                    case '*':
                        left = rightside.Split('*')[0];
                        right = rightside.Split('*')[1];
                        dataGridView2.Rows.Add(i, '*', left, right);
                        i++;
                        dataGridView2.Rows.Add(i, '=', leftside, --i);
                        break;
                    default:
                        break;
                }
            }
            if (arr.Length == 2)
            {
                switch (arr[0])
                {
                    case '-':
                        string right = arr[1].ToString();
                        dataGridView2.Rows.Add(i, '-', right, "");
                        i++;
                        dataGridView2.Rows.Add(i, '=', leftside, --i);
                        break;
                    default:
                        break;
                }
            }
            if (arr.Length == 1)
            {
                dataGridView2.Rows.Add(i, '=', leftside, rightside);
            }
        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (this.richTextBox1.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.richTextBox1.Select((index + startIndex), word.Length);
                    this.richTextBox1.SelectionColor = color;
                    this.richTextBox1.Select(selectStart, 0);
                    this.richTextBox1.SelectionColor = Color.Black;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            #region  DataType Colors
            this.CheckKeyword("number", Color.Blue, 0);
            this.CheckKeyword("point", Color.Blue, 0);
            this.CheckKeyword("double", Color.Blue, 0);
            this.CheckKeyword("alphabet", Color.Blue, 0);
            this.CheckKeyword("sentence", Color.Blue, 0);
            this.CheckKeyword("correct", Color.Blue, 0);
            this.CheckKeyword("odd", Color.Blue, 0);
            this.CheckKeyword("even", Color.Blue, 0);
            this.CheckKeyword("incorrect", Color.Blue, 0);
            this.CheckKeyword("repeat", Color.Blue, 0);
            this.CheckKeyword("input", Color.Blue, 0);
            this.CheckKeyword("return", Color.Blue, 0);
            this.CheckKeyword("}", Color.Blue, 0);
            this.CheckKeyword("{", Color.Blue, 0);
            this.CheckKeyword("output", Color.Blue, 0);
            this.CheckKeyword("int", Color.Red, 0);
            this.CheckKeyword("float", Color.Red, 0);
            this.CheckKeyword("char", Color.Red, 0);
            this.CheckKeyword("string", Color.Red, 0);
            this.CheckKeyword("for", Color.Red, 0);
            this.CheckKeyword("if", Color.Red, 0);
            this.CheckKeyword("else", Color.Red, 0);
            #endregion

            #region  Variable Regex
            Regex VarDeclr = new Regex(@"^(number|point|alphabet|double|sentence|odd|even)(\s+)[a-zA-Z][a-zA-Z0-9_]*(,[a-zA-Z][a-zA-Z0-9_]*)*;$");
          //  Regex VarInit = new Regex(@"^([a-zA-Z][a-zA-Z0-9_]*)=[0-9]*.[0-9];$");
            Regex VarInit = new Regex(@"^([a-zA-Z][a-zA-Z0-9_]*)=([0-9]*|[a-zA-Z][a-zA-Z0-9_]*)((\+|\-|\/|\*)([0-9]*|[a-zA-Z][a-zA-Z0-9_]*))*;$");
            Regex VariDeclrAndInit = new Regex(@"^(number|point|alphabet|double)(\s+)[a-zA-Z][a-zA-Z0-9_]*=([0-9]*|[a-zA-Z][a-zA-Z0-9_]*)((\+|\-|\/|\*)([0-9]*|[a-zA-Z][a-zA-Z0-9_]*))*;$");
            #endregion

            #region If-Else Regex
            Regex ifRgx = new Regex(@"^correct\<([a-zA-Z][a-zA-Z0-9]*|[0-9]*)(\<|\>|\=\=|\!\=|\<\=|\>=)([a-zA-Z][a-zA-Z0-9]*|[0-9]*)((\&\&|\|\|)([a-zA-Z][a-zA-Z0-9]*|[0-9]*)(\<|\>|\=\=|\!\=|\<\=|\>=)([a-zA-Z][a-zA-Z0-9]*|[0-9]*))*\>$");
            Regex strtBrkt = new Regex(@"^\{$");
            Regex body = new Regex(@"^([a-zA-Z][a-zA-Z0-9]*)$");
            Regex endBrkt = new Regex(@"^\}$");
            Regex elseRgx = new Regex(@"^incorrect$");
            #endregion

            #region For-Loop Regex
            Regex forloopRgx = new Regex(@"^repeat\<(((number|point|double)(\s+)[a-zA-Z][a-zA-Z0-9]*)|((number|point|double)(\s+)[a-zA-Z][a-zA-Z0-9]*=[0-9]*|[a-zA-Z0-9]*));([a-zA-Z][a-zA-Z0-9]*)(\<|\>|\<\=|\>=|\=\=)(\d+|[a-zA-Z][a-zA-Z0-9]*);([a-zA-Z][a-zA-Z0-9]*(\+\+|\-\-|\+\d+|\-\d+))\>$");
            #endregion

            #region Input Output Regex
            Regex cinRgx = new Regex(@"^input\<\<[a-zA-Z][a-zA-Z]*;$");
            Regex coutRgx = new Regex(@"^output\>\>(""[a-zA-Z0-9]*""|[a-zA-Z][a-zA-Z0-9]*)(\>\>(""[a-zA-Z0-9]*""|[a-zA-Z][a-zA-Z0-9]*))*;$");
            #endregion

            List<string> variableslist = new List<string>();
            Hashtable ht = new Hashtable();
            int i = 1;
            listBox1.Items.Clear();

            foreach (string linecode in richTextBox1.Text.Split('\n'))
            {

                #region   Variable Declaration
                if ((VarDeclr.IsMatch(linecode.Trim())))
                {
                    if (linecode.Length > 8 || linecode.Length > 9 || linecode.Length > 11)
                    {
                        string varlist = linecode.Split(';')[0].Split(' ')[1].ToString();
                        foreach (string var in varlist.Split(','))
                        {
                            bool flag = false;
                            foreach (string asgnvar in variableslist)
                            {
                                if (var == asgnvar)
                                {
                                    flag = true;
                                    label1.Text = "Variable Already Exist";
                                    label1.ForeColor = Color.Red;
                                }
                            }

                            if (flag == false)
                            {
                                listBox1.Items.Add(var);
                                variableslist.Add(var);
                                label1.Text = "Syntax is Correct";
                                label1.ForeColor = Color.Green;
                            }
                        }
                    }
                    else
                    {
                        string Assignd_variable = linecode.Split(';')[0].Split(' ')[1].ToString();
                        bool flag = false;
                        foreach (string asgnvar in variableslist)
                        {
                            if (Assignd_variable == asgnvar)
                            {
                                flag = true;
                                label1.Text = "Variable Already Exist";
                                label1.ForeColor = Color.Red;
                            }
                        }
                        if (flag == false)
                        {
                            listBox1.Items.Add(Assignd_variable);
                            variableslist.Add(Assignd_variable);
                            label1.Text = "Syntax is Correct";
                            label1.ForeColor = Color.Green;
                        }
                    }
                }
                #endregion

                #region  Variable Initialization
                else
                if ((VarInit.IsMatch(linecode.Trim())))
                {
                    string var = linecode.Split(';')[0].Split('=')[0].ToString();
                    bool flag = false;
                    foreach (string asgnvar in variableslist)
                    {

                        if (var == asgnvar)
                        {
                            flag = true;
                            string value = linecode.Split(';')[0].Split('=')[1].ToString();
                            string variable = linecode.Split(';')[0].Split('=')[0].ToString();

                            char[] arrval = value.ToCharArray();
                            if (arrval[0] != '-')
                            {

                                //EvaluateStrings es = new EvaluateStrings();
                                string exp = linecode.Split(';')[0].Split('=')[1];
                                string newexp = Spacing(exp);
                                string result = evaluate(newexp).ToString();
                                //listBox1.Items.Add(variable + " : " + result);

                                ICollection keys = ht.Keys;
                                foreach (string k in keys)
                                {
                                    if (k == variable)
                                    {
                                        ht.Remove(k);
                                        break;
                                    }
                                }
                           
                                int r = int.Parse(result);
                                if (r%2==0)
                                {
                                    label1.Text = "even value not supported";
                                }

                                else
                                {
                                    ht.Add(variable, result);
                                    listBox1.Items.Add(variable + " : " + result);
                                    //if (linecode != "")
                                    //{
                                    //    string code = linecode.Split(';')[0];
                                    //    quadRupples(code);
                                    //    tripRupples(code);
                                    //}
                                    label1.Text = "Syntax is Correct";
                                    label1.ForeColor = Color.Green;
                                }
                            }
                            else
                            {
                                ICollection keys = ht.Keys;
                                foreach (string k in keys)
                                {
                                    if (k == variable)
                                    {
                                        ht.Remove(k);
                                        break;
                                    }
                                }
                                ht.Add(variable, value);
                                listBox1.Items.Add(variable + " : " + value);
                                label1.Text = "Syntax is Correct";
                                label1.ForeColor = Color.Green;
                                if (linecode != "")
                                {
                                    string code = linecode.Split(';')[0];
                                    quadRupples(code);
                                    tripRupples(code);
                                }
                            }
                        }
                    }
                    if (flag == false)
                    {
                        label1.Text = "Variable is not Declared";
                        label1.ForeColor = Color.Red;
                    }
                }
                #endregion

                #region  Variable Declaration & Initialization
                else
                if (VariDeclrAndInit.IsMatch(linecode.Trim()))
                {
                    string value = linecode.Split(';')[0].Split('=')[1].ToString();
                    string variable = linecode.Split(';')[0].Split('=')[0].Split(' ')[1].ToString();
                    bool flag = false;
                    foreach (string asgnvar in variableslist)
                    {
                        if (variable == asgnvar)
                        {
                            flag = true;
                            label1.Text = "Variable Already Exist";
                            label1.ForeColor = Color.Red;
                        }
                    }

                    if (flag == false)
                    {
                        listBox1.Items.Add(variable);
                        variableslist.Add(variable);
                        label1.Text = "Syntax is Correct";
                        label1.ForeColor = Color.Green;
                    }
                    ICollection keys = ht.Keys;
                    bool flag1 = true;
                    foreach (string k in keys)
                    {
                        if (k == variable)
                        {
                            flag1 = false;
                            label1.Text = "Variable Already Assigned";
                            label1.ForeColor = Color.Red;
                        }
                    }
                    if (flag1 == true)
                    {
                        char[] arrval = value.ToCharArray();
                        if (arrval[0] != '-')
                        {
                            //EvaluateStrings es = new EvaluateStrings();
                            string exp = linecode.Split(';')[0].Split('=')[1];
                            string newexp = Spacing(exp);
                            string result = evaluate(newexp).ToString();
                            //listBox1.Items.Add(variable + " : " + result);
                            ht.Add(variable, result);
                            listBox1.Items.Add(variable + " : " + result);
                            if (linecode != "")
                            {
                                string code = linecode.Split(';')[0].Split(' ')[1];
                                quadRupples(code);
                                tripRupples(code);
                            }
                        }
                        else
                        {
                            ht.Add(variable, value);
                            listBox1.Items.Add(variable + " : " + value);
                        }
                        label1.Text = "Syntax is Correct";
                        label1.ForeColor = Color.Green;
                        if (linecode != "")
                        {
                            string code = linecode.Split(';')[0].Split(' ')[1];
                            quadRupples(code);
                            tripRupples(code);
                        }
                    }
                }
                #endregion

                #region If-else
                else
                if (ifRgx.IsMatch(linecode.Trim()))
                {
                    List<string> varlist = new List<string>();
                    string var1 = linecode.Split('t')[1].Split('\n')[0].ToString();
                    string sbstr = var1.Substring(1, var1.Length - 2);
                    char[] sbstrarr = sbstr.ToCharArray();
                    string newVar = "";
                    for (int j = 0; j < sbstrarr.Length; j++)
                    {
                        if (sbstrarr[j] == '>' || sbstrarr[j] == '<' || sbstrarr[j] == '=' || sbstrarr[j] == '|' || sbstrarr[j] == '&')
                        {
                            if (sbstrarr[j + 1] == '=' || sbstrarr[j + 1] == '|' || sbstrarr[j + 1] == '&')
                            {
                                j++;
                            }
                            varlist.Add(newVar);
                            newVar = "";
                        }
                        else
                        {
                            newVar += sbstrarr[j];
                        }
                    }
                    varlist.Add(newVar);

                    foreach (string var in varlist)
                    {
                        listBox1.Items.Add(var);
                    }
                    label1.Text = "Syntax is Correct";
                    label1.ForeColor = Color.Green;
                }
                else
                if (strtBrkt.IsMatch(linecode.Trim()))
                {
                    label1.Text = "Syntax is Correct";
                    label1.ForeColor = Color.Green;
                }
                //else
                //    if (body.IsMatch(linecode.Trim()))
                //    {
                //        label1.Text = "Syntax is Correct";
                //        label1.ForeColor = Color.Green;
                //    }
                else
                if (endBrkt.IsMatch(linecode.Trim()))
                {
                    label1.Text = "Syntax is Correct";
                    label1.ForeColor = Color.Green;
                }
                else
                if (elseRgx.IsMatch(linecode.Trim()))
                {
                    label1.Text = "Syntax is Correct";
                    label1.ForeColor = Color.Green;
                }
                #endregion

                #region For-Loop
                else
                if (forloopRgx.IsMatch(linecode.Trim()))
                {
                    List<string> varlist = new List<string>();
                    string var1 = linecode.Split('t')[1].Split('\n')[0].ToString();
                    string sbstr = var1.Substring(1, var1.Length - 2);
                    string newsbstring = sbstr.Split(' ')[1];
                    char[] sbstrarr = newsbstring.ToCharArray();
                    string newVar = "";
                    for (int j = 0; j < sbstrarr.Length; j++)
                    {
                        if (sbstrarr[j] == '>' || sbstrarr[j] == '<' || sbstrarr[j] == '=' || sbstrarr[j] == '+' || sbstrarr[j] == ';' || sbstrarr[j] == '-')
                        {
                            if (sbstrarr[j + 1] == '=' || sbstrarr[j + 1] == '|' || sbstrarr[j + 1] == '+' || sbstrarr[j + 1] == '-')
                            {
                                j++;
                            }
                            varlist.Add(newVar);
                            newVar = "";
                        }
                        else
                        {
                            newVar += sbstrarr[j];
                        }
                    }
                    varlist.Add(newVar);

                    foreach (string var in varlist)
                    {
                        listBox1.Items.Add(var);
                    }

                    label1.Text = "Syntax is Correct";
                    label1.ForeColor = Color.Green;
                }
                #endregion

                #region Input
                else
                if (cinRgx.IsMatch(linecode.Trim()))
                {
                    string inpuut = linecode.Split(';')[0].Split('p')[1].Split('t')[1];
                    char[] strarrr = inpuut.ToCharArray();
                    string newstr = "";
                    for (int j = 2; j < strarrr.Length; j++)
                    {
                        newstr += strarrr[j];
                    }
                    listBox1.Items.Add(newstr);
                    label1.Text = "Syntax is Correct";
                    label1.ForeColor = Color.Green;
                }
                #endregion

                #region Output
                else
                if (coutRgx.IsMatch(linecode.Trim()))
                {
                    string output = linecode.Split(';')[0].Split('p')[1].Split('t')[1];
                    char[] strarrr = output.ToCharArray();
                    string newstr = "";
                    button1.BackColor = Color.Green;
                    for (int j = 2; j < strarrr.Length; j++)
                    {
                        newstr += strarrr[j];
                    }
                    listBox1.Items.Add(newstr);
                    char[] newarr = newstr.ToCharArray();
                    string outString = "";
                    if (newarr[0] == '"')
                    {
                        for (int k = 1; k < newarr.Length - 1; k++)
                        {
                            outString += newarr[k];
                        }
                        text = outString;
                        label1.Text = "Syntax is Correct";
                        label1.ForeColor = Color.Green;
                        button1.BackColor = Color.Green;
                    }
                    else
                    {
                        ICollection keys = ht.Keys;
                        if (keys.Count != 0)
                        {
                            foreach (string k in keys)
                            {
                                if (k == newstr)
                                {
                                    text = ht[k].ToString();
                                }
                                else
                                {
                                    listBox1.Items.Add(newstr + " Variable not Exist");
                                    label1.Text = "Syntax is Correct But Variable Not Declared";
                                    label1.ForeColor = Color.Red;
                                    button1.BackColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            listBox1.Items.Add(newstr + " Declared But Not Initialized");
                            label1.Text = "Syntax is Correct But Variable Not Initialized";
                            label1.ForeColor = Color.Red;
                            button1.BackColor = Color.Red;
                        }
                    }

                }
                #endregion
                else
                {
                    if (linecode != "")
                    {
                        label1.Text = "Syntax Error At Line " + i;
                        label1.ForeColor = Color.Red;
                        button1.BackColor = Color.Red;
                    }
                }
                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            text = "";
        }
    }
}
