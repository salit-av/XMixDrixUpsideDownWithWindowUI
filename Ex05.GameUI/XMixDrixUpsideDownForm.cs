using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Ex05.GameLogic;

namespace Ex05.GameUI
{
    public partial class XMixDrixUpsideDownForm : Form
    {
        private const int k_ButtonHight = 40;
        private const int k_ButtonWidth = 50;
        private const int k_SpaceBetweenButtons = 5;
        private const string k_StartingScore = "0";

        private Game m_Game;
        private GameButton[,] m_GameBoardButtons;
        private Label m_LabelPlayer1Name;
        private Label m_LabelPlayer1Score;
        private Label m_LabelPlayer2Name;
        private Label m_LabelPlayer2Score;

        public XMixDrixUpsideDownForm(int i_BoardSize, string i_NameOfPlayer1, string i_NameOfPlayer2, bool i_IsPlayer2Computer)
        {
            createGameLogic(i_BoardSize, i_IsPlayer2Computer, i_NameOfPlayer1, i_NameOfPlayer2);
            InitializeComponnent(i_BoardSize, i_NameOfPlayer1, i_NameOfPlayer2);
            this.Text = "TicTacToeMisere";
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new Padding(10, 10, 10, 15);
        }

        private void InitializeComponnent(int i_BoardSize, string i_NameOfPlayer1, string i_NameOfPlayer2)
        {
            initializeGameBoardButtons(i_BoardSize);
            InitializeLabels(i_BoardSize, i_NameOfPlayer1, i_NameOfPlayer2);
        }

        private void createGameLogic(int i_BoardSize, bool i_IsPlayer2Computer, string i_NameOfPlayer1, string i_NameOfPlayer2)
        {
            if (i_IsPlayer2Computer)
            {
                m_Game = new Game(Game.eGameType.AgainstTheCumputer, i_BoardSize, i_NameOfPlayer1, i_NameOfPlayer2);
            }
            else
            {
                m_Game = new Game(Game.eGameType.TwoHumanPlayers, i_BoardSize, i_NameOfPlayer1, i_NameOfPlayer2);
            }

            m_Game.OnBoardChanged += m_Game_OnBoardChanged;
            m_Game.OnGameTie += m_Game_OnGameTie;
            m_Game.OnGameLosed += m_Game_OnGameLosed;
            m_Game.OnChangeTurn += m_Game_OnChangeTurn;
        }

        private void m_Game_OnChangeTurn(bool i_IsFirstPlayerTurn)
        {
            if (i_IsFirstPlayerTurn)
            {
                m_LabelPlayer1Name.Font = new Font(m_LabelPlayer1Name.Font, FontStyle.Bold);
                m_LabelPlayer1Score.Font = new Font(m_LabelPlayer1Score.Font, FontStyle.Bold);
                m_LabelPlayer2Name.Font = new Font(m_LabelPlayer2Name.Font, FontStyle.Regular);
                m_LabelPlayer2Score.Font = new Font(m_LabelPlayer2Score.Font, FontStyle.Regular);
            }
            else
            {
                m_LabelPlayer1Name.Font = new Font(m_LabelPlayer1Name.Font, FontStyle.Regular);
                m_LabelPlayer1Score.Font = new Font(m_LabelPlayer1Score.Font, FontStyle.Regular);
                m_LabelPlayer2Name.Font = new Font(m_LabelPlayer2Name.Font, FontStyle.Bold);
                m_LabelPlayer2Score.Font = new Font(m_LabelPlayer2Score.Font, FontStyle.Bold);
            }
        }

        private void m_Game_OnBoardChanged(int i_Row, int i_Col, char i_PlayerSign)
        {
            m_GameBoardButtons[i_Row, i_Col].Text = i_PlayerSign.ToString();
            m_GameBoardButtons[i_Row, i_Col].Enabled = false;
        }

