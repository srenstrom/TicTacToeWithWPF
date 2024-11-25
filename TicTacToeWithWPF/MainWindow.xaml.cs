using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToeWithWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string playerMove = "X";
        string computerMove = "O";
        int xWinsCounter = 0;
        int oWinsCounter = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;
            if (IsGameOver())
            {
                return;
            }
            PlayerMove(button);
            if (IsGameOver())
            {
                return;
            }
            ComputerMove();
            if (IsGameOver())
            {
                return;
            }

        }
        private void PlayerMove(Button btn)
        {
            btn.Content = playerMove;
            btn.IsEnabled = false;
        }
        private void ComputerMove()
        {
            Random rnd = new Random();
            var availableButtons = GetAvailableButtons().Where(btn => btn.IsEnabled == true).ToList();
            if (availableButtons.Count == 0)
            {
                return;
            }
            var index = rnd.Next(availableButtons.Count);
            var chosenBtn = availableButtons[index];
            chosenBtn.Content = computerMove;
            chosenBtn.IsEnabled = false;
        }
        private bool IsGameOver()
        {
            var availableButtons = GetAvailableButtons().Where(button => button.IsEnabled == true).ToList();
            var possibleWinningCombinations = new List<(Button, Button, Button)>
        {
            (btnA1, btnA2, btnA3), //: --- row 1
            (btnB1, btnB2, btnB3), //: --- row 2
            (btnC1, btnC2, btnC3), //: --- row 3
            (btnA1, btnB1, btnC1), //: --- row 4
            (btnA2, btnB2, btnC2), //: --- row 5
            (btnA3, btnB3, btnC3), //: --- row 6
            (btnA1, btnB2, btnC3), //: \ upper left corner to down right corner
            (btnA3, btnB2, btnC1)  //: / down right corner to upper left corner

        };
            foreach (var (btn1, btn2, btn3) in possibleWinningCombinations)
            {
                if (!btn1.IsEnabled && !btn2.IsEnabled && !btn3.IsEnabled && btn1.Content == btn2.Content && btn1.Content == btn3.Content)
                {
                    ConfirmGameOver(btn1.Content.ToString());
                    return true;
                }
            }
            if (availableButtons.Count == 0)
            {
                ConfirmGameOver("");
                return true;
            }
            return false;
        }
        private void ConfirmGameOver(string? winner)
        {
            if (winner == "X")
            {
                CurrentWinner.Content = $"Du vann!";
                CurrentWinner.Visibility = Visibility.Visible;
                XWins.Content = $"Dina vinster: {++xWinsCounter}";
            }
            if (winner == "O")
            {
                CurrentWinner.Content = $"Datorn vann!";
                CurrentWinner.Visibility = Visibility.Visible;
                OWins.Content = $"Datorns vinster: {++oWinsCounter}";
            }
            else if (winner == "")
            {
                CurrentWinner.Content = "Oavgjort!";
                CurrentWinner.Visibility = Visibility.Visible;
            }
            var allButtons = GetAvailableButtons();
            foreach (var btn in allButtons)
            {
                btn.Content = "";
                btn.IsEnabled = false;

            }
            Reset.Visibility = Visibility.Visible;
        }
        private List<Button> GetAvailableButtons()
        {
            List<Button> buttons =
            [
                btnA1,
                btnA2,
                btnA3,
                btnB1,
                btnB2,
                btnB3,
                btnC1,
                btnC2,
                btnC3
            ];
            return buttons;
        }
        private void ResetButtons_Click(object sender, RoutedEventArgs e)
        {
            CurrentWinner.Visibility = Visibility.Hidden;
            var allButtons = GetAvailableButtons();
            foreach (var btn in allButtons)
            {
                btn.IsEnabled = true;
            }
            Reset.Visibility = Visibility.Hidden;
        }
    }
}
    
