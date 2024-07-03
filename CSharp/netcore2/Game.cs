﻿using System;
using System.Collections.Generic;
using System.Linq;
using trivia;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players;

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        private Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
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

        public String CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return (this._players.Count >= 2);
        }

        public bool Add(String playerName)
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
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(this._players[currentPlayer].Name + " is getting out of the penalty box");
                    this._players[currentPlayer].Move(roll);

                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(this._players[currentPlayer].Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                this._players[currentPlayer].Move(roll);

                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }

        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
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
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    currentPlayer++;
                    if (currentPlayer == this._players.Count) currentPlayer = 0;
                    this._players[currentPlayer].AddPurse();

                    bool winner = this._players[currentPlayer].DidPlayerWin();
                    
                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == this._players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                this._players[currentPlayer].AddPurse();

                bool winner = this._players[currentPlayer].DidPlayerWin();
                currentPlayer++;
                if (currentPlayer == this._players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(this._players[currentPlayer].Name + " was sent to the penalty box");
            this._players[currentPlayer].PutInPenaltyBox();

            currentPlayer++;
            if (currentPlayer == this._players.Count) currentPlayer = 0;
            return true;
        }
    }

}