        private void m_Game_OnGameTie(int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            updateLabelScore(i_FirstPlayerScore, i_SecondPlayerScore);
            TieOrLosedForm tieOrLosedForm = new TieOrLosedForm("A Tie!", "Tie!");
            
            if (tieOrLosedForm.ShowDialog() == DialogResult.Yes)
            {
               startNewGame();
            }
            else
            {
                this.Close();
            }
        }

        private void m_Game_OnGameLosed(string i_PlayerName, int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            updateLabelScore(i_FirstPlayerScore, i_SecondPlayerScore);
            TieOrLosedForm tieOrLosedForm = new TieOrLosedForm("A Win!", string.Format("{0}{1}{2}", "The winner is ", i_PlayerName, "!"));

            if (tieOrLosedForm.ShowDialog() == DialogResult.Yes)
            {
                startNewGame();
            }
            else
            {
                this.Close();
            }
        }

        private void updateLabelScore(int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            m_LabelPlayer1Score.Text = i_FirstPlayerScore.ToString();
            m_LabelPlayer2Score.Text = i_SecondPlayerScore.ToString();
        }

        private void startNewGame()
        {
            m_Game.StartNewGame();

            foreach(GameButton gameButton in m_GameBoardButtons)
            {
                gameButton.Text = "";
                gameButton.Enabled = true;
            }
        }

        private void initializeGameBoardButtons(int i_BoardSize)
        {
            m_GameBoardButtons = new GameButton[i_BoardSize, i_BoardSize];

            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_GameBoardButtons[i, j] = new GameButton(i, j);
                    m_GameBoardButtons[i, j].Location = new Point(j * k_ButtonWidth + (j + 1) * k_SpaceBetweenButtons, i * k_ButtonHight + (i + 1) * k_SpaceBetweenButtons);
                    m_GameBoardButtons[i, j].Size = new Size(k_ButtonWidth, k_ButtonHight);
                    m_GameBoardButtons[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    m_GameBoardButtons[i, j].Click += new EventHandler(buttonClicked);
                    this.Controls.Add(m_GameBoardButtons[i, j]);
                }
            }
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            int row = (sender as GameButton).Row;
            int col = (sender as GameButton).Col;

            m_Game.HumanMove(row, col);

            if(m_Game.GameType.Equals(Game.eGameType.AgainstTheCumputer))
            {
                m_Game.ComputerMove();
            }
        }

        private void InitializeLabels(int i_BoardSize, string i_NameOfPlayer1, string i_NameOfPlayer2)
        {
            int labelHight = i_BoardSize * k_ButtonHight + i_BoardSize * k_SpaceBetweenButtons;
            int width = (int)((this.Width / 2) - (2 * k_ButtonWidth));

            m_LabelPlayer1Name = new Label { Text = i_NameOfPlayer1, Location = new Point(width, labelHight), AutoSize = true };
            m_LabelPlayer1Score = new Label { Text = k_StartingScore, Location = new Point(m_LabelPlayer1Name.Width +5, labelHight), AutoSize = true, BackColor = Color.Red};
            m_LabelPlayer1Name.Font = new Font(m_LabelPlayer1Name.Font, FontStyle.Bold);
            m_LabelPlayer1Score.Font = new Font(m_LabelPlayer1Score.Font, FontStyle.Bold);
            m_LabelPlayer2Name = new Label { Text = i_NameOfPlayer2, Location = new Point((int)(this.Width / 2), labelHight), AutoSize = true };
            m_LabelPlayer2Score = new Label { Text = k_StartingScore, Location = new Point(m_LabelPlayer2Name.Width + m_LabelPlayer2Name.Text.Length *10, labelHight), AutoSize = true, BackColor = Color.Green};

            this.Controls.AddRange(new Control[] { m_LabelPlayer1Name, m_LabelPlayer1Score, m_LabelPlayer2Name, m_LabelPlayer2Score });
        }
    }
}
