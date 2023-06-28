using System;
using System.Windows.Forms;
using Ex05.GameLogic;

namespace Ex05.GameUI
{
    internal class GameButton : Button
    {
        private int m_Row;
        private int m_Col;
        public GameButton(int i_Row, int i_Col)
        {
            this.m_Row = i_Row;
            this.m_Col = i_Col;
        }

        internal int Row
        {
            get
            {
                return m_Row;
            }
        }
        internal int Col
        {
            get
            {
                return m_Col;
            }
        }
    }
}