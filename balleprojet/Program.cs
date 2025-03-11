/// Programmation Orientée Objet en C#
/// Projet: Jeu de la balle
/// Prénom et nom : Matias Denis
/// Cours: I320
/// Classe: FID1
/// Description: la classe Program contient le point d'entrée 
///              principal du programme avec la méthode Main 
///              qui instancie un objet de la classe Game et 
///              démarre le jeu en appelant sa méthode Start.

// Déclaration de l'espace de noms "balleprojet"
using System;

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
        static void Main()
        {
            // Définition de la taille de la fenêtre console et effacement de l'écran
            Console.SetWindowSize(150, 40); // au début du programme

            // Création d'une instance de la classe "Game"
            Game game = new Game();

            // Appel de la méthode "Start" de l'objet "game" pour
            game.Start();
        }
    }
}


/// régler problèmes collisions/positions balle - mur, 
/// ajouter se rater contre son propre mur, 
/// ajouter son à l'impact  
/// rapport à compléter en expliquant la différence entre les méthodes de départ et celles utilisées, 
/// ainsi que l'aide de l'IA
/// Balle sortie du terrain