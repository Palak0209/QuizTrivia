using Microsoft.Win32;
using QuizGame.Models;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for FinalReport.xaml
    /// </summary>
    public partial class FinalReport : Window, INotifyPropertyChanged
    {
        private string _filename = "Quiz_Results.csv";

        private int _score;

        public FinalReport()
        {
            InitializeComponent();
            DataContext = this;

            _score = 0;
            AddStackPanelElementsDynamically();
        }

        private int i;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void AddStackPanelElementsDynamically()
        {
            for (i = 0; i < QuizTriviaApp.NumberOfQuestions; i++)
            {
                TextBlock textBlock1 = new TextBlock
                {
                    Text = $"Question {i + 1}:",
                    FontSize = 25,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5),
                    Background = Brushes.AliceBlue
                };

                QuestionsStackPanel.Children.Add(textBlock1);

                TextBlock textBlock2 = new TextBlock
                {
                    Text = $"Selected: {SelectedAnswer(i)}",
                    FontSize = 25,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5),
                    Background = Brushes.LightYellow
                };

                textBlock2.Foreground = Brushes.CornflowerBlue;

                QuestionsStackPanel.Children.Add(textBlock2);

                TextBlock textBlock3 = new TextBlock
                {
                    Text = Question(i),
                    FontSize = 25,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5),
                    Background = Brushes.AliceBlue
                };

                AnswersStackPanel.Children.Add(textBlock3);

                TextBlock textBlock4 = new TextBlock
                {
                    Text = $"Right Answer: {CorrectAnswer(i)}",
                    FontSize = 25,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5),
                    Background = Brushes.LightYellow
                };

                textBlock4.Foreground = Brushes.ForestGreen;

                AnswersStackPanel.Children.Add(textBlock4);

                //Changes score according to each selected answer and correct answer
                Score = "";
            }
        }

        public string Question(int index)
        {
            return QuizTriviaApp.QuizList[index].Question;
        }

        public string SelectedAnswer(int index)
        {
            return QuizTriviaApp.QuizList[index].SelectedOption;
        }

        public string CorrectAnswer(int index)
        {
            return QuizTriviaApp.QuizList[index].Answer;
        }

        public string Score
        {
            get { return $"{_score}/{QuizTriviaApp.NumberOfQuestions}"; }
            set
            {
                if (QuizTriviaApp.QuizList[i].IsRight())
                    _score++;
            }
        }

        private void Save_Button_Clicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            //The results are being saved to a csv file since it is easier to read the results when the question, selected answer and the right answer are placed in columns neatly.

            saveFileDialog.Title = "Save CSV File";
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";

            // Set the initial directory to the executable location
            saveFileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

            saveFileDialog.FileName = _filename;

            if (saveFileDialog.ShowDialog() == true)
            {
                _filename = saveFileDialog.FileName;
                // Perform the saving logic
                SaveResultsToFile();
            }
        }

        private void SaveResultsToFile()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"Quiz Attempt Results\n");

                for (int i = 0; i < QuizTriviaApp.NumberOfQuestions; i++)
                {
                    sb.AppendLine($"{QuizTriviaApp.QuizList[i].Question},{QuizTriviaApp.QuizList[i].SelectedOption},{QuizTriviaApp.QuizList[i].Answer}");
                }
                sb.AppendLine($"\nTotal Score: {_score}/{QuizTriviaApp.NumberOfQuestions}");

                File.AppendAllText(_filename, sb.ToString());

                MessageBox.Show("File saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}