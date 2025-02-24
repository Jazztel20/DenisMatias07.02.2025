namespace balleprojet
{
    internal class Wall
    {
        public int[,] Cells { get; private set; }

        public Wall()
        {
            Cells = new int[6, 1]; // 6 cases formant le mur
            for (int i = 0; i < 6; i++)
            {
                Cells[i, 0] = 1; // Cellule présente
            }
        }

        public void Hit(int position)
        {
            if (Cells[position, 0] == 1)
            {
                Cells[position, 0] = 0; // Case touchée disparaît
            }
        }
    }
}
