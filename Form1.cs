using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            throw new System.NotImplementedException();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (program.write_user_data(textBox1.Text, textBox2.Text))
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
    }
}