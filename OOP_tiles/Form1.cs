using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

using DrawPattern;
using Pattern;

namespace OOP_tiles
{
    [Serializable]
    public partial class Form1 : Form
    {
        int SIZE_PATTERN_ROW = 20;           //dimension of a pattern in x direction
        int SIZE_PATTERN_COLUMN = 20;      //dimension of a pattern in y direction

        const int SIZE_TILES = 15;         //size of a pixel for selected input and output pattern

        CDrawPattern drawPattern1 = new CDrawPattern(); //draw object for input patterns

        Color frontColor = Color.Transparent;

        public Form1()
        {
            InitializeComponent();
            textBox1.KeyPress += Validation;
            textBox2.KeyPress += Validation;
        }

        public List<PictureBox> pictureBoxes = new List<PictureBox>();
        public List<PictureLoad> pictureBoxesLoad = new List<PictureLoad>();


        // Логические переменные
        bool IsClicked = false;
        bool canMove = false;
        public bool isEnabled = false;

        Point p = new Point();

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (canMove == false && e.Button == MouseButtons.Left && !isEnabled)
            {
                canMove = true;
                p = e.Location;
            }

            if (IsClicked == false && e.Button == MouseButtons.Right && isEnabled)
            {
                IsClicked = true;
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = false;
            IsClicked = false;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove && e.Button == MouseButtons.Left && !isEnabled)
            {
                PictureBox pic = (PictureBox)sender;


                if (pic.Top + ((e.Y - p.Y) - ((e.Y - p.Y) % SIZE_TILES)) >= pictureBox1.Location.Y
                   && pic.Top + ((e.Y - p.Y) - ((e.Y - p.Y) % SIZE_TILES) + pic.Height) < pictureBox1.Location.Y + (SIZE_PATTERN_COLUMN + 1) * SIZE_TILES)
                {
                    pic.Top += (e.Y - p.Y) - ((e.Y - p.Y) % SIZE_TILES);
                }



                if (pic.Left + ((e.X - p.X) - ((e.X - p.X) % SIZE_TILES)) >= pictureBox1.Location.X
                    && pic.Left + ((e.X - p.X) - ((e.X - p.X) % SIZE_TILES) + pic.Width) < pictureBox1.Location.X + (SIZE_PATTERN_ROW + 1) * SIZE_TILES)
                {
                    pic.Left += (e.X - p.X) - ((e.X - p.X) % SIZE_TILES);
                }

            }
            if (IsClicked && e.Button == MouseButtons.Right && isEnabled)
            {
                pictureBox1.Invalidate();
            }
        }

        public void CreatePictureBox(int widht, int height)
        {
            PictureBox picture_Boxes = new PictureBox();
            picture_Boxes.Image = null;

            picture_Boxes.Parent = pictureBox1;

            picture_Boxes.Location = new System.Drawing.Point(pictureBox1.Location.X + 1, pictureBox1.Location.Y + 1);
            picture_Boxes.Name = "picture_Boxes";
            picture_Boxes.Size = new System.Drawing.Size(height, widht);
            picture_Boxes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            picture_Boxes.TabIndex = 1;
            picture_Boxes.TabStop = false;

            this.Controls.Add(picture_Boxes);
            picture_Boxes.BackColor = frontColor;
            picture_Boxes.BorderStyle = BorderStyle.FixedSingle;
            picture_Boxes.BringToFront();

            picture_Boxes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            picture_Boxes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            picture_Boxes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);

