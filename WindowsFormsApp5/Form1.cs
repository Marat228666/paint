using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        bool pen = true;
        bool rectangle=false;
        bool ellipse=false;
        bool ended = false;
        

        static SolidBrush brush = new SolidBrush(Color.Black);
        Pen mypen= new Pen(brush, 1);

        Point StartPoint;
        bool mouseDown = false;
        public Form1()
        {
            InitializeComponent();
            panel1.BackColor = Color.Black;
            radioButton1.Checked = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            StartPoint = e.Location;
            

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                if (StartPoint!= null)
                {
                    if (pen)
                    {
                        if (pictureBox1.Image==null)
                        {
                            pictureBox1.Image=new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        }
                        using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                        {
                            if(e.Button==MouseButtons.Left)
                            {
                                mypen.Color = panel1.BackColor;
                            }
                            else if (e.Button == MouseButtons.Right)
                            {
                                mypen.Color = Color.White;
                            }
                            g.DrawLine(mypen, StartPoint, e.Location);
                            pictureBox1.Invalidate();
                            
                        }
                    }
                    
                }
            }
            if (pen)
            {
                StartPoint = e.Location;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (rectangle||ellipse)
            {
                if (pictureBox1.Image == null)
                {
                    pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                }
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    if (rectangle)
                    {
                        if(e.Location.X>StartPoint.X && e.Location.Y>StartPoint.Y)
                        {
                            g.DrawRectangle(mypen, StartPoint.X, StartPoint.Y, e.Location.X-StartPoint.X, e.Location.Y-StartPoint.Y);
                            
                        }
                        
                    }
                    if (ellipse)
                    {
                        g.DrawEllipse(mypen, StartPoint.X, StartPoint.Y, e.Location.X - StartPoint.X, e.Location.Y - StartPoint.Y);
                    }
                    pictureBox1.Invalidate();
                }
            }
            
            StartPoint = Point.Empty;
            mouseDown = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mypen.Width=(float)numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK) 
            {
                mypen.Color = colorDialog1.Color;
                panel1.BackColor= colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
        private void insrumentChanged()
        {
            if (radioButton1.Checked)
            {
                pen = true;
                rectangle = ellipse = false;
            }
            else if (radioButton2.Checked)
            {
                rectangle = true;
                pen = ellipse = false;
            }
            else if (radioButton3.Checked)
            {
                ellipse = true;
                rectangle = pen = false;
            }
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            insrumentChanged();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPEG|*.jpg|ALL FILES|*.*";
            saveFileDialog1.Title = "SAVE FILE";
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                Bitmap buffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.DrawToBitmap(buffer, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
                buffer.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPEG|*.jpg|ALL FILES|*.*";
            openFileDialog1.Title = "OPEN FILE";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image==null)
                {
                    pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        g.DrawImage( new Bitmap(openFileDialog1.FileName),0,0,pictureBox1.Width, pictureBox1.Height);

                    }
                }
            }
        }
    }
}
