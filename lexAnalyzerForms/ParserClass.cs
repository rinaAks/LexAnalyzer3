using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static lexAnalyzerForms.Lexer;
using static lexAnalyzerForms.ParserClass.StateParser;
using static lexAnalyzerForms.OPSTok;
using System.Reflection.Emit;

namespace lexAnalyzerForms
{
    public class ParserClass
    {
        public class StateParser
        {
            public StateLetter state;
            public LexemType type;
            public StateParser()
            {
                this.state = StateLetter.F;
                this.type = LexemType.EMPTY;
            }


            public enum StateLetter
            {
                None,
                F,
                I,
                M,
                A,
                N,
                Q,
                S,
                U,
                T,
                V,
                L,
                H,
                C,
                Y,
                K,
                D,
                O,
                J,
                B,
                W,
                END
            };

        };


        public class Parser
        {

            public OPSTok oPSTok;
            public List<StateParser> magasin;
            public List<Action> generator;
            StateParser parsForAdding;
            public Parser()
            {
                magasin = new List<StateParser>();
                generator = new List<Action>();
                oPSTok = new OPSTok();
                //magasin.Add(new StateParser());
                //magasin[0].state = StateLetter.F;
            }


            int i = 0;
            int j = 0;
            public Lexem Lex()
            {
                return Form1.myStorage[i];
            }
            Lexem lex;
            public void FillMagazine()
            {
                //-while (lex.Type != LexemType.END && magasin[i].state != StateLetter.END)
                lex = Lex(); // берём нулевую лексему из myStorage

                magasin.Add(new StateParser());
                magasin[0].state = StateLetter.F;

                generator.Add(empty);
                //generator[0] = new Action(empty);

                while (lex.Type != LexemType.END)
                {
                    StateParser stateMagas = new StateParser();
                    stateMagas = magasin[0];
                    magasin.RemoveAt(0);
                    generator[0]();
                    generator.RemoveAt(0);
                    //надо найти пару
                    if (stateMagas.type != LexemType.END)
                    {
                        //вызываем функцию pair
                        //в pair передавать нулевое состояние магазина 
                        //и тип лексемы, чтобы понимать, какая комбинация - пара
                        Pair(stateMagas.state, lex.Type);
                    }



                    if (i > Form1.myStorage.Count - 1)
                        break;


                    if (stateMagas.type == lex.Type)
                    {
                        i++;
                        lex = Lex();

                        //magasin.RemoveAt(0);

                    }
                    // берём следующую лексему из myStorage
                }
            }


            public void name()
            {

                oPSTok.ops.Add(lex);
                //Console.WriteLine(lex);
            }
            public void empty()
            {
                return;
            }

