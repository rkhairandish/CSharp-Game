using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CSharpAssignment


{   
    public partial class Form1 : Form
    {
        //Declare Variables
        Button[] ButtonShape = new Button[12];
        Bitmap[] ImageArray = new Bitmap[12];
        Timer delay;
        bool waiting = false;
        DateTime now;

        int pos1 = -1;
        int pos2 = -1;


        int[] shapeArray=new int [12] {0,0,1,1,2,2,3,3,4,4,5,5};

        int[] buttonConnection= new int[12];

        int tempPos=-1;    //temporarily store position of button

        public Form1()
        {
            InitializeComponent();
        }

        //This displays the buttons 
        //Assign x and y to specific areas 
        //Allow loop to create 4 buttons the start a new line with another 4, 3 times.
        private void Form1_Load(object sender, EventArgs e)
        {
            setup();          
        }

        private void setup()
        {
            for(int i =0 ; i < ButtonShape.Length; i++)
            {
                if(ButtonShape[i] != null)
                {
                    this.Controls.Remove(ButtonShape[i]);
                }
            }
            int y = 150;
            int x = 50;
            for (int i = 0; i < 12; i++)
            {

                if (i == 4)
                {
                    y = y + 120;
                }
                else if (i == 8)
                {
                    y = y + 120;
                }
                else if (i == 12)
                {
                    y = y + 120;
                }

                if (i == 4 || i == 8 || i == 12)
                {
                    x = 50;
                }

                ButtonShape[i] = new Button();
                ButtonShape[i].Location = new Point(x, y);
                ButtonShape[i].Size = new Size(100, 100);
                ButtonShape[i].Click += ButtonClicked;
                ButtonShape[i].Name = i.ToString();
                this.Controls.Add(ButtonShape[i]);
   
                x = x + 120;

            }
            //Calls the method 
            GenerateImages();
            now = DateTime.Now;
        }

        //Implements when button is clicked a random shape is shown
        private void ButtonClicked(object sender, EventArgs e)
        {
            if (waiting) return;


            int pos = Convert.ToInt32(((Button)sender).Name);
            Console.WriteLine(pos); //print button "position"
            ((Button)sender).Image = ImageArray[pos];
          

            if (pos1 == -1)
            {
                pos1 = pos;
            }
            else
            {
                pos2 = pos;
                if (delay == null)
                {
                    delay = new Timer();
                    delay.Interval = 500;
                    delay.Tick += new EventHandler(CheckPairs);
                    delay.Start();
                    waiting = true;
                }
            }
        }

        private void WinCondition()
        {
            for(int i =0; i < ButtonShape.Length; i++)
            {
                if (ButtonShape[i].Image == null)
                    return;
            }

            MessageBox.Show("Congratulations! You WIN!");
            MessageBox.Show("You Completed the game in: " + (DateTime.Now - now).ToString());
            DateTime localDate = DateTime.Now; 

        }

        //return -1=no pair yet, 1=pair, 0=not a pair
        private void CheckPairs(object sender, EventArgs e)
        {
            delay.Stop();
            delay = null;
            waiting = false;
            if (buttonConnection[pos1] == buttonConnection[pos2]) //if 2nd is same as 1st
            {

                //remove events
                ButtonShape[pos1].Click -= ButtonClicked;
                ButtonShape[pos2].Click -= ButtonClicked;
                Console.WriteLine("You got a pair!");

                WinCondition();
            }
            else //if pair doesn't match
            {
                //turn both cards upside down again

                ButtonShape[pos1].Image = null;
                ButtonShape[pos2].Image = null;
                Console.WriteLine("Try again!");

                tempPos = -1;
                //return 0;
            }

            pos1 = -1;
            pos2 = -1;
            
        }

      
        //Randomises the Shapes
        private void GenerateImages()
        {

            for(int i = 0; i < ImageArray.Length; i++)
            {
                ImageArray[i] = null;
            }

            Random rnd = new Random();

            for (int i = 0; i < 12; i++)
            {
                int newRand = rnd.Next(0, 12); //get num 0-11

                while (ImageArray[newRand] != null) //if random number has been generated again (an image already exists)
                {
                    newRand = rnd.Next(0, 12); //get num 0-11
                }

                ImageArray[newRand] = getImage(shapeArray[i]); //access random button / get image at value pointed at by  shapeArray
                
                //image at pos rand store shape value
                buttonConnection[newRand] = shapeArray[i];
            }
        }


        //Return the shapes/images
        private Bitmap getImage(int imageNum)
        {

            switch(imageNum)
            {

                case 0 :
                    {
                        return new Bitmap(CSharpAssignment.Properties.Resources.circle);
                    }
                case 1:
                    {
                        return new Bitmap(CSharpAssignment.Properties.Resources.cross);
                    }
                case 2:
                    {
                        return new Bitmap(CSharpAssignment.Properties.Resources.diamond);
                    }
                case 3:
                    {
                        return new Bitmap(CSharpAssignment.Properties.Resources.flash);
                    }
                case 4:
                    {
                        return new Bitmap(CSharpAssignment.Properties.Resources.star);
                    }
                case 5:
                    {
                        return new Bitmap(CSharpAssignment.Properties.Resources.triangle);
                    } 
                default:
                    {
                        return new Bitmap(CSharpAssignment.Properties.Resources.circle);
                    }
     
            }

            
        }

        private void CheckClick ()
        {

        }
        

        private void button2_Click(object sender, EventArgs e)
        {

        }

        //Implement Exit Button
        private void button13_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Implement Reset Button
        private void button1_Click(object sender, EventArgs e)
        {
            setup();
        }

    }
}
