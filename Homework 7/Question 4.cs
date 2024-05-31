using System;

namespace tamrin_seri_7_soal_4
{
    class Program
    {
        static void Main()
        {
            try
            {
                Elp elp = new Elp();
                Horse horse = new Horse();
                Majesty majesty = new Majesty();
                Castle castle = new Castle();

                GameBoard board = new GameBoard(castle, elp, horse, majesty);

                Random random = new Random();
                int randNum = 0;

                while (board.CheckGameIsOverOrNot())
                {
                    randNum = Math.Abs(random.Next()) % 4;
                    if ((randNum == 0) && (elp.AliveOrDead))
                    {
                        elp.Move();
                        board.RemoveCharacterOrNot(elp);
                    }
                    else if ((randNum == 1) && (horse.AliveOrDead))
                    {
                        horse.Move();
                        board.RemoveCharacterOrNot(horse);
                    }
                    else if ((randNum == 2) && (majesty.AliveOrDead))
                    {
                        majesty.Move();
                        board.RemoveCharacterOrNot(majesty);
                    }
                    else if ((randNum == 3) && (castle.AliveOrDead))
                    {
                        castle.Move();
                        board.RemoveCharacterOrNot(castle);
                    }
                    else
                    {
                        continue;
                    }
                    board.UpdateGameBoard();
                    board.ShowGameBoard();
                }
                Console.WriteLine(board.WhoWinTheGame());
            }
            catch (IndexOutOfRangeException error)when(error.Message== "ElephantWin")
            {
                Console.WriteLine("In the next move, Elephant wins!");
            }
            catch(IndexOutOfRangeException error)when(error.Message== "HorseWin")
            {
                Console.WriteLine("In the next move, Horse win!");
            }
            catch(IndexOutOfRangeException error)when(error.Message== "MajestyWin")
            {
                Console.WriteLine("In the next move, Majesty wins!");
            }
            catch(IndexOutOfRangeException error)when(error.Message== "CastleWin")
            {
                Console.WriteLine("In the next move, Castle wins!");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Not enough memory on the device!\nGame stopped!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
    }
    
    interface Characters
    {
        bool AliveOrDead
        {
            get;
            set;
        }
        int row
        {
            get;
        }
        int column
        {
            get;
        }
        void Move();
    }
    
    class Castle : Characters
    {
        private bool _aliveOrDead = true;
        public bool AliveOrDead
        {
            get { return this._aliveOrDead; }
            set { this._aliveOrDead = value; }
        }

        private int _row = 7;
        public int row
        {
            get { return this._row; }
            private set { this._row = value; }
        }

        private int _column = 0;
        public int column
        {
            get { return this._column; }
            private set { this._column = value; }
        }

        public void Move()
        {
            if (row == 7)
            {
                row--;
                return;
            }
            else
            {
                Random random = new Random();
                int randNum = 0;
                while (true)
                {
                    randNum = Math.Abs(random.Next()) % 3;
                    if ((randNum == 0) && (column == 0))
                    {
                        continue;
                    }
                    else if ((randNum == 2) && (column == 3))
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                if (randNum == 0)
                    column--;
                else if (randNum == 1)
                    row--;
                else if (randNum == 2)
                    column++;
                else {; }

                return;
                //0==to left,1==to up,2==to right
            }
        }
    }
    
    class Elp : Characters
    {
        private bool _aliveOrDead = true;
        public bool AliveOrDead
        {
            get { return this._aliveOrDead; }
            set { this._aliveOrDead = value; }
        }

        private int _row = 7;
        public int row
        {
            get { return this._row; }
            private set { this._row = value; }
        }

        private int _column = 3;
        public int column
        {
            get { return this._column; }
            private set { this._column = value; }
        }

        public void Move()
        {
            if (row == 7)
            {
                row--;
                column--;
            }
            else
            {
                int randNum = 0;
                while (true)
                {
                    //0==up and left , 1==up and right
                    Random random = new Random();
                    randNum = Math.Abs(random.Next()) % 2;
                    if ((randNum == 0) && (column == 0))
                    {
                        continue;
                    }
                    else if ((randNum == 1) && (column == 3))
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                //0==up and left , 1==up and right
                if (randNum == 0)
                {
                    row--;
                    column--;
                }
                else if (randNum == 1)
                {
                    row--;
                    column++;
                }
                else {; }
                return;
            }
        }
    }
    
    class Horse : Characters
    {
        private bool _aliveOrDead = true;
        public bool AliveOrDead
        {
            get { return this._aliveOrDead; }
            set { this._aliveOrDead = value; }
        }

        private int _row = 7;
        public int row
        {
            get { return this._row; }
            private set { this._row = value; }
        }

        private int _column = 2;
        public int column
        {
            get { return this._column; }
            private set { this._column = value; }
        }

        public void Move()
        {
            if (row == 7)
            {
                //0==two up one left, 1==two up one right ,2==one up two left
                int randNum = 0;
                Random random = new Random();
                randNum = Math.Abs(random.Next()) % 3;
                if (randNum == 0)
                {
                    row -= 2;
                    column--;
                }
                else if (randNum == 1)
                {
                    row -= 2;
                    column++;
                }
                else if (randNum == 2)
                {
                    row--;
                    column -= 2;
                }
                else
                {
                    return;
                }
            }
            else
            {
                //0==one up,two left 
                //1==two up,one left
                //2==two up, one right
                //3=one up,two right
                int randNum = 0;
                Random random = new Random();
                while (true)
                {
                    randNum = Math.Abs(random.Next()) % 4;

                    if ((randNum == 0) && (column <= 2))
                        continue;
                    else if ((randNum == 1) && (column == 0))
                        continue;
                    else if ((randNum == 2) && (column == 3))
                        continue;
                    else if ((randNum == 3) && (column >= 2))
                        continue;
                    else
                        break;
                }
                if (randNum == 0)
                {
                    row--;
                    column -= 2;
                }
                else if (randNum == 1)
                {
                    row -= 2;
                    column--;
                }
                else if (randNum == 2)
                {
                    row -= 2;
                    column++;
                }
                else if (randNum == 3)
                {
                    row--;
                    column += 2;
                }
                else
                {
                    return;
                }
            }
        }
    }
    
    class Majesty : Characters
    {
        //true==elephantMode,   false==castleMode
        private bool _elephantMode_castleMode = true;

        private bool _aliveOrDead = true;
        public bool AliveOrDead
        {
            get { return this._aliveOrDead; }
            set { this._aliveOrDead = value; }
        }

        private int _row = 7;
        public int row
        {
            get { return this._row; }
            private set { this._row = value; }
        }

        private int _column = 1;
        public int column
        {
            get { return this._column; }
            private set { this._column = value; }
        }

        //true==elephantMode,   false==castleMode
        //elephant mode
        //0==one ip one left , 1==one up one right
        //castle mode
        //0==one to left, 1==obe to up , 2== one to right
        public void Move()
        {
            //elephant mode by default
            if (row == 7)
            {
                Random random = new Random();
                int randNum = Math.Abs(random.Next()) % 2;
                if (randNum == 0)
                {
                    row--;
                    column--;
                    return;
                }
                else
                {
                    row--;
                    column++;
                    return;
                }
            }
            else
            {
                if (_elephantMode_castleMode)
                {
                    Random random = new Random();
                    int randNum = 0;
                    while (true)
                    {
                        randNum = Math.Abs(random.Next()) % 2;
                        if ((randNum == 0) && (column == 0))
                            continue;
                        else if ((randNum == 1) && (column == 3))
                            continue;
                        else
                            break;
                    }
                    if (randNum == 0)
                    {
                        row--;
                        column--;
                    }
                    else if (randNum == 1)
                    {
                        row--;
                        column++;
                    }
                    else {; }
                    _elephantMode_castleMode = false;
                }
                //castle mode
                //0==one left, 1==one up, 2==one right
                else
                {
                    Random random = new Random();
                    int randNum = 0;
                    while (true)
                    {
                        randNum = Math.Abs(random.Next()) % 3;
                        if ((randNum == 0) && (column == 0))
                            continue;
                        else if ((randNum == 2) && (column == 3))
                            continue;
                        else
                            break;
                    }
                    if (randNum == 0)
                        column--;
                    else if (randNum == 1)
                        row--;
                    else if (randNum == 2)
                        column++;
                    else {; }
                    _elephantMode_castleMode = true;
                    return;
                }
            }
        }
    }
    
    class GameBoard
    {
        public void CreateGameBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    board[row, column] = new house(row, column, null);
                }
            }
            board[7, 3].character = elephant;
            board[7, 2].character = horse;
            board[7, 1].character = majesty;
            board[7, 0].character = castle;
        }

        public void ShowGameBoard()
        {
            for (int column = 0; column < 4; column++)
            {
                for (int row = 0; row < 8; row++)
                {
                    if ((board[row, column].character == castle) && (castle.AliveOrDead))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('C');
                    }
                    else if ((board[row, column].character == majesty) && (majesty.AliveOrDead))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('M');
                    }
                    else if ((board[row, column].character == elephant) && (elephant.AliveOrDead))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('E');
                    }
                    else if ((board[row, column].character == horse) && (horse.AliveOrDead))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write('H');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('.');
                    }
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void EmptyBoard()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 4; j++)
                    board[i, j].character = null;
        }

        public bool CheckGameIsOverOrNot()
        {
            if (castle.row <= 0)
                return false;
            else if (elephant.row <= 0)
                return false;
            else if (horse.row <= 0)
                return false;
            else if (majesty.row <= 0)
                return false;
            else if (!((castle.AliveOrDead) || (elephant.AliveOrDead) || (horse.AliveOrDead)))
                return false;
            else if (!((majesty.AliveOrDead) || (elephant.AliveOrDead) || (horse.AliveOrDead)))
                return false;
            else if (!((majesty.AliveOrDead) || (castle.AliveOrDead) || (horse.AliveOrDead)))
                return false;
            else if (!((majesty.AliveOrDead) || (castle.AliveOrDead) || (elephant.AliveOrDead)))
                return false;
            else
                return true;
        }

        public string WhoWinTheGame()
        {
            if (elephant.row <= 0)
                return "Elephant wins the game!";
            else if (horse.row <= 0)
                return "Horse wins the game!";
            else if (majesty.row <= 0)
                return "Majesty wins the game!";
            else if (castle.row <= 0)
                return "Castle wins the game!";
            else if (!((castle.AliveOrDead) || (majesty.AliveOrDead) || (elephant.AliveOrDead)))
                return "Horse wins the game!";
            else if (!((castle.AliveOrDead) || (majesty.AliveOrDead) || (horse.AliveOrDead)))
                return "Elephant wins the game!";
            else if (!((castle.AliveOrDead) || (horse.AliveOrDead) || (elephant.AliveOrDead)))
                return "Majesty wins the game!";
            else if (!((majesty.AliveOrDead) || (horse.AliveOrDead) || (elephant.AliveOrDead)))
                return "Castle wins the game!";
            else
                return "";
        }

        public void UpdateGameBoard()
        {
            this.EmptyBoard();
            if (elephant.AliveOrDead)
            {
                try
                {
                    board[elephant.row, elephant.column].character = elephant;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException("ElephantWin");
                }
            }
            if (horse.AliveOrDead)
            {
                try
                {
                    board[horse.row, horse.column].character = horse;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException("HorseWin");
                }
            }
            if (majesty.AliveOrDead)
            {
                try
                {
                    board[majesty.row, majesty.column].character = majesty;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException("MajestyWin");
                }
            }
            if (castle.AliveOrDead)
            {
                try
                {
                    board[castle.row, castle.column].character = castle;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException("CastleWin");
                }
            }
            return;
        }

        public GameBoard(Castle castle, Elp elp, Horse horse, Majesty majesty)
        {
            this.castle = castle;
            this.elephant = elp;
            this.horse = horse;
            this.majesty = majesty;
            this.CreateGameBoard();
            this.UpdateGameBoard();
        }

        public void RemoveCharacterOrNot(Elp elp)
        {
            if ((castle.row == elp.row) && (castle.column == elp.column))
            {
                if (castle.AliveOrDead)
                {
                    castle.AliveOrDead = false;
                    return;
                }
            }
            else if ((horse.row == elp.row) && (horse.column == elp.column))
            {
                if (horse.AliveOrDead)
                {
                    horse.AliveOrDead = false;
                    return;
                }
            }
            else if ((majesty.row == elp.row) && (majesty.column == elp.column))
            {
                if (majesty.AliveOrDead)
                {
                    majesty.AliveOrDead = false;
                    return;
                }
            }
            else
            {
                return;
            }
        }

        public void RemoveCharacterOrNot(Majesty majesty)
        {
            if ((castle.row == majesty.row) && (castle.column == majesty.column))
            {
                if (castle.AliveOrDead)
                {
                    castle.AliveOrDead = false;
                    return;
                }
            }
            else if ((horse.row == majesty.row) && (horse.column == majesty.column))
            {
                if (horse.AliveOrDead)
                {
                    horse.AliveOrDead = false;
                    return;
                }
            }
            else if ((elephant.row == majesty.row) && (elephant.column == majesty.column))
            {
                if (elephant.AliveOrDead)
                {
                    elephant.AliveOrDead = false;
                    return;
                }
            }
            else
            {
                return;
            }
        }

        public void RemoveCharacterOrNot(Castle cas)
        {
            if ((majesty.row == cas.row) && (majesty.column == cas.column))
            {
                if (majesty.AliveOrDead)
                {
                    majesty.AliveOrDead = false;
                    return;
                }
            }
            else if ((horse.row == cas.row) && (horse.column == cas.column))
            {
                if (horse.AliveOrDead)
                {
                    horse.AliveOrDead = false;
                    return;
                }
            }
            else if ((elephant.row == cas.row) && (elephant.column == cas.column))
            {
                if (elephant.AliveOrDead)
                {
                    elephant.AliveOrDead = false;
                    return;
                }
            }
            else
            {
                return;
            }
        }

        public void RemoveCharacterOrNot(Horse hor)
        {
            if ((castle.row == hor.row) && (castle.column == hor.column))
            {
                if (castle.AliveOrDead)
                {
                    castle.AliveOrDead = false;
                    return;
                }
            }
            else if ((elephant.row == hor.row) && (elephant.column == hor.column))
            {
                if (elephant.AliveOrDead)
                {
                    elephant.AliveOrDead = false;
                    return;
                }
            }
            else if ((majesty.row == hor.row) && (majesty.column == hor.column))
            {
                if (majesty.AliveOrDead)
                {
                    majesty.AliveOrDead = false;
                    return;
                }
            }
            else
            {
                return;
            }
        }


        house[,] board = new house[8, 4];
        Castle castle;
        Elp elephant;
        Horse horse;
        Majesty majesty;
    }
    
    struct house
    {
        public int row;
        public int column;
        public Characters character;
        public house(int row, int column, Characters character)
        {
            this.row = row;
            this.column = column;
            this.character = character;
        }
    }
}
