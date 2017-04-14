using UnityEngine;
using DK.Asteroids.GameLogic.Classes;

public class SaucerController : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectileSprite;
    public Transform shotSpawn;

    public float rateOfFire;
    public float movementSpeed;

    public int pointsValue;

    public GameObject explosion;
    public AudioClip explosionAudio;

    public GameObject thisObject;
    public GameObject thisObjectSprite;

    private Saucer saucer;

	void Start ()
    {
        saucer = new Saucer(thisObject, thisObjectSprite, gameObject, rateOfFire, movementSpeed, pointsValue, explosion, explosionAudio);
        StartCoroutine(saucer.ShotAtPlayer(projectile, projectileSprite, shotSpawn));
	}

	void Update ()
    {
        saucer.OutOfBoundary();

        if (Input.GetButtonDown(Constants.Inputs.Representation))
        {
            saucer.ChangeRepresentation();
        }

        var playerTransform = Player.GetPlayerTransform();
        if (playerTransform)
        {
            saucer.AlignToTarget(playerTransform);
            saucer.MoveToTarget(playerTransform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (saucer != null) saucer.ActionOnCollide(other);
    }
}
