using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    public enum BulletType { PewPew, Zoooms };
    // Start is called before the first frame update
    void Start()
    {
        GenericFactory<BulletType, Bullet, Bullet.BulletStats> bulletFactory = new GenericFactory<BulletType, Bullet, Bullet.BulletStats>();
        bulletFactory.Create(BulletType.PewPew,
            new Bullet.BulletStats
            {
                firingDir = new Vector2(3, 4).normalized,
                speed = 3f
            }
            );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
