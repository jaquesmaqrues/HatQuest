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


namespace External_Tool
{

    public partial class CombatCreator : Form
    {

        List<Combat> combats;//List of combats
        int numSourceEnemies = 6;//Number of pictureBoxes in the top row, easy to add more by increasing int value
        PictureBox currentEnemy;//Picturebox that the user clicked on in the groupboxResultEnemy
        int currentSourceEnemy;//Int value for the source pictureBox each picturebox has an int value used for enemyNum in each enemy
        int numResultEnemies;//Number of pictureboxes in the groupboxResultEnemy

        public CombatCreator()
        {
            InitializeComponent();
            combats = new List<Combat>();
            currentEnemy = new PictureBox();
            numResultEnemies = groupBoxEnemResult.Controls.Count;

            //Creates the pictureBoxes in the top row
            for(int x = 0; x<numSourceEnemies; x++)
            {
                groupBoxEnemSource.Controls.Add(new PictureBox());
                groupBoxEnemSource.Controls[x].BackColor = Color.Black;
                groupBoxEnemSource.Controls[x].Size = new Size(100, 100);
                //Spaces the pictureboxes equally 
                groupBoxEnemSource.Controls[x].Location = new Point(groupBoxEnemSource.Size.Width/5 *x + 6, 19);
                groupBoxEnemSource.Controls[x].MouseDown += pictureBoxSource_MouseDown;
                //Gets the image from the resources folder
                ((PictureBox)groupBoxEnemSource.Controls[x]).Image = (Image)Properties.Resources.ResourceManager.GetObject("monster" + x);
                //Sets the SizeMode so the image is centered and the entire image is visible
                ((PictureBox)groupBoxEnemSource.Controls[x]).SizeMode = PictureBoxSizeMode.StretchImage;
            }
            //Goes through all the pictureBoxes in groupBoxResult
            foreach(PictureBox pb in groupBoxEnemResult.Controls)
            {
                pb.BackColor = Color.Black;
                pb.AllowDrop = true;
                pb.DragEnter += pictureBoxResult_DragEnter;
                pb.DragEnter += pictureBoxResult_DragDrop;
                pb.MouseDown += pictureBoxResult_MouseDown;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            //Creates the first entry in the combats list and add an option in comboBox
            combats.Add(new Combat(new Enemy[numResultEnemies]));
            comboBoxCombats.Items.Add("Combat 0");
            comboBoxCombats.Text = "Combat 0";
            comboBoxCombats.DropDownStyle = ComboBoxStyle.DropDownList;

            hScrollBar.Maximum = (numSourceEnemies - 1) * 90;
        }

        //Mousdown for source Pictureboxes 
        private void pictureBoxSource_MouseDown(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

                //Saves the index of the chosen pictureBox
                currentSourceEnemy = groupBoxEnemSource.Controls.IndexOf(((PictureBox)sender));

                //Code for drag and drop
                PictureBox temp = (PictureBox)sender;
                Image img = temp.Image;
                if (temp == null)
                    return;
                if (DoDragDrop(img, DragDropEffects.Move) == DragDropEffects.Move)
                    temp.Image = temp.Image;
        }

        private void pictureBoxResult_MouseDown(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            if (me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ((PictureBox)sender).Image = null;
                combats[comboBoxCombats.SelectedIndex][groupBoxEnemResult.Controls.IndexOf(((PictureBox)sender))] = null;
            }
        }

        //Code for Drag Entering a result pictureBox
        private void pictureBoxResult_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Move;
        }

        //Sets the pictureBox in results groupbox image when you drag an image
        private void pictureBoxResult_DragDrop(object sender, DragEventArgs e)
        {
            Image temp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            ((PictureBox)sender).Image = temp;

            //Adds a new enemy to the array
            combats[comboBoxCombats.SelectedIndex][groupBoxEnemResult.Controls.IndexOf(((PictureBox)sender))] = new Enemy(currentSourceEnemy);//UGLY
        }

        //Adds a new combat
        private void buttonAddCombat_Click(object sender, EventArgs e)
        {
            combats.Add(new Combat(new Enemy[numResultEnemies]));
            comboBoxCombats.Items.Add("Combat " + comboBoxCombats.Items.Count);
            comboBoxCombats.SelectedItem = comboBoxCombats.Items[comboBoxCombats.Items.Count - 1];

            //Resets picturebox color
            foreach(PictureBox pb in groupBoxEnemResult.Controls)
            {
                pb.BackColor = Color.Black;
            }
        }

