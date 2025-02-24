using System;
using System.Diagnostics;

namespace balleprojet
{
    internal class Game
    {
        private Player player1;
        private Player player2;
        private Ball ball;
        private Wall wall1;
        private Wall wall2;
        private Player currentPlayer;

        public Game()
        {
            player1 = new Player("Joueur 1");
            player2 = new Player("Joueur 2");
            wall1 = new Wall();
            wall2 = new Wall();
            currentPlayer = player1; // Le joueur 1 commence
        }

        public void Start()
        {
            Console.SetWindowSize(150, 40);
            Console.Clear();

            while (player1.Lives > 0 && player2.Lives > 0 && player1.Score < 7 && player2.Score < 7)
            {
                DisplayInterface();
                PlayTurn();
                SwitchPlayer();
            }
            EndGame();
        }

        private void DisplayInterface()
        {
            Console.Clear();
            Console.SetCursorPosition(5, 1);
            Console.Write($"Vies: {player1.Name} [♥{new string('♥', player1.Lives)}] | Score: {player1.Score}");
            Console.SetCursorPosition(100, 1);
            Console.Write($"Vies: {player2.Name} [♥{new string('♥', player2.Lives)}] | Score: {player2.Score}");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(new string('_', 150));

            DrawPlayer(20, 32, true);
            DrawPlayer(125, 32, false);

            DrawWall(110, 30);
            DrawWall(35, 30);

            Console.SetCursorPosition(0, 35);
            Console.WriteLine(new string('-', 150));
            Console.SetCursorPosition(0, 38);
            Console.Write("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }

        private void PlayTurn()
        {
            Console.WriteLine($"{currentPlayer.Name}'s turn");
            int angle = ChooseAngle();
            int power = ChoosePower();  // Déterminer la puissance de tir
            ball = new Ball(angle, power);  // Créer une balle avec l'angle et la puissance

            // Position de départ en fonction du point sélectionné
            int playerX = currentPlayer == player1 ? 20 : 125;
            int playerY = 32;
            int startX = currentPlayer == player1 ? playerX + 2 + 2 * (angle / 10) : playerX - (2 + 2 * (angle / 10));
            int startY = playerY - (angle / 10);

            Console.Clear();
            DisplayInterface();
            ball.CalculateTrajectory(startX, startY, power, currentPlayer == player1, player1, player2, wall1, wall2);  // Passer les références des joueurs et des murs
                                                                                                                        // Code pour gérer les collisions avec le mur et l'adversaire
        }


        private int DisplayAngleSelection(int playerX, int playerY, bool isPlayer1)
        {
            int[][] points;

            if (isPlayer1)
            {
                // Sélection d'angle pour le joueur 1 (tir vers la droite)
                points = new int[][]
                {
                    new int[] {playerX     + 2, playerY - 3},   // Point 1 (70°)
                    new int[] {playerX + 4, playerY - 2},       // Point 2 (45°)
                    new int[] {playerX + 6, playerY - 1},       // Point 3 (35°)
                    new int[] {playerX + 8, playerY},           // Point 4 (25°)
                    new int[] {playerX + 10, playerY + 1}       // Point 5 (15°)
                };
            }
            else
            {
                // Sélection d'angle pour le joueur 2 (tir vers la gauche)
                points = new int[][]
                {
                    new int[] {playerX - 2, playerY - 3},   // Point 1 (70°)
                    new int[] {playerX - 4, playerY - 2},   // Point 2 (45°)
                    new int[] {playerX - 6, playerY - 1},   // Point 3 (35°)
                    new int[] {playerX - 8, playerY},       // Point 4 (25°)
                    new int[] {playerX - 10, playerY + 1}   // Point 5 (15°)
                };
            }

            int[] angles = { 70, 45, 35, 25, 15 };
            int selectedIndex = 0;

            while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Spacebar)
            {
                // Effacer les anciens points
                foreach (var point in points)
                {
                    Console.SetCursorPosition(point[0], point[1]);
                    Console.Write(' ');  // Effacer le point
                }

                // Afficher les nouveaux points
                for (int i = 0; i < points.Length; i++)
                {
                    Console.SetCursorPosition(points[i][0], points[i][1]);
                    if (i == selectedIndex)
                    {
                        Console.Write('O');  // Point sélectionné
                        Console.SetCursorPosition(points[i][0] + (isPlayer1 ? 2 : -2), points[i][1]);
                    }
                    else
                    {
                        Console.Write('*');  // Point non sélectionné
                    }
                }

                // Changer la sélection de l'angle (clignotement)
                selectedIndex = (selectedIndex + 1) % points.Length;
                System.Threading.Thread.Sleep(200);  // Délai pour le clignotement
            }

            return angles[selectedIndex];
        }


        private int ChooseAngle()
        {
            Console.Clear();
            DisplayInterface();
            int playerX = currentPlayer == player1 ? 20 : 125;
            int playerY = 32;

            int angle = DisplayAngleSelection(playerX, playerY, currentPlayer == player1);
            Debug.WriteLine($"\nAngle sélectionné: {angle}°");

            return angle;
        }

        private int ChoosePower()
        {
            Console.WriteLine("Appuyez sur espace pour déterminer la puissance du tir...");
            int power = DisplayPowerBar();
            Console.WriteLine($"\nPuissance sélectionnée: {power}");

            return power;
        }

        private int DisplayPowerBar()
        {
            int power = 0;
            Console.Clear();
            DisplayInterface();

            Console.SetCursorPosition(5, 5);
            Console.Write("[");
            for (int i = 0; i < 100; i++) Console.Write(" ");
            Console.Write("]");

            while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Spacebar)
            {
                if (power < 100)
                {
                    power++;
                    Console.SetCursorPosition(6 + power, 5);
                    Console.Write("|");
                }
                System.Threading.Thread.Sleep(10); // Vitesse de la barre de progression
            }

            return power;
        }

        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == player1 ? player2 : player1;
        }

        private void EndGame()
        {
            Player winner = player1.Score >= 7 || player2.Lives <= 0 ? player1 : player2;
            Console.WriteLine($"Le gagnant est {winner.Name} !");
        }

        private void DrawPlayer(int x, int y, bool isLeft)
        {
            if (isLeft)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("  o/ ");
                Console.SetCursorPosition(x, y + 1);
                Console.Write(" /| ");
                Console.SetCursorPosition(x, y + 2);
                Console.Write(" / \\");
            }
            else
            {
                Console.SetCursorPosition(x, y);
                Console.Write("\\o ");
                Console.SetCursorPosition(x, y + 1);
                Console.Write(" |\\ ");
                Console.SetCursorPosition(x, y + 2);
                Console.Write("/ \\");
            }
        }

        private void DrawWall(int x, int y)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("███");
            }

        }
    }
}
