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
    public float shootInterval = 0.1f; // Reduced interval in seconds between shots

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
        List<string> buffs = new List<string>();

        while (Input.GetMouseButton(0))
        {
            while (a < Bullets.Length && Bullets[a] == null)
            {
                a++;
                if (a == Bullets.Length)
                {
                    a = 0;
                }
            }

            if (a < Bullets.Length && Bullets[a] != null)
            {
                if (Bullets[a].CompareTag("buffbullet"))
                {
                    if (Bullets[a].GetComponent<TripleShotBullet>() != null)
                    {
                        buffs.Add("TripleShot");
                    }
                    if (Bullets[a].GetComponent<CurveBullet>() != null)
                    {
                        buffs.Add("CurveBullet");
                    }
                    a++;
                    if (a == Bullets.Length)
                    {
                        a = 0;
                    }
                }
                else
                {
                    Debug.Log("Shooting with buffs: " + string.Join(", ", buffs));
                    Shooting(Bullets[a], buffs);
                    a++;
                    if (a == Bullets.Length)
                    {
                        a = 0;
                    }
                    buffs.Clear();
                }
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

    void Shooting(GameObject bullet, List<string> buffs)
    {
        int tripleShotCount = buffs.FindAll(buff => buff == "TripleShot").Count;

        if (tripleShotCount > 0)
        {
            ShootTripleShot(bullet, buffs.Contains("CurveBullet"), tripleShotCount);
        }
        else
        {
            ShootBullet(bullet, buffs.Contains("CurveBullet"));
        }
    }

    void ShootTripleShot(GameObject bullet, bool curveBullet, int tripleShotCount)
    {
        int totalShots = tripleShotCount * 3;
        float angleIncrement = 30f / (totalShots - 1); // Spread shots over 30 degrees

        for (int i = 0; i < totalShots; i++)
        {
            float angle = -15f + angleIncrement * i;
            GameObject BulletIns = Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);
            BulletIns.transform.Rotate(0, 0, angle);
            Rigidbody2D rb = BulletIns.GetComponent<Rigidbody2D>();

            if (rb == null)
            {
                rb = BulletIns.AddComponent<Rigidbody2D>();
            }

            if (curveBullet)
            {
                ZigZagMovement zigzagMovement = BulletIns.GetComponent<ZigZagMovement>();
                if (zigzagMovement != null)
                {
                    zigzagMovement.ActivateZigZag(BulletSpeed);
                }
            }
            else
            {
                rb.AddForce(BulletIns.transform.right * BulletSpeed, ForceMode2D.Impulse);
            }

            Destroy(BulletIns, 3); // Destroy after 3 seconds to ensure it has enough time to move
        }
    }

    void ShootBullet(GameObject bullet, bool curveBullet)
    {
        GameObject BulletIns = Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);
        Rigidbody2D rb = BulletIns.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = BulletIns.AddComponent<Rigidbody2D>();
        }

        if (curveBullet)
        {
            ZigZagMovement zigzagMovement = BulletIns.GetComponent<ZigZagMovement>();
            if (zigzagMovement != null)
            {
                zigzagMovement.ActivateZigZag(BulletSpeed);
            }
        }
        else
        {
            rb.AddForce(BulletIns.transform.right * BulletSpeed, ForceMode2D.Impulse);
        }

        Destroy(BulletIns, 3); // Destroy after 3 seconds to ensure it has enough time to move
    }
}
