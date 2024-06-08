using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexAnalyzerForms
{
    public class OPS_exe
    {
        public List<Lexer.Lexem> lexems;
        private string programOutput;

        public OPS_exe(List<Lexer.Lexem> lexems)
        {
            this.lexems = lexems;
        }
        // "int a 5 :="
        public void DO()
        {
            foreach (Lexer.Lexem lex in lexems)
            {
                //if(lex.Type == Lexer.LexemType.INT_DECLARE)
                //{

                //}
                //else if (lex.Type == Lexer.LexemType.ASSIGN)
                //{
                //    Form1.myVarStorage.Add(lexems[]);
                //}
            }
        }




        public object Calculate()
        {
            // List<object> rpn = ConvertToRPN(s);
            Stack<object> numberStack = new Stack<object>();
            foreach (Lexer.Lexem token in lexems)
            {
                if (token.Type != Lexer.LexemType.END)
                {
                    if (token.Type == Lexer.LexemType.INTEGER)
                    {
                        numberStack.Push((int)token.Value);
                    }

                    if (token.Type == Lexer.LexemType.DECIMAL)
                    {
                        numberStack.Push((double)token.Value);
                    }

                    if (token.Type == Lexer.LexemType.PLUS || token.Type == Lexer.LexemType.MINUS || token.Type == Lexer.LexemType.MULTIPLY || token.Type == Lexer.LexemType.DIVIDE)
                    {
                        var oper = token.Name;
                        var a = numberStack.Pop();
                        var b = numberStack.Pop();
                        if (a.GetType() == typeof(int) && b.GetType() == typeof(int))
                        {
                            numberStack.Push(CalculateArithmOperatorInt((int)b, (int)a, token.Type));
                        }
                        if (a.GetType() == typeof(double) && b.GetType() == typeof(double))
                        {
                            numberStack.Push(CalculateArithmOperatorDecimal((double)b, (double)a, token.Type));
                        }
                    }
                    /*
                    if (token is IOneArgFunction)
                    {
                        var function = (IOneArgFunction)token;
                        var a = numberStack.Pop();
                        numberStack.Push(function.isNegative ? function.CalculateFunction(a) * -1 : function.CalculateFunction(a));
                    }*/
                }
            }
            return numberStack.Pop();
        }

        public int CalculateArithmOperatorInt(int b, int a, Lexer.LexemType type)
        {
            if(type == Lexer.LexemType.PLUS)
            {
                return b + a;
            }
            if (type == Lexer.LexemType.MINUS)
            {
                return b - a;
            }
            if (type == Lexer.LexemType.MULTIPLY)
            {
                return b * a;
            }
            if (type == Lexer.LexemType.DIVIDE)
            {
                return b / a;
            }
            return 0;
        }

        public double CalculateArithmOperatorDecimal(double b, double a, Lexer.LexemType type)
        {
            if (type == Lexer.LexemType.PLUS)
            {
                return b + a;
            }
            if (type == Lexer.LexemType.MINUS)
            {
                return b - a;
            }
            if (type == Lexer.LexemType.MULTIPLY)
            {
                return b * a;
            }
            if (type == Lexer.LexemType.DIVIDE)
            {
                return b / a;
            }
            return 0;
        }

        public string getProgramOutput()
        {
            return programOutput;
        }

    }
}
