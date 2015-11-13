using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PitRaportParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<String> bazaTestow = new List<string>(); 

        private void button1_Click(object sender, EventArgs e)
        {
            string s = richTextBox1.Text;
            var temp = s.Split('\n',' ');
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = temp[i].Trim();
                if (temp[i].Length < 10)
                {
                    continue;
                }
                else
                {
                    if (bazaTestow.Count > 0)
                    {
                        bool czyZnaleziony = false;
                        foreach (string s1 in bazaTestow)
                        {
                            if (s1.Equals(temp[i]))
                            {
                                czyZnaleziony = true;
                                break;
                            }
                        }
                        if (!czyZnaleziony)
                        {
                            bazaTestow.Add(temp[i]);
                        }
                    }
                    else
                    {
                        bazaTestow.Add(temp[i]);
                    }
                }
            }

      PrintTestData();
            
        }

        private void PrintTestData()
        {
            string s = "";
            for (int i = 0; i < bazaTestow.Count; i++)
            {
                s +=bazaTestow[i]+"\n";
            }
            richTextBox4.Text = s;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = richTextBox2.Text;
            var temp = s.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = temp[i].Trim();
            }
            string result = "";//"org.easymock.tests2.CaptureTest.testCapture(org.easymock.tests2.CaptureTest)"
            foreach (var s1 in temp)//"org.easymock.tests2.CaptureTest.testCapture(org.easymock.tests2.CaptureTest) "
            {
                for (int i = 0; i < bazaTestow.Count;i++)
                {
                    if (s1.Equals(bazaTestow[i]))
                    {
                        result += (i + 1) + ";";
                    }
                }
            
            }
            richTextBox3.Text = result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                BroweTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Run.Enabled = false;
            Repleace("<p class='KILLED'><span class='pop'>", "<p class='KILLED'><span>", ",", ",</br>", checkBox1.Checked);
            Run.Enabled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="find"></param>
        /// <param name="repleace"></param>
        /// <param name="lineFind">Tutaj wstawiamy jakie znaki wewnatrz linji chcielibysmy wymienic dodatkowo jeśli nic wstawic NILL</param>
        /// <param name="lineRepleace">Tutaj wstawiamy jakie znaki wewnatrz linji chcielibysmy wymienic dodatkowo jeśli nic wstawic NILL</param>
        private bool Repleace(string find, string repleace, string lineFind, string lineRepleace, bool trimMs)
        {
            try
            {
                string[] files = Directory.GetFiles(@"" + BroweTextBox.Text, "*.html", SearchOption.AllDirectories);

                string[] fText;
                List<string> NewfText = new List<string>();
                foreach (string file in files)
                {
                    //   fText = File.ReadAllText(file);
                    fText = File.ReadAllLines(file);
                    richTextBox5.Text += "Przetwarza: " + file + Environment.NewLine;
                    foreach (string line in fText)
                    {
                        if (line.Contains(find))
                        {
                            var text = line.Replace(find, repleace);
                            if (lineRepleace != "NILL" || lineFind != "NILL")
                            {
                                text = text.Replace(lineFind, lineRepleace);
                            }
                            NewfText.Add(text);
                        }
                        else
                        {
                            if (trimMs)
                            {

                                string s = TrimMs(line);
                                NewfText.Add(s);
                            }
                            else
                                NewfText.Add(line);

                        }
                    }

                    // File.WriteAllText(file, NewfText.);
                    File.WriteAllLines(file, NewfText);
                    NewfText.Clear();
                }
            }
            catch (Exception e)
            {
                richTextBox5.Text += "ERROR: błąd krytyczny Parsera: " + e.ToString() + Environment.NewLine;
                return false;
            }
            richTextBox5.Text += "Info: Parsowanie zaończone" + Environment.NewLine;
            return true;
        }
        /// <summary>
        ///  usuwa milisekundy
        /// </summary>
        private string TrimMs(string input)
        {

            var text = Regex.Replace(input, @"\(\d+", "");
            text = text.Replace("ms)", "");
            return text;

        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            button3.Enabled = false;
            Repleace(richTextBox7.Text, richTextBox6.Text, "NILL", "NILL", false);
            button3.Enabled = true;
        }
    }
}
