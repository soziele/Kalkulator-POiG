using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Kalkulator
{
    public class Calculator
    {
        #region prop

        private List<char> operators = new List<char>() { '+', '-', '*', '/' };
        public string OperationDisplay { get; set; } = "";
        public bool ResultClicked { get; set; } = false;
        #endregion

        #region methods
        public string getResult()
        {
            string resultDisplay = OperationDisplay;
            if (resultDisplay.LastIndexOfAny(operators.ToArray()) == resultDisplay.Length - 1)
            {
                resultDisplay = resultDisplay.Remove(resultDisplay.Length - 1);
            }
            bool negativeFirst = false;
            if (resultDisplay.IndexOf('-') == 0)
            {
                resultDisplay = resultDisplay.Trim('-');
                negativeFirst = true;
            }
                //utworzenie tablicy wszystkich liczb w wyrażeniu
                string[] nums = resultDisplay.Split(operators.ToArray());
                double[] numbers = new double[nums.Length];

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberGroupSeparator = ".";
         
                for (int i = 0; i < nums.Length; i++)
                {
                    numbers[i] = Convert.ToDouble(nums[i],provider);
                }
            if (negativeFirst) numbers[0] = -numbers[0];
                double result = numbers[0];

                //utworzenie tablicy wszystkich operatorów w wyrażeniu
                char[] tmp = resultDisplay.ToCharArray();
                char[] currentOperators = new char[numbers.Length - 1];
                int n = 0;
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (operators.Contains(tmp[i]))
                    {
                        currentOperators[n] = tmp[i];
                        n++;
                    }
                }

                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    if (currentOperators[i] == operators[0])
                    {
                        result += numbers[i + 1];
                    }
                    else if (currentOperators[i] == operators[1])
                    {
                        result -= numbers[i + 1];
                    }
                    else if (currentOperators[i] == operators[2])
                    {
                        result *= numbers[i + 1];
                    }
                    else if (currentOperators[i] == operators[3])
                    {
                        if (numbers[i + 1] != 0) result /= numbers[i + 1];
                        else return "Próba podzielenia przez zero!";
                    }

            }
            string resultString = Convert.ToString(result);
            resultString = resultString.Replace(',', '.');
            return resultString;
        }

        public bool noDoubleOperators()
        {
                if (OperationDisplay.LastIndexOfAny(operators.ToArray()) == OperationDisplay.Length - 1 )
                {
                    OperationDisplay = OperationDisplay.Trim(operators.ToArray());
                }

            bool anyPreviousOperations = false;
            for (int i = 0; i < 4; i++)
            {
                if (OperationDisplay.Contains(operators[i])) anyPreviousOperations = true;
            }
            return anyPreviousOperations;
        }

        public void noWrongDots()
        {
            if (((OperationDisplay.LastIndexOf('.') < OperationDisplay.LastIndexOfAny(operators.ToArray())) || (!OperationDisplay.Contains('.')))&&OperationDisplay!="")
            {
                if(OperationDisplay.LastIndexOfAny(operators.ToArray())!=OperationDisplay.Length-1)
                OperationDisplay += '.';
            }
            
        }

        public void undo()
        {
            if (OperationDisplay != "")
            {
                OperationDisplay = OperationDisplay.Remove(OperationDisplay.Length - 1);
            }
        }

        public void clear()
        {
            int n = OperationDisplay.Length;
            for (int i = 0; i < n; i++)
            {
                undo();
            }
        }

        public bool operatorAtEnd()
        {
            bool oAE = false;
            for (int i = 0; i < 4; i++)
            {
                if (OperationDisplay.EndsWith(operators[i].ToString())) oAE = true;
            }
            return oAE;
        }
        
        #endregion

    }
}
