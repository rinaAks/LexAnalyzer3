using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lexAnalyzerForms.Lexer;

namespace lexAnalyzerForms
{
    public class OPSTok
    {
        public List<Lexem> ops;

        public LexemType type;

        public OPSTok() 
        {
            this.ops = new List<Lexem>();
        }
    }


    //public class OPSer
    //{
        

    //    void addOPS(OPSer opsTok)
    //    {
    //        ops.Add(opsTok);
    //    }
    //}
}
