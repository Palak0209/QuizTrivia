using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Quiz
    {
        const int NUMBER_OF_OPTIONS = 4;

        #region Data Members

        private string _question;
        private string _answer;
        private string[] _options = new string[NUMBER_OF_OPTIONS];
        private string _selectedOption;

        #endregion

        #region Properties

        public string Question
        {
            get { return _question; }
            set
            {
                if (value is null)
                    throw new ArgumentNullException("value");

                _question = value;
            }
        }

        public string Answer
        {
            get { return _answer; }
            set
            {
                if (value is null)
                    throw new ArgumentNullException("value");

                _answer = value;
            }
        }

        public string[] Options
        {
            get { return _options; }
            set
            {
                if (value is null)
                    throw new ArgumentNullException("value");

                _options = value;
            }
        }

        public string SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                if (value is null)
                    throw new ArgumentNullException("value");

                _selectedOption = value;
            }
        }

        #endregion

        #region Method Members

        public bool IsRight()
        {
            if (this.SelectedOption is null)
                return false;

            return this.SelectedOption == this.Answer;
        }

        #endregion
    }
}
