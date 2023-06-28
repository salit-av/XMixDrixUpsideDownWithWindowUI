using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex05.GameLogic
{
    internal struct Player
    {
        private char m_Sign;
        private int m_Score;
        private string m_NameOfPlayer;

        internal Player(char i_Sign, int i_Score, string i_NameOfPlayer)
        {
            m_Sign = i_Sign;
            m_Score = i_Score;
            m_NameOfPlayer = i_NameOfPlayer;
        }

        internal char Sign
        {
            get
            {
                return m_Sign;
            }

            set
            {
                m_Sign = value;
            }
        }

        internal int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        internal string Name
        {
            get
            {
                return m_NameOfPlayer;
            }
        }
        internal void SmarterComputerMove(ref Board io_Board, ref int io_Row, ref int io_Column)
        {
            int i = 0;

            getBlankRandomRowAndCol(io_Board, out io_Row, out io_Column);

            while (i < 3 && io_Board.IsThisCellCloseSequence(io_Row, io_Column, m_Sign))
            {
                getBlankRandomRowAndCol(io_Board, out io_Row, out io_Column);
                i++;
            }

            io_Board.AddPlayerSign(io_Row, io_Column, m_Sign);
        }

        private void getBlankRandomRowAndCol(Board i_Board, out int o_Row, out int o_Column)
        {
            Random random = new Random();
            GetRowAndCol(random, i_Board.BoardSize, out o_Row, out o_Column);
            while (!i_Board.IsThisCellClear(o_Row, o_Column))
            {
                GetRowAndCol(random, i_Board.BoardSize, out o_Row, out o_Column);
            }
        }
        private void GetRowAndCol(Random i_Random, int i_BoardSize, out int o_Row, out int o_Column)
        {
            o_Row = i_Random.Next(0, i_BoardSize);
            o_Column = i_Random.Next(0, i_BoardSize);
        }

        internal void HumanMove(ref Board io_Board, int i_Row, int i_Column)
        {
            io_Board.AddPlayerSign(i_Row, i_Column, m_Sign);
        }
    }
}
