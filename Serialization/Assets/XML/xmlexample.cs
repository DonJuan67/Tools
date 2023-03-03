using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class xmlexample : MonoBehaviour {

    public void Start()
    {
        MonsterContainer monsterCollection = new MonsterContainer();
        monsterCollection.Save(Path.Combine(Application.streamingAssetsPath, "monsters.xml"));
        monsterCollection.monsters[0].Name = "Shinagami";
        monsterCollection = MonsterContainer.Load(Application.streamingAssetsPath + "/monsters.xml");
        //var xmlData = @"<MonsterCollection><Monsters><Monster name=""a""><Health>5</Health></Monster></Monsters></MonsterCollection>";
        //monsterCollection = MonsterContainer.LoadFromText(xmlData);
        string s = "";
    }
}
