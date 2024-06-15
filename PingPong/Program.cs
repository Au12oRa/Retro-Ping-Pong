using System;
using System.Data;
using System.Numerics;

namespace PingPong {
    class Program {

        static int firstPlayerPadSize = 6;
        static int secondPlayerPadSize = 6;

        static int ballPositionX = 0;
        static int ballPositionY = 0;
        static bool ballDirectionUp = true;
        static bool ballDirectionRight = true;
        static bool ballDirectionDown = false;
        static bool ballDirectionLeft = false;


        static int firstPlayerPostion = 0;
        static int secondPlayerPostion = 0;

        static int firstPlayerPoints = 0;
        static int secontPlayerPoints = 0;

        static Random randomGen = new Random();

        static void Main(string[] args) {

            RemoveBufferBar();
            PositionItems();

            while (true) {

                if (Console.KeyAvailable) {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.UpArrow) { 
                        MoveFirstPlayerUp();
                    }

                    if (keyInfo.Key == ConsoleKey.DownArrow) {
                        MoveFirstPlayerDown();
                    }
                }

                SecondPlayerAIMove();
                MoveBall();
                Console.Clear();
                DrawingFristPlayer();
                DrawingSecondPlayer();
                DrawingBall();
                PointsCounter();
                Thread.Sleep(60);
            }

        }

        static void RemoveBufferBar() {
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        static void PositionItems() {
            firstPlayerPostion = Console.WindowHeight / 2 - firstPlayerPadSize / 2;
            secondPlayerPostion = Console.WindowHeight / 2 - secondPlayerPadSize / 2;
            SetBallAtTheMiddleOfTheField();
        }

        static void SetBallAtTheMiddleOfTheField() {
            ballPositionX = Console.WindowWidth / 2;
            ballPositionY = Console.WindowHeight / 2;
        }

        static void MoveFirstPlayerUp() {
            if (firstPlayerPostion > 0) {
                firstPlayerPostion--;
            }
        }

        static void MoveFirstPlayerDown() {
            if (firstPlayerPostion < Console.WindowHeight - firstPlayerPadSize) {
                firstPlayerPostion++;
            }
        }

        static void MoveSecondPlayerUp() {
            if (secondPlayerPostion > 0) {
                secondPlayerPostion--;  
            }
        }

        static void MoveSecondPlayerDown() { 
            if (secondPlayerPostion < Console.WindowHeight - secondPlayerPadSize) {
                secondPlayerPostion++;
            }
        }

        static void SecondPlayerAIMove() {
            int randomNumber = randomGen.Next(0, 6);

            if (randomNumber == 4) {

                //if (randomNumber == 0) { 
                //    MoveSecondPlayerUp();
                //}

                //if (randomNumber == 1) { 
                //    MoveSecondPlayerDown();
                //}

                if (ballDirectionUp == true) {
                    MoveSecondPlayerUp();
                }

                else {
                    MoveSecondPlayerDown();
                }
            }
        }

        static void MoveBall() {
            if (ballPositionY == 0) {
                ballDirectionUp = false;
            }

            if (ballPositionY == Console.WindowHeight - 1) { 
                ballDirectionUp = true;
            }

            if (ballPositionX == Console.WindowWidth - 1) {
                SetBallAtTheMiddleOfTheField();
                ballDirectionRight = false;
                firstPlayerPoints++;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("First Player Wins!" +  " Press Enter to Continue");
                Console.ReadKey();

            }

            if (ballPositionX == 0) {
                SetBallAtTheMiddleOfTheField();
                ballDirectionRight = true;
                secontPlayerPoints++;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("Second Player Win" + "Press Enter to Continue");
                Console.ReadKey();
            }

            if (ballPositionX < 3) {
                if (ballPositionY >= firstPlayerPostion && ballPositionY < firstPlayerPostion + firstPlayerPadSize) {
                    ballDirectionRight = true;
                }
            }

            if (ballPositionX  >= Console.WindowWidth - 3 - 1) {
                if (ballPositionY >= secondPlayerPostion && ballPositionY < secondPlayerPostion + secondPlayerPadSize) {
                    ballDirectionRight = false;
                }
            }

            if (ballDirectionUp) {
                ballPositionY--;
            }

            else { 
                ballPositionY++;
            }

            if (ballDirectionRight) { 
                ballPositionX++;
            }

            else {
                ballPositionX--;
            }

        }

        static void DrawingFristPlayer() {
            for (int y = firstPlayerPostion; y < firstPlayerPadSize + firstPlayerPostion ; y++)
                PrintAtPosition(0, y, '|');
        }

        static void DrawingSecondPlayer() {
            for (int y = secondPlayerPostion; y < secondPlayerPostion + secondPlayerPadSize; y++)
                PrintAtPosition(Console.WindowWidth - 1, y, '|');
        }

        static void PrintAtPosition(int x, int y, char symbol) {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);


        }

        static void DrawingBall() {
            PrintAtPosition(ballPositionX, ballPositionY, '+'); 
        }

        static void PointsCounter() { 
            Console.SetCursorPosition(Console.WindowWidth / 2 - 1, 0);
            Console.Write("{0} - {1}", firstPlayerPoints, secontPlayerPoints);
        }
    }
          
}