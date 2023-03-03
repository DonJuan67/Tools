using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;
using System.Linq;

public class SimpleJSONExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string filePathToLoad = Path.Combine(Application.streamingAssetsPath, "Examples of formats/GrottoTemple.json");
        JSONNode jnode = JSON.Parse(filePathToLoad);

        string cardName = jnode["cardName"];
        
        JSONNode isJetpackActive = jnode["playerLoadoutGameData"]["jetpackActive"];
        string isJetpackActiveString = isJetpackActive.Value;
        bool isJetpackActiveBool = bool.Parse(isJetpackActiveString);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
