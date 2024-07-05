using System;
using System.Collections.Generic;
using System.Linq;

namespace trivia
{
    internal class QuestionsDeck
    {
        private static readonly Lazy<QuestionsDeck> _lazyValue = new Lazy<QuestionsDeck>(() => new QuestionsDeck());
        private static QuestionsDeck _instance => QuestionsDeck._lazyValue.Value;

        public static QuestionsDeck GetQuestionsDeck() => QuestionsDeck._instance;

        private Dictionary<string, QuestionList> _categoryToquestionListMap;

        private QuestionsDeck()
        {
            this._categoryToquestionListMap = new Dictionary<string, QuestionList>();

            PrepareQuestionsPercategory("Pop");
            PrepareQuestionsPercategory("Science");
            PrepareQuestionsPercategory("Sports");
            PrepareQuestionsPercategory("Rock");

            void PrepareQuestionsPercategory(string category)
            {
                var questions = new List<string>();

                for (int i = 0; i < 50; i++)
                {
                    questions.Add($"{category} Question " + i);
                }

                this._categoryToquestionListMap.Add(category, new QuestionList(questions));
            }
        }

        public void AskNextQuestionFromCategory(string questionCategory)
        {
            this._categoryToquestionListMap[questionCategory].AskNextQuestion();
        }
    }
}
