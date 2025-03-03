/********************************************************************************
 Programmation Orientée Objet en C#
 Projet: Jeu de la balle
 Prénom et nom : Matias Denis
 Cours: I320
 Classe: FID1
 *******************************************************************************/

using System;

// Déclaration de l'espace de noms "balleprojet"
namespace balleprojet
{
    /// <summary>
    /// Déclaration de classe "Ball"
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
        
        // ??
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
        public void CalculateTrajectory(int startX, int startY, int power, bool isRight, Player player1, Player player2, Wall wall1, Wall wall2)
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
                double nextX = isRight ? (startX + initialVelocity * Math.Cos(angleInRadians) * t) : (startX - initialVelocity * Math.Cos(angleInRadians) * t);
                double nextY = startY - (initialVelocity * Math.Sin(angleInRadians) * t - 0.5 * gravity * t * t);
                if (nextY > posY)
                {
                    Color = ConsoleColor.Yellow;
                }

                // Affichage de la position de la balle
                Console.SetCursorPosition((int)nextX, (int)nextY);
                Console.ForegroundColor = _color;
                Console.Write('o');
                Console.ResetColor();

                // Vérification des collisions avec le mur adverse
                if (isRight && (int)nextX >= 110 && (int)nextX <= 113 && (int)nextY >= 25 && (int)nextY <= 30)      // Joueur 1 tire sur le mur du joueur 2
                {
                    wall2.Hit((int)nextY - 25);                                                                     // Disparaître la case touchée
                    player1.Score++;                                                                                // Le joueur 1 marque un point
                    break;
                }
                else if (!isRight && (int)nextX >= 35 && (int)nextX <= 38 && (int)nextY >= 25 && (int)nextY <= 30)  // Joueur 2 tire sur le mur du joueur 1
                {
                    wall1.Hit((int)nextY - 25);     // Disparaître la case touchée
                    player2.Score++;                // Le joueur 2 marque un point
                    break;
                }

                // Vérification des collisions avec le joueur adverse
                if (isRight && (int)nextX >= 125 && (int)nextX <= 130 && (int)nextY >= 32 && (int)nextY <= 34) // Joueur 1 tire sur le joueur 2
                {
                    player2.Lives--;        // Le joueur 2 perd une vie
                    player1.Score++;        // Le joueur 1 marque un point
                    break;
                }
                else if (!isRight && (int)nextX >= 20 && (int)nextX <= 25 && (int)nextY >= 32 && (int)nextY <= 34) // Joueur 2 tire sur le joueur 1
                {
                    player1.Lives--;        // Le joueur 1 perd une vie
                    player2.Score++;        // Le joueur 2 marque un point
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
    }
}
