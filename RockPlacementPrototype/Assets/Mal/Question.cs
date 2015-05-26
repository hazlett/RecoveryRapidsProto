using System.Collections.Generic;

namespace OhioState.CanyonAdventure
{
    class Question
    {
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }

        public Question(string questionText)
        {
            QuestionText = questionText;
        }

        public Question(string questionText, List<Answer> answers)
        {
            QuestionText = questionText;
            Answers = new List<Answer>(answers);
        }
    }
}
