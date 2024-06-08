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




        public Lexer.Lexem Calculate()
        {
            // List<object> rpn = ConvertToRPN(s);
            Stack<object> numberStack = new Stack<object>();
            List<Lexer.Lexem> variableStack = Form1.myVarStorage;

            Stack<Lexer.Lexem> lexemStack = new Stack<Lexer.Lexem>();

            foreach (Lexer.Lexem token in lexems)
            {
                if (token.Type != Lexer.LexemType.END)
                {
                    if (token.Type == Lexer.LexemType.INTEGER)
                    {
                        numberStack.Push((int)token.Value);
                        //lexemStack.Push(token);
                    }

                    if (token.Type == Lexer.LexemType.DECIMAL)
                    {
                        //numberStack.Push((double)token.Value); 
                        lexemStack.Push(token);
                    }

                    if (token.Type == Lexer.LexemType.NAME)
                    {
                        lexemStack.Push(token);
                    }
                    /*
                    if (token.Type == Lexer.LexemType.PROGRAM)
                    {
                        runProgram(token.Value);
                        //numberStack.Push((double)token.Value);
                    }*/

                    if (token.Type == Lexer.LexemType.PLUS || token.Type == Lexer.LexemType.MINUS || token.Type == Lexer.LexemType.MULTIPLY || token.Type == Lexer.LexemType.DIVIDE)
                    {
                        var oper = token.Name;
                        Lexer.Lexem a = lexemStack.Pop();
                        Lexer.Lexem b = lexemStack.Pop();
                        
                        if (a.Type == Lexer.LexemType.INTEGER && b.Type == Lexer.LexemType.INTEGER)
                        {
                            token.Value = CalculateArithmOperatorInt((int)b.Value, (int)a.Value, token.Type);
                            lexemStack.Push(token);
                        }
                        if (a.Value.GetType() == typeof(double) && b.GetType() == typeof(double))
                        {
                            //lexemStack.Push(CalculateArithmOperatorDecimal((double)b, (double)a, token.Type));
                            token.Value = CalculateArithmOperatorDecimal((double)b.Value, (double)a.Value,  token.Type);
                            lexemStack.Push(token);
                        }
                    }
                    
                    if(token.Type == Lexer.LexemType.ASSIGN)
                    {
                        Lexer.Lexem a = lexemStack.Pop();
                        Lexer.Lexem b = lexemStack.Pop();

                        b.Value = a.Value;
                        int ind = Form1.myVarStorage.IndexOf(a);
                        Form1.myVarStorage[ind-1].Value = b;
                        //    //numberStack.Push(CalculateArithmOperatorInt((int)b, (int)a, token.Type));
                        
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
            return lexemStack.Pop();
        }

        private void runProgram(object value)
        {
            /*
            if((int)value == 1)
            {
                object operand = _magazine.top();
                numberStac.pop();

                object name =operand.token.value;

                if (getType(name) == "float")
                {
                    read_tableOfFloats[name];
                }
            if((int)value == 2)
            {

            }*/
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
                if (a == 0)
                {
                    throw new Exception("Нельзя делить на ноль!");
                    return 0;
                }
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
                if(a == 0)
                {
                    throw new Exception("Нельзя делить на ноль!");
                    return 0;
                }
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
