/********************************************************************************
 Programmation Orientée Objet en C#
 Projet: Jeu de la balle
 Prénom et nom : Matias Denis
 Cours: I320
 Classe: FID1
 *******************************************************************************/

// Déclaration de l'espace de noms "balleprojet"
namespace balleprojet
{
    /// <summary>
    /// Déclaration de la classe "Wall"
    /// </summary>
    internal class Wall
    {
        /// <summary>
        /// Propriété publique en lecture seule qui représente les cellules du mur
        /// </summary>
        public int[,] Cells { get; private set; }

        /// <summary>
        /// Constructeur de la classe "Wall"
        /// </summary>
        public Wall()
        {
            // Initialise les cellules avec un tableau 2D de 6 lignes et 1 colonne
            Cells = new int[6, 1];      // 6 cases formant le mur
            
            // Boucle pour initialiser chaque cellule à 1 
            for (int i = 0; i < 6; i++)
            {
                Cells[i, 0] = 1;        // Cellule présente
            }
        }

        /// <summary>
        /// Méthode pour gérer les coups sur le mur 
        /// </summary>
        /// <param name="position">Position</param>
        public void Hit(int position)
        {
            // Si la cellule à la position donnée est présente
            if (Cells[position, 0] == 1)
            {
                // La cellule touchée disparaît (devient 0)
                Cells[position, 0] = 0; 
            }
        }
    }
}
