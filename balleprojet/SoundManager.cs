/// Programmation Orientée Objet en C#
/// Projet: Jeu de la balle
/// Prénom et nom : Matias Denis
/// Cours: I320
/// Classe: FID1

// Ligne pour utiliser Sound Manager
using System.Media;

// Déclaration de l'espace de noms "balleprojet"
namespace balleprojet
{
    /// <summary>
    /// La classe SoundManager gère la lecture de fichiers son
    /// en utilisant la classe SoundPlayer, avec un constructeur
    /// pour initialiser le fichier audio et une méthode PlaySound
    /// pourjouer avec les sons.
    /// </summary>
    internal class SoundManager
    {
        /// <summary>
        /// 
        /// </summary>
        private SoundPlayer shotSound;
        private SoundPlayer hitWallSound;
        private SoundPlayer hitPlayerSound;

        /// <summary>
        /// Constructeur pour initialiser tous les sons
        /// </summary>
        public SoundManager(string shotSoundPath, string hitWallPath, string hitPlayerPath)
        {
            shotSound = new SoundPlayer(shotSoundPath);
            hitWallSound = new SoundPlayer(hitWallPath);
            hitPlayerSound = new SoundPlayer(hitPlayerPath);
        }

        /// <summary>
        /// Joue le son du tir
        /// </summary>
        public void PlayShot()
        {
            shotSound.Play();
        }

        /// <summary>
        /// Joue le son d'impact sur mur
        /// </summary>
        public void PlayWallHit()
        {
            hitWallSound.Play();
        }

        /// <summary>
        /// Joue le son d'impact sur joueur
        /// </summary>
        public void PlayPlayerHit()
        {
            hitPlayerSound.Play();
        }
    }
}