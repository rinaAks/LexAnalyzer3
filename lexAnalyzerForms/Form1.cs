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
            parser.runGenerator();
            tbOutput.Text += parser.printGenerator();
        }
    }

}
