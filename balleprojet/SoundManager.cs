/********************************************************************************
 Programmation Orientée Objet en C#
 Projet: Jeu de la balle
 Prénom et nom : Matias Denis
 Cours: I320
 Classe: FID1
 *******************************************************************************/

// Ligne pour utiliser Sound Manager
using System.Media;

// Déclaration de l'espace de noms "balleprojet"
namespace balleprojet
{
    /// <summary>
    /// Déclaration de la classe "SoundManager" pour gérer les sons
    /// </summary>
    internal class SoundManager
    {
        // Déclaration de SoundPlayer
        private SoundPlayer soundPlayer;

        /// <summary>
        /// Constructeur de la classe SoundManager
        /// </summary>
        /// <param name="soundFilePath"></param>
        public SoundManager(string soundFilePath)
        {
            // Initialisation de SoundPlayer avec le fichier son
            soundPlayer = new SoundPlayer(soundFilePath);
        }

        /// <summary>
        /// Méthode pour jouer le son 
        /// </summary>
        public void PlaySound()
        {
            // Joue le son
            soundPlayer.Play();
        }
    }
}
