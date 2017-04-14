using UnityEngine;
using DK.Asteroids.GameLogic.Classes;

public class ProjectileController : MonoBehaviour {

    public int projectileType; //0 - bullet, 1 - laser
    public float projectileSpeed;

    public float laserLifeTime = 0.1f;

    private Projectile projectile;

    void Start()
    {
        switch (projectileType)
        {
            case 0:
                projectile = new Projectile(gameObject, projectileSpeed, projectileType);
                projectile.MoveProjectile(GetComponent<Rigidbody>());
                break;
            case 1:
                projectile = new Projectile(gameObject, projectileSpeed, projectileType);
                Destroy(gameObject, laserLifeTime);
                break;
        }
    }

    void Update()
    {
        projectile.OutOfBoundary();
    }

    void OnTriggerEnter(Collider other)
    {
        projectile.OnCollideTriggerEnter(other);
        
    }
}
