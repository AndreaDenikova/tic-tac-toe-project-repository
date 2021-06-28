using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            buttonNewGame.Visible = false;
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
                checkWinner(symbolString(!isPlayer), true); 
            }
            else if (movesLeft() == false && checkWinner("X", false) == false)
            {
                disableBoard();
                MessageBox.Show("tie");
                buttonNewGame.Visible = true;
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
                                buttonNewGame.Visible = true;
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
                int best = int.MinValue;

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
                int best = int.MaxValue;

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

        private async void findBestMove(Button[,] board)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int bestValue = int.MinValue;

            Move bestMove = new Move { row = -1, col = -1 };

            Dictionary<Move, int> results = new Dictionary<Move, int>();

            var moves = new List<Move>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j].Text == "")
                    {
                        Move emptyMove = new Move { row = i, col = j };
                        moves.Add(emptyMove);
                    }
                }
            }

            var tasks = new List<Task<int>>();

            foreach (var move in moves)
            {
                Task<int> t = new Task<int>(() =>
                {
                    if (board[move.row, move.col].InvokeRequired)
                    {
                        return (int)Invoke(new Func<int>(() => helperFunction(board, move, results)));
                        //return (int)Invoke((MethodInvoker)delegate { helperFunction(board, move, results); });
                    }
                    return helperFunction(board, move, results);

                });
                tasks.Add(t);
                t.Start();
            }

            await Task.WhenAll(tasks.ToArray());

            foreach (var keyValuePair in results)
            {
                if (keyValuePair.Value > bestValue)
                {
                    bestValue = keyValuePair.Value;
                    bestMove = keyValuePair.Key;
                }
            }

            board[bestMove.row, bestMove.col].Text = symbolString(isPlayer);
            isPlayer = true;
            stopWatch.Stop();
            MessageBox.Show("elasped time " + stopWatch.Elapsed);
        }

        private int helperFunction(Button[,] board, Move move, Dictionary<Move, int> results)
        {
            board[move.row, move.col].Text = symbolString(isPlayer);
            int moveVal = minimax(board, 0, false);
            results[move] = moveVal;
            board[move.row, move.col].Text = "";
            return moveVal;
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

        private void clearBoard()
        {
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";
            button9.Text = "";
        }

        private void enableBoard()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            clearBoard();
            enableBoard();
            buttonNewGame.Visible = false;
        }
    }

    class Move
    {
        public int row, col;
    };
}
