using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;


namespace papki
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                listBox1.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo(fbd.SelectedPath);
                FileInfo[] files = dir.GetFiles("*.tif");
                foreach (FileInfo fi in files)
                {
                    listBox1.Items.Add(fi.ToString());
                    var dat = fi.CreationTime;
                }

                
                textBox1.Text = fbd.SelectedPath;
                label1.Text = "Документов: " + (listBox1.Items.Count).ToString();

                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   
           

            if (listBox1.Items.Count != 0)
            {
                string str = "";

                string requiredString0 = "";
                string requiredString1 = "";
                string requiredString2 = "";

                string reqString0 = "";
                string reqString1 = "";
                string reqString2 = "";

                int chislo1 = 0;
                int chislo2 = 0;

                listBox2.Items.Clear();

                /*
                if (listBox1.Items.Count > 0)
                {
                    Directory.CreateDirectory(textBox1.Text + "\\Актуальное");
                
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {

                        str = listBox1.Items[i].ToString();

                        requiredString0 = Regex.Match(str, @"(.*)и").Groups[1].Value;
                        requiredString1 = Regex.Match(str, @"и(.*)л").Groups[1].Value;
                        requiredString2 = Regex.Match(str, @"л(.*)").Groups[1].Value;

                        if (requiredString1 != "")
                        {
                            chislo1 = int.Parse(requiredString1);


                            if ((reqString0 == requiredString0 && reqString2 == requiredString2) && chislo1 > chislo2)
                            {
                                listBox2.Items.Add(requiredString0 + "и" + chislo2 + "л" + requiredString2);
                            }

                            reqString0 = requiredString0;
                            reqString1 = requiredString1;
                            reqString2 = requiredString2;
                            chislo2 = chislo1;

                            //  textBox2.Text += str + " \n  " + requiredString0 + "\n  " + requiredString1 + "\n " + requiredString2 + "\n " + reqString0 + "\n " + reqString1 + "\n " + reqString2 + "\n " + "\n " + chislo1 + "\n " + chislo2 + "\n ";
                        }
                    }


                    for (int n = 0; n < listBox2.Items.Count; n++)
                    {
                        string str1 = listBox2.Items[n].ToString();

                        for (int m = 0; m < listBox1.Items.Count; m++)
                        { 
                            string str2 = listBox1.Items[m].ToString();

                            if (str1 == str2)
                            {
                              //  MessageBox.Show("Строка2 =" + str2 + "Строка1 =" + str1);
                                listBox1.Items.RemoveAt(m);
                            }
                        }
                    }
                    label1.Text = "Документов: " + (listBox1.Items.Count).ToString();
                }
                else
                {
                    MessageBox.Show("Нет докуметов");
                    Application.Exit();
                }

               for (int i = 0; i < listBox1.Items.Count; i++)
                {

                    string str3 = listBox1.Items[i].ToString();

                    if (File.Exists(textBox1.Text + "\\Актуальное\\" + str3) == false)
                    {
                        File.Copy(textBox1.Text + "\\" + str3, textBox1.Text + "\\Актуальное\\" + str3, true);
                    }
                }
                button2.Enabled = false;
                //  MessageBox.Show("Сортировка завершена");
                using (StreamWriter sw = new StreamWriter(textBox1.Text + "\\Актуальное\\spisok.txt", true, System.Text.Encoding.Default))
                {
                    foreach (string s in listBox1.Items)
                    sw.WriteLine(s);
                }
                */

                Dictionary<string, string> map = new Dictionary<string, string>();
            
                ArrayList list = new ArrayList();
                List<int> ch = new List<int>();
                Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();

                if (listBox1.Items.Count > 0)
                {
                    Directory.CreateDirectory(textBox1.Text + "\\Актуальное");

                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        str = listBox1.Items[i].ToString();

                        requiredString0 = Regex.Match(str, @"(.*)и").Groups[1].Value;
                        requiredString1 = Regex.Match(str, @"и(.*)л").Groups[1].Value;
                        requiredString2 = Regex.Match(str, @"л(.*)").Groups[1].Value;

                        if (requiredString1 != "")
                        {

                            if (!map.ContainsKey(requiredString0 + requiredString1 + requiredString2) && int.TryParse(requiredString1, out int d))
                            {
                                map.Add((requiredString0 + requiredString1 + requiredString2), str);
                                if (!dict.ContainsKey(requiredString0 + "@" + requiredString2))
                                    dict[requiredString0 + "@" + requiredString2] = new List<int>();
                                dict[requiredString0 + "@" + requiredString2].Add(Convert.ToInt32(requiredString1));
                                dict[requiredString0 + "@" + requiredString2].Sort();
                            }
                        }
                    }

                    List<string> result = new List<string>();

                    foreach (var s in dict)
                    {
                        string k = s.Key;
                        string v = s.Value[+s.Value.Count - 1].ToString();
                        string tp = k.Replace("@", v);

                        foreach (var t in map)
                        {
                            if (t.Key == tp)
                                result.Add(t.Value);
                        }
                    }

                    for (int i = 0; i < result.Count; i++)
                    {

                        string str3 = result[i].ToString();

                        if (File.Exists(textBox1.Text + "\\Актуальное\\" + str3) == false)
                        {
                            File.Copy(textBox1.Text + "\\" + str3, textBox1.Text + "\\Актуальное\\" + str3, true);
                        }
                    }

                    using (StreamWriter sw = new StreamWriter(textBox1.Text + "\\Актуальное\\spisok.txt", true, System.Text.Encoding.Default))
                    {
                        foreach (string s in result)
                            sw.WriteLine(s);
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {
            List<string> list_mas = new List<string>();

            if (listBox1.Items.Count != 0)

                Directory.CreateDirectory(textBox1.Text + "\\Актуальное" + "\\Структура");
            {
                for (int a = 0; a < listBox1.Items.Count; a++)
                {
                    string s = listBox1.Items[a].ToString();
                    string str = s.Replace('+', '&');
                    list_mas.Add(str);
                }


               
                for (int i = 0; i < list_mas.Count; i++)
                {

                    string NamIzdel1 = list_mas[i].ToString();
                    string NamPapki1 = NamIzdel1.Substring(0, NamIzdel1.IndexOf('&'));


                    if (!Directory.Exists(textBox1.Text + "\\Актуальное" + "\\Структура\\" + NamPapki1))
                    {
                        Directory.CreateDirectory(textBox1.Text + "\\Актуальное" + "\\Структура\\" + NamPapki1);
                    }

                    for (int j = 0; j < list_mas.Count; j++)
                    {

                        string NamIzdel2 = list_mas[j].ToString();
                        string NamPapki2 = Regex.Match(NamIzdel2, @"&(.*)&").Groups[1].Value;

                        if (!Directory.Exists(textBox1.Text + "\\Актуальное" + "\\Структура\\" + NamPapki1 + "\\" + NamPapki1 + "." + NamPapki2))
                        {
                            Directory.CreateDirectory(textBox1.Text + "\\Актуальное" + "\\Структура\\" + NamPapki1 + "\\" + NamPapki1 + "." + NamPapki2);
                        }


                        for (int g = 0; g < list_mas.Count; g++)
                        {
                            string NamIzdel3 = list_mas[g].ToString();
                            string NamPapki3 = Regex.Match(NamIzdel2, @"&(.*)и").Groups[1].Value + "и";
                            NamPapki3.Remove(0, 1);
                            string NamPapki4 = Regex.Match(NamPapki3, @"&(.*)и").Groups[1].Value;
                            string TMP = NamIzdel3;
                            string sTMP = TMP.Replace("&","+");


                            if (!Directory.Exists(textBox1.Text + "\\Актуальное" + "\\Структура\\" + NamPapki1 + "\\" + NamPapki1 + "." + NamPapki2 + "\\" + NamPapki1 + "." + NamPapki2 + "." + NamPapki4))
                            {
                                Directory.CreateDirectory(textBox1.Text + "\\Актуальное" + "\\Структура\\" + NamPapki1 + "\\" + NamPapki1 + "." + NamPapki2 + "\\" + NamPapki1 + "." + NamPapki2 + "." + NamPapki4);
                            }

                            if (File.Exists(textBox1.Text + "\\Актуальное" + "\\Структура\\" + NamPapki1 + "\\" + NamPapki1 + "." + NamPapki2 + "\\" + NamPapki1 + "." + NamPapki2 + "." + NamPapki4 + "\\" + sTMP) == false)
                            {
                                File.Copy(textBox1.Text + "\\Актуальное\\" + sTMP, textBox1.Text + "\\Актуальное" + "\\Структура\\" + NamPapki1 + "\\" + NamPapki1 + "." + NamPapki2 + "\\" + NamPapki1 + "." + NamPapki2 + "." + NamPapki4 + "\\" + TMP, true);
                            }
                            
                        }
                    }
                }
                
                Run(textBox1.Text + "\\Актуальное" + "\\Структура");
            }
            
        }

        public void Run(string rootDir)
        {
            DirectoryInfo dir = new DirectoryInfo(rootDir);
            TreeNode rootNode = Tree_Run_1(dir);
            treeView1.Nodes.Add(rootNode);
        }

        public TreeNode Tree_Run_1(DirectoryInfo dir)
        {
            TreeNode vrt = new TreeNode(dir.Name);
            foreach (DirectoryInfo inf in dir.GetDirectories())
            {
                TreeNode branch = Tree_Run_1(inf);
                vrt.Nodes.Add(branch);
            }
            foreach (FileInfo inf in dir.GetFiles())
            {
                vrt.Nodes.Add(new TreeNode(inf.Name, 1, 1));

            }
            return vrt;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
        }

        private void PictureBoxLoadImage(string path)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = treeView1.SelectedNode.Text;
            PictureBoxLoadImage(textBox1.Text + "\\Актуальное\\" + text);
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        { 
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
