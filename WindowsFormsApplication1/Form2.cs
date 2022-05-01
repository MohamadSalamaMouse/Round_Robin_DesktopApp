using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {

        static float avarageTw;
        static float averageTa;
        static string[] processes = { "p1", "p2", "p3", "p4", "p5" };
        static int num;
        static int[] burst_time;
        static int[] arrival;
        static int quantum;

        static int[] waiting = new int[num];
        static int[] compatio = new int[num];
        public Form2()
        {

            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            quantum = int.Parse(txtquantim.Text);
            num = int.Parse(txtN.Text);
            burst_time = new int[num];
            arrival = new int[num];
            arrival[0] = int.Parse(txt1.Text);
            arrival[1] = int.Parse(txt2.Text);
            arrival[2] = int.Parse(txt3.Text);
            arrival[3] = int.Parse(txt4.Text);
            arrival[4] = int.Parse(txt5.Text);

            burst_time[0] = int.Parse(txtp1.Text);
            burst_time[1] = int.Parse(txtp2.Text);
            burst_time[2] = int.Parse(txtp3.Text);
            burst_time[3] = int.Parse(txtp4.Text);
            burst_time[4] = int.Parse(txtp5.Text);

            roundRobin(processes, arrival, burst_time, quantum);
            for (int i = 0; i < num; i++)
            {

                string[] row = { (i + 1).ToString(), burst_time[i].ToString(), waiting[i].ToString(), compatio[i].ToString() };
                var listviewitem = new ListViewItem(row);
                listView1.Items.Add(listviewitem);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Round Robin algorithm: with arrival time  ", "Hello sir");
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.Columns.Add("processes", 70);
            listView1.Columns.Add("burest time", 90);
            listView1.Columns.Add("waiting", 70);
            listView1.Columns.Add("compilation", 90);
           
        }


        private void txt2_TextChanged(object sender, EventArgs e)
        {

        }
        public static void roundRobin(String[] p, int[] a,
                                  int[] b, int n)
        {
            // result of average times
            int res = 0;
            int resc = 0;

            // for sequence storage
            String seq = "";

            // copy the burst array and arrival array
            // for not effecting the actual array
            int[] res_b = new int[b.Length];
            int[] res_a = new int[a.Length];

            for (int i = 0; i < res_b.Length; i++)
            {
                res_b[i] = b[i];
                res_a[i] = a[i];
            }

            // critical time of system
            int t = 0;

            // for store the waiting time
            int[] w = new int[p.Length];
            

            // for store the Completion time
            int[] comp = new int[p.Length];
            

            while (true)
            {
                Boolean flag = true;
                for (int i = 0; i < p.Length; i++)
                {

                    // these condition for if
                    // arrival is not on zero

                    // check that if there come before qtime
                    if (res_a[i] <= t)
                    {
                        if (res_a[i] <= n)
                        {
                            if (res_b[i] > 0)
                            {
                                flag = false;
                                if (res_b[i] > n)
                                {

                                    // make decrease the b time
                                    t = t + n;
                                    res_b[i] = res_b[i] - n;
                                    res_a[i] = res_a[i] + n;
                                    seq += "->" + p[i];
                                }
                                else
                                {

                                    // for last time
                                    t = t + res_b[i];

                                    // store comp time
                                    comp[i] = t - a[i];

                                    // store wait time
                                    w[i] = t - b[i] - a[i];
                                    res_b[i] = 0;

                                    // add sequence
                                    seq += "->" + p[i];
                                }
                            }
                        }
                        else if (res_a[i] > n)
                        {

                            // is any have less arrival time
                            // the coming process then execute
                            // them
                            for (int j = 0; j < p.Length; j++)
                            {

                                // compare
                                if (res_a[j] < res_a[i])
                                {
                                    if (res_b[j] > 0)
                                    {
                                        flag = false;
                                        if (res_b[j] > n)
                                        {
                                            t = t + n;
                                            res_b[j]
                                                = res_b[j] - n;
                                            res_a[j]
                                                = res_a[j] + n;
                                            seq += "->" + p[j];
                                        }
                                        else
                                        {
                                            t = t + res_b[j];
                                            comp[j] = t - a[j];
                                            w[j] = t - b[j]
                                                   - a[j];
                                            res_b[j] = 0;
                                            seq += "->" + p[j];
                                        }
                                    }
                                }
                            }

                            // now the previous process
                            // according to ith is process
                            if (res_b[i] > 0)
                            {
                                flag = false;

                                // Check for greaters
                                if (res_b[i] > n)
                                {
                                    t = t + n;
                                    res_b[i] = res_b[i] - n;
                                    res_a[i] = res_a[i] + n;
                                    seq += "->" + p[i];
                                }
                                else
                                {
                                    t = t + res_b[i];
                                    comp[i] = t - a[i];
                                    w[i] = t - b[i] - a[i];
                                    res_b[i] = 0;
                                    seq += "->" + p[i];
                                }
                            }
                        }
                    }

                    // if no process is come on the critical
                    else if (res_a[i] > t)
                    {
                        t++;
                        i--;
                    }
                }

                // for exit the while loop
                if (flag)
                {
                    break;
                }
            }
            waiting = w;
            compatio = comp;
        }
    }
}
