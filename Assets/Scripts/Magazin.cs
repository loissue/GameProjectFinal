using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magazin : MonoBehaviour
{
    public Shoot shoot;

    public GameObject bullet1;
    public GameObject bullet2;
    public GunInven GunInven;

    public GameObject[] Bullets;
    // Start is called before the first frame update
    void Start()
    {
        //giveoutMagazin();
        Bullets = new GameObject[4];
        Bullets[0] = bullet1;
        Bullets[1] = bullet2;
        Bullets[2] = bullet1;
        Bullets[3] = bullet1;
        

    }
    public void addbullettomagazin(GameObject bullet)
    {
        for (int i=0; i <Bullets.Length;i++)
        {
            if (Bullets[i] == null)
            {
                Bullets[i] = bullet;
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        shoot.getbulletlist(Bullets);
        GunInven.GetFirstGun(Bullets, this.gameObject);
    }
    //public void giveoutMagazin() 
    //{
        

    //    GunInven.GetFirstGun(Bullets,this.gameObject);
    //}
}
