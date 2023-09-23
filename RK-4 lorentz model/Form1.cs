using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RK_4_lorentz_model
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int size = 50000;
        double sigma = 10.0, b = 8.0 / 3.0, dt = 0.001;
        double r = 25;

        private void button1_Click(object sender, EventArgs e)
        {
            double k1, k2, k3, k4;
            double l1, l2, l3, l4;
            double m1, m2, m3, m4;

            double[] x = new double[size];
            double[] y = new double[size];
            double[] z = new double[size];
            double[] t = new double[size];
            x[0] = 1;
            Graphics gg = CreateGraphics();
            SolidBrush sb = new SolidBrush(Color.Blue);
            DataTable dt1 = new DataTable();
            
            dt1.Columns.Add("x");
            dt1.Columns.Add("y");
            dt1.Columns.Add("z");
            dt1.Columns.Add("t");
            DataRow r1;


            for (int i = 0; i < size - 1; i++)
            {
                k1 = dt * f1(x[i], y[i]);
                l1 = dt * f2(x[i], y[i], z[i]);
                m1 = dt * f3(x[i], y[i], z[i]);
                k2 = dt * f1(x[i] + k1 / 2, y[i] + l1 / 2);
                l2 = dt * f2(x[i] + k1 / 2, y[i] + l1 / 2, z[i] + m1 / 2);
                m2 = dt * f3(x[i] + k1 / 2, y[i] + l1 / 2, z[i] + m1 / 2);
                k3 = dt * f1(x[i] + k2 / 2, y[i] + l2 / 2);
                l3 = dt * f2(x[i] + k2 / 2, y[i] + l2 / 2, z[i] + m2 / 2);
                m3 = dt * f3(x[i] + l2 / 2, y[i] + l2 / 2, z[i] + m2 / 2);
                k4 = dt * f1(x[i] + k3, y[i] + l3);
                l4 = dt * f2(x[i] + k3, y[i] + l3, z[i] + m3);

                m4 = dt * f3(x[i] + k3, y[i] + l3, z[i] + m3);

                x[i + 1] = x[i] + (1 / 6.0) * (k1 + 2 * (k2 + k3) + k4);
                y[i + 1] = y[i] + (1 / 6.0) * (l1 + 2 * (l2 + l3) + l4);
                z[i + 1] = z[i] + (1 / 6.0) * (m1 + 2 * (m2 + m3) + m4);
                t[i + 1] = t[i] + dt;
              

                r1 = dt1.NewRow();
                r1[0] = x[i];
                r1[1] = y[i];
                r1[2] = z[i];
                r1[3] = t[i];
                dt1.Rows.Add(r1);

                gg.FillEllipse(sb, 850 + (float)x[i] * 10, 500 - (float)z[i] * 10, 5, 5);


            }
            dataGridView1.DataSource = dt1;
        }
            double f1( double x1, double y1)
            {
                return (sigma*(y1-x1));
            }
            double f2(double x1, double y1, double z1)
            {
                return (-x1 * z1 + r * x1 - y1);
            }
            double f3(double x1, double y1, double z1)
            {
                return (x1*y1-b*z1);
            }
        }
    }

