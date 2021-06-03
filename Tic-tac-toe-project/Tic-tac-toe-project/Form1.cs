using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_tac_toe_project
{
    class Move
    {
        public int row, col;
    };

    public partial class Form1 : Form
    {
        bool isPlayer = true;

        public Form1()
        {
            Button[,] Board = { { button1, button2, button3 },
                                { button4, button5, button6 },
                                { button7, button8, button9 }};
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
            // throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);

        }

        private void changeBtnText(Button btn)
        {
            if (btn.Text == "")
            {
                btn.Text = "X";
                checkWinner(symbolString(isPlayer));
                isPlayer = false;
            }
        }

        private string symbolString(bool isPlayer)
        {
            return isPlayer ? "X" : "O";
        }

        private bool checkWinner(string CurrentPlayer)
        {
            Button[,] winningCombinations = new Button[8, 3]
            {
                //rows
                { button1, button2, button3 }, { button4, button5, button6 }, { button7, button8, button9 },
                //columns
                { button1, button4, button7},{button2, button5, button8 },{ button3, button6, button9},
                //diagonals
                {button1, button5, button9 },{ button3, button5, button7 }
            };

            int validInARow;

            for (int possibleWin = 0; possibleWin < winningCombinations.GetLength(0); possibleWin++)
            {

                validInARow = 0;
                for (int index = 0; index < winningCombinations.GetLength(1); index++)
                {
                    if (winningCombinations[possibleWin, index].Text == CurrentPlayer) //currentPlayer.Symbol
                    {
                        validInARow++;
                        if (validInARow == winningCombinations.GetLength(1))
                        {
                            MessageBox.Show(CurrentPlayer + " wins");
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private int checkScore()
        {
            if (checkWinner(symbolString(isPlayer))) return +10;
            else if (checkWinner(symbolString(!isPlayer))) return -10;
            else return 0;
        }

        private void findBestMove(Button[,] board)
        {
            int bestValue = int.MinValue;

            Move bestMove = new Move { row = -1, col = -1 };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j].Text == "")
                    {
                        board[i, j].Text = symbolString(isPlayer);

                        int moveVal = minimax(board, 0, false);

                        board[i, j].Text = "";

                        if (moveVal > bestValue)
                        {
                            bestMove.row = i;
                            bestMove.col = j;
                            bestValue = moveVal;
                        }
                    }
                }
            }
            board[bestMove.row, bestMove.col].Text = symbolString(isPlayer);
            isPlayer = true;
        }

        private bool movesLeft()
        {
            Button[] allButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            foreach (var button in allButtons)
            {
                if (button.Text == "")
                    return true;
            }
            return false;
        }

    }
}