        //Removes a combat
        private void buttonRemoveCombat_Click(object sender, EventArgs e)
        {
            //Checks if combat is the last combat
            if (comboBoxCombats.SelectedIndex != 0)
            {
                int currentIndex = comboBoxCombats.SelectedIndex - 1;
                combats.RemoveAt(comboBoxCombats.SelectedIndex);
                comboBoxCombats.Items.Remove(comboBoxCombats.Text);
                comboBoxCombats.SelectedIndex = currentIndex;
                //Renames all the combats in the comboBox list
                for (int x = 0; x < comboBoxCombats.Items.Count; x++)
                {
                    comboBoxCombats.Items[x] = "Combat " + x;
                }
                //Changes the images to the previous combat entry
                for (int x = 0; x < groupBoxEnemResult.Controls.Count; x++)
                {
                    if (combats[comboBoxCombats.SelectedIndex][x] != null)
                    {
                        ((PictureBox)groupBoxEnemResult.Controls[x]).Image = ((PictureBox)groupBoxEnemSource.Controls[combats[comboBoxCombats.SelectedIndex][x].EnemyNum]).Image;
                    }
                    else
                    {
                        ((PictureBox)groupBoxEnemResult.Controls[x]).Image = null;
                    }
                }
            }
        }

        //Changes images when the index in the comboBox is changed
        private void comboBoxCombats_SelectedIndexChanged(object sender, EventArgs e)
        {
            int z = 0;
            //Goes through all the pictureBoxes and changes image and difficulty
            foreach(PictureBox pb in groupBoxEnemResult.Controls)
            {
                pb.Image = null;
                if (combats.Count != 0 && combats[((ComboBox)sender).SelectedIndex][z] != null)
                {
                    pb.Image = ((PictureBox)groupBoxEnemSource.Controls[combats[((ComboBox)sender).SelectedIndex][z].EnemyNum]).Image;
                    
                }
                z++;
            }
        }

        //Loads a .combat file and draws it to the screen
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            //Opens file menu to choose file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open a combat file";
            openFileDialog.Filter = "Combat files (*.combat)|*.combat";

            if (openFileDialog.ShowDialog() == DialogResult.OK)//Checks if user chose a file
            {

                string fileName = openFileDialog.FileName;
                FileStream inStream = null;
                BinaryReader reader = null;

                try//Loads level values from file
                {
                    inStream = File.OpenRead(fileName);
                    reader = new BinaryReader(inStream);
                    combats.Clear();
                    comboBoxCombats.Items.Clear();
                    
                    int combatAmount = reader.ReadInt32();

                    for (int x = 0; x< combatAmount; x++)
                    {
                        combats.Add(new Combat(new Enemy[numResultEnemies]));
                        for (int y = 0; y < numResultEnemies; y++)
                        {
                            int readerValue = reader.ReadInt32();
                            if (readerValue != -1)
                            {
                                combats[x][y] = new Enemy(readerValue);
                                ((PictureBox)groupBoxEnemResult.Controls[y]).Image = ((PictureBox)groupBoxEnemSource.Controls[combats[x][y].EnemyNum]).Image;
                            }

                            else
                            {
                                combats[x][y] = null;
                            }
                        }
                        comboBoxCombats.Items.Add("Combot " + x);
                        comboBoxCombats.Text = "Combot " + x;
                    }

                    Text = "Combat Creator - " + openFileDialog.SafeFileName;
                    MessageBox.Show("File loaded successfully", "File loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (!Visible)
                    {
                        ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error reading!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //Creates save dialog object and opens file menu
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save a combat file";
            saveFileDialog.Filter = "Combat Files (*.combat)|.combat";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)//Checks if user clicked ok
            {
                string fileName = saveFileDialog.FileName;

                FileStream outStream = null;
                BinaryWriter writer = null;

                try//Saves width, height, and color in a set order
                {
                    outStream = File.OpenWrite(fileName);
                    writer = new BinaryWriter(outStream);

                    writer.Write((int)combats.Count);

                    for(int x = 0; x < combats.Count; x++)
                    {
                        for(int y = 0; y < numResultEnemies; y++)
                        {
                            if(combats[x][y] != null)
                            {
                                writer.Write((int)combats[x][y].EnemyNum);
                            }
                            else
                            {
                                writer.Write(-1);
                            }
                        }
                    }
                    Text = "Combat Creator - " + Path.GetFileName(saveFileDialog.FileName);
                    MessageBox.Show("File saved successfully", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error writting!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {

            if (e.OldValue != e.NewValue)
            {
                if (e.OldValue > e.NewValue)
                {
                    foreach (PictureBox pb in groupBoxEnemSource.Controls)
                    {
                        pb.Location = new Point(pb.Location.X + e.OldValue - e.NewValue, pb.Location.Y);
                    }
                }
                else
                {
                    foreach (PictureBox pb in groupBoxEnemSource.Controls)
                    {
                        pb.Location = new Point(pb.Location.X + e.OldValue - e.NewValue, pb.Location.Y);
                    }
                }
            }
        }
    }
}
