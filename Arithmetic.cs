using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoIDEA
{
    public enum Arithmetic
    {
        None,
        Multiplication,
        Division,
        Addition,
        Substraction
    }

    struct element
    {
        public bool IsOperand;
        public bool IsNumber;
        public double Value;
        public Arithmetic operation;
        

        public element(bool isOperand, bool isNumber, double value = 0, Arithmetic op = Arithmetic.None)
        {
            IsOperand = isOperand;
            IsNumber = isNumber;
            Value = value;
            operation = op;
        }

        public override string ToString()
        {
            return $"IsOperand: {IsOperand} | IsNumber: {IsNumber} | Value: {Value} | Operation: {operation.ToString()}";
        }

        public string GetAtom()
        {
            if (IsOperand)
            {
                switch (operation)
                {
                    case Arithmetic.Addition:
                        return "+";
                    case Arithmetic.Substraction:
                        return "-";
                    case Arithmetic.Multiplication:
                        return "*";
                    case Arithmetic.Division:
                        return "/";
                    default:
                        return "ERROR";
                }
            }
            else
            {
                return Value.ToString("0.##");
            }
        }

    }

    public static class Do
    {
        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            return a / b;
        }

        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Substract(double a, double b)
        {
            return a - b;
        }
    }
}
