using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Exercice1();
        Exercice3();
        Exercice4();
        Exercice5();
        Exercice6();
    }
    private void FixedUpdate()
    {
        Exercice2();
    }


    void Exercice1()
    {
        Debug.Log("Exercice 1:");
        Vector2 vector = new Vector2(1, 5);
        Debug.Log(vector.GetRandomNumberBetween());
        Vector2 vector1 = new Vector2(1, 100);
        Debug.Log(vector1.GetRandomNumberBetween());
        Vector2 vector2 = new Vector2(5, 10);
        Debug.Log(vector2.GetRandomNumberBetween());
    }
    void Exercice2()
    {
        gameObject.GetComponent<Rigidbody>().SpeedLimit(5);
    }
    void Exercice3()
    {
        Debug.Log("Exercice 3:");
        string[] facts = new string[6] { "Need", "Some", "Sleep", "Like", "4real", "4real" };
        string stringWithLabel = facts.OutputWithLabel("Straight Facts");
        Debug.Log(stringWithLabel);
    }

    void Exercice4()
    {
        Debug.Log("Exercice 4:");
        string[] facts = new string[6] { "Need", "Some", "Sleep", "Like", "4real", "4real" };
        string stringWithLabel = facts.OutputWithLabelSequel("Straight Facts");
        Debug.Log(stringWithLabel);
        float[] numbersF = new float[3] { 3.5f, 10.85f, 90.3124f };
        stringWithLabel = numbersF.OutputWithLabelSequel("Some floats numbers");
        Debug.Log(stringWithLabel);
        int[] numbersI = new int[4] { 1, 2, 3, 4 };
        stringWithLabel = numbersI.OutputWithLabelSequel("Some int numbers");
        Debug.Log(stringWithLabel);
    }

    void Exercice5()
    {
        Debug.Log("Exercice 5:");
        string[] facts = new string[6] { "Need", "Some", "Sleep", "Like", "4real", "4real" };
        bool result = facts.CheckIfArrayContains("4real");
        Debug.Log(result);
        float[] numbersF = new float[3] { 3.5f, 10.85f, 90.3124f };
        result = numbersF.CheckIfArrayContains(10.85f);
        Debug.Log(result);
        int[] numbersI = new int[4] { 1, 2, 3, 4 };
        result = numbersI.CheckIfArrayContains(4);
        Debug.Log(result);
        Vector2[] v2 = new Vector2[2] { new Vector2(3, 4), new Vector2(5, 6) };
        result = v2.CheckIfArrayContains(new Vector2(5, 6));
        Debug.Log(result);
    }

    void Exercice6()
    {
        Debug.Log("Exercice 6:");
        int[] arrInt = new int[5] { 1, 2, 3, 4, 5 };
        Debug.Log(arrInt.TrueForAll((c) => { return c > 0; }));
    }
}
