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

namespace Kalkulator
{
    
    public partial class MainWindow : Window
    {
        private Calculator cal = new Calculator();
        public MainWindow()
        {
            InitializeComponent();   
        }

        private void buttonNumber_Click(object sender, RoutedEventArgs e)
        {
            if (cal.ResultClicked)
            {
                cal.ResultClicked = false;
                if (!cal.operatorAtEnd())
                {
                    cal.OperationDisplay = "";
                    textBlockResult.Text = "";
                }
            }
            Button butt = (Button)sender;

            if(cal.isNegative()) cal.OperationDisplay = cal.OperationDisplay.Insert(cal.OperationDisplay.Length - 1, butt.Content.ToString());
            else cal.OperationDisplay += butt.Content;

            textBlockScreen.Text = cal.OperationDisplay; 
        }


        private void buttonOperator_Click(object sender, RoutedEventArgs e)
        {
            if(cal.ResultClicked)
            {
                cal.OperationDisplay = textBlockResult.Text;
                cal.ResultClicked = false;
            }
            Button butt = (Button)sender;
            bool doubleOperators = cal.noDoubleOperators();
            if (cal.OperationDisplay != "") cal.OperationDisplay += butt.Content;
            textBlockScreen.Text = cal.OperationDisplay;
            if(doubleOperators) textBlockResult.Text = cal.getResult();
            if (textBlockResult.Text.Contains("zero")) textBlockResult.FontSize = 18;
            
        }

       
        private void buttonUndo_Click(object sender, RoutedEventArgs e)
        {
            cal.undo();
            textBlockScreen.Text = cal.OperationDisplay;
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            cal.clear();
            textBlockResult.Text = "";
            textBlockScreen.Text = cal.OperationDisplay;
        }

        private void buttonResult_Click(object sender, RoutedEventArgs e)
        {
            cal.getResult();
            textBlockResult.Text = cal.getResult();
            cal.ResultClicked = true;
            if (textBlockResult.Text.Contains("zero")) textBlockResult.FontSize = 18;
        }

        private void buttonDot_Click(object sender, RoutedEventArgs e)
        {
            cal.noWrongDots();
            textBlockScreen.Text = cal.OperationDisplay;
        }

        private void buttonSign_Click(object sender, RoutedEventArgs e)
        {
            cal.signChange();
            textBlockScreen.Text = cal.OperationDisplay;
        }
    }
}
