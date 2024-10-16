using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwoPointers
{
    /*
    class Move
    { 
        public int x;
        public int y;


        public Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Player
    {
        int id;
        char symbol;

        public Player(char symbol)
        {
            this.symbol = symbol;
        }

        public Move ProvideMove()
        {
            
        }
    }

    class Game
    {
        private char[,] board;
        private int moves;
        Player player1;
        Player player2;
        Player winner;

        public Game(char[,] board, Player player1, Player player2)
        {
            this.board = board;
            this.Player1 = player1;
            this.player2 = player2;
            moves = 0;
        }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public bool IsOver()
        {
            return moves == board.Length * board.Length;
        }

        bool Move(Player player, Move provideMove)
        {
            // check boundres
            // if not already taken

            board[provideMove.x, provideMove.y] = player.symbol;
            moves++;

            if(IsOver() && IsWinningMove(player, provideMove))
            {
                winner = player;
            }
        }

        private bool IsWinningMove(Player player, Move move)
        {
            for (int i = 0; i <board.Length; i++){
                if(player.symnol != board[move.x][i])
                {
                    return false;
                }
            }

            for (int i = 0; i < board.Length; i++)
            {
                if (player.symnol != board[board.Length - 1 - move.x][i])
                {
                    return false;
                }
            }



            return true;
        }
    }
    class TicTacToe
    {

        Game game;
        enum Status
        {
            INPROGRESS, DONE
        }
        
  

        public TicTacToe(int n) {
            game = new Game(new char[n, n], new Player('X'), new Player('0'));
        }

        public Player? StartGame()
        {
            bool isPlayer1Turn = true;
            while (!game.IsOver())
            {
                if(isPlayer1Turn)
                {
                    game.Move(game.Player1, game.Player1.ProvideMove());
                }
                else
                {
                    game.Move(game.Player2, game.Player1.ProvideMove());
                }

                isPlayer1Turn = !isPlayer1Turn;
            }

            return game.Winner ?? null;
        }

    }
    */
}
