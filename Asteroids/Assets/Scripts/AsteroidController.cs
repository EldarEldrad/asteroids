using UnityEngine;
using DK.Asteroids.GameLogic.Classes;

public class AsteroidController : MonoBehaviour
{
    public float speedCoeff;
    public int pointsValue;
    public GameObject explosion;

    public GameObject smallerAsteroid;
    public GameObject smallerAsteroidSprite;

    public GameObject thisObject;
    public GameObject thisObjectSprite;

    private Asteroid asteroid;

	void Start ()
    {
        var rb = GetComponent<Rigidbody>();

        if (rb.velocity.magnitude == 0)
        {
            rb.velocity = Random.insideUnitSphere * speedCoeff;
        }

        asteroid = new Asteroid(thisObject, thisObjectSprite, gameObject, explosion, smallerAsteroid, smallerAsteroidSprite, pointsValue);
	}

    void Update()
    {
        asteroid.OutOfBoundary();

        if (Input.GetButtonDown(Constants.Inputs.Representation))
        {
            asteroid.ChangeRepresentation();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(asteroid != null)
        {
            asteroid.ActionOnCollide(other);
        }
    }
}
