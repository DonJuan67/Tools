using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public static class EnemiesGenerator
{
    static string prebabPath = "Assets/Resources/Prefabs";
    static string oldAIScriptPath = "Assets/Scripts/Old Scripts";
    static string newAIScriptPath = "Assets/Scripts/New Scripts/AITypes";

    [MenuItem("Helper Tool/Generate 20 Enemies")]
    public static void GenerateEnemies()
    {
        string spritePath = "EnemySprites";
        Object[] sprites = Resources.LoadAll(spritePath, typeof(Sprite));

        List<MonoScript> scripts = new List<MonoScript>();
        GetAIScripts(scripts, oldAIScriptPath);

        if (sprites.Length == 0)
            Debug.Log("No sprites found in Resources/" + spritePath + "/ folder");
        else if (scripts.Count == 0)
            Debug.Log("No Enemy Scripts found in " + oldAIScriptPath + "/  folder");
        else
        {
            List<string> fullNameList = new List<string>();

            PrefabDirectoryPath();
            List<string> prefabNames = new List<string>();
            Object[] prefabs = GetPrefabs();
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

    [MenuItem("Helper Tool/Fix Prefab's AIScripts")]
    public static void FixPrefabsAIScripts()
    {
        PrefabDirectoryPath();
        Object[] prefabs = GetPrefabs();
        List<GameObject> gameObjects = new List<GameObject>();

        if (prefabs.Length != 0)
        {
            foreach (GameObject item in prefabs)
            {
                gameObjects.Add(item);
            }

            List<MonoScript> oldScripts = new List<MonoScript>();
            GetAIScripts(oldScripts, oldAIScriptPath);
            List<MonoScript> newScripts = new List<MonoScript>();
            GetAIScripts(newScripts, newAIScriptPath);
            string scriptableObjectsPath = "Scriptable Assets";
            Object[] scriptableObjects = Resources.LoadAll(scriptableObjectsPath, typeof(ScriptableObject));

            if (scriptableObjects.Length != 0)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    for (int j = 0; j < oldScripts.Count; j++)
                    {
                        if (gameObjects[i].GetComponent(oldScripts[j].GetClass()))
                        {
                            Old_AIBase old_AIBase = (Old_AIBase)gameObjects[i].GetComponent(oldScripts[j].GetClass());
                            for (int l = 0; l < newScripts.Count; l++)
                            {
                                if (oldScripts[j].name.Contains(newScripts[l].name))
                                {
                                    AIBase aIBase = (AIBase)gameObjects[i].AddComponent(newScripts[l].GetClass());
                                    aIBase.enemyType = old_AIBase.enemyType;
                                    aIBase.rb = old_AIBase.rb;
                                    aIBase.coli = old_AIBase.coli;
                                    aIBase.sr = old_AIBase.sr;
                                    aIBase.aiStats = (AIStats)scriptableObjects[Random.Range(0, scriptableObjects.Length)];

                                    GameObject.DestroyImmediate(gameObjects[i].GetComponent(oldScripts[j].GetClass()), true);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("No AIStats(ScriptableObject) found in " + scriptableObjectsPath + " Folder");
            }
        }
        else
        {
            Debug.Log("No Prefabs found in " + prebabPath + " Folder");
        }
        AssetDatabase.Refresh();
    }

    private static Object[] GetPrefabs()
    {
        return Resources.LoadAll("Prefabs", typeof(GameObject));
    }

    private static void PrefabDirectoryPath()
    {
        if (!Directory.Exists(prebabPath))
            AssetDatabase.CreateFolder("Assets/Resources", "Prefabs");
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

                if (nameTriedList.Contains(fullName))
                    nameAlredyTried = true;

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
            if (fullNameList.Contains(fullName))
            {
                nameAlreadyExist = true;
                nameTriedList.Add(fullName);
            }
            if (prefabNames.Contains(fullName))
            {
                nameAlreadyExist = true;
                nameTriedList.Add(fullName);
            }
            nbLoops++;
        } while (nameAlreadyExist);
        return fullName;
    }

    private static void GetAIScripts(List<MonoScript> scriptsListToUpdate, string scriptsPath)
    {
        DirectoryInfo dir = new DirectoryInfo(scriptsPath);
        FileInfo[] info = dir.GetFiles();
        for (int i = 0; i < info.Length; i++)
        {
            if (!info[i].Name.EndsWith(".meta") && !info[i].Name.Contains("Base") && !info[i].Name.Contains("base"))
            {
                string s = scriptsPath + "/" + info[i].Name;
                Object scriptTemp = AssetDatabase.LoadAssetAtPath(s, typeof(Object));
                scriptsListToUpdate.Add((MonoScript)scriptTemp);
            }
        }
    }
}
