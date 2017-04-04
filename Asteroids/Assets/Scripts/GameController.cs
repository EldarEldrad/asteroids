using UnityEngine;
using UnityEngine.UI;
using DK.Asteroids.GameLogic.Classes;

public class GameController : MonoBehaviour
{
    public Text scoresText;
    public Text laserText;
    public Text gameOverText;

    public GameObject background;

    public GameObject playerShip;
    public GameObject playerShipSprite;

    public float spawnRate;
    public GameObject[] hazards;
    public GameObject[] hazardsSprite;

    private Player player;

    void Start()
    {
        if(!GameLoop.isSpriteRepresentation)
        {
            Instantiate(background, new Vector3(0f, -1f, 0f), Quaternion.identity);
        }

        GameBoundary.height = GameObject.FindGameObjectWithTag(Constants.Tags.MainCamera).GetComponent<Camera>().orthographicSize * 0.9f;
        GameBoundary.width = GameBoundary.height / Screen.height * Screen.width;

        Player.SetPlayerToNull();
        player = Player.GetPlayer(playerShipSprite, playerShip);

        gameOverText.enabled = false;
        StartCoroutine(Spawner.SpawnHazards(hazards, hazardsSprite, spawnRate));
    }

    public Player GetPlayer()
    {
        return player;
    }

    void Update()
    {
        SetScoresText();
        SetLaserText();

        if (Input.GetButtonDown(Constants.Inputs.ExitButton)) Application.Quit();

        else if(Input.GetButtonDown(Constants.Inputs.Representation))
        {
            GameLoop.ChangeBackgroundDependingOnRepres(background);

            GameLoop.isSpriteRepresentation = !GameLoop.isSpriteRepresentation;
        }

        if (GameLoop.isStopped)
        {
            DisplayGameOverText();
            if (Input.GetButtonDown(Constants.Inputs.Restart))
            {
                gameOverText.enabled = false;
                GameLoop.Restart();
            }
        }
    }

    private void DisplayGameOverText()
    {
        gameOverText.enabled = true;
        gameOverText.text = string.Format(Constants.UITexts.DestroyedUIText,
                                            Scores.GetCurrentScore());
        if (Scores.GetCurrentScore() > Scores.GetHighScore())
        {
            gameOverText.text += Constants.UITexts.NewRecordUIText;
        }
    }

    private void SetScoresText()
    {
        scoresText.text = string.Format(Constants.UITexts.ScoresUIText, Scores.GetCurrentScore(), Scores.GetHighScore());
    }

    private void SetLaserText()
    {
        if (player == null) return;
        laserText.text = string.Format(Constants.UITexts.LaserChargesUIText, player.currentLaserShots);
    }
}
