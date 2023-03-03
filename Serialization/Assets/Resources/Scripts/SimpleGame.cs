using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGame : MonoBehaviour
{
    public int nbOfObjects;
    public Transform boundMin;
    public Transform boundMax;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nbOfObjects; i++)
        {
            int rnd = Random.Range((int)PrimitiveType.Sphere, ((int)PrimitiveType.Cube + 1));
            GameObject g = GameObject.CreatePrimitive((PrimitiveType)rnd);
            g.transform.position = new Vector3(Random.Range(boundMin.position.x, boundMax.position.x), 9, Random.Range(boundMin.position.z, boundMax.position.z));
            g.transform.rotation = new Quaternion(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
            g.AddComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
