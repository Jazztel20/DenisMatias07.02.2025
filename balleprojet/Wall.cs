
/// Programmation Orientée Objet en C#
/// Projet: Jeu de la balle
/// Prénom et nom : Matias Denis
/// Cours: I320
/// Classe: FID1

// Déclaration de l'espace de noms "balleprojet"
namespace balleprojet
{
    /// <summary>
    /// La classe Wall représente un mur composé d'un tableau 
    /// 2D de cellules (Mur), initialisées et manipulées via
    /// un constructeur, permettant de gérer l'état de visibilité 
    /// des cellules lorsqu'elles sont touchées grâce à la méthode Hit.
    /// </summary>
    internal class Wall
    {
        /// <summary>
        /// Tableau à deux dimension pour les murs
        /// </summary>
        public Mur[,] Cells { get; private set; }

        /// <summary>
        /// Constructeur de la classe "Wall"
        /// </summary>
        public Wall()
        {
            // Initialise les cellules avec un tableau 2D de 6 lignes et 1 colonne
            Cells = new Mur[6, 3];      // 6 cases formant le mur

            // Boucle pour initialiser chaque cellule à 1 
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cells[i, j] = new Mur();      // Cellule présente
                }
            }
        }

        /// <summary>
        /// Méthode pour gérer les coups sur le mur 
        /// </summary>
        /// <param name="position">Position</param>
        public bool Hit(int row, int col)
        {
            // Si la cellule à la position donnée est présente
            if (row >= 0 && row < Cells.GetLength(0) && col >= 0 && col < Cells.GetLength(1) && Cells[row, col].EstVisible)
            {
                // La cellule touchée disparaît (devient 0)
                Cells[row, col].EstVisible = false;
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Classe Mur
    /// </summary>
    internal class Mur
    {
        public bool EstVisible { get; set; } = true;
    }
}
