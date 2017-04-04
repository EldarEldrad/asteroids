namespace DK.Asteroids.GameLogic.Classes
{
    public static class Scores
    {
        private static int currentScore = 0;
        private static int highScore = 0;

        public static int GetHighScore()
        {
            return highScore;
        }

        public static int GetCurrentScore()
        {
            return currentScore;
        }

        public static void ResetScore()
        {
            currentScore = 0;
        }

        public static void AddScore(int addend)
        {
            currentScore += addend;
        }
        
        public static void UpdateHighScores()
        {
            if (currentScore > highScore)
            {
                highScore = currentScore;
            }
        }
    }
}
