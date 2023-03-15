using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IFactoryInitializable<Bullet.BulletStats>, IPoolable
{
    BulletStats bulletStat;

    public void Depool()
    {
        
    }

    public void Initalize(BulletStats data)
    {
        bulletStat = data;
    }

    public void Pool()
    {
        throw new System.NotImplementedException();
    }

    public class BulletStats
    {
        public Vector2 firingDir;
        public float speed;
    }
}
