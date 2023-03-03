using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToJsonify : MonoBehaviour  {
    public Dictionary<string, string> someDict = new Dictionary<string, string>() { {"shaloom","whale"} };
    public int someInt = 3;
    public string shanagin = "Whoopla";
    [SerializeField] public TestJsonScript.SomeMiniClass miniClass;

	
}
