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
            createGameLogic(i_BoardSize, i_IsPlayer2Computer);
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

        private void createGameLogic(int i_BoardSize, bool i_IsPlayer2Computer)
        {
            if (i_IsPlayer2Computer)
            {
                m_Game = new Game(Game.eGameType.AgainstTheCumputer, i_BoardSize);
            }
            else
            {
                m_Game = new Game(Game.eGameType.TwoHumanPlayers, i_BoardSize);
            }

            m_Game.OnBoardChanged += M_Game_OnBoardChanged;
        }

        private void M_Game_OnBoardChanged(int i_Row, int i_Col, char i_PlayerSign)
        {
            m_GameBoardButtons[i_Row, i_Col].Text = i_PlayerSign.ToString();
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

            if(!m_Game.isWinOrTie() && m_Game.GameType.Equals(Game.eGameType.AgainstTheCumputer))
            {
                m_Game.ComputerMove();
            }

            if (m_Game.isWinOrTie())
            {
                handleWinOrTie();
            }
        }

        private void handleWinOrTie()
        {

        }
        private void InitializeLabels(int i_BoardSize, string i_NameOfPlayer1, string i_NameOfPlayer2)
        {
            int labelHight = i_BoardSize * k_ButtonHight + i_BoardSize * k_SpaceBetweenButtons;
            int width = (int)((this.Width / 2) - (k_ButtonWidth));

            m_LabelPlayer1Name = new Label { Text = i_NameOfPlayer1, Location = new Point(width, labelHight), AutoSize = true };
            m_LabelPlayer1Score = new Label { Text = k_StartingScore, Location = new Point(m_LabelPlayer1Name.Location.X + m_LabelPlayer1Name.Width + 2, labelHight), AutoSize = true };
            m_LabelPlayer2Name = new Label { Text = i_NameOfPlayer2, Location = new Point((int)(this.Width / 2), labelHight), AutoSize = true };
            m_LabelPlayer2Score = new Label { Text = k_StartingScore, Location = new Point(m_LabelPlayer2Name.Location.X + m_LabelPlayer2Name.Width + 5, labelHight), AutoSize = true };

            this.Controls.AddRange(new Control[] { m_LabelPlayer1Name, m_LabelPlayer1Score, m_LabelPlayer2Name, m_LabelPlayer2Score });
        }
    }
}
