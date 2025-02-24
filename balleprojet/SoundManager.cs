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
    /// Déclaration de la classe "SoundManager"
    /// </summary>
    internal class SoundManager
    {
        private SoundPlayer soundPlayer;

        public SoundManager(string soundFilePath)
        {
            // Initialisation de SoundPlayer avec le fichier son
            soundPlayer = new SoundPlayer(soundFilePath);
        }

        public void PlaySiund()
        {
            // Jouer le son
            soundPlayer.Play();
        }
    }
}
