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

namespace text_editor
{
    public partial class Form : System.Windows.Forms.Form
    {
        private string szCurrentFile = String.Empty;

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private SaveFileDialog saveFileDialog = new SaveFileDialog();

        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            openFileDialog.FileName = @"data\text.txt";
            openFileDialog.Filter =
            "Текстовые файлы (*.txt)|*.txt|All files (*.*)|*.*";

            saveFileDialog.FileName = @"data\text.txt";
            saveFileDialog.Filter =
                     "Текстовые файлы (*.txt)|*.txt|All files (*.*)|*.*";
        }

        private void update_title ( )
        {
            this.Text = "Текстовый редактор";
            if (szCurrentFile != String.Empty)
                this.Text += " - " + szCurrentFile;
        }

        private void reload_file ( )
        {
            if (szCurrentFile != String.Empty)
            {
                Array.Clear ( textBox1.Lines, 0, textBox1.Lines.Length );

                try
                {
                    var arrFileContent = File.ReadLines ( szCurrentFile );
                    textBox1.Lines = arrFileContent.ToArray ( );
                    update_title ( );
                }
                catch (Exception exception)
                {
                    MessageBox.Show ( exception.Message, "Ошибка" );
                }
            }
        }

        private void save_file()
        {
            if (szCurrentFile != String.Empty)
            {
                try
                {
                    File.WriteAllLines ( szCurrentFile, textBox1.Lines );
                    update_title();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка");
                }
            }
            else
            {
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    szCurrentFile = saveFileDialog.FileName;
                    save_file();
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if ( openFileDialog.ShowDialog (this) == DialogResult.OK )
                szCurrentFile = openFileDialog.FileName;
            openFileDialog.Reset();

            reload_file();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save_file ( );
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit ( );
        }
    }
}
