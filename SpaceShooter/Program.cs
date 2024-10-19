using System;
using System.Threading;

namespace SpaceShooter
{
    class Program
    {
        static int shipPosX = Console.WindowWidth / 2;
        static int shipPosY = Console.WindowHeight - 2;
        static int score = 0;
        static bool isPlaying = true;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Thread inputThread = new Thread(ReadInput);
            inputThread.Start();

            while (isPlaying)
            {
                DrawShip();
                UpdateGame();
                Thread.Sleep(100);
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine($"Game Over! Your score: {score}");
        }

        static void DrawShip()
        {
            Console.Clear();
            Console.SetCursorPosition(shipPosX, shipPosY);
            Console.Write("  ^  ");

            Console.SetCursorPosition(shipPosX, shipPosY + 1);
            Console.Write(" /|\\ ");

            Console.SetCursorPosition(shipPosX, shipPosY + 2);
            Console.Write("/_|_\\");

            Console.SetCursorPosition(shipPosX, shipPosY + 3);
            Console.Write("  |  ");
        }

        static void ReadInput()
        {
            while (isPlaying)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N && shipPosX > 0)
                {
                    shipPosX--;
                }
                else if (key == ConsoleKey.M && shipPosX < Console.WindowWidth - 1)
                {
                    shipPosX++;
                }
                else if (key == ConsoleKey.Escape)
                {
                    isPlaying = false;
                }
            }
        }

        static void UpdateGame()
        {
            Random rand = new Random();
            if (rand.Next(10) < 2) // Genera asteroides aleatoriamente
            {
                int asteroidX = rand.Next(Console.WindowWidth);
                int asteroidY = 0;

                while (asteroidY < Console.WindowHeight)
                {
                    if (!isPlaying) return;
                    Console.SetCursorPosition(asteroidX, asteroidY);
                    Console.Write(" ");
                    asteroidY++;
                    Console.SetCursorPosition(asteroidX, asteroidY);
                    Console.Write("O");

                    if (asteroidX == shipPosX && asteroidY == shipPosY)
                    {
                        isPlaying = false;
                        break;
                    }

                    Thread.Sleep(100);
                }

                score++;
            }
        }
    }
}
