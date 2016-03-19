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

namespace TestApplication
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            startStat();
            nf.Owner = this;
        }


        public void setNMPath(int i, int j, string p)
        {
            n = i;
            m = j;
            path = p;
        }

        public bool checkNM(int i, int j)
        {
            int r = i * j;
            if (r % 2 == 0) return true;
            else return false;
        }

        public void initButton()
        {
            ButImage.nn = n * m / 2;
            massButt = new ButImage[n, m];
            setMass();

            mButt = new Button[n, m];
            setButt();
        }

        public void setMass()
        {
            int x,y;
            int k = 0;
            bool b = false;
            for (int i = 0; (i < n)&&(k< ButImage.nn); i++)
                for (int j = 0; (j < m) && (k < ButImage.nn); j++)
                {
                    if(massButt[i, j]==null)
                    {
                        b = false;
                        massButt[i, j] = new ButImage();
                        massButt[i, j].setIndexImag(k, path);
                        while(b==false)
                        {
                            x = rand.Next(0, n);
                            y = rand.Next(0, m);
                            if(massButt[x, y] == null)
                            {
                                massButt[x, y] = new ButImage();
                                massButt[x, y].setIndexImag(k, path);
                                b = true;
                                k++;
                            }
                        }
                        
                    }
                }
        }

        public void setButt()
        {
            int x = xCenter -m * 74 / 2, y = yCenter - n * 74 / 2;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    mButt[i, j] = new Button();
                    mButt[i, j].Location = new Point(x + 2, y + 2);
                    mButt[i, j].Width = 70;
                    mButt[i, j].Height = 70;
                    this.Controls.Add(mButt[i, j]);
                    massButt[i, j].hashBut = mButt[i, j].GetHashCode();

                    x += 74;
                }
                x = xCenter - m * 74 / 2;
                y += 74;
            }

            foreach (Button bt in mButt)
                bt.Click += (b, eArgs) =>
                {
                    bool f = false;
                    int i = 0,j = 0;


                    for (i = 0; i < n; i++)
                        for (j = 0; j < m; j++)
                            if (mButt[i, j].GetHashCode() == massButt[i, j].hashBut)
                                if (massButt[i, j].open == false)
                                    mButt[i, j].BackgroundImage = null;

                    foreach (ButImage bi in massButt)
                        if (bt.GetHashCode() == bi.hashBut)
                            but = bi;

                    if (but.open == false)
                    {
                        if (but1 == null)
                        {
                            but2 = null;
                            foreach (ButImage bi in massButt)
                                if (bt.GetHashCode() == bi.hashBut)
                                {
                                    bi.open = true;
                                    but1 = bi;
                                }
                            bt.BackgroundImage = Image.FromFile(but1.imagePath);
                        }
                        else
                        {
                            foreach (ButImage bi in massButt)
                                if (bt.GetHashCode() == bi.hashBut)
                                {
                                    bi.open = true;
                                    but2 = bi;
                                }
                            bt.BackgroundImage = Image.FromFile(but2.imagePath);

                            if (but1.index == but2.index)
                            {
                                res +=10;
                                but1 = null;
                                but2 = null;
                                k++;
                            }
                            else
                            {
                                resNot++;
                                res -= 2;
                                foreach (ButImage bi in massButt)
                                {
                                    if (but1.hashBut == bi.hashBut)
                                        bi.open = false;
                                    else if (but2.hashBut == bi.hashBut)
                                        bi.open = false;
                                }
                                but1 = null;
                                but2 = null;

                            }
                        }
                    }
                    labelResult.Text = "Количество очков: " + res;
                    labelResNot.Text = "Количество неудачных попыток: " + resNot;
                    if (k == n * m / 2)
                    {
                        MessageBox.Show(string.Format("Вы выиграли!"));
                    }

                };
        }


        NewGameForm nf = new NewGameForm();
        ButImage[,] massButt;
        ButImage but1,but2,but;
        Button[,] mButt;
        int n, m,k;
        string path,statist="";
        Random rand = new Random();
        int xCenter = 326, yCenter = 346;

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format(statist));
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int res = 0,resNot = 0;

        public void startStat()
        {
            int countGame = 0, maxRes = 0;
            double srRes = 0;
            string s;
            string[] str;

                FileStream fs = new FileStream(Application.StartupPath + @"\stat.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

                StreamReader sr = new StreamReader(fs);
                s = sr.ReadLine();
                sr.Close();
                fs.Close();


                if (s != null)
                {
                    str = s.Split(' ');
                    countGame = Convert.ToInt32(str[0]);
                    maxRes = Convert.ToInt32(str[1]);
                    srRes = Convert.ToDouble(str[2]);
                }
                
                fs = new FileStream(Application.StartupPath + @"\stat.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(countGame + " " + maxRes + " " + srRes);
                sw.Close();
                fs.Close();

                statist = "Количество проведенных игр: " + countGame + "\nМаксимальное количество заработанных очков: " + maxRes + "\nСреднее количество заработанных очков: " + srRes;
        }

        public void stat()
        {
            int countGame = 0, maxRes = 0;
            double srRes = 0;
            string s;
            string[] str;

            if(res!=0)
            {
                FileStream fs = new FileStream(Application.StartupPath + @"\stat.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

                StreamReader sr = new StreamReader(fs);
                s = sr.ReadLine();
                sr.Close();
                fs.Close();
                

                if (s != null)
                {
                    str = s.Split(' ');
                    countGame = Convert.ToInt32(str[0]);
                    maxRes = Convert.ToInt32(str[1]);
                    srRes = Convert.ToDouble(str[2]);
                }
                if (res > maxRes)
                    maxRes = res;
                srRes = (srRes * countGame + res) / (countGame + 1);
                countGame++;


                fs = new FileStream(Application.StartupPath + @"\stat.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(countGame +" " + maxRes + " " + srRes);
                sw.Close();
                fs.Close();

                statist = "Количество проведенных игр: "+ countGame+"\nМаксимальное количество заработанных очков: "+ maxRes+ "\nСреднее количество заработанных очков: " + srRes;
            }
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            if (massButt != null)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                        massButt[i, j] = null;
            if (mButt != null)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                        mButt[i, j].Dispose();
            massButt = null;
            mButt = null;        
            nf.ShowDialog();
            initButton();
            stat();
            k = 0;
            res = 0;
            resNot = 0;
            labelResult.Text = "Количество очков: "+ res;
            labelResNot.Text = "Количество неудачных попыток: " + resNot;
        }
    }
}
