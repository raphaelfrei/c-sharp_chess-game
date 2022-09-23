/* *************************************************

           Raphael Frei - (c) 2022.
        Chess game with Console and C#

        https://github.com/raphaelfrei

    Versions:
        - 1.0.0: 23/09/2022

************************************************* */

namespace CHSS {
    public class Program {

        // Board management
        static public string[,] board = new string[8, 8];

        // Pieces 
        static readonly string pawn = "P";
        static readonly string bishop = "B";
        static readonly string knight = "N";
        static readonly string rook = "R";
        static readonly string queen = "Q";
        static readonly string king = "K";
        static readonly string empty = "  ";
        static string? color;

        // Game Settings
        static bool endOfGame = false;
        static bool isWhite = true;

        // Current Piece Positions
        static int curPosX;
        static int curPosY;
        static int nextPieceX;
        static int nextPieceY;

        public static void Main() {

            EmptyBoard();
            SetBoard();

            PrintBoard();

            // While the game doesn't end
            while (!endOfGame) {

                // If is white turn
                if (isWhite) {

                    Console.Clear();
                    PrintBoard();

                    WhiteMove();

                    Console.ReadLine();

                    isWhite = false;
                }

                // If not white, is black's turn
                if (!isWhite) {

                    Console.Clear();
                    PrintBoard();

                    BlackMove();

                    Console.ReadLine();

                    isWhite = true;
                }
            }
        }

        // Control White's Moves
        static void WhiteMove() {
            Console.WriteLine("\nThis is White turn: Which piece do you want to move?\nType Piece X:");

            curPosX = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Type Piece Y:");
            curPosY = Convert.ToInt32(Console.ReadLine());

            string curPiece = GetPiece(curPosX - 1, curPosY - 1);

            // If piece is Black or Empty, cannot select - Choose piece to move
            if (curPiece.StartsWith("B") || curPiece.StartsWith(" ")) {
                Console.Clear();
                PrintBoard();
                
                Console.WriteLine("You can't select a Black Piece. Please try again.\n");

                WhiteMove();

            } else
                Console.WriteLine($"Do you want to move {GetFullPieceName(curPiece)}?\n<YES> or <OTHER>");

            bool status = GetYesOrNo(Console.ReadLine());

            if (status) {
                bool canMoveNext = false;

                while (!canMoveNext) {
                    nextPieceX = 0;
                    nextPieceY = 0;

                    Console.WriteLine("Type next position X:");
                    nextPieceX = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Type next position Y:");
                    nextPieceY = Convert.ToInt32(Console.ReadLine());

                    canMoveNext = ValidadeMove(curPiece, curPosX - 1, curPosY - 1, nextPieceX - 1, nextPieceY - 1);

                    if (!canMoveNext)
                        Console.WriteLine("Move is invalid. Please try again.");

                }

                board[curPosX - 1, curPosY - 1] = empty;
                board[nextPieceX - 1, nextPieceY - 1] = curPiece;

            } else {
                Console.Clear();
                PrintBoard();
                WhiteMove();
            }
        }

        // Control Black's Moves
        static void BlackMove() {
            Console.WriteLine("\nThis is Black turn: Which piece do you want to move?\nType Piece X:");

            curPosX = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Type Piece Y:");
            curPosY = Convert.ToInt32(Console.ReadLine());

            string curPiece = GetPiece(curPosX - 1, curPosY - 1);

            // If piece is White or Empty, cannot select - Choose piece to move
            if (curPiece.StartsWith("W") || curPiece.StartsWith(" ")) {
                Console.Clear();
                PrintBoard();

                Console.WriteLine("You can't select a White Piece. Please try again.\n");

                BlackMove();

            } else
                Console.WriteLine($"Do you want to move {GetFullPieceName(curPiece)}?\n<YES> or <OTHER>");

            bool status = GetYesOrNo(Console.ReadLine());

            if (status) {
                bool canMoveNext = false;

                while (!canMoveNext) {
                    nextPieceX = 0;
                    nextPieceY = 0;

                    Console.WriteLine("Type next position X:");
                    nextPieceX = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Type next position Y:");
                    nextPieceY = Convert.ToInt32(Console.ReadLine());

                    canMoveNext = ValidadeMove(curPiece, curPosX - 1, curPosY - 1, nextPieceX - 1, nextPieceY - 1);

                    if (!canMoveNext)
                        Console.WriteLine("Move is invalid. Please try again.");

                }

                board[curPosX - 1, curPosY - 1] = empty;
                board[nextPieceX - 1, nextPieceY - 1] = curPiece;

            } else {
                Console.Clear();
                PrintBoard();
                BlackMove();
            }

        }

