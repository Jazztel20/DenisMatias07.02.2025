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
    /// Déclaration de la classe "Program"
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Déclaration de la méthode principale "Main"
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Création d'une instance de la classe "Game"
            Game game = new Game();

            // Appel de la méthode "Start" de l'objet "game" pour démarrer le jeu
            game.Start();
        }
    }
}