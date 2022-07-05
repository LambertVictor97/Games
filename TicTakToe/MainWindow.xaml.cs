using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTakToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private members
        /// <summary>
        /// holds the current result of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if is players 1's turn (x) or player 2's turn (0)
        /// </summary>

        private bool mPlayer1Turn;

        /// <summary>
        /// ture if the game has ended;
        /// </summary>

        private bool mGameEnded;
        #endregion

        #region constructor

        public MainWindow()
        {
            InitializeComponent();
            newGame();
        }
        #endregion

        /// <summary>
        /// start a new game and clears all values to the start
        /// </summary>
        public void newGame()
        {
            // create a new blank arry of free cells
            mResults = new MarkType[9];
            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //make sure player 1 starts the game
            mPlayer1Turn = true;

            
            //interate every button on the grid...
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //change content, background and foreground, to default
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            //make sure the game hasn't finished
            mGameEnded = false;
        }

        /// <summary>
        /// handles a button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click (object sender, RoutedEventArgs e)
        {
            if(mGameEnded == true)
            {
                if(MessageBox.Show("Do u want to start a new game?", "new game?",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    newGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }

            // cast the sender to a button
            var button= (Button) sender;

            //finds the button in the arry (marktype)
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            //set the cell value based on wich players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //set button.text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            //Change the noughts to red
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //Toggle the players turn (bool flip to true and false)
            mPlayer1Turn ^= true;

            //check for a winner
            CheckForWinner();
        }
        /// <summary>
        /// chec if there is a winner of the 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            //check for diagonal wins
            //
            //-diagonal 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //game ends
                mGameEnded = true;

                //hightlight winning cells in Green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.LightGreen;
            }
            //check for diagonal wins
            //
            //-diagonal 1
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //game ends
                mGameEnded = true;

                //hightlight winning cells in Green
                Button0_2.Background = Button1_1.Background = Button2_1.Background = Brushes.LightGreen;
            }
            //check for vertical wins
            //-
            //-colums 0
            //
            if (mResults[0] !=MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //game ends
                mGameEnded = true;

                //hightlights winning cells in Green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.LightGreen;
            }
            //check for vertical wins
            //
            //-colums 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //game ends
                mGameEnded = true;

                //hightlights winning cells in Green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.LightGreen;
            }
            //check for vertical wins
            //
            //-colums 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //game ends
                mGameEnded = true;

                //hightlights winning cells in Green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.LightGreen;
            }

            //check for horizontal wins
            //
            //-row 0
            //
            if (mResults[0] !=MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //game ends
                mGameEnded = true;

                //highlights winning cells in Green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.LightGreen;
            }
            //check for horizontal wins
            //
            //-row 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //game ends
                mGameEnded = true;

                //highlights winning cells in Green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.LightGreen;
            }
            //check for horizontal wins
            //
            //-row 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //game ends
                mGameEnded = true;

                //highlights winning cells in Green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.LightGreen;
            }
            if (!mResults.Any(f => f == MarkType.Free))
            {
                //game end
                mGameEnded = true;

                //turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    //change content, background and foreground, to default
                    button.Background = Brushes.Orange;
                });
            }
        }
    }
}
