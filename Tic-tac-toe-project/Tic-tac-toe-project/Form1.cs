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
        public Form1()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            checkWinner();
            changeBtnText(btn);
            // throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
            checkWinner();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
            checkWinner();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
            checkWinner();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
            checkWinner();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
            checkWinner();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
            checkWinner();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
            checkWinner();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            changeBtnText(btn);
            checkWinner();
        }

        private void changeBtnText(Button btn)
        {
            if (btn.Text == "")
            {
                btn.Text = "X";
            }
        }

        private void checkWinner()
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
                    if (winningCombinations[possibleWin, index].Text == "X") //currentPlayer.Symbol
                    {
                        validInARow++;
                        if (validInARow == winningCombinations.GetLength(1))
                        {
                            MessageBox.Show("X wins");
                            return;
                        }
                    }
                }
            }
        }

        private bool isDraw()
        {
            Button[] allButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            foreach (var button in allButtons)
            {
                if (button.Text == "")
                    return false;
            }
            return true;
        }

    }
}
