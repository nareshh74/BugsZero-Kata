using System;
using System.Collections.Generic;
using trivia;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players;

        private int currentPlayer = 0;

        private Game()
        {
            Questions.PrepareQuestions();
            }

        public Game(List<string> players) : this()
        {
            if (players.Count < 2)
            {
                throw new ArgumentException("You need at least 2 players to play the game");
            }
            if (players.Count > 6)
            {
                throw new ArgumentException("You need at most 6 players to play the game");
            }

            this._players = new List<Player>();

            foreach (var player in players)
            {
                this.Add(player);
            }
        }

        public bool IsPlayable()
        {
            return (this._players.Count >= 2);
        }

        private bool Add(String playerName)
        {

            this._players.Add(new Player(playerName));

            Console.WriteLine(playerName + " was Added");
            Console.WriteLine("They are player number " + this._players.Count);
            return true;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(this._players[currentPlayer].Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (this._players[currentPlayer].IsInPenaltyBox())
            {
                if (roll % 2 != 0)
                {
                    this._players[currentPlayer].MightGetOutOfPenaltyBox();

                    Console.WriteLine(this._players[currentPlayer].Name + " is getting out of the penalty box");
                    this._players[currentPlayer].Move(roll);

                    Console.WriteLine("The category is " + this.CurrentCategory());
                    this.AskQuestion();
                }
                else
                {
                    Console.WriteLine(this._players[currentPlayer].Name + " is not getting out of the penalty box");
                    this._players[currentPlayer].CantGetOutOfPenaltyBox();
                }

            }
            else
            {

                this._players[currentPlayer].Move(roll);

                Console.WriteLine("The category is " + this.CurrentCategory());
                this.AskQuestion();
            }

        }

        private void AskQuestion()
        {
            Questions.Ask(this.CurrentCategory());
        }


        private String CurrentCategory()
        {
            int currentPlayerPlace = this._players[currentPlayer].GetPlace();
            if (currentPlayerPlace == 0) return "Pop";
            if (currentPlayerPlace == 4) return "Pop";
            if (currentPlayerPlace == 8) return "Pop";
            if (currentPlayerPlace == 1) return "Science";
            if (currentPlayerPlace == 5) return "Science";
            if (currentPlayerPlace == 9) return "Science";
            if (currentPlayerPlace == 2) return "Sports";
            if (currentPlayerPlace == 6) return "Sports";
            if (currentPlayerPlace == 10) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered()
        {
            if (this._players[currentPlayer].IsInPenaltyBox())
            {
                if (this._players[currentPlayer].CanGetOutOfPenaltyBox())
                {
                    Console.WriteLine("Answer was correct!!!!");
                    this.GiveTurnToNextPlayer();
                    this._players[currentPlayer].AddPurse();

                    bool winner = this._players[currentPlayer].DidPlayerWin();

                    return winner;
                }
                else
                {
                    this.GiveTurnToNextPlayer();
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                this._players[currentPlayer].AddPurse();

                bool winner = this._players[currentPlayer].DidPlayerWin();
                this.GiveTurnToNextPlayer();

                return winner;
            }
        }

        private void GiveTurnToNextPlayer()
        {
            currentPlayer++;
            if (currentPlayer == this._players.Count) currentPlayer = 0;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(this._players[currentPlayer].Name + " was sent to the penalty box");
            this._players[currentPlayer].PutInPenaltyBox();

            this.GiveTurnToNextPlayer();
            return true;
        }
    }

}
