using UnityEngine;
using DK.Asteroids.GameLogic.Classes;

public class PlayerController : MonoBehaviour
{
    public Transform playerShotBulletSpawn;
    public Transform playerShotLaserSpawn;
    public GameObject bulletShot;
    public GameObject bulletShotSprite;
    public GameObject laserShot;

    public GameObject explosion;

    public float thrustSpeed;
    public float rotateSpeed;
    public float bulletFireRate;
    public float addingLaserRate;

    public AudioClip bulletAudio;
    public AudioClip laserAudio;
    public AudioClip explosionAudio;

    public GameObject thisObject;
    public GameObject thisObjectSprite;

    private Player player;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = Player.GetPlayer(thisObjectSprite, thisObject);
        if (player != null) player.SetPlayerSettings(thisObject, thisObjectSprite, thrustSpeed, rotateSpeed, bulletFireRate, addingLaserRate, explosion);
    }

    void Update ()
    {
        player.OutOfBoundary();

        if (player != null)
        {
            player.UpdateAddLaser();

            if (Input.GetButtonDown(Constants.Inputs.Representation))
            {
                player.ChangeRepresentation();
            }

            if (Input.GetButton(Constants.Inputs.BulletShot) && player.TryShootBullet())
            {
                audioSource.clip = bulletAudio;
                audioSource.Play();
                var bullet = Instantiate(GameLoop.isSpriteRepresentation ? bulletShotSprite : bulletShot,
                    playerShotBulletSpawn.position, playerShotBulletSpawn.rotation);
                Destroy(bullet, 2f);
            }
            if (Input.GetButtonDown(Constants.Inputs.LaserShot) && player.TryShootLaser())
            {
                audioSource.clip = laserAudio;
                audioSource.Play();
                Instantiate(laserShot, playerShotLaserSpawn.position, DefineRotation());
            }
        }
    }

    void FixedUpdate ()
    {
        if(player != null)
        {
            var thrust = Input.GetAxisRaw(Constants.Inputs.Vertical);
            var turning = Input.GetAxisRaw(Constants.Inputs.Horizontal);

            player.MovePlayer(thrust, turning);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != Constants.Tags.Boundary)
        {
            audioSource.clip = explosionAudio;
            audioSource.Play();
            player.CollideWithDangerous(gameObject, explosionAudio.length);
        }
    }

    private Quaternion DefineRotation()
    {
        if (GameLoop.isSpriteRepresentation)
        {
            return Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
        return transform.rotation;
    }
}
