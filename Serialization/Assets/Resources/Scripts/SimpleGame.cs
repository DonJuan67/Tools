using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

public class SimpleGame : MonoBehaviour, ISavable
{
    public int nbOfObjects;
    List<GameObject> g = new List<GameObject>();
    List<Rigidbody> rb = new List<Rigidbody>();
    Shapes shapes = new Shapes();
    public string fileName;
    string filePath;
    public Transform boundMin;
    public Transform boundMax;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nbOfObjects; i++)
        {
            int rnd = Random.Range((int)PrimitiveType.Sphere, ((int)PrimitiveType.Cube + 1));
            shapes.shapesDatas.Add(new ShapesData());
            shapes.shapesDatas[i].shape = (PrimitiveType)rnd;
            g.Add(GameObject.CreatePrimitive((PrimitiveType)rnd));
            g[i].transform.position = new Vector3(Random.Range(boundMin.position.x, boundMax.position.x), Random.Range(9, 20), Random.Range(boundMin.position.z, boundMax.position.z));
            g[i].transform.rotation = new Quaternion(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
            rb.Add(g[i].AddComponent<Rigidbody>());
        }

        GetFilePathJson(fileName);
    }

    #region GetPath
    private string GetFilePathJson(string fileName)
    {
        string directoryPath = Path.Combine(Application.streamingAssetsPath, "JsonExamples/");
        return FilePath(fileName, directoryPath);
    }
    private string GetFilePathXML(string fileName)
    {
        string directoryPath = Path.Combine(Application.streamingAssetsPath, "XML/");
        return FilePath(fileName, directoryPath);
    }
    private string GetFilePathBinary(string fileName)
    {
        string directoryPath = Path.Combine(Application.streamingAssetsPath, "Binary/");
        return FilePath(fileName, directoryPath);
    }

    private string FilePath(string fileName, string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        filePath = Path.Combine(directoryPath, fileName + ".txt");
        return filePath;
    }
    #endregion

    #region HandleData
    private void SetShapeData()
    {
        for (int i = 0; i < nbOfObjects; i++)
        {
            shapes.shapesDatas[i].posX = g[i].transform.position.x;
            shapes.shapesDatas[i].posY = g[i].transform.position.y;
            shapes.shapesDatas[i].posZ = g[i].transform.position.z;
            shapes.shapesDatas[i].rotX = g[i].transform.rotation.x;
            shapes.shapesDatas[i].rotY = g[i].transform.rotation.y;
            shapes.shapesDatas[i].rotZ = g[i].transform.rotation.z;
            shapes.shapesDatas[i].rotW = g[i].transform.rotation.w;
            shapes.shapesDatas[i].velX = rb[i].velocity.x;
            shapes.shapesDatas[i].velY = rb[i].velocity.y;
            shapes.shapesDatas[i].velZ = rb[i].velocity.z;
            shapes.shapesDatas[i].angVelX = rb[i].angularVelocity.x;
            shapes.shapesDatas[i].angVelY = rb[i].angularVelocity.y;
            shapes.shapesDatas[i].angVelZ = rb[i].angularVelocity.z;
        }
        shapes.nbOfShapes = nbOfObjects;
    }
    private void ResetScene()
    {
        if (g.Count != 0)
        {
            for (int i = 0; i < nbOfObjects; i++)
            {
                Destroy(g[i].gameObject);
            }
        }
        g.Clear();
        rb.Clear();
    }
    private void LoadShapes()
    {
        nbOfObjects = shapes.nbOfShapes;

        for (int i = 0; i < shapes.nbOfShapes; i++)
        {
            g.Add(GameObject.CreatePrimitive(shapes.shapesDatas[i].shape));
            rb.Add(g[i].AddComponent<Rigidbody>());

            g[i].transform.position = new Vector3(shapes.shapesDatas[i].posX, shapes.shapesDatas[i].posY, shapes.shapesDatas[i].posZ);
            g[i].transform.rotation = new Quaternion(shapes.shapesDatas[i].rotX, shapes.shapesDatas[i].rotY, shapes.shapesDatas[i].rotZ, shapes.shapesDatas[i].rotW);
            rb[i].velocity = new Vector3(shapes.shapesDatas[i].velX, shapes.shapesDatas[i].velY, shapes.shapesDatas[i].velZ);
            rb[i].angularVelocity = new Vector3(shapes.shapesDatas[i].angVelX, shapes.shapesDatas[i].angVelY, shapes.shapesDatas[i].angVelZ);
        }
    }
    #endregion

    #region Json
    public void SaveJson()
    {
        SetShapeData();
        string jsonSerialization = JsonUtility.ToJson(shapes, true);
        File.WriteAllText(GetFilePathJson(fileName), jsonSerialization);
    }

    public void LoadJson()
    {
        ResetScene();
        if (File.Exists(GetFilePathJson(fileName)))
        {
            string jsonDeserialized = File.ReadAllText(filePath);
            shapes = JsonUtility.FromJson<Shapes>(jsonDeserialized);
            LoadShapes();
        }
        else
            Debug.Log("Json File doesn't exist");
    }
    #endregion

    #region XML
    public void SaveXML()
    {
        SetShapeData();
        var serializer = new XmlSerializer(typeof(Shapes));
        using (var stream = new FileStream(GetFilePathXML(fileName), FileMode.Create))
        {
            serializer.Serialize(stream, shapes);
        }
    }

    public void LoadXML()
    {
        ResetScene();
        if (File.Exists(GetFilePathXML(fileName)))
        {
            var serializer = new XmlSerializer(typeof(Shapes));
            using (var stream = new FileStream(GetFilePathXML(fileName), FileMode.Open))
            {
                shapes = serializer.Deserialize(stream) as Shapes;
            }
            LoadShapes();
        }
        else
            Debug.Log("XML File doesn't exist");
    }

    #endregion

    #region Binary
    public void SaveBinary()
    {
        SetShapeData();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetFilePathBinary(fileName));
        bf.Serialize(file, shapes);
    }

    public void LoadBinary()
    {
        ResetScene();
        if (File.Exists(GetFilePathBinary(fileName)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var stream = new FileStream(GetFilePathBinary(fileName), FileMode.Open))
            {
                shapes = (Shapes)bf.Deserialize(stream);
            }
            LoadShapes();
        }
        else
            Debug.Log("Binary File doesn't exist");
    }
    #endregion

    [System.Serializable]
    public class Shapes
    {
        public List<ShapesData> shapesDatas = new List<ShapesData>();
        public int nbOfShapes;
    }

    [System.Serializable]
    public class ShapesData
    {
        public PrimitiveType shape;
        public float posX;
        public float posY;
        public float posZ;
        public float rotX;
        public float rotY;
        public float rotZ;
        public float rotW;
        public float velX;
        public float velY;
        public float velZ;
        public float angVelX;
        public float angVelY;
        public float angVelZ;
    }
}
