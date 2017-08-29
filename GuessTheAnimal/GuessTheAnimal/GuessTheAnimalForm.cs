using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessTheAnimal
{
    public partial class GuessTheAnimalForm : Form
    {
        readonly GuessTheAnimalGame _currentGame;
        public GuessTheAnimalForm()
        {
            InitializeComponent();
            _currentGame = new GuessTheAnimalGame();
            answerLabel.Visible = false;
            answerTextBox.Visible = false;
            UpdateMessageTextBox(_currentGame.GetQuestion());
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            _currentGame.AnswerQuestion(true, answerTextBox.Text);
            UpdateMessageTextBox(_currentGame.GetQuestion());
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            _currentGame.AnswerQuestion(false, answerTextBox.Text);
            UpdateMessageTextBox(_currentGame.GetQuestion());
        }

        private void UpdateMessageTextBox(string message)
        {
            messageTextBox.Text = message;
            answerTextBox.Text = "";
            switch (_currentGame.CurrentGamePhase)
            {
                case GuessTheAnimalGame.GamePhase.EndGame:
                    Dispose();
                    break;
                case GuessTheAnimalGame.GamePhase.AddAnimal:
                    answerLabel.Visible = true;
                    answerTextBox.Visible = true;
                    break;
                case GuessTheAnimalGame.GamePhase.AskToPlay:
                    answerLabel.Visible = false;
                    answerTextBox.Visible = false;
                    break;
            }
        }
    }
}