        // Transform string into bool
        static bool GetYesOrNo(string status) {
            if (status == "yes" || status == "YES" || status == "y" || status == "Y")
                return true;
            else
                return false;
        }

        // Get current piece in position
        static string GetPiece(int pieceX, int pieceY) {

            string piece;

            piece = board[pieceX, pieceY];

            return piece;
        }

        // Get full piece name
        static string GetFullPieceName(string piece) {

            string fullName = piece switch {
                "WP" => "White Pawn",
                "BP" => "Black Pawn",
                "WB" => "Black Bishop",
                "BB" => "Black Bishop",
                "WN" => "White Knight",
                "BN" => "Black Knight",
                "WR" => "White Rook",
                "BR" => "Black Rook",
                "WQ" => "White Queen",
                "BQ" => "Black Queen",
                "WK" => "White King",
                "BK" => "Black King",
                _ => "",
            };

            return fullName;
        }

        // Validade if move is possible
        static bool ValidadeMove(string piece,int curX, int curY, int nextX, int nextY) {
            bool canMove = false;

            int difX = Math.Abs(curX - nextX);
            int difY = Math.Abs(curY - nextY);

            switch (piece.Substring(1, 1)) {
                // Pawn - 1 or 2 forward
                case "P":
                    canMove = (difX == 2 && difY == 0) || (difX == 1 && difY == 0);
                    break;

                // Bishop - Only diagonal
                case "B":
                    canMove = (difX == difY) && (difX > 0);
                    break;

                // Knight - L movement
                case "N":
                    canMove = (difX == 2 && difY == 1) || (difX == 1 && difY == 2);
                    break;

                // Rook - Move Left/Right/Forward/Backward
                case "R":
                    canMove = (difX > 0 && difY == 0) || (difX == 0 && difY > 0);
                    break;

                // Queen - Bishop moves + Any of the adjascent
                case "Q":
                    canMove = (difX > 0 && difX < 2) || (difY > 0 && difY < 2) || (difX > 0 && difY == 0) || (difX == 0 && difY > 0);
                    break;

                // King - Only the adjascent
                case "K":
                    canMove = (difX > 0 && difX < 2) || (difY > 0 && difY < 2);
                    break;

            }

            return canMove;
        }

        // Set chess pieces in board
        static void SetBoard() {

            //// Black
            color = "B";

            // Rook
            board[0, 0] = color + rook;
            board[0, 7] = color + rook;

            // Knight
            board[0, 1] = color + knight;
            board[0, 6] = color + knight;

            // Bishop
            board[0, 2] = color + bishop;
            board[0, 5] = color + bishop;

            // King and Queen
            board[0, 3] = color + queen;
            board[0, 4] = color + king;

            // Pawn
            for (int i = 0; i < 8; i++) {
                board[1, i] = color + pawn;
            }

            //// White
            color = "W";

            // Rook
            board[7, 0] = color + rook;
            board[7, 7] = color + rook;

            // Knight
            board[7, 1] = color + knight;
            board[7, 6] = color + knight;

            // Bishop
            board[7, 2] = color + bishop;
            board[7, 5] = color + bishop;

            // King and Queen
            board[7, 3] = color + queen;
            board[7, 4] = color + king;

            // Pawn
            for (int i = 0; i < 8; i++) {
                board[6, i] = color + pawn;
            }

        }

        // Clear the Board
        static void EmptyBoard() {

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    board[i, j] = empty;
                }
            }
        }

        // Print the board elements into the console
        static void PrintBoard() {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (j == 0)
                        Console.Write($"0{i + 1} - {board[i, j]} ");
                    else
                        Console.Write($"{board[i, j]} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            for (int i = 1; i < 9; i++) {
                if (i == 1)
                    Console.Write($"     0{i} ");
                else
                    Console.Write($"0{i} ");
            }

            Console.WriteLine();
        }
    }
}