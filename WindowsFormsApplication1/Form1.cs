using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        static float avarageTw;
        static float averageTa;
        bool clicked=false;
        static int[] processes = { 1, 2, 3};
        static int n;
        static int[] burst_time;
        static int quantum;
        int pr1;
        int pr2;
        int pr3;


        public Form1()
        {
             pr1 = 500;
             pr2 = 550;
             pr3 = 600;
           
            InitializeComponent();
          

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {//quantum
            quantum = int.Parse(txtquantim.Text);
            n = int.Parse(txtN.Text);
            burst_time = new int[n];
            checkedListBox1.Items.Add(txtp1.Text);
            checkedListBox1.Items.Add(txtp2.Text);
            checkedListBox1.Items.Add(txtp3.Text);
           
            burst_time[0] = int.Parse(txtp1.Text);
            burst_time[1] = int.Parse(txtp2.Text);
            burst_time[2] = int.Parse(txtp3.Text);
           
            findAverage(processes, n, burst_time, quantum);

            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Round Robin algorithm: suppose arrival time =0 ","Hello sir");
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.Columns.Add("processes", 70);
            listView1.Columns.Add("burest time", 90);
            listView1.Columns.Add("waiting", 70);
            listView1.Columns.Add("trunAround", 90);

          
        }
        public  void findWaiting(int[] processes, int n, int[] bt, int quantim, int[] waiting_time)
        {

            //n>>number processes
            //bt>>burst time
            //int []waiting_time >>waiting time each process
            // waiting time each process هنستخدمه فى ايجاد 
            int time = 0;
            int[] remaing_bt = new int[n];
            for (int i = 0; i < n; i++)
            {
                //intialize remaing burst time with >>burst time
                remaing_bt[i] = bt[i];
            }
            while (true)
            {
                //while loop هنستخدمه علشان نطلع من 
                bool flag = true;
                //loop each process
                for (int i = 0; i < n; i++)
                {
                    if (remaing_bt[i] > 0)
                    {
                        flag = false;
                        if (remaing_bt[i] > quantim)
                        {
                            time = time + quantim;
                            remaing_bt[i] = remaing_bt[i] - quantim;

                        }
                        // else >> remaing_bt[i]<=quantim
                        else
                        {
                            time = time + remaing_bt[i];
                            waiting_time[i] = time - bt[i];
                            remaing_bt[i] = 0;

                        }

                    }

                }

                // end while loop >>when remaing time each processes=0
                if (flag == true)
                {
                    break;
                }

            }


        }
        public  void findTurnAround(int[] processes, int n, int[] bt, int[] wt, int[] tat)
        {
            //total turnAround=waiting+burst 
            for (int i = 0; i < n; i++)
                tat[i] = bt[i] + wt[i];

        }
        public void findAverage(int[] processes, int n, int[] bt, int quantum)
        {
            int[] waiting_time = new int[n];
            int[] tat = new int[n];
            float total_wt = 0;
            float total_ta = 0;
            findWaiting(processes, n, bt, quantum, waiting_time);
            findTurnAround(processes, n, bt, waiting_time, tat);
            for (int i = 0; i < n; i++)
            {
                total_ta += tat[i];
                total_wt += waiting_time[i];
                string[] row = { (i + 1).ToString(), burst_time[i].ToString(), waiting_time[i].ToString(), tat[i].ToString() };
                var listviewitem = new ListViewItem(row);
                listView1.Items.Add(listviewitem);
            }
            avarageTw = total_wt / n;
            averageTa = total_ta / n;
          
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'c' || e.KeyChar == 'C')
            {
                listView1.Items.Clear();
            }

        }

        private void checkedListBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'c' || e.KeyChar == 'C')
            {
                checkedListBox1.Items.Clear();
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            int y = 330;
           
            string drawstring = "Gantt Chart";
            Font drawfont = new Font("Arial", 20);
            PointF drawpoint = new PointF(10, 280);
            Graphics g1 = e.Graphics;

            Pen mypen = new Pen(Color.Red);
            Brush draw = new SolidBrush(Color.Green);
            Brush mybrush = new SolidBrush(Color.Blue);
            Brush mybrush1 = new SolidBrush(Color.Red);
            Brush mybrush2 = new SolidBrush(Color.Green);
            g1.DrawRectangle(mypen, 0, 310, 800, 170);
            g1.DrawString(drawstring, drawfont, draw, drawpoint);
            

        
            int[] id = { 1, 2, 3 };
            int quantim = quantum;
            int var = 1;
            int time = 0;
            int[] remaing_bt = new int[n];
           
            for (int i = 0; i < n; i++)
            {
                //intialize remaing burst time with >>burst time
                remaing_bt[i] = burst_time[i];
            }
          if (clicked == true)
           {
                while (true)
                {
                    //while loop هنستخدمه علشان نطلع من 
                    bool flag = true;
                    //loop each process
                    for (int i = 0; i < n; i++)
                    {
                        if (remaing_bt[i] > 0)
                        {
                            flag = false;
                            if (remaing_bt[i] > quantim)
                            {
                                //g1.DrawLine(mypen, time, 30, time, 60);
                                //if (id[i] == 1 && i==0)
                                //    g1.FillRectangle(mybrush, x, y, quantim, 100);

                                if (id[i] == 1)
                                {
                                    g1.FillRectangle(mybrush, var * time, y, quantim, 100);
                                    var++;
                                }

                                else if (id[i] == 2)
                                {
                                    g1.FillRectangle(mybrush1, var * time, y, quantim, 100);
                                    var++;
                                }

                                else if (id[i] == 3)
                                {
                                    g1.FillRectangle(mybrush2, var * time, y, quantim, 100);
                                    var++;
                                }

                                time = time + quantim;
                                remaing_bt[i] = remaing_bt[i] - quantim;

                            }
                            // else >> remaing_bt[i]<=quantim
                            else
                            {

                               //  g1.DrawLine(mypen, time, 30, time, 60);



                                if (id[i] == 1)
                                {
                                    g1.FillRectangle(mybrush, var * time, y, quantim, 100);
                                    var++;
                                }

                                else if (id[i] == 2)
                                {
                                    g1.FillRectangle(mybrush1, var * time, y, quantim, 100);
                                    var++;
                                }

                                else if (id[i] == 3)
                                {
                                    g1.FillRectangle(mybrush2, var * time, y, quantim, 100);
                                    var++;
                                }


                                time = time + remaing_bt[i];
                                remaing_bt[i] = 0;

                            }
                        }




                    }
                    if (flag == true)
                    {
                        break;
                    }

                }

                // end while loop >>when remaing time each processes=0
            }

            //show information color
            if (clicked == true)
            {
                Font process=new Font("Arial", 15);
                g1.FillRectangle(mybrush,850 ,pr1 , 50, 50);
                g1.FillRectangle(mybrush1,850 , pr2, 50,50);
                g1.FillRectangle(mybrush2, 850, pr3, 50, 50);
                g1.DrawString("process1", process,mybrush , 900,pr1);
                g1.DrawString("process2", process, mybrush1, 900, pr2);
                g1.DrawString("process3", process, mybrush2, 900, pr3);
            }




        }
        public void button1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            cpu();
            if (pr1 > 340)
                pr1 -= 1;
            if (pr2 > 390)
                pr2 -= 1;
            if (pr3 > 440)
                pr3 -= 1;
           if(clicked==true)
            Invalidate();
           
        }

        private void txtquantim_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtp1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("average Waiting Time = " + avarageTw + "\n" + "average TrunAround Time = " + averageTa,"show information ");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clicked = true;
            Invalidate();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtp1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                txtN.Clear();
                txtquantim.Clear();
                txtp1.Clear();
                txtp2.Clear();
                txtp3.Clear();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
          

        }
        public void cpu()
        {

            int[] id = { 1, 2, 3 };
            int qua =quantum ;

            int timeit = 0;
            int[] remaing = new int[n];

            for (int i = 0; i < n; i++)
            {
                //intialize remaing burst time with >>burst time
                remaing[i] = burst_time[i];
            }

            while (true)
            {
                //while loop هنستخدمه علشان نطلع من 
                bool flag = true;
                //loop each process
                for (int i = 0; i < n; i++)
                {
                    if (remaing[i] > 0)
                    {
                        flag = false;
                        if (remaing[i] > qua)
                        {
                           
                            if (id[i] == 1)
                            {
                                timer1.Interval = Convert.ToInt32(checkedListBox1.Items[0] + "000");
                                textBox5.Text = "p1";

                            }

                            else if (id[i] == 2)
                            {
                                timer1.Interval = Convert.ToInt32(checkedListBox1.Items[1] + "000");
                                textBox5.Text = "p2";
                            }

                            else if (id[i] == 3)
                            {
                                timer1.Interval = Convert.ToInt32(checkedListBox1.Items[2] + "000");
                                textBox5.Text = "p3";
                            }

                            timeit = timeit + qua;
                            remaing[i] = remaing[i] - qua;

                        }
                        // else >> remaing_bt[i]<=quantim
                        else
                        {


                            if (id[i] == 1)
                            {
                                timer1.Interval = Convert.ToInt32(checkedListBox1.Items[0] + "000");
                                textBox5.Text = "p1";

                            }

                            else if (id[i] == 2)
                            {
                                timer1.Interval = Convert.ToInt32(checkedListBox1.Items[1] + "000");
                                textBox5.Text = "p2";
                            }

                            else if (id[i] == 3)
                            {
                                timer1.Interval = Convert.ToInt32(checkedListBox1.Items[2] + "000");
                                textBox5.Text = "p3";
                            }


                            timeit = timeit + remaing[i];
                            remaing[i] = 0;

                        }
                    }



                   
                }
                if (flag == true)
                {
                    break;
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f = new Form2();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
    }

