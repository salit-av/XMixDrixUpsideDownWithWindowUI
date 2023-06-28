using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex05.GameLogic
{
    public class Game
    {
        public enum eGameType
        {
            AgainstTheCumputer,
            TwoHumanPlayers
        }

        private enum ePlayersSigns
        {
            X = 'X',
            O = 'O'
        }

        private const int k_InvalidBoardLocation = -1;
        private readonly eGameType r_GameType;
        private bool m_IsFirstPlayerMove;
        private bool m_IsPlayerLosed;
        private bool m_IsTie;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private Board m_Board;
        public Game(eGameType i_GameType, int i_BoardSize)
        {
            r_GameType = i_GameType;
        }

        public eGameType GameType
        {
            get
            {
                return r_GameType;
            }
        }

        public void HumanMove(int i_Row, int i_Col)
        {
            if (m_IsFirstPlayerMove)
            {
                m_FirstPlayer.HumanMove(ref m_Board, i_Row, i_Col);
            }
            else if (r_GameType == eGameType.TwoHumanPlayers)
            {
                m_SecondPlayer.HumanMove(ref m_Board, i_Row, i_Col);
            }
            checkGameStatus(i_Row, i_Col);
            m_IsFirstPlayerMove = !m_IsFirstPlayerMove;
        }

        public void ComputerMove()
        {
            int row = k_InvalidBoardLocation, column = k_InvalidBoardLocation;

            m_SecondPlayer.SmarterComputerMove(ref m_Board, ref row, ref column);
            checkGameStatus(row, column);
            m_IsFirstPlayerMove = !m_IsFirstPlayerMove;
        }

        private void checkGameStatus(int row, int col)
        {
            checkIfTie();
            checkIfLose(row, col);
        }

        private void checkIfTie()
        {
            if (m_Board.IsGameFinishedWithTie())
            {
                m_FirstPlayer.Score++;
                m_SecondPlayer.Score++;
                m_IsTie = true;
            }
        }
        private void checkIfLose(int i_Row, int i_Column)
        {
            if (m_Board.IsGameFinishedWithLost(getCurrentPlayerSign(), i_Row, i_Column))
            {
                addPlayerScore();
                m_IsPlayerLosed = true;
            }
        }

        private char getCurrentPlayerSign()
        {
            char resSign;

            if (m_IsFirstPlayerMove)
            {
                resSign = m_FirstPlayer.Sign;
            }
            else
            {
                resSign = m_SecondPlayer.Sign;
            }

            return resSign;
        }
        private void addPlayerScore()
        {
            if (m_IsFirstPlayerMove)
            {
                m_SecondPlayer.Score++;
            }
            else
            {
                m_FirstPlayer.Score++;
            }
        }
        public bool isWinOrTie()
        {
            return m_IsPlayerLosed && m_IsTie;
        }
    }
}