using System;
using System.Collections.Generic;
using System.Linq;

namespace trivia
{
    internal static class Questions
    {
        private static LinkedList<string> popQuestions = new LinkedList<string>();
        private static LinkedList<string> scienceQuestions = new LinkedList<string>();
        private static LinkedList<string> sportsQuestions = new LinkedList<string>();
        private static LinkedList<string> rockQuestions = new LinkedList<string>();

        public static void PrepareQuestions()
        {
            for (int i = 0; i < 50; i++)
            {
                Questions.popQuestions.AddLast("Pop Question " + i);
                Questions.scienceQuestions.AddLast(("Science Question " + i));
                Questions.sportsQuestions.AddLast(("Sports Question " + i));
                Questions.rockQuestions.AddLast("Rock Question " + i);
            }
        }

        public static void Ask(string questionCategory)
        {
            if (questionCategory == "Pop")
            {
                Console.WriteLine(Questions.popQuestions.First());
                Questions.popQuestions.RemoveFirst();
            }
            if (questionCategory == "Science")
            {
                Console.WriteLine(Questions.scienceQuestions.First());
                Questions.scienceQuestions.RemoveFirst();
            }
            if (questionCategory == "Sports")
            {
                Console.WriteLine(Questions.sportsQuestions.First());
                Questions.sportsQuestions.RemoveFirst();
            }
            if (questionCategory == "Rock")
            {
                Console.WriteLine(Questions.rockQuestions.First());
                Questions.rockQuestions.RemoveFirst();
            }
        }
    }
}
