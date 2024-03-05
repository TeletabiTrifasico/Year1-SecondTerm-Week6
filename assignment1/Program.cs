namespace assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.Start();
        }
        void Start()
        {
            ChessPiece[,] chessboard = new ChessPiece[8, 8];
            InitChessboard(chessboard);
            DisplayChessboard(chessboard);
            PlayChess(chessboard);
        }
        void DoMove(ChessPiece[,] chessboard, Position from, Position to)
        {
            CheckMove(chessboard, from, to);
            chessboard[to.row, to.column] = chessboard[from.row, from.column];
            chessboard[from.row, from.column] = null;
        }
        void PlayChess(ChessPiece[,] chessboard)
        {
            string move = "";
            do
            {
                try
                {
                    Console.Write("Enter move (e.g. a2 a3): ");
                    move = Console.ReadLine();
                    if (move != "stop")
                    {
                        string[] moves = move.Split(' ');
                        Position from = String2Position(moves[0]);
                        Position to = String2Position(moves[1]);
                        Console.WriteLine($"move from {moves[0]} to {moves[1]}");
                        Console.WriteLine(" ");
                        DoMove(chessboard, from, to);
                        DisplayChessboard(chessboard);
                    }
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exception.Message);
                    Console.ResetColor();
                }
            } while (move != "stop");
        }
        void CheckMove(ChessPiece[,] chessboard, Position from, Position to)
        {
            bool exception = true;
            int hor = Math.Abs(from.column - to.column);
            int ver = Math.Abs(from.row - to.row);

            if (chessboard[from.row, from.column] == null)
            {
                throw new Exception("No chess piece at from-position");
            }
            if (chessboard[to.row, to.column] != null)
            {
                if (chessboard[to.row, to.column].Color == chessboard[from.row, from.column].Color)
                {
                    throw new Exception("Can not take a chess piece of same color");
                }
            }
            switch (chessboard[from.row, from.column].Type)
            {
                case ChessPieceType.Pawn:
                {
                    if (hor != 0 || ver != 1)
                    {
                        exception = false;
                    }
                    break;
                }
                case ChessPieceType.Rook:
                {
                    if (hor * ver != 0)
                    {
                        exception = false;
                    }
                    break;
                }
                case ChessPieceType.Knight:
                {
                    if (hor * ver != 2)
                    {
                        exception = false;
                    }
                    break;
                }
                case ChessPieceType.Bishop:
                {
                    if (hor != ver)
                    {
                        exception = false;
                    }
                    break;
                }
                case ChessPieceType.King:
                {
                    if (hor != 1 || ver != 1 || hor != 1 && ver != 1)
                    {
                        exception = false;
                    }
                    break;
                }
                case ChessPieceType.Queen:
                {
                    if (hor * ver != 0 || hor == ver)
                    {
                        exception = false;
                    }
                    break;
                }
            }

            if (exception == false)
            {
                throw new Exception($"Invalid move for chess piece {chessboard[from.row, from.column].Type}");
            }
        }
        void DisplayChessPiece(ChessPiece chessPiece)
        {
            if (chessPiece == null)
                Console.Write("   ");
            else
            {
                if (chessPiece.Color == ChessPieceColor.White)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                if (chessPiece.Type == ChessPieceType.Pawn)
                {
                    Console.Write($" p ");
                }
                else if (chessPiece.Type == ChessPieceType.Rook)
                {
                    Console.Write($" r ");
                }
                else if (chessPiece.Type == ChessPieceType.Knight)
                {
                    Console.Write($" k ");
                }
                else if (chessPiece.Type == ChessPieceType.Bishop)
                {
                    Console.Write($" b ");
                }
                else if (chessPiece.Type == ChessPieceType.Queen)
                {
                    Console.Write($" Q ");
                }
                else if (chessPiece.Type == ChessPieceType.King)
                {
                    Console.Write($" K ");
                }
            }
        }
        Position String2Position(string pos)
        {
            Position position = new Position();
            int column = pos[0] - 'a';
            int row = 8 - int.Parse(pos[1].ToString());
            if (column < 0 || column > 7 || row < 0 || row > 7)
            {
                throw new Exception($"invalid position: {pos}");
            }
            position.column = column;
            position.row = row;
            return position;
        }
        void InitChessboard(ChessPiece[,] chessboard)
        {
            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    chessboard[i, j] = null;
                }
            }
            PutChessPieces(chessboard);
        }
        void DisplayChessboard(ChessPiece[,] chessboard)
        {
            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                Console.Write($"{chessboard.GetLength(0) - i} ");
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    DisplayChessPiece(chessboard[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.Write(" ");
            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                if (i == 0)
                {
                    Console.Write($"  a");
                }
                else if (i == 1)
                {
                    Console.Write($"  b");
                }
                else if (i == 2)
                {
                    Console.Write($"  c");
                }
                else if (i == 3)
                {
                    Console.Write($"  d");
                }
                else if (i == 4)
                {
                    Console.Write($"  e");
                }
                else if (i == 5)
                {
                    Console.Write($"  f");
                }
                else if (i == 6)
                {
                    Console.Write($"  g");
                }
                else if (i == 7)
                {
                    Console.WriteLine($"  h");
                }
            }
        }
        void PutChessPieces(ChessPiece[,] chessboard)
        {
            ChessPieceType[] order = { ChessPieceType.Rook, ChessPieceType.Knight, ChessPieceType.Bishop, ChessPieceType.Queen, ChessPieceType.King, ChessPieceType.Bishop, ChessPieceType.Knight, ChessPieceType.Rook };
            for (int j = 0; j < chessboard.GetLength(1); j++)
            {
                chessboard[0, j] = new ChessPiece();
                chessboard[1, j] = new ChessPiece();
                chessboard[6, j] = new ChessPiece();
                chessboard[7, j] = new ChessPiece();
                chessboard[0, j].Type = chessboard[7, j].Type = order[j];
                chessboard[1, j].Type = chessboard[6, j].Type = ChessPieceType.Pawn;
                chessboard[0, j].Color = chessboard[1, j].Color = ChessPieceColor.Black;
                chessboard[6, j].Color = chessboard[7, j].Color = ChessPieceColor.White;
            }
        }
    }
}
