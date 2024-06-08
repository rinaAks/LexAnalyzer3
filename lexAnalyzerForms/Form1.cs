using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using static lexAnalyzerForms.Lexer;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lexAnalyzerForms
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public static List<Lexem> myStorage;
        public static List<Lexem> myVarStorage;
        public static List<Lexem> opsLexemsStorage;
        private void button1_Click(object sender, EventArgs e)
        {
            //myStorage.Clear();
            //myVarStorage.Clear();
            //myStorage = new List<Lexem>();
            //myVarStorage = new List<Lexem>();
            Lexer lexer = new Lexer(tbInput.Text);
            myStorage = lexer.GetLexemStorage();
            myVarStorage = lexer.GetVariableStorage();
            // tbOutput.Text = lexer.GetOutputText();
            
            ParserClass.Parser parser = new ParserClass.Parser();
            parser.FillMagazine();
            
            //tbOutput.Text += parser.printMagasin();
            //parser.runGenerator();
            tbOutput.Text += parser.printGenerator();

            //opsLexemsStorage = parser.getOPS();

            //OPS_exe ooops = new OPS_exe(opsLexemsStorage);
            //tbOutput.Text += ooops.Calculate().ToString();
            
            //OPS_exe ooops = new OPS_exe(myStorage);
            //tbOutput.Text += ooops.Calculate().Value.ToString() + '\n';

            foreach(Lexem lexem in myVarStorage) 
            {
                tbOutput.Text += lexem.Name.ToString() + '\n';
                tbOutput.Text += lexem.Value.ToString() + '\n';
            }

        }
    }

}
