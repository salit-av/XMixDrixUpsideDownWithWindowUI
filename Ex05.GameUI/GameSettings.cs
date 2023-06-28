using System;
using System.Windows.Forms;

namespace Ex05.GameUI
{
    public partial class GameSettings : Form
    {
        public GameSettings()
        {
            InitializeComponent();
        }

        private void GameSettings_Load(object sender, EventArgs e)
        {

        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = checkBoxPlayer2.Checked;

            if (textBoxPlayer2.Enabled)
            {
                textBoxPlayer2.Text = "";
            }
            else
            {
                textBoxPlayer2.Text = "[Computer]";
            }
        }

        private void numericUpDownRows_TextChanged(object sender, EventArgs e)
        {
            numericUpDownCols.Text = numericUpDownRows.Text;
        }

        private void numericUpDownCols_TextChanged(object sender, EventArgs e)
        {
            numericUpDownRows.Text = numericUpDownCols.Text;
        }

        private void buttonStart_Clicked(object sender, EventArgs e)
        {
            this.Hide();
            XMixDrixUpsideDownForm xMixDrixUpsideDownForm = new XMixDrixUpsideDownForm(int.Parse(numericUpDownRows.Text), textBoxPlayer1.Text, textBoxPlayer2.Text, !checkBoxPlayer2.Checked);
            xMixDrixUpsideDownForm.ShowDialog();
            this.Close();
        }
    }
}