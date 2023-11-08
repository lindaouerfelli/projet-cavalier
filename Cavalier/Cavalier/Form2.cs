using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Cavalier
{
    public partial class Form2 : Form
    {
        const int chessboardSize = 8; 
        const int taillebouton = 50; 
        static int[,] echec = new int[12, 12];
        static int[] depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        int[] btnHelp = new int[8];
        
        int[] bClicked = new int[64];
       
        Button[,] grille1 = new Button[8, 8];        
        public Form2()
        {
            InitializeComponent();
        }
        private Button[,] grille = new Button[12, 12];
        private void Terrain_vide()
        {

            for (int c = 0; c < 12; c++)
            {
                for (int l = 0; l < 12; l++)
                {
                    Button b;
                    b = new Button();


                    b.Location = new Point(50+ l * 49,-80+ c * 49);
                    b.Size = new Size(50, 50);
                    b.Font = new Font("Aria", 10.00F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.Controls.Add(b);
                    b.BringToFront();
                    grille[l, c] = b;

                    if (l < 2 || c < 2 || l > 9 || c > 9)
                    {
                        grille[l, c].Enabled = false;
                        grille[l, c].Visible = false;
                    }
                    
                }
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.RoyalBlue;
            this.Text = "Mode Simulation";
            Terrain_vide();
        }
        static int fuite(int i, int j)
        {
            int n, l;

            for (l = 0, n = 8; l < 8; l++)
                if (echec[i + depi[l], j + depj[l]] != 0) n--;

            return (n == 0) ? 9 : n;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Terrain_vide();

            int nb_fuite, min_fuite, lmin_fuite = 0;
            int i, j, k, l, ii, jj;
            Random random = new Random();
            ii = random.Next(1, 8);
            jj = random.Next(1, 8);


            textBox1.Text = "Départ : " + (ii) + "  " + (jj);

            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);

            i = ii + 1; j = jj + 1;

            echec[i, j] = 1;
            grille[i, j].Text = "1";
            Thread.Sleep(600);

            for (k = 2; k <= 64; k++)
            {

                for (l = 0, min_fuite = 11; l < 8; l++)
                {
                    ii = i + depi[l]; jj = j + depj[l];

                    nb_fuite = ((echec[ii, jj] != 0) ? 10 : fuite(ii, jj));

                    if (nb_fuite < min_fuite)
                    {
                        min_fuite = nb_fuite; lmin_fuite = l;
                    }

                }
                if (min_fuite == 9 & k != 64)
                {
                    textBox1.Text = "";
                    break;
                }
                i += depi[lmin_fuite]; j += depj[lmin_fuite];

                echec[i, j] = k;

                grille[i, j].Text = Convert.ToString(k);
                Application.DoEvents();
                Thread.Sleep(600);

            }

            textBox1.Text = "Simulation avec succès! ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var reponse = MessageBox.Show("Voulez-vous quitter ?", "Quitter", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (reponse == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

      

        private void règlesDuJeuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regles = "Faut parcourir le cavalier sur les cases sans passer deux fois sur la même case.";
            MessageBox.Show(regles);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
