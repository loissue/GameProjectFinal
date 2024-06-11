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

    public Transform ShootPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moupos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = moupos - (Vector2)Gun.position;
        gunFace();

        if(Input.GetMouseButtonDown(0))
        {
            Shooting();
        }

    }

    void gunFace()
    {
        Gun.transform.right = direction;
    }
    void Shooting()
    {
        GameObject BulletIns = Instantiate(Bullet,ShootPoint.position,ShootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * BulletSpeed);
        Destroy(BulletIns,3);
    }
}