            pictureBoxes.Add(picture_Boxes);
        }

        public void LoadPictureBox(int alpha, int red, int green, int blue, Point pos, Size size)
        {
            PictureBox picture_Boxes = new PictureBox();
            picture_Boxes.Image = null;

            picture_Boxes.Location = pos;
            picture_Boxes.Name = "picture_Boxes";
            picture_Boxes.Size = size;
            picture_Boxes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            picture_Boxes.TabIndex = 1;
            picture_Boxes.TabStop = false;

            this.Controls.Add(picture_Boxes);
            picture_Boxes.BackColor = Color.FromArgb(alpha, red, green, blue);
            picture_Boxes.BorderStyle = BorderStyle.FixedSingle;
            picture_Boxes.BringToFront();

            picture_Boxes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            picture_Boxes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            picture_Boxes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);

            pictureBoxes.Add(picture_Boxes);
        }

        public void FillList(PictureBox picture_Boxes)
        {
            PictureLoad pictureLoad = new PictureLoad();
            pictureLoad.Color = picture_Boxes.BackColor;
            pictureLoad.getColor(pictureLoad.Color);
            pictureLoad.Size = picture_Boxes.Size;
            pictureLoad.Pos = picture_Boxes.Location;
            pictureBoxesLoad.Add(pictureLoad);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxesLoad.Clear();
            for (int i = 0; i < pictureBoxes.Count; i++)
            {
                FillList(pictureBoxes[i]);
            }

            XmlSerializer picSerializer = new XmlSerializer(pictureBoxesLoad.GetType());
            using (Stream fStream = new FileStream("pictures.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {

                for (int i = 0; i < pictureBoxesLoad.Count; i++)
                {
                    pictureBoxesLoad[i].Pos = pictureBoxes[i].Location;
                    //pictureBoxesLoad[i].Color = pictureBoxes[i].BackColor;
                }

                picSerializer.Serialize(fStream, pictureBoxesLoad);
                fStream.Close();
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pictureBoxes.Count; i++)
            {
                pictureBoxes[i].Dispose();
            }
            pictureBoxesLoad.Clear();
            pictureBoxes.Clear();

            XmlSerializer picSerializer = new XmlSerializer(pictureBoxesLoad.GetType());
            using (Stream fStream = new FileStream("pictures.xml", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                pictureBoxesLoad = (List<PictureLoad>)picSerializer.Deserialize(fStream);
                fStream.Close();
            }

            for (int i = 0; i < pictureBoxesLoad.Count; i++)
            {
                LoadPictureBox(pictureBoxesLoad[i].Alpha, pictureBoxesLoad[i].Red, pictureBoxesLoad[i].Green, pictureBoxesLoad[i].Blue,
                    pictureBoxesLoad[i].Pos, pictureBoxesLoad[i].Size);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SIZE_PATTERN_ROW = Convert.ToInt32(textBox1.Text);         //dimension of a pattern in x direction 10 85
            
            if (SIZE_PATTERN_ROW < 10 || SIZE_PATTERN_ROW > 85)
            {
                MessageBox.Show("Допустимый диапазон для длины: от 10 до 85!", "Ошибка");
                return;
            }
            
            SIZE_PATTERN_COLUMN = Convert.ToInt32(textBox2.Text);      //dimension of a pattern in y direction 10 50

            if (SIZE_PATTERN_COLUMN < 10 || SIZE_PATTERN_COLUMN > 50)
            {
                MessageBox.Show("Допустимый диапазон для ширины: от 10 до 50!", "Ошибка");
                return;
            }

            pictureBox1.Size = new System.Drawing.Size(SIZE_PATTERN_ROW * SIZE_TILES + 1, SIZE_PATTERN_COLUMN * SIZE_TILES + 1);
            this.Size = new System.Drawing.Size(pictureBox1.Size.Width + 250, pictureBox1.Size.Height + 100);
            drawPattern1.createScene(pictureBox1);
            Point point_s = new Point(0, 0);
            int[,] EMPTY = CPattern.getEmptyPattern(SIZE_PATTERN_ROW, SIZE_PATTERN_COLUMN);
            drawPattern1.drawPattern(pictureBox1, EMPTY, SIZE_PATTERN_ROW, SIZE_PATTERN_COLUMN, SIZE_TILES, point_s, Color.LightGray, Color.LightGray, true, true);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, this.PointToClient(Cursor.Position));
            }
        }

        private void addQuadItem_Click(object sender, EventArgs e)
        {
            var form = new FormSetValues();
            form.ShowDialog();

            if (form.rectWidth == 0 || form.rectHeight == 0) return;
            if (form.rectWidth > SIZE_PATTERN_COLUMN || form.rectHeight > SIZE_PATTERN_ROW)
            {
                MessageBox.Show("Размеры плитки превышают размеры сетки!", "Ошибка");
                return;
            }
            CreatePictureBox(form.rectWidth * SIZE_TILES, form.rectHeight * SIZE_TILES);
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorButton.BackColor = colorDialog1.Color;
                frontColor = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < pictureBoxes.Count; i++)
            {
                pictureBoxes[i].Dispose();
            }

            pictureBoxes.Clear();
            pictureBoxesLoad.Clear();
        }

        private void Validation(object sender, KeyPressEventArgs e)
        {
            var Sender = ((TextBox)sender);

            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new About();
            form.Show();
        }
    }


}
