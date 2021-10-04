using System;

namespace Hanoi_Tower
{
    class Hanoi
    {
        private static int ramProc = 0;
        private static int HanoiTall;
        private static int HanoiCounts = 3;

        public Hanoi()
        {
            Operator();
        }

        internal class State
        {
            public State next;
            private int[,] board;
            private string way;
            private int topA, topB, topC, lastMove;
            internal State(string name, int HanoiTall, string way, int[,] b, int lastMove)
            {
                board = b;
                this.way = way + name;
                this.lastMove = lastMove;
            }

            internal string getWay()
            {
                return way;
            }

            internal int[,] getBoard()
            {
                return this.board;
            }
            internal int getLastMove()
            {
                return this.lastMove;
            }


            internal int getTop(int i)
            {
                int k;
                for (k = HanoiTall - 1; k >= 0 && board[0, k] == 0; k--) ;
                topA = k;

                for (k = HanoiTall - 1; k >= 0 && board[1, k] == 0; k--) ;
                topB = k;

                for (k = HanoiTall - 1; k >= 0 && board[2, k] == 0; k--) ;
                topC = k;


                switch (i)
                {
                    case 0: return topA;
                    case 1: return topB;
                    case 2: return topC;
                    default: return 0;
                }
            }
        }

        private int[,] swap(State cS, int a, int b)
        {
            int[,] board = new int[HanoiCounts, HanoiTall];
            int[,] CsBoard = cS.getBoard();

            for (int i = 0; i < HanoiCounts; i++)
                for (int j = 0; j < HanoiTall; j++)
                    board[i, j] = CsBoard[i, j];

            board[b, cS.getTop(b) + 1] = board[a, cS.getTop(a)];
            board[a, cS.getTop(a)] = 0;

            return board;
        }

        private bool GOAL(State currentState)
        {
            int[,] board = currentState.getBoard();

            if (currentState.getWay().Length > (5 * 30))
            {
                Console.WriteLine("Out of ram");
                return true;
            }
            for (int i = 0; i < HanoiTall; i++)
                if (board[2, i] != HanoiTall - i)
                    return false;

            Console.WriteLine("way: " + currentState.getWay() + "\n[" + ramProc + " states has been checked]");
            return true;
        }

        private void Operator()
        {
            int temp = 0;
            ramProc = 0; // checking ram
        a:
            Console.Clear();
            Console.WriteLine("HanoiTall (1-4): ");
            temp = Console.Read();
            if (temp < 49 || temp > 54) goto a;

            HanoiTall = temp - 48;
            Console.WriteLine("Loading...");
            int[,] board = new int[HanoiCounts, HanoiTall];
            for (int i = 0; i < HanoiCounts; i++)
                for (int j = 0; j < HanoiTall; j++)
                    board[i, j] = 0;
            for (int i = 0; i < HanoiTall; i++)
                board[0, i] = HanoiTall - i;

            State currentState = new State(" ", HanoiTall, " ", board, 0);
            Agenda agenda = new Agenda(currentState);

            while (!GOAL(currentState))
            {
                int topA = currentState.getTop(0), topB = currentState.getTop(1), topC = currentState.getTop(2), LastMove = currentState.getLastMove();
                int[,] _board = currentState.getBoard();
                string way = currentState.getWay();

                //Console.WriteLine();
                //Console.WriteLine(topA + " - " + topB + " - " + topC);
                //for (int i = 0; i < HanoiTall; i++)
                //    Console.Write(_board[0, i] + " - ");
                //Console.WriteLine();
                //for (int i = 0; i < HanoiTall; i++)
                //    Console.Write(_board[1, i] + " - ");
                //Console.WriteLine();
                //for (int i = 0; i < HanoiTall; i++)
                //    Console.Write(_board[2, i] + " - ");
                //Console.WriteLine();
               // Console.WriteLine("way: " + way);

                if (topA != -1)
                {
                    //   Console.WriteLine("topA: " + _board[0, topA]);
                    if (LastMove != 21)
                        if (topB == -1) agenda.Add("1->2 ", HanoiTall, way, swap(currentState, 0, 1), 12);
                        else if (_board[1, topB] > _board[0, topA] && topB < HanoiTall)
                            agenda.Add("1->2 ", HanoiTall, way, swap(currentState, 0, 1), 12);

                    if (LastMove != 31)
                        if (topC == -1) agenda.Add("1->3 ", HanoiTall, way, swap(currentState, 0, 2), 13);
                        else if (_board[2, topC] > _board[0, topA] && topC < HanoiTall)
                            agenda.Add("1->3 ", HanoiTall, way, swap(currentState, 0, 2), 13);
                }

                if (topB != -1)
                {
                    //   Console.WriteLine("topB: " + _board[1, topB]);
                    if (LastMove != 12)
                        if (topA == -1) agenda.Add("2->1 ", HanoiTall, way, swap(currentState, 1, 0), 21);
                        else if (_board[0, topA] > _board[1, topB] && topA < HanoiTall)
                            agenda.Add("2->1 ", HanoiTall, way, swap(currentState, 1, 0), 21);

                    if (LastMove != 32)
                        if (topC == -1) agenda.Add("2->3 ", HanoiTall, way, swap(currentState, 1, 2), 23);
                        else if (_board[2, topC] > _board[1, topB] && topC < HanoiTall)
                            agenda.Add("2->3 ", HanoiTall, way, swap(currentState, 1, 2), 23);
                }

                if (topC != -1)
                {
                    //   Console.WriteLine("topC: " + _board[2, topC]);

                    if (LastMove != 13)
                        if (topA == -1) agenda.Add("3->1 ", HanoiTall, way, swap(currentState, 2, 0), 31);
                        else if (_board[0, topA] > _board[2, topC] && topA < HanoiTall)
                            agenda.Add("3->1 ", HanoiTall, way, swap(currentState, 2, 0), 31);

                    if (LastMove != 23)
                        if (topB == -1) agenda.Add("3->2 ", HanoiTall, way, swap(currentState, 2, 1), 32);
                        else if (_board[1, topB] > _board[2, topC] && topB < HanoiTall)
                            agenda.Add("3->2 ", HanoiTall, way, swap(currentState, 2, 1), 32);
                }

                ramProc++;
                if (way.Length < 4) agenda.Remove(ref currentState);
                if (!agenda.Remove(ref currentState)) { Console.WriteLine("false!"); break; }
            }

            //   Console.WriteLine("way: " + currentState.getWay());
            Console.ReadKey();
        }


        internal class Agenda
        {
            private State front, rear;
            internal Agenda(State currnetState)
            {
                front = currnetState;
                rear = currnetState;
                front.next = rear;
            }
            internal bool Add(String name, int HanoiTall, string way, int[,] b, int lastMove)
            {
                State s = new State(name, HanoiTall, way, b, lastMove);
                rear.next = s;
                rear = s;
                return true;
            }

            internal bool Remove(ref State item)
            {
                if (front == null) return false;
                item = front;
                if (front == rear) rear = front = null;
                else front = front.next;
                return true;
            }
        }

    }
}
