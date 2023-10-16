using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 16;

        int screenX = Console.WindowWidth;
        int screenY = Console.WindowHeight;

        int screenMidX = screenX / 2;
        int screenMidY = screenY / 2;

        Random random = new Random();

        int score = 0;
        bool gameOver = false;

        Pixel head = new Pixel();

        head.XPos = screenX / 2;
        head.YPos = screenY / 2;

        head.color = ConsoleColor.Red;

        string movement = "Right";
        List<int> BodyElemsX = new List<int>();
        List<int> BodyElemsY = new List<int>();

        int bonusX = random.Next(1, screenX);
        int bonusY = random.Next(1, screenY);

        DateTime time = DateTime.Now;
        DateTime time1 = DateTime.Now;

        bool buttonPressed = false;
        bool hitItself = false;

        DrawBonus(bonusX, bonusY);

        while (true)
        {
            Console.CursorVisible = false;
            time = DateTime.Now;
            buttonPressed = false;

            if (time.Subtract(time1).TotalMilliseconds > 500)
            {
                time1 = DateTime.Now;

                // Move the player based on the current movement direction
                switch (movement)
                {
                    case "Right":
                        head.XPos++;
                        break;
                    case "Left":
                        head.XPos--;
                        break;
                    case "Up":
                        head.YPos--;
                        break;
                    case "Down":
                        head.YPos++;
                        break;
                }

                // Update the body position
                BodyElemsX.Insert(0, head.XPos);
                BodyElemsY.Insert(0, head.YPos);

                if (BodyElemsX.Count > score)
                {
                    BodyElemsX.RemoveAt(BodyElemsX.Count - 1);
                    BodyElemsY.RemoveAt(BodyElemsY.Count - 1);
                }

                Console.Clear();
                hitItself = false;

                for (int i = 0; i < BodyElemsX.Count; i++)
                {
                    DrawPlayer(head);
                    if (BodyElemsX[i] == head.XPos && BodyElemsY[i] == head.YPos && i > 0)
                    {
                        hitItself = true;
                    }
                }

                if (hitItself)
                {
                    GameOver(gameOver, screenMidX, screenMidY);
                }

                if (bonusX == head.XPos && bonusY == head.YPos)
                {
                score++;
                bonusX = random.Next(1, screenX);
                bonusY = random.Next(1, screenY);
                
                BodyElemsX.Insert(0, head.XPos);
                BodyElemsY.Insert(0, head.YPos);
                }



                DrawBonus(bonusX, bonusY);

                try
                {
                    DrawPlayer(head);
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    GameOver(gameOver, screenMidX, screenMidY);
                }
            }

            // Check for key press outside the time delay loop
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.W:
                        if (movement != "Down" && !buttonPressed)
                        {
                            movement = "Up";
                            buttonPressed = true;
                        }
                        break;
                    case ConsoleKey.S:
                        if (movement != "Up" && !buttonPressed)
                        {
                            movement = "Down";
                            buttonPressed = true;
                        }
                        break;
                    case ConsoleKey.A:
                        if (movement != "Right" && !buttonPressed)
                        {
                            movement = "Left";
                            buttonPressed = true;
                        }
                        break;
                    case ConsoleKey.D:
                        if (movement != "Left" && !buttonPressed)
                        {
                            movement = "Right";
                            buttonPressed = true;
                        }
                        break;
                }
            }
        }
    }

    static void DrawBonus(int bonusX, int bonusY)
    {
        Console.SetCursorPosition(bonusX, bonusY);
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine("■");
    }

    static void DrawPlayer(Pixel head)
    {
        Console.SetCursorPosition(head.XPos, head.YPos);
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine("■");
    }

    static void GameOver(bool gameOver, int screenMidX, int screenMidY)
    {
        gameOver = true;
        Console.Clear();
        Console.SetCursorPosition(screenMidX, screenMidY);
        System.Console.WriteLine("You lost");
        Console.SetCursorPosition(screenMidX, screenMidY + 1);
        System.Console.WriteLine("Better luck next time;)");
    }

    public class Pixel
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public ConsoleColor color { get; set; }
    }
}