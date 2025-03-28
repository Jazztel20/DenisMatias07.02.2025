﻿/// Programmation Orientée Objet en C#
/// Projet: Jeu de la balle
/// Prénom et nom : Matias Denis
/// Cours: I320
/// Classe: FID1

using System;

// Déclaration de l'espace de noms "balleprojet"
namespace balleprojet
{
    /// <summary>
    /// La classe Ball représente une balle avec des propriétés 
    /// comme l'angle, la puissance et la couleur, utilise un 
    /// constructeur pour initialiser ses attributs, et comprend 
    /// une méthode CalculateTrajectory pour simuler et visualiser
    /// le déplacement et les collisions de la balle dans le jeu.
    /// </summary>
    internal class Ball
    {
        // Déclarations publiques de la classe "Ball"

        /// <summary>
        /// Angle de tir de la balle
        /// </summary>
        public int Angle { get; set; }

        /// <summary>
        /// Puissance de tir de la balle
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// ??
        /// </summary>
        public ConsoleColor Color { get => _color; set => _color = value; }

        /// <summary>
        /// Constructeur de la classe "Ball"
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="power"></param>

        //
        private ConsoleColor _color = ConsoleColor.Green;

        /// <summary>
        /// Constructeur de la classe "Ball"
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="power"></param>
        /// <param name="color"></param>
        public Ball(int angle, int power, ConsoleColor color)
        {
            Angle = angle;      // Initialise la propriété Angle avec le paramètre angle
            Power = power;      // Initialise la propriété Power avec le paramètre power
            _color = color;     // Initialise la couleur 
        }

        /// <summary>
        /// Méthode pour calculer la trajectoire de la balle
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="power"></param>
        /// <param name="isRight"></param>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="wall1"></param>
        /// <param name="wall2"></param>
        public void CalculateTrajectory(int startX, int startY, int power, bool isRight, Player player1, Player player2, Wall wall1, Wall wall2, SoundManager soundManager)
        {
            // Constantes de la simulation
            double gravity = 9.81;                              // m/s^2
            double angleInRadians = Angle * (Math.PI / 180);    // Conversion en radians
            double initialVelocity = power * 0.5;               // Vitesse initiale proportionnelle à la puissance
            double timeStep = 0.1;                              // Intervalle de temps pour la simulation
            double t = 0.0;                                     // Temps initial
            double posX = startX;                               // Position initiale en X
            double posY = startY;                               // Position initiale en Y

            // Boucle qui fonctionne que tant que la balle est dans les limites du terrain
            while (posY <= 32 && posX >= 0 && posX <= 150)
            {
                // Calcul des positions suivantes de la balle
                double nextX = isRight
                    ? (startX + initialVelocity * Math.Cos(angleInRadians) * t)
                    : (startX - initialVelocity * Math.Cos(angleInRadians) * t);
                double nextY = startY - (initialVelocity * Math.Sin(angleInRadians) * t - 0.5 * gravity * t * t);
                nextY += 1.0; // Ajustement de la position verticale 

                // Arrondir les positions pour simplifier la détection de collision
                int roundedX = (int)Math.Round(nextX);
                int roundedY = (int)Math.Round(nextY);

                // Affichage de la position de la balle
                Console.SetCursorPosition((int)nextX, (int)nextY);
                Console.ForegroundColor = _color;
                Console.Write('o');
                Console.ResetColor();

                // Vérification des collisions avec le mur adverse
                if (isRight && CheckWallCollision(roundedX, roundedY, wall2, true, player1, soundManager)) break;
                if (!isRight && CheckWallCollision(roundedX, roundedY, wall1, false, player2, soundManager)) break;

                // Vérification collision avec son propre mur
                if (isRight && CheckOwnWallCollision(roundedX, roundedY, wall1, isRight, player1, player2, soundManager)) break;
                if (!isRight && CheckOwnWallCollision(roundedX, roundedY, wall2, isRight, player2, player1, soundManager)) break;

                // Collision joueurs
                if (isRight && roundedX >= 130 && roundedX <= 135 && roundedY >= 32 && roundedY <= 34)
                {
                    player2.Lives--;
                    player1.Score++;
                    soundManager.PlayPlayerHit(); // Son impact joueur
                    break;
                }
                else if (!isRight && roundedX >= 15 && roundedX <= 20 && roundedY >= 32 && roundedY <= 34)
                {
                    player1.Lives--;
                    player2.Score++;
                    soundManager.PlayPlayerHit(); // Son impact joueur
                    break;
                }


                // Pause pour visualiser le mouvement
                System.Threading.Thread.Sleep(50);

                // Effacer la position précédente de la balle
                Console.SetCursorPosition((int)posX, (int)posY);
                Console.Write(' ');

                // Mise à jour des positions
                posX = nextX;
                posY = nextY;

                // Augmentation du temps
                t += timeStep;
            }
        }

        /// <summary>
        /// Vérifie si la balle touche son propre mur. Si oui, perte de vie et gain de score pour l'adversaire.
        /// </summary>
        private bool CheckOwnWallCollision(int roundedX, int roundedY, Wall ownWall, bool isRight, Player attacker, Player opponent, SoundManager soundManager)
        {
            int wallX = isRight ? 35 : 110; // Le mur du joueur qui tire
            int wallY = 25;

            if (roundedX >= wallX && roundedX <= wallX + 2 && roundedY >= wallY && roundedY < wallY + 6)
            {
                int col = roundedX - wallX;
                int row = roundedY - wallY;

                if (col >= 0 && col < 3 && row >= 0 && row < 6)
                {
                    if (ownWall.Hit(row, col)) // Si la cellule est touchée et encore visible
                    {
                        attacker.Lives--; // Le tireur perd une vie
                        opponent.Score++; // L'adversaire gagne un point

                        // Effacer visuellement la cellule touchée
                        Console.SetCursorPosition(wallX + col, wallY + row);
                        Console.Write(' ');

                        soundManager.PlayWallHit();

                        return true; // Collision avec son propre mur détectée
                    }
                }
            }
            return false; // Pas de collision
        }

        /// <summary>
        /// Vérifie collision précise avec mur, mise à jour score.
        /// </summary>
        /// <param name="roundedX"></param>
        /// <param name="roundedY"></param>
        /// <param name="wall"></param>
        /// <param name="isRight"></param>
        /// <param name="attacker"></param>
        /// <returns></returns>
        private bool CheckWallCollision(int roundedX, int roundedY, Wall wall, bool isRight, Player attacker, SoundManager soundManager)
        {

            int wallX = isRight ? 110 : 35;     // X de départ du mur
            int wallY = 30;                     // Y de départ du mur

            // Vérification que la balle est dans la zone du mur
            if (roundedX >= wallX && roundedX <= wallX + 2 && roundedY >= wallY && roundedY < wallY + 4)
            {
                int col = roundedX - wallX; // Calcul précis de la colonne
                int row = roundedY - wallY; // Calcul précis de la ligne

                // Vérification sécurité : indices dans les limites du tableau
                if (col >= 0 && col < 3 && row >= 0 && row < 5)
                {
                    if (wall.Hit(row, col)) // Vérifie si la cellule est encore visible
                    {
                        attacker.Score++; // Mise à jour du score

                        // Efface visuellement la cellule touchée
                        Console.SetCursorPosition(wallX + col, wallY + row);
                        Console.Write(' ');

                        soundManager.PlayWallHit();

                        return true; // Collision détectée
                    }
                }
            }
            return false; // Pas de collision
        }
    }
}
