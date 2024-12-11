using System.IO;

namespace QuizGame.Models
{
    public static class QuizTriviaApp
    {
        const int NUMBER_OF_OPTIONS = 4;
        const string FILENAME = "../../../QuizFile/Quiz_File.txt";

        #region Data Members
        private static int _numberOfQuestions;
        private static List<Quiz> _quizList = new List<Quiz>();
        #endregion

        #region Properties
        public static List<Quiz> QuizList
        {
            get { return _quizList; }
            set
            {
                if (value is null)
                    throw new ArgumentNullException("value");

                _quizList = value;
            }
        }

        public static int NumberOfQuestions
        {
            get { return _numberOfQuestions; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                _numberOfQuestions = value;
            }
        }
        #endregion

        #region Data Method
        public static void ReadQuizContentFromFile()
        {
            StreamReader streamReader;

            try
            {
                if (File.Exists(FILENAME))
                {
                    streamReader = new StreamReader(FILENAME);
                    string text = streamReader.ReadLine();
                    NumberOfQuestions = 0;

                    while (text != null)
                    {
                        //If the next question is available, the number of questions increase by one
                        NumberOfQuestions++;

                        Quiz quiz = new Quiz();
                        quiz.Question = text;

                        for (int i = 0; i < NUMBER_OF_OPTIONS; i++)
                        {
                            quiz.Options[i] = streamReader.ReadLine();
                        }

                        quiz.Answer = streamReader.ReadLine();

                        QuizList.Add(quiz);

                        text = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
