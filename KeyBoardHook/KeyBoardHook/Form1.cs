using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace KeyBoardHook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        GlobalKeyboardHook gHook;

        private void Form1_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook();
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
        }

        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            textBox1.Text += ((char)e.KeyValue).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gHook.hook();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           gHook.unhook();
            
        }
         private void Form1_ForeClosing(object sender, FormClosingEventArgs e)
        {
            gHook.unhook();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.ToString() == "s")
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Doc|.txt", ValidateNames = true })
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            sw.WriteLineAsync(textBox1.Text);
                        }
                    }
            }
        }
    }
}
