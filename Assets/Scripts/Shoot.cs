using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform Gun;
    public float damage = 10f;
    Vector2 direction;
    public GameObject Bullet;
    public float BulletSpeed;
    public Magazin Magazin;
    public Transform ShootPoint;
    public GunInven guninven;
    public GameObject[] Bullets;
    int a = 0;
    private bool isShooting = false;
    public  float shootInterval = 1.0f; // Interval in seconds between shots

    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moupos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = moupos - (Vector2)Gun.position;
        gunFace();

        if (Input.GetMouseButton(0) && !isShooting) // Check if mouse button is held down and if not already shooting
        {
            StartCoroutine(ShootCoroutine());
        }
    }

    private IEnumerator ShootCoroutine()
    {
        isShooting = true;

        while (Input.GetMouseButton(0))
        {
            if (Bullets[a] != null)
            {
                if (Bullets[a].GetComponent<CurveBullet>() != null)
                {
                    a++;
                    if (a < Bullets.Length && Bullets[a] != null)
                    {
                        Debug.Log("Shooting with ZigZag");
                        Shooting(Bullets[a], true);
                        a++;
                    }
                }
                else
                {
                    Debug.Log("Shooting");
                    Shooting(Bullets[a], false);
                    a++;
                }
            }

            while (a < Bullets.Length && Bullets[a] == null)
            {
                a++;
                if (a == Bullets.Length)
                {
                    a = 0;
                }
            }

            if (a == Bullets.Length)
            {
                a = 0;
            }

            yield return new WaitForSeconds(shootInterval); // Wait for the specified interval before the next shot
        }

        isShooting = false;
    }

    public void getbulletlist(GameObject[] bulletLists)
    {
        Bullets = bulletLists;
    }

    void gunFace()
    {
        Gun.transform.right = direction;
    }

    void Shooting(GameObject bullet, bool zigzag)
    {
        GameObject BulletIns = Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);
        Rigidbody2D rb = BulletIns.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = BulletIns.AddComponent<Rigidbody2D>();
        }

        if (zigzag)
        {
            var zigzagMovement = BulletIns.AddComponent<ZigZagMovement>();
            zigzagMovement.BulletSpeed = BulletSpeed; // Set the BulletSpeed
        }
        else
        {
            rb.AddForce(BulletIns.transform.right * BulletSpeed, ForceMode2D.Impulse);
        }

        Destroy(BulletIns, 3); // Destroy after 3 seconds to ensure it has enough time to move
    }
}
