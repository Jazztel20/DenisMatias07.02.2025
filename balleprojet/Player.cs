namespace balleprojet
{
    internal class Player
    {
        public string Name { get; set; }
        public int Lives { get; set; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
            Lives = 6;
            Score = 0;
        }
    }
}