            public void Pair(StateLetter stateLex, LexemType type)
            {
                List<StateParser> stateParser = new List<StateParser>();
                List<Action> fucts = new List<Action>();

                if (stateLex == StateLetter.F && type == LexemType.NAME)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.NAME;
                    stateParser[1].state = StateLetter.H;
                    stateParser[2].type = LexemType.ASSIGN;
                    stateParser[3].state = StateLetter.S;
                    stateParser[4].type = LexemType.SEMICOLON;
                    stateParser[5].state = StateLetter.Q;
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                    fucts.RemoveAt(5);
                    fucts.Insert(5, name);
                }
                if (stateLex == StateLetter.F && type == LexemType.INT_DECLARE)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.INT_DECLARE;
                    stateParser[1].state = StateLetter.I;
                    stateParser[2].state = StateLetter.F;

                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                }
                if (stateLex == StateLetter.F && type == LexemType.DECIMAL_DECLARE)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.DECIMAL_DECLARE;
                    stateParser[1].state = StateLetter.I;
                    stateParser[2].state = StateLetter.F;
                    
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                }
                if (stateLex == StateLetter.F && type == LexemType.ARRAY_DECLARE)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.ARRAY_DECLARE;
                    stateParser[1].state = StateLetter.A;
                    stateParser[2].state = StateLetter.F;
                    
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                }
                if (stateLex == StateLetter.F && type == LexemType.INPUT)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.INPUT;
                    stateParser[1].type = LexemType.LPAREN;
                    stateParser[2].type = LexemType.NAME;
                    stateParser[3].state = StateLetter.H;
                    stateParser[4].type = LexemType.RPAREN;
                    stateParser[5].type = LexemType.SEMICOLON;
                    stateParser[6].state = StateLetter.Q;
                    
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                    


                }
                ///////    I    //////////
                if (stateLex == StateLetter.I && type == LexemType.NAME)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.NAME;
                    stateParser[1].state = StateLetter.M;
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);

                }

                ///////    M    //////////
                if (stateLex == StateLetter.M && type == LexemType.COMMA)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    };
                    stateParser[0].type = LexemType.COMMA;
                    stateParser[1].type = LexemType.NAME;
                    stateParser[2].state = StateLetter.M;
                    fucts.RemoveAt(1);
                    fucts.Insert(1, name);
                }
                if (stateLex == StateLetter.M && type == LexemType.SEMICOLON)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.SEMICOLON;
                }

                ///////////        S        /////////////////////
                if (stateLex == StateLetter.S && type == LexemType.NAME)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.NAME;
                    stateParser[1].state = StateLetter.H;
                    stateParser[2].state = StateLetter.V;
                    stateParser[3].state = StateLetter.U;
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                }

                if (stateLex == StateLetter.S && type == LexemType.DECIMAL)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.DECIMAL;
                    stateParser[1].state = StateLetter.V;
                    stateParser[2].state = StateLetter.U;
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                }
                if (stateLex == StateLetter.S && type == LexemType.INTEGER)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.INTEGER;
                    stateParser[1].state = StateLetter.V;
                    stateParser[2].state = StateLetter.U;
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                }
                if (stateLex == StateLetter.S && type == LexemType.LPAREN)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.LPAREN;
                    stateParser[1].state = StateLetter.S;
                    stateParser[2].type = LexemType.RPAREN;
                    stateParser[3].state = StateLetter.V;
                    stateParser[4].state = StateLetter.U;
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);

                }

                ////  H /////////

                if (stateLex == StateLetter.H && type == LexemType.LSQUARE)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.LSQUARE;
                    stateParser[1].state = StateLetter.S;
                    stateParser[2].type = LexemType.RSQUARE;
                    fucts.RemoveAt(1);
                    fucts.Insert(1, name);
                }

                ///////// Q /////////


                if (stateLex == StateLetter.Q && type == LexemType.NAME)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.NAME;
                    stateParser[1].state = StateLetter.H;
                    stateParser[2].type = LexemType.ASSIGN;
                    stateParser[3].state = StateLetter.S;
                    stateParser[4].type = LexemType.SEMICOLON;
                    stateParser[5].state = StateLetter.Q;
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                    fucts.RemoveAt(5);
                    fucts.Insert(5, name);
                }
                if (stateLex == StateLetter.Q && type == LexemType.INPUT)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.INPUT;
                    stateParser[1].type = LexemType.LPAREN;
                    stateParser[2].type = LexemType.NAME;
                    stateParser[3].state = StateLetter.H;
                    stateParser[4].type = LexemType.RPAREN;
                    stateParser[5].type = LexemType.SEMICOLON;
                    stateParser[6].state = StateLetter.Q;
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);

                }
                if (stateLex == StateLetter.Q && type == LexemType.OUTPUT)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.OUTPUT;
                    stateParser[1].type = LexemType.LPAREN;
                    stateParser[2].state = StateLetter.S;
                    stateParser[3].type = LexemType.RPAREN;
                    stateParser[4].type = LexemType.SEMICOLON;
                    stateParser[5].state = StateLetter.Q;
                    fucts.RemoveAt(0);
                    fucts.Insert(0, name);
                }
                if (stateLex == StateLetter.Q && type == LexemType.IF)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.IF;
                    stateParser[1].type = LexemType.LPAREN;
                    stateParser[2].state = StateLetter.C;
                    stateParser[3].type = LexemType.RPAREN;
                    stateParser[4].type = LexemType.LBRACE;
                    stateParser[5].state = StateLetter.Q;
                    stateParser[6].type = LexemType.RBRACE;
                    stateParser[7].state = StateLetter.D;
                    stateParser[8].state = StateLetter.Q;
                }

                if (stateLex == StateLetter.Q && type == LexemType.WHILE)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.WHILE;
                    stateParser[1].type = LexemType.LPAREN;
                    stateParser[2].state = StateLetter.C;
                    stateParser[3].type = LexemType.RPAREN;
                    stateParser[4].type = LexemType.LBRACE;
                    stateParser[5].state = StateLetter.Q;
                    stateParser[6].type = LexemType.RBRACE;
                    stateParser[7].state = StateLetter.Q;
                }


                for (int i = 0; i < stateParser.Count; i++)
                {
                    magasin.Insert(0, stateParser[stateParser.Count - 1 - i]);
                    generator.Insert(0, fucts[fucts.Count - 1 - i]);
                }


                // magasin.AddRange(stateParser);
                // generator.AddRange(fucts);
            }

            public string printMagasin()
            {
                string text = "MAGASIN\n";
                foreach (StateParser s in magasin)
                {
                    text += "type: " + s.type.ToString() + '\n';
                    text += "state: " + s.state.ToString() + '\n' + '\n';
                }

                return text;
            }
            public string printGenerator()
            {
                string text = "Generator\n";

                for (int i = 0; i < oPSTok.ops.Count; i++)
                {
                    text += "type: " + oPSTok.ops[i].Type.ToString() + '\n';
                }

                return text;
            }

            //public void runGenerator()
            //{
            //    foreach (Action act in generator)
            //    {
            //        act();
            //    }
            //}
            public List<Lexem> getOPS()
            {
                /*List<Lexem> listTest = new List<Lexem>();
                listTest.Add(new Lexem(LexemType.INTEGER, "7", 7));
                listTest.Add(new Lexem(LexemType.INTEGER, "9", 9));
                listTest.Add(new Lexem(LexemType.PLUS, "+"));
                oPSTok.opsTest.AddRange(listTest);

                return oPSTok.opsTest;*/
                return oPSTok.ops;
            }

        }
    }
}
