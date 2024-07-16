using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private float shootInterval = 1.0f; // Interval in seconds between shots
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
                
                Shooting(Bullets[a]); // Assuming BulletList has a property Bullet of type GameObject
                a++;
                if (a == Bullets.Length)
                {

                    a = 0;
                }
                while (Bullets[a] == null)
                {
                    a++;
                    Debug.Log(Bullets.Length);
                    if (a == Bullets.Length)
                    {

                        a = 0;
                    }
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
        //guninven.GetBUllet(Bullets);
    }
    void gunFace()
    {
        Gun.transform.right = direction;
    }
    void Shooting(GameObject bullet)
    {
        
            
                
                GameObject BulletIns = Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);
                BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * BulletSpeed);
                Destroy(BulletIns, 3);
            
            
        
        
    }
}
