using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public static class EnemiesGenerator
{
    [MenuItem("Helper Tool/Generate 20 Enemies")]
    public static void GenerateEnemies()
    {
        string spritePath = "EnemySprites";
        Object[] sprites = Resources.LoadAll(spritePath, typeof(Sprite));

        List<MonoScript> scripts = new List<MonoScript>();
        string scriptsPath = GetOldAIScripts(scripts);

        if (sprites.Length == 0)
            Debug.Log("No sprites found in Resources/" + spritePath + "/ folder");
        else if (scripts.Count == 0)
            Debug.Log("No Enemy Scripts found in " + scriptsPath + "/  folder");
        else
        {
            List<string> fullNameList = new List<string>();


            string prebabPath = "Assets/Resources/Prefabs";
            if (!Directory.Exists(prebabPath))
                AssetDatabase.CreateFolder("Assets/Resources", "Prefabs");

            List<string> prefabNames = new List<string>();
            Object[] prefabs = Resources.LoadAll("Prefabs", typeof(GameObject));
            for (int i = 0; i < prefabs.Length; i++)
            {
                prefabNames.Add(prefabs[i].name);
            }

            for (int i = 0; i < 20; i++)
            {
                //Crate Unique Name
                bool noNameAvailable = false;
                string fullName = GetRandomName(fullNameList, prefabNames, ref noNameAvailable);

                if (noNameAvailable)
                    break;

                fullNameList.Add(fullName);
                //Creating enemy with componant
                GameObject gameObject = new GameObject(fullName);
                BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
                Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
                SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
                sr.sprite = (Sprite)sprites[Random.Range(0, sprites.Length)];
                Old_AIBase aIBase = (Old_AIBase)gameObject.AddComponent(scripts[Random.Range(0, scripts.Count)].GetClass());

                //Link Stuff
                aIBase.enemyType = (EnumReferences.EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnumReferences.EnemyType)).Length);
                aIBase.rb = rigidbody;
                aIBase.coli = boxCollider;
                aIBase.sr = sr;

                //Save as Prefab
                string localPath = prebabPath + "/" + gameObject.name + ".prefab";
                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
                bool prefabSuccess;
                PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, localPath, InteractionMode.UserAction, out prefabSuccess);
                if (prefabSuccess)
                    Debug.Log("Prefab was saved successfully");
                else
                    Debug.Log("Prefab failed to save" + prefabSuccess);
                AssetDatabase.Refresh();
            }
        }
    }

    private static string GetRandomName(List<string> fullNameList, List<string> prefabNames, ref bool noNameAvailable)
    {
        string firstName;
        string lastName;
        string fullName;
        int nbLoops = 0;
        bool nameAlreadyExist;
        int possibleCombination = RandomName.randomName.Count * RandomName.randomName.Count;
        List<string> nameTriedList = new List<string>();
        do
        {
            nameAlreadyExist = false;
            bool nameAlredyTried;
            int nbNameTried = 0;
            do
            {
                nameAlredyTried = false;

                int rnd = Random.Range(0, RandomName.randomName.Count);
                firstName = RandomName.randomName[rnd];
                rnd = Random.Range(0, RandomName.randomName.Count);
                lastName = RandomName.randomName[rnd];

                fullName = firstName + " " + lastName;


                for (int i = 0; i < nameTriedList.Count; i++)
                {
                    if (nameTriedList[i] == fullName)
                        nameAlredyTried = true;
                }

                if (nbNameTried >= possibleCombination)
                {
                    break;
                }
                nbNameTried++;
            } while (nameAlredyTried);

            if (nbLoops >= possibleCombination)
            {
                noNameAvailable = true;
                Debug.Log("Max possible combination of name reach, Add names in RandomName.cs");
                break;
            }
            for (int j = 0; j < fullNameList.Count; j++)
            {
                if (fullNameList[j] == fullName)
                {
                    nameAlreadyExist = true;
                    nameTriedList.Add(fullName);
                    break;
                }
            }
            for (int i = 0; i < prefabNames.Count; i++)
            {
                if (prefabNames[i] == fullName)
                {
                    nameAlreadyExist = true;
                    nameTriedList.Add(fullName);
                    break;
                }
            }
            nbLoops++;
        } while (nameAlreadyExist);
        return fullName;
    }

    private static string GetOldAIScripts(List<MonoScript> scripts)
    {
        string scriptsPath = "Assets/Scripts/Old Scripts";
        DirectoryInfo dir = new DirectoryInfo(scriptsPath);
        FileInfo[] info = dir.GetFiles();
        for (int i = 0; i < info.Length; i++)
        {
            if (!info[i].Name.EndsWith(".meta") && !info[i].Name.Contains("Base") && !info[i].Name.Contains("base"))
            {
                string s = scriptsPath + "/" + info[i].Name;
                Object scriptTemp = AssetDatabase.LoadAssetAtPath(s, typeof(Object));
                scripts.Add((MonoScript)scriptTemp);
            }
        }

        return scriptsPath;
    }
}
