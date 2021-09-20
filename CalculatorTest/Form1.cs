using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtDisplay.Text);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            previousOperation = Operation.None;
            txtDisplay.Clear();
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

                previousOperation = Operation.Div;
            txtDisplay.Text += (sender as Button).Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

            previousOperation = Operation.Add;
            txtDisplay.Text += (sender as Button).Text;
        }



        private void btnSub_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

            previousOperation = Operation.Sub;
            txtDisplay.Text += (sender as Button).Text;
        }

        private void btnMult_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

            previousOperation = Operation.Mul;
            txtDisplay.Text += (sender as Button).Text;
        }

        private void btnSum_Click(object sender, EventArgs e)
        {
            if (previousOperation == Operation.None)
                return; 
            else
                PerformCalculation(previousOperation);
        }

        private void nNum_Click(object btn, EventArgs e)
        {
            txtDisplay.Text += (btn as Button).Text;
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length > 0)
            {
                double d;
                if(double.TryParse(txtDisplay.Text[txtDisplay.Text.Length - 1].ToString(), out d))
                {
                    previousOperation = Operation.None;
                }
                
                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1, 1);
            }
        }
        private void PerformCalculation(Operation previousOperation)
        {

            

            if (!ValidateInput(txtDisplay.Text, out List<double> firstNum))
                return;
            //List<double> firstNum = new List<double>();
            
            switch (previousOperation)
            {
                case Operation.Add:
                    
                    txtDisplay.Text = (firstNum[0] + firstNum[1]).ToString();

                    break;

                case Operation.Sub:
                    
                    txtDisplay.Text = (firstNum[0] - firstNum[1]).ToString();
                    break;
                case Operation.Mul:
                    
                    txtDisplay.Text = (firstNum[0] * firstNum[1]).ToString();
                    break;
                case Operation.Div:

                    try
                    {
                        
                        txtDisplay.Text = (firstNum[0] / firstNum[1]).ToString();
                    }
                    catch (DivideByZeroException)
                    {
                        txtDisplay.Text = "Forbidden calc. Want to cause an accident?";
                    }
                    break;
                case Operation.Sum:
                    txtDisplay.Text = firstNum.ToString();
                    break;
                case Operation.None:
                    break;
                default:
                    break;
            }
        }

        private char GetOperatorChar(Operation previousOperation)
        {
            switch (previousOperation)
            {
                case Operation.Add:
                    return '+';

                case Operation.Sub:
                    return '-';

                case Operation.Mul:
                    return '*';

                case Operation.Div:
                    return '/';

                case Operation.Sum:
                    return '=';

                default:
                    return '%';

            }
        }

        private bool ValidateInput(string text, out List<double> firstNum)
        {
            firstNum = new List<double>(); // test comment
            foreach (string s in text.Split(GetOperatorChar(previousOperation)))
            {
                if (!double.TryParse(s, out double result))
                    return false;
                firstNum.Add(result);
            }
            if (firstNum.Count != 2)
                return false;
            //else if (!double.TryParse(firstNum[0], out x) || !double.TryParse(firstNum[1], out y)) 
            //    return false;
            return true;
        }

        enum Operation
        {
            Add,
            Sub,
            Mul,
            Div,
            Sum,
            None
        }
        static Operation previousOperation = Operation.None;
    }
}
