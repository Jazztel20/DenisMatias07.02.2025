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
    /// Déclaration de la classe "Player"
    /// </summary>
    internal class Player
    {
        // Propriétés publiques de la classe Player

        /// <summary>
        /// Nom du joueur
        /// </summary>
        public string Name { get; set; }    

        /// <summary>
        /// Nombre de vies du joueur
        /// </summary>
        public int Lives { get; set; }      

        /// <summary>
        /// Score du joueur
        /// </summary>
        public int Score { get; set; } 

        /// <summary>
        /// Constructeur de la classe Player
        /// </summary>
        /// <param name="name">Nom</param>
        public Player(string name)
        {
            Name = name;    // Initialise la propriété name avec le paramètre name
            Lives = 5;      // Initialise la propriété Lives à 6
            Score = 0;      // Initialise la propriété Score à 0
        }
    }
}
