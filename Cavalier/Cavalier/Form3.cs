using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Cavalier
{
    public partial class Form3 : Form
    {
        const int TailleDuTerrain = 8;
        const int taillebouton = 50; 
        int X = 0;
        int Y = 0;

        int nbButtonClicked = 0;
        int bName;

        Image cavalier;
       
        Button[,] grille = new Button[8, 8];        
        int[] bClicked = new int[64];               
        int[] btnSuggestion = new int[8];                    
        bool finDeJeu = true;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {



            cavalier = Image.FromFile(@"img\cavalier.jpg");        
          
            
            damier(TailleDuTerrain);               
            Colorchess(TailleDuTerrain);
            this.Text = "jouer";
            label2.Text = "";
            this.BackColor = Color.RoyalBlue;
            disableButton();
            button1.Text = "Commencer";
            button2.Visible = false;
            
            button2.Text = "Commencer par sélectionner la première case";
            
            label1.Visible = false;
            button4.Text = "Rejouer ";
            button4.Visible = false;
        }
        private void btn_clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            bName = Convert.ToInt32(b.Name);          
            bClicked[nbButtonClicked] = bName;          
            nbButtonClicked += 1;                            
            b.Text = nbButtonClicked.ToString();                                     
            disableButton();
            btnSuggestion[0] = bName - 21;                        
            btnSuggestion[1] = bName - 19;    
            btnSuggestion[2] = bName - 12;    
            btnSuggestion[3] = bName - 8;     
            btnSuggestion[4] = bName + 8;     
            btnSuggestion[5] = bName + 12;    
            btnSuggestion[6] = bName + 19;    
            btnSuggestion[7] = bName + 21;    

            Colorchess(TailleDuTerrain);
            colorButton(bClicked);
            b.Image = cavalier;

            finDeJeu = true;

            foreach (int helpButton in btnSuggestion)
            {
                int isValid = checkIfIsInArray(bClicked, helpButton);
                if (isValid == 0)
                {

                    foreach (Button buttonToEnable in grille)     
                    {
                        if (helpButton == Convert.ToInt32(buttonToEnable.Name))
                        {
                            buttonToEnable.Enabled = true;
                            buttonToEnable.BackColor = Color.SandyBrown;
                            finDeJeu = false;
                        }
                    }
                }
            }

            if (finDeJeu)
            {
                endTheGame(nbButtonClicked);
            }
        }

        private int checkIfIsInArray(int[] array, int value)    
        {

            foreach (int isInArray in array)
            {
                if (value == isInArray)                
                {
                    return 1;
                }
            }
            return 0;
        }


        private void damier(int chesboardSize)    
        {

            for (int i = 0; i < chesboardSize; i++)
            {

                X = 0;

                for (int j = 0; j < chesboardSize; j++)
                {
                    grille[i, j] = new Button();
                    ((Button)grille[i, j]).Name = "" + (i + 1) + j;      
                    ((Button)grille[i, j]).Size = new Size(taillebouton, taillebouton);
                    ((Button)grille[i, j]).Location = new Point(X, Y);
                    ((Button)grille[i, j]).Click += new EventHandler(btn_clicked); 
                    panel1.Controls.Add((Button)grille[i, j]);
                    X += taillebouton;
                }
                Y += taillebouton;
            }
        }


        private void Colorchess(int chesboardSize)     
        {
            for (int l = 0; l < chesboardSize; l++)
            {
                for (int c = 0; c < chesboardSize; c++)
                {
                    if (l % 2 == 0)
                    {
                        if (c % 2 == 0)
                            ((Button)grille[l, c]).BackColor = Color.Wheat;
                        else
                            ((Button)grille[l, c]).BackColor = Color.Black;
                    }
                    else if (l % 2 != 0)
                    {
                        if (c % 2 == 0)
                            ((Button)grille[l, c]).BackColor = Color.Black;
                        else
                            ((Button)grille[l, c]).BackColor = Color.Wheat;
                    }

                }

            }
        }



        private void colorButton(int[] buttonUsed)
        {
            foreach (Button colorButton in grille)
            {
                for (int i = 0; i < nbButtonClicked; i++)
                {
                    if (buttonUsed[i] == Convert.ToInt32(colorButton.Name))
                    {
                        
                    }
                }
            }

        }



        private void endTheGame(int nbButtonClicked)
        {
            label1.Text = ""; 
            disableButton();

            if (nbButtonClicked == 64)
            {
                label2.Text = " Félicitaions !";
                button4.Visible = true;
                label2.Visible = true;
            }
            else
            {
               
                label2.Text = "Points : " + nbButtonClicked ;
                button4.Visible = true;
                label2.Visible = true;
            }

        }
        private void disableButton()
        {
            foreach (Button buttonDisabled in grille) 
            {
                buttonDisabled.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)    
        {

            button1.Enabled = false;
            button2.Visible = true;
         
        }
        private void enableButton()  
        {
            foreach (Button buttonDisabled in grille)
            {
                buttonDisabled.Enabled = true;
            }
        }

        private void clearboard(int chesboardSize)
        {

            for (int l = 0; l < TailleDuTerrain; l++)
            {
                for (int c = 0; c < TailleDuTerrain; c++)
                {
                    if (l % 2 == 0)
                    {
                        if (c % 2 == 0)
                            ((Button)grille[l, c]).BackColor = Color.Wheat;
                        else
                            ((Button)grille[l, c]).BackColor = Color.Black;
                    }
                    else if (l % 2 != 0)
                    {
                        if (c % 2 == 0)
                            ((Button)grille[l, c]).BackColor = Color.Black;
                        else
                            ((Button)grille[l, c]).BackColor = Color.Wheat;
                    }
                    grille[l, c].Image = null;
                    grille[l, c].Text = "";

                }

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)        
        {
            DialogResult reponse = MessageBox.Show(
            "voulez vous fermer la fenêtre ??",
            " fermé ",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button3,
            MessageBoxOptions.RightAlign);
            if (reponse == DialogResult.Yes) ;
            else if (reponse == DialogResult.No)
                e.Cancel = true;
            else
                e.Cancel = true;
        }

 

       

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            clearboard(TailleDuTerrain);
         
            Array.Clear(btnSuggestion, 0, btnSuggestion.Length);
            Array.Clear(bClicked, 0, bClicked.Length);                      
            label2.Visible = false;
            disableButton();
            nbButtonClicked = 0;

            button4.Visible = false;

            button2.Visible = true;
            button2.Enabled = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            enableButton();
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var reponse = MessageBox.Show("Voulez-vous quitter ? ", "Quitter", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (reponse == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
