using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WindowsProject
{
    public partial class Notepad : Form
    {
        public int index;
        public Notepad()
        {
            InitializeComponent();
            richTextBox1.TabIndex = 5;

            undoToolStripMenuItem.Enabled = false;

            richTextBox1.Focus();
            this.Text = "Untitled.txt";
        }
        


        private void NewFile()
        {
            if (richTextBox1.Modified==true)
            {
                DialogResult dr = MessageBox.Show("Do you want to save the file", "save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr.Equals(DialogResult.Yes))           
                {
                    SaveFile();
                    richTextBox1.Clear();
                    this.Text = "Untitled.txt";
                }
                else if (dr.Equals(DialogResult.No))          
                {
                    richTextBox1.Clear();
                    this.Text = "Untitled.txt";
                }
            }
            else
            {
                richTextBox1.Clear();
                this.Text = "Untitled.txt";
            }
        }



        private void OpenFile()
        {
            openFileDialog1.DefaultExt = "txt";     
            openFileDialog1.Filter = "Text Files|*.txt";   

            openFileDialog1.FileName = string.Empty;//setting filename box to blank       

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName == String.Empty)
                {
                    return;
                }
                else
                {
                    //reading or loading selected file into richtextbox      
                    this.richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                    int lastSlashPos = openFileDialog1.FileName.LastIndexOf("\\");
                    string fileName = openFileDialog1.FileName.Substring(lastSlashPos + 1);
                    this.Text = fileName;
                }

            }
        }



        private void SaveFile()
        {
            //setting title of savefiledialog to Save As  
            saveFileDialog1.Title = "Save As";
            saveFileDialog1.Filter = "Text Document|*.txt";//applied filter       

            saveFileDialog1.DefaultExt = "txt";//applied default extension    

                if (this.Text != "Untitled.txt")
                {                  
                    richTextBox1.SaveFile(this.Text, RichTextBoxStreamType.PlainText);
                }
                else if (this.Text == "Untitled.txt")
                {
                saveFileDialog1.FileName = this.Text;

                if (saveFileDialog1.ShowDialog()==DialogResult.OK)
                    { 
                        File.WriteAllText(saveFileDialog1.FileName, this.richTextBox1.Text);
                        this.Text = saveFileDialog1.FileName;
                    }
                }
        }
            

        private void SaveFileAs()
        {
            saveFileDialog1.Filter = "Text Document|*.txt";

            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.FileName = this.Text;

            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                this.Text = saveFileDialog1.FileName;
            }
        }



        // to open a new file //
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }
        

        // to open a file //
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified == true)//checking either richtext box have entered value or not     
            {
                DialogResult dr = MessageBox.Show("Do you want to save changes to the opened file", "unsaved document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No)
                {
                    richTextBox1.Modified = false;
                    OpenFile();//calling OpenFile user defined function              
                }
                else
                {
                    if (this.Text == "Untitled.txt")//checking form Title to Untitled-Digital Diary      
                    {
                        ///Calling SaveFile and OpenFile user defined functions     
                        SaveFile();
                        OpenFile();
                    }
                    else
                    {
                        DialogResult dr1 = MessageBox.Show("the text in the file has been changed.Do you want to save the changes", "Open", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dr1 == DialogResult.Yes)
                        {
                            richTextBox1.SaveFile(this.Text);
                            OpenFile();
                        }
                        else
                        {
                            OpenFile();
                        }
                    }
                }
            }
            else
            {
                OpenFile();
            }
        }



        // to save a file //
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }



        // to save as a file //
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }



        // to page Setup //
        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.Document = printDocument1;
            pageSetupDialog1.ShowDialog();
        }



        //To Print the Page//
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }



        // to exit and close notepad//
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified == true)
            {
                DialogResult dr = MessageBox.Show("Do you want to save the file before exiting", "unsaved file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    SaveFile();
                    richTextBox1.Modified = false;
                    Close();
                }
                else
                {
                    richTextBox1.Modified = false;
                    Close();
                }
            }
            else
            {
                Close();
            }          
        }



        //For Undo//
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
                richTextBox1.Undo();
        }



        //For Redo//
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
                richTextBox1.Redo();
        }



        //For Cut//
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }



        //For Copy//
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }



        //For Paste//
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }



        //For Delete//
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text=richTextBox1.Text.Remove(richTextBox1.SelectionStart, richTextBox1.SelectionLength);
        }



        //For Select All//
        private void selectallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }



        //For DateTime//
        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += Convert.ToString(DateTime.Now);
            
        }



        //For Wordwrap//
        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked)
                wordWrapToolStripMenuItem.Checked = richTextBox1.WordWrap = false;
            else
                wordWrapToolStripMenuItem.Checked = richTextBox1.WordWrap = true;
        }



        //For Font//
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = fontToolStripMenuItem.Font;
            DialogResult dr = fontDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog1.Font;
            }
        }



        //For Zoom In//
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float currentSize = richTextBox1.Font.Size;
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, currentSize + 2);
        }



        //For Zoom Out//
        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float currentSize = richTextBox1.Font.Size;
            if (currentSize > 4)
            {
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, currentSize - 2);
            }
        }



        //For Restore Default Zoom//
        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, 12);
        }



        //For StatusBar//
        private void stToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stToolStripMenuItem.Checked)
            {
                stToolStripMenuItem.Checked = statusStrip1.Visible = false;

            }
            else
            {
                stToolStripMenuItem.Checked = statusStrip1.Visible = true;

            }
        }



        //For About//
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Notepad Application\nVersion 1.0\nDeveloped By: Niranjan Samal", "About");
        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = true;

            toolStripStatusLabel1.Text = "Ln " + (richTextBox1.GetLineFromCharIndex(Int32.MaxValue) + 1) + "   Col " + richTextBox1.Text.Length;
        }
    }
}
