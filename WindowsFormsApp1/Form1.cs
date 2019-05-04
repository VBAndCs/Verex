// Verex 1.0
// By: Mohammad Hamdy Ghanem
// Egypt, 2018
// Look at the PatternsTest.cs file in the VerexTests project for more sample about how to use Patterns.
// Do not forget to add these using statments :
// using RegexBuilder;
// using static RegexBuilder.Patterns;
// using static RegexBuilder.Patterns.Symbols;

using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RegexBuilder;
using static RegexBuilder.Patterns;
using static RegexBuilder.Patterns.Symbols;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            var ValidFirstChar = EnglishLetterOrHyphen;
            var ValidChars = EnglishLetterOrHyphenOrDigit & '.' & '-';

            var email = WordEdge +
                 ValidFirstChar +
                 ValidChars[0, 19] +
                 "@" +
                 ValidFirstChar +
                 ValidChars[2, 14] +
                 "." +
                 ValidFirstChar +
                 ValidChars[1, 9] +
                 WordEdge;

            textBox1.Text = email.ToString();

            var vrx = new Verex(email);
            var txt = "My email is msvbnet@hotmail.com .";
            var m = vrx.Match(txt);
            if (m.Success)
                MessageBox.Show(m.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var Name = (
                                    InRange(('A', 'Z'), ('a', 'z'), ('0', '9')) & '-'
                                )[1,20];
            var url = WordEdge +
                           ("http" + "s".Maybe() + "://").NoneOrOnce() +
                           "www.".NoneOrOnce() +
                           Name + ("." + Name).NoneOrMany() +
                           "." + OptionsGroup(Text("com") | "org" | "net").IgnoreCase() +
                           Maybe(@"/") +
                           WordEdge;

            textBox1.Text = url.Expression;

            var vrx = new Verex(url, RegexOptions.IgnoreCase);
            var txt = "My site is http://mhmdhmdy.blogspot.com/ .";
            var m = vrx.Match(txt);
            if (m.Success)
                MessageBox.Show(m.Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var validNo = (Maybe("1") + Digit[1, 2]) |
                       ("2" +
                            (
                                (InRange('0', '4') + Digit) | ("5" + InRange('0', '5'))
                            )
                       );

            var IP = WordEdge + validNo + ("." + validNo)[3] + WordEdge;

            textBox1.Text = IP.Expression;
            var vrx = new Verex(IP);
            var txt = "My IP is 10.0.127.212 .";
            var m = vrx.Match(txt);
            if (m.Success)
                MessageBox.Show(m.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string input = "((x+2) * (4+3))/2";
            var x = Verex.BalancedContents(input, "(", ")");
            textBox1.Text = Patterns.BalancedBracketsContent('(', ')').Expression;
            var t = "Input string: ((x + 2) * (4 + 3)) / 2";
            if (x == null)
                t += ("\r\nThe ( ) are not balanced");
            else
                foreach (Content cap in x)
                    t += "\r\n" + cap.Value;

            MessageBox.Show(t);
        }
    }
}

