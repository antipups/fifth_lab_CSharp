using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.IO.StreamReader;

namespace fifth_lab
{
    
    public partial class Form1 : Form
    {
        Person program = new Person();
        public Form1()
        {
            InitializeComponent();
            label1.Visible = false;
            label2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button5.Visible = false;
            label3.Visible = false;
            button6.Visible = false;
            String currentDirectory = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().IndexOf("bin", StringComparison.Ordinal));
            program.path_to_xml = currentDirectory + "testFile.xml";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
            label1.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button5.Visible = true;
            label3.Visible = true;
            button6.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            program.write_template();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            program.write_to_xml();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
            // label3.Text = program.read_from_xml();
            label3.Text = program.read_from_xml();
            label3.Visible = true;
            pictureBox1.Image = program.get_image();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (program.write_user_data(textBox1.Text, textBox2.Text, openFileDialog1.InitialDirectory + openFileDialog1.FileName))
            {
                label3.Text = @"Запись добавлена";
                label3.ForeColor = Color.Green;
            }
            else
            {
                label3.Text = @"Ошибка при вводе";
                label3.ForeColor = Color.Red;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().IndexOf("bin", StringComparison.Ordinal)) + "For5Lab";
            while (true)
            {
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.IndexOf(".png", StringComparison.Ordinal) > 0 || openFileDialog1.FileName.IndexOf(".jpg", StringComparison.Ordinal) > 0 || openFileDialog1.FileName.IndexOf(".jpeg", StringComparison.Ordinal) > 0)
                    break;
                    
            }
            pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Text = @"Добавленная картинка";
        }
    }
}