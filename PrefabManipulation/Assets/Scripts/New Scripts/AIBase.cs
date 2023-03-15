using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : MonoBehaviour
{
    public EnumReferences.EnemyType enemyType;
    public Rigidbody2D rb;
    public Collider2D coli;
    public SpriteRenderer sr;
    public AIStats aiStats;

    


    public void Start()
    {
        

    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            aiStats.hp--;
            if(aiStats.hp < 0)
            {
                GameObject.Destroy(gameObject);
            }
        }

    }
}
