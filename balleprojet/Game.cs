/********************************************************************************
 Programmation Orientée Objet en C#
 Projet: Jeu de la balle
 Prénom et nom : Matias Denis
 Cours: I320
 Classe: FID1
 *******************************************************************************/

using System;
using System.Diagnostics;

// Déclaration de l'espace de noms "balleprojet"
namespace balleprojet
{
    /// <summary>
    /// Déclaration de classe "Game"
    /// </summary>
    internal class Game
    {
        // Variables privées de la classe "Game"
        private Player player1;         // Joueur 1
        private Player player2;         // Joueur 2
        private Ball ball;              // Balle
        private Wall wall1;             // Mur 1
        private Wall wall2;             // Mur 2
        private Player currentPlayer;   // Joueur actuel
        private Random _random = new Random();   // Random
        private SoundManager soundManager;
       

        /// <summary>
        /// Constructeur de la classe Game
        /// </summary>
        public Game()
        {
            // Initialisation des joueurs et des murs
            player1 = new Player("Joueur 1");   // Création du joueur 1
            player2 = new Player("Joueur 2");   // Création du joueur 2
            wall1 = new Wall();                 // Création du mur du joueur 1
            wall2 = new Wall();                 // Création du mur du joueur 2
            currentPlayer = player1;            // Le joueur 1 commence
            soundManager = new SoundManager("C:\\Users\\pb58unc\\Desktop\\DenisMatias07.02.2025\\laser-shot-ingame-230500.wav"); // Déclaration de SoundManager
        }

        /// <summary>
        /// Méthode pour démarrer le jeu
        /// </summary>
        public void Start()
        {
            // Définition de la taille de la fenêtre console et effacement de l'écran
            Console.SetWindowSize(150, 40);
            Console.Clear();

            // Boucle de jeu principale
            while (player1.Lives > 0 && player2.Lives > 0 && player1.Score < 7 && player2.Score < 7)
            {
                DisplayInterface();     // Affichage de l'interface
                PlayTurn();             // Exécution du tour de jeu
                SwitchPlayer();         // Changement de joueur
            }
            EndGame();                  // Fin de la partie
        }

        /// <summary>
        /// Méthode pour afficher l'interface du jeu
        /// </summary>
        private void DisplayInterface()
        {
            Console.Clear();                                                                                        // Effacer l'écrant
            Console.SetCursorPosition(5, 1);                                                                        // Positionner le curseur
            Console.Write($"Vies: {player1.Name} [♥{new string('♥', player1.Lives)}] | Score: {player1.Score}");    // Afficher les vies et le score du joueur 1
            Console.SetCursorPosition(100, 1);                                                                      // Positionner le curseur
            Console.Write($"Vies: {player2.Name} [♥{new string('♥', player2.Lives)}] | Score: {player2.Score}");    // Afficher les vies et le score du joueur 2
            Console.SetCursorPosition(0, 2);                                                                        // Positionner le curseur
            Console.WriteLine(new string('_', 150));                                                                // Afficher une ligne de séparation

            DrawPlayer(20, 32, true);       // Dessiner le joueur 1
            DrawPlayer(125, 32, false);     // Dessiner le joueur 2

            DrawWall(110, 30, wall2);              // Dessiner le mur du joueur 1
            DrawWall(35, 30, wall1);               // Dessiner le mur du joueur 2

            Console.SetCursorPosition(0, 35);                           // Positionner le curseur
            Console.WriteLine(new string('-', 150));                    // Afficher une ligne
            Console.SetCursorPosition(0, 38);                           // Positionner le curseur
            Console.Write("Appuyez sur une touche pour continuer...");  // Afficher le message d'attente
            Console.ReadKey();                                          // Attendre une touche
        }

        /// <summary>
        /// Méthode pour exécuter un tour de jeu
        /// </summary>
        private void PlayTurn()
        {
            Console.WriteLine($"{currentPlayer.Name}'s turn");                  // Afficher le tour du joueur courant
            int angle = ChooseAngle();                                          // Choisir l'angle de tir
            int power = ChoosePower();                                          // Déterminer la puissance de tir
            ball = new Ball(angle, power, (ConsoleColor)_random.Next(2,8));     // Créer une balle avec l'angle et la puissance

            // Jouer le son lorsque la balle est lancée
            soundManager.PlaySound();

            // Position de départ en fonction du point sélectionné
            int playerX = currentPlayer == player1 ? 20 : 125;
            int playerY = 32;
            int startX = currentPlayer == player1 ? playerX + 2 + 2 * (angle / 10) : playerX - (2 + 2 * (angle / 10));
            int startY = playerY - (angle / 10);

            Console.Clear();        // Effacer l'écran
            DisplayInterface();     // Afficher l'interface

            // Calcul de la trajectoire de la balle et gestion des collisions
            ball.CalculateTrajectory(startX, startY, power, currentPlayer == player1, player1, player2, wall1, wall2);  
            // Code à mettre pour gérer les collisions avec le mur et l'adversaire
        }

        /// <summary>
        /// Méthode pour afficher la sélection d'angle 
        /// </summary>
        /// <param name="playerX"></param>
        /// <param name="playerY"></param>
        /// <param name="isPlayer1"></param>
        /// <returns></returns>
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

            // Boucle pour afficher la sélection de l'angle
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

        /// <summary>
        /// Méthode pour choisir l'angle de tir
        /// </summary>
        /// <returns></returns>
        private int ChooseAngle()
        {
            Console.Clear();        // Effacer l'écran 
            DisplayInterface();     // Afficher l'interface
            int playerX = currentPlayer == player1 ? 20 : 125; // Opérateur ternaire 

            int playerY = 32;

            int angle = DisplayAngleSelection(playerX, playerY, currentPlayer == player1);

            // Afficher l'angle sélectionné dans le débogueur
            Debug.WriteLine($"\nAngle sélectionné: {angle}°");

            return angle;
        }

        /// <summary>
        /// Méthode pour afficher la barre de puissance
        /// </summary>
        /// <returns></returns>
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
                // Vitesse de la barre de progression
                System.Threading.Thread.Sleep(10); 
            }

            return power;
        }

        /// <summary>
        /// Méthode pour changer de joueur
        /// </summary>
        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == player1 ? player2 : player1;
        }

        /// <summary>
        /// Méthode pour afficher la fin de la partie
        /// </summary>
        private void EndGame()
        {
            Player winner = player1.Score >= 7 || player2.Lives <= 0 ? player1 : player2;
            Console.WriteLine($"Le gagnant est {winner.Name} !");
        }

        /// <summary>
        /// Méthode pour dessiner un joueur
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isLeft"></param>
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

        /// <summary>
        /// Méthode pour dessiner un mur
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawWall(int x, int y, Wall wall)
        {
            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(x, y + i);
                if (wall.Cells[i, 0].EstVisible)
                {
                    Console.Write("███");
                }
                else
                {
                    Console.Write("   "); // Effacer la cellule touchée 
                }
            }

        }
    }
}
