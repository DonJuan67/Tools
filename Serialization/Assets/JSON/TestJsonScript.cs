using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestJsonScript : MonoBehaviour {
    //XML Examples: http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer
    //Binary Formater


    //https://docs.unity3d.com/Manual/JSONSerialization.html
    // Use this for initialization


    void Start ()
    {
        //We start by serializing an existing class 
        //First we create the class
        SomeMiniClass newClass = new SomeMiniClass(5, 10, 8, new List<string>() { "hello", "world" });

        //Then we serialize it into a JSON format, which is a string
        string jsonSerializationOfNewClass = JsonUtility.ToJson(newClass);

        //Then we can write it to a file
        string directoryPath = Path.Combine(Application.streamingAssetsPath,"JsonExamples/");  //Path.combine is like just adding them +, but takes care of merging / for you (DO NOT PUT / in fron of JsonExamples)
        if (!Directory.Exists(directoryPath))                                                   //Create path if doesnt exist or error
            Directory.CreateDirectory(directoryPath);                                           

        string filePath = Path.Combine(directoryPath, "myFile.txt");                            //Get the file path
        File.WriteAllText(filePath, jsonSerializationOfNewClass);                               //Create text file, write json to it



        //Now we will read from the file, and convert back into the original class
        //First we need the text data 
        string jsonDeserialized = File.ReadAllText(filePath);

        //Then we deserialize
        SomeMiniClass newClassLoadedFromJson = JsonUtility.FromJson<SomeMiniClass>(jsonDeserialized);

        Debug.Log("Done!");


        //We will notice that 'c' was not serialized! this is because it was private.
        //We can mark it as serializable so it can be serialized


        //This library has limitations, and does not show the full functionality of JSONs in the job field,
        //Newtonsoft, a very famous JSON library has the additional feature of JSON Nodes, which will be shown from another project






    }



    [System.Serializable]
    public class SomeMiniClass
    {
        public int a, b;
        public List<string> shooka;
        [SerializeField] private int c;
        public SomeMiniClass(int _a, int _b, int _c, List<string> _shooka)
        {
            a = _a;
            b = _b;
            c = _c;
            shooka = _shooka;
        }
    }
	
}
