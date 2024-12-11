using QuizGame.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int i;
        private Button[] buttons;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            //Buttons are declared here since there will always be 4 options for quiz questions
            buttons = new Button[] { Option1, Option2, Option3, Option4 };

            //When this contructor is called, the trivia app instance is assigned the quiz content read from the file
            QuizTriviaApp.ReadQuizContentFromFile();
            i = 0;
        }

        public string NextQuestion
        {
            get { return QuizTriviaApp.QuizList[i].Question; }
        }

        public string[] NextOptions
        {
            get { return QuizTriviaApp.QuizList[i].Options; }
        }

        //This property can be null since the user can select no option and click on submit button
        public string? SelectedOption
        {
            get { return QuizTriviaApp.QuizList[i].SelectedOption; }
            set { QuizTriviaApp.QuizList[i].SelectedOption = value; }
        }

        public int TotalNoOfQuestions
        {
            get { return QuizTriviaApp.NumberOfQuestions; }
        }

        public string GetResult()
        {
            if (QuizTriviaApp.QuizList[i].IsRight())
                return "Right answer :)";

            else
            {
                //If no option is selected, the value of selected option will be changed to NA
                if (SelectedOption is null)
                {
                    SelectedOption = "NA";
                    return "No option selected!";
                }

                else
                    return "Wrong answer :(";
            }
        }

        private void Option_Button_Clicked(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                SelectedOption = clickedButton.Content.ToString();

                //Changing color of the selected option
                foreach (Button button in buttons)
                    button.Background = Brushes.LightGray;

                clickedButton.Background = Brushes.LightBlue;
            }
        }

        private void Submit_Button_Clicked(object sender, RoutedEventArgs e)
        {
            Result.Text = GetResult();

            //Disabling the buttons once submit button is clicked
            foreach (Button button in buttons)
                button.IsEnabled = false;

            //Hiding the visiblity of submit button and showing next question button
            submit_button.Visibility = Visibility.Hidden;
            submit_button.IsEnabled = false;

            //The last question will be at index length-1
            if (i < TotalNoOfQuestions - 1)
            {
                nextquestion_button.Visibility = Visibility.Visible;
                nextquestion_button.IsEnabled = true;
            }

            //If the question is the last one, next question will be visible but disabled
            else
            {
                nextquestion_button.Visibility = Visibility.Visible;
                nextquestion_button.IsEnabled = false;
            }
        }

        private void Next_Question_Button_Click(object sender, RoutedEventArgs e)
        {
            //Enabling the buttons once next question button is clicked
            foreach (Button button in buttons)
                button.IsEnabled = true;

            //Changing the button colors back to blue
            foreach (Button button in buttons)
                button.Background = Brushes.LightGray;

            //Changing the result back to empty
            Result.Text = "";

            //Hiding the visiblity of next question button and showing submit button
            submit_button.Visibility = Visibility.Visible;
            submit_button.IsEnabled = true;

            nextquestion_button.Visibility = Visibility.Hidden;
            nextquestion_button.IsEnabled = false;

            //Increasing the value of i to move on to the next index in the quiz list
            i++;

            try
            {
                Question.Text = NextQuestion;
                Option1.Content = NextOptions[0];
                Option2.Content = NextOptions[1];
                Option3.Content = NextOptions[2];
                Option4.Content = NextOptions[3];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Question number changed
            QNo.Text = $"Ques. {i + 1} out of {TotalNoOfQuestions}";
        }

        private void End_Quiz_Button_Clicked(object sender, RoutedEventArgs e)
        {
            //The last question will be at index length-1
            if (i < TotalNoOfQuestions)
            {
                //This loop goes from i to the end of questions in case the user ends the quiz before attempting all the questions
                for (int j = i; i < TotalNoOfQuestions; i++)
                {
                    /* If the user selected an option and instead of going to the next question, decided to end the quiz,
                     * the selected answer for that question should not be changed to NA */
                    if (SelectedOption is null)
                        SelectedOption = "NA";
                }
            }

            //Final Report xaml class is passing the trivia app object that contains the quiz content
            FinalReport finalReport = new FinalReport();
            finalReport.Show();
            this.Close();
        }
    }
}