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

    public partial class Form1 : Form
    {
        bool isPlayer = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
            checkWinner(symbolString(isPlayer), true);
            isPlayer = false;

            if (movesLeft())
            {
                Button[,] Board = { { button1, button2, button3 },
                                { button4, button5, button6 },
                                { button7, button8, button9 }};

                findBestMove(Board);
                checkWinner(symbolString(!isPlayer), true); ;
            }
            else if (movesLeft() == false && checkWinner("X", false) == false)
            {
                disableBoard();
                MessageBox.Show("tie");
            }
        }

        private void changeBtnText(Button btn)
        {
            if (btn.Text == "")
            {
                btn.Text = "X";
            }
        }

        private string symbolString(bool isPlayer)
        {
            return isPlayer ? "X" : "O";
        }

        private bool checkWinner(string CurrentPlayer, bool showMessage)
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
                            if (showMessage)
                            {
                                disableBoard();
                                MessageBox.Show(CurrentPlayer + " wins");
                            }

                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private int checkScore()
        {
            if (checkWinner(symbolString(isPlayer), false)) return +10;
            else if (checkWinner(symbolString(!isPlayer), false)) return -10;
            else return 0;
        }

        private int minimax(Button[,] board, int depth, Boolean isMax)
        {
            int score = checkScore();

            if (score == 10)
            {
                return score;
            }

            if (score == -10)
            {
                return score;
            }

            if (movesLeft() == false) //tie
            {
                return 0;
            }

            if (isMax)
            {
                int best = -1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j].Text == "")
                        {
                            board[i, j].Text = symbolString(isPlayer);

                            best = Math.Max(best, minimax(board, depth + 1, !isMax));

                            board[i, j].Text = "";
                        }
                    }
                }
                return best;
            }

            else
            {
                int best = 1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j].Text == "")
                        {
                            board[i, j].Text = symbolString(!isPlayer); //opponent

                            best = Math.Min(best, minimax(board, depth + 1, !isMax));

                            board[i, j].Text = "";
                        }
                    }
                }
                return best;
            }
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

        private void disableBoard()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
        }
    }

    class Move
    {
        public int row, col;
    };
}
