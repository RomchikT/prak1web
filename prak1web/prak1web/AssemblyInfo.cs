using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private bool isPlayerXTurn;
        private bool isGameOver;

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            isPlayerXTurn = !isPlayerXTurn; // Switch the starting player
            isGameOver = false;

            foreach (var button in gameGrid.Children)
            {
                if (button is Button)
                {
                    var btn = (Button)button;
                    btn.Content = "";
                    btn.IsEnabled = true;
                }
            }

            resultText.Text = "";

            if (!isPlayerXTurn)
            {
                MakeRobotMove(); // Make the first move if the player is "O"
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isGameOver)
            {
                return;
            }

            var button = (Button)sender;
            if (!string.IsNullOrEmpty(button.Content.ToString()))
            {
                return;
            }

            if (isPlayerXTurn)
            {
                button.Content = "X";
            }
            else
            {
                button.Content = "O";
            }

            button.IsEnabled = false;

            if (CheckForWinner())
            {
                isGameOver = true;
                resultText.Text = isPlayerXTurn ? "Победа X!" : "Победа O!";
                return;
            }

            if (CheckForTie())
            {
                isGameOver = true;
                resultText.Text = "Ничья!";
                return;
            }

            isPlayerXTurn = !isPlayerXTurn;

            if (!isPlayerXTurn)
            {
                MakeRobotMove();
            }
        }

        private bool CheckForWinner()
        {
            string[] values = new string[9];

            int index = 0;
            foreach (var button in gameGrid.Children)
            {
                if (button is Button)
                {
                    var btn = (Button)button;
                    values[index] = btn.Content.ToString();
                    index++;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(values[i * 3]) &&
                    values[i * 3] == values[i * 3 + 1] &&
                    values[i * 3] == values[i * 3 + 2])
                {
                    return true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(values[i]) &&
                    values[i] == values[i + 3] &&
                    values[i] == values[i + 6])
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(values[0]) &&
                values[0] == values[4] &&
                values[0] == values[8])
            {
                return true;
            }

            if (!string.IsNullOrEmpty(values[2]) &&
                values[2] == values[4] &&
                values[2] == values[6])
            {
                return true;
            }

            return false;
        }

        private bool CheckForTie()
        {
            foreach (var button in gameGrid.Children)
            {
                if (button is Button)
                {
                    var btn = (Button)button;
                    if (string.IsNullOrEmpty(btn.Content.ToString()))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void MakeRobotMove()
        {
            Random random = new Random();

            while (true)
            {
                int index = random.Next(0, 9);
                var button = (Button)gameGrid.Children[index];

                if (string.IsNullOrEmpty(button.Content.ToString()))
                {
                    button.Content = "O";
                    button.IsEnabled = false;
                    break;
                }
            }

            if (CheckForWinner())
            {
                isGameOver = true;
                resultText.Text = isPlayerXTurn ? "Победа X!" : "Победа O!";
                return;
            }

            if (CheckForTie())
            {
                isGameOver = true;
                resultText.Text = "Ничья!";
                return;
            }
            isPlayerXTurn = !isPlayerXTurn;
        }
    }
}