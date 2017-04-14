using UnityEngine;
using UnityEngine.SceneManagement;

namespace DK.Asteroids.GameLogic.Classes
{
    public static class GameLoop
    {
        public static bool isStopped = false;
        public static bool isSpriteRepresentation = false;

        public static void GameOver()
        {
            isStopped = true;
            Spawner.StopSpawn();
            Time.timeScale = 0;
        }

        public static void Restart()
        {
            Scores.UpdateHighScores();

            Time.timeScale = 1;
            Spawner.ResumeSpawn();
            Scores.ResetScore();
            isStopped = false;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        public static void ChangeBackgroundDependingOnRepres(GameObject background)
        {
            var bgr = GameObject.FindGameObjectWithTag(Constants.Tags.Background);
            if (bgr)
            {
                Object.Destroy(bgr);
            }
            else
            {
                Object.Instantiate(background, new Vector3(0f, -1f, 0f), Quaternion.identity);
            }
        }
    }
}
