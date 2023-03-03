using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefExample : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //PlayerPrefs.SetInt("Number of Cubes", 5);


        int r = PlayerPrefs.GetInt("Number of Cubes");
        Debug.Log("cubes: " + r);

	}


}
