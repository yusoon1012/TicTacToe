using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //OX표시 기본 X좌표 :  6    +15 씩 x한칸
            //OX표시 기본 Y좌표 :  3   +6 씩 y한칸
            //기본 별 X좌표 : 10  +15씩  x한칸
            //기본 별 Y좌표 : 5  +6씩 y한칸
            int[,] board = new int[3,3];//3x3틱택토판이될 int형 배열
            const int USER_PICK = 40; //int형 배열에 값을 변경해줄 임의의 유저값
            const int CPU_PICK = 50;//int형 배열에 값을 변경해줄 임의의 cpu값
            int posX = 10;//별이 움직일 X좌표
            int posY = 5;//별이 움직일 Y좌표
            int boardPosX = 0;//실질적으로 배열의 주소값에서 움직일 X좌표
            int boardPosY = 0;//실질적으로 배열의 주소값에서 움직일 Y좌표
            Random rnd = new Random();//CPU가 랜덤한 칸을 차지하게 하기위해 랜덤함수선언
            int cpuPosX = 0;//CPU가 선택할 배열의 주소값 X좌표
            int cpuPosY = 0;//CPU가 선택할 배열의 주소값 Y좌표


            DrawBoard();
            Console.WriteLine("=================================================");
            Console.WriteLine("                    TIC TAC TOE");
            Console.WriteLine("=================================================");
            Console.WriteLine();
            Console.WriteLine("            [ W ] ");
            Console.WriteLine(" 이동  [ A ][ S ][ D ]              확인 [ ENTER ]              ");

            while (true)//while
            {
                CursorPosition(0, 0);
             

                int posyIdx = 0;//별을 그릴위치에 추가적으로 필요한 인덱스변수
                int posxIdx = 0;
                ConsoleKeyInfo userInput = Console.ReadKey();//WASD등 키입력을받기위해 READKEY를 사용
                char userInputChar = (char)userInput.KeyChar;//위에서 입력받은값을 CHAR형으로 변환해서 변수에적용
                
                switch (userInputChar)//WASD를 눌렀을때 동작
                {
                    case 'w':
                        if (boardPosY>0)
                        {
                            posY -= 6;//별의위치 이동
                            boardPosY -= 1;//배열에서의 칸이동
                        }
                        break;
                    case 's':
                        if (boardPosY<2)
                        {
                            posY += 6;
                            boardPosY += 1;

                        }
                        break;
                    case 'a':
                        if (boardPosX>0)
                        {
                            posX -= 15;
                            boardPosX -= 1;
                        }
                        break;
                    case 'd':
                        if (boardPosX<2)
                        {
                            posX += 15;
                            boardPosX += 1;
                        }
                        break;
                   
                    default:
                        break;
                }
              
                if (userInput.Key == ConsoleKey.Enter)//엔터키를 체크
                {
                    if (board[boardPosY,boardPosX]==USER_PICK|| board[boardPosY, boardPosX]==CPU_PICK)//이미 선택된곳을 골랐을시 아무것도 적용하지않고 다시고름
                    {
                        continue;
                    }
                    if (board[boardPosY, boardPosX] == 0)//현재 WASD를 눌러 이동한 배열 주소값에 아무것도 들어있지않다면 유저의 값을 넣는다.
                    {
                        board[boardPosY, boardPosX] = USER_PICK;
                        
                    }
                    if (MyWin(board, USER_PICK))
                    {

                        break;

                    }
                    else if (MyWin(board, CPU_PICK))
                    {

                        break;

                    }
                    //cpuPosY = rnd.Next(0, 2);
                    //cpuPosX = rnd.Next(0, 2);

                    bool allCheck=false;//모든칸이 체크되었는지 확인하는변수
                    int checkCount = 0;//칸을 체크해주는변수
                    while (board[cpuPosY, cpuPosX] != 0&&allCheck==false )
                    {
                        cpuPosY = rnd.Next(3);
                        cpuPosX = rnd.Next(3);
                       
                       
                        Console.WriteLine("{0}",checkCount);
                        if (board[cpuPosY, cpuPosX]!=0)
                        {
                            checkCount++;
                        }
                        if (checkCount>=9)
                        {
                            allCheck = true;
                        }
                        if (allCheck == true)
                        {
                            break;
                        }
                    }
                    if (board[cpuPosY,cpuPosX]!=USER_PICK&&allCheck==false)
                    {

                    board[cpuPosY, cpuPosX] = CPU_PICK;
                    }
                    if (allCheck == true)
                    {
                        break;
                    }

                }
               
                DrawBoard();//판을그려주는함수

                for (int y = 0; y < 3; y++)//OX를 그려주는 부분
                {
                    posxIdx = 0;
                    for (int x = 0; x < 3; x++)
                    {
                        if (board[y, x] == USER_PICK)//배열의 해당 위치에 유저가 선택하여 값이들어가면
                        {
                            CursorPosition(0, 0);
                            DrawX(6 + posxIdx, 3 + posyIdx);//X를 출력해준다.
                        }
                        else if (board[y, x] == CPU_PICK)//CPU가 랜덤 선택하여 고른위치에 CPU의값이들어가면
                        {
                            CursorPosition(0, 0);
                            DrawO(6 + posxIdx, 3 + posyIdx);//O를 출력해준다.
                        }
                        posxIdx += 15;//출력시 간격조정을위한 인덱스 추가
                    }
                    posyIdx += 6;//출력시 간격조정을위한 인덱스 추가
                }
                posyIdx = 0;
                posxIdx = 0;


                if (MyWin(board, USER_PICK))
                {
            
                    break;

                }
                else if (MyWin(board, CPU_PICK))
                {
            
                    break;

                }
                

                CursorPosition(posX, posY);
                Console.Write("★");
            }//while끝

            Console.Clear();
            DrawBoard();
            
            int indexY = 0;
            for (int y = 0; y < 3; y++)
            {
               int indexX = 0;
                for (int x = 0; x < 3; x++)
                {
                    if (board[y, x] == 40)
                    {
                        CursorPosition(0, 0);
                        DrawX(6 + indexX, 3 + indexY);
                    }
                    else if (board[y, x] == 50)
                    {
                        CursorPosition(0, 0);
                        DrawO(6 + indexX, 3 + indexY);
                    }
                    indexX += 15;
                }
                indexY += 6;
            }
            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("                    TIC TAC TOE");
            Console.WriteLine("=================================================");
            if (MyWin(board, USER_PICK))//유저의 승리체크
            {

                Console.WriteLine();
                Console.WriteLine("=================================================");
                Console.WriteLine("                 플레이어의 승리.");
                Console.WriteLine("=================================================");


            }
            else if (MyWin(board, CPU_PICK))//CPU의 승리체크
            {

                Console.WriteLine();
                Console.WriteLine("=================================================");
                Console.WriteLine("                 컴퓨터의 승리.");
                Console.WriteLine("=================================================");


            }



        }
        static void DrawBoard()//판 그리기
        {
            CursorPosition(3, 3);
            Console.Write("              │              │               ");
            CursorPosition(3, 4);
            Console.Write("              │              │              ");
            CursorPosition(3, 5);
            Console.Write("              │              │              ");
            CursorPosition(3, 6);  
            Console.Write("              │              │              ");
            CursorPosition(3, 7);
            Console.Write("              │              │              ");
            CursorPosition(3, 8);
            Console.Write("──────────────┼──────────────┼──────────────");
            CursorPosition(3, 9);
            Console.Write("              │              │              ");
            CursorPosition(3, 10);
            Console.Write("              │              │              ");
            CursorPosition(3, 11);
            Console.Write("              │              │               ");
            CursorPosition(3, 12);
            Console.Write("              │              │              ");
            CursorPosition(3, 13);
            Console.Write("              │              │              ");
            CursorPosition(3, 14);
            Console.Write("──────────────┼──────────────┼──────────────");
            CursorPosition(3, 15);
            Console.Write("              │              │              ");
            CursorPosition(3, 16);
            Console.Write("              │              │              ");
            CursorPosition(3, 17);
            Console.Write("              │              │              ");
            CursorPosition(3, 18);
            Console.Write("              │              │              ");
            CursorPosition(3, 19);  
            Console.Write("              │              │              ");
            Console.WriteLine();
            Console.WriteLine();


        }//판 그리기

    
      
        static void CursorPosition(int x, int y)//커서포지션 지정
        {
            Console.SetCursorPosition(x, y);
        }//커서포지션 지정
        static void DrawX(int x,int y)//X그리기
        {
            CursorPosition(x,y);
            Console.Write("■      ■");
            CursorPosition(x, y+1);
            Console.Write("  ■  ■  ");
            CursorPosition(x, y+2);
            Console.Write("    ■    ");
            CursorPosition(x, y+3);
            Console.Write("  ■  ■  ");
            CursorPosition(x, y+4);
            Console.Write("■      ■");
        }//X그리기
        static void DrawO(int x,int y)//O그리기
        {
            CursorPosition(x, y);
            Console.Write("  ○○○  ");
            CursorPosition(x, y + 1);
            Console.Write("○      ○");
            CursorPosition(x, y + 2);
            Console.Write("○      ○");
            CursorPosition(x, y + 3);
            Console.Write("○      ○");
            CursorPosition(x, y + 4);
            Console.Write("  ○○○  ");
        }//O그리기
       
        static bool MyWin(int[,] board, int player)//승리체크
        {
            for(int i=0;i<3;i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i,2]== player)//가로체크
                {
                    return true;
                }
            }
            for(int i=0;i<3;i++)
            {
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player)//세로체크
                {
                    return true;
                }

            }

            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
            { 
                return true;
            }

            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
            {
                return true;
            }
            return false;
        }//승리체크

    }
}                             
                              