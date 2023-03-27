using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    Dictionary<string, MethodInfo> myDict = new Dictionary<string, MethodInfo>();
    // Start is called before the first frame update
    void Start()
    {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        Dog bobDog = new Dog("Bob", 5, 500, false);
        Dog whiskerCat = new Dog("Whisker", 2, 1500, true);
        //System
        Type dogType = typeof(Dog);
        //dogType = bobDog.GetType();
        //dogType = Type.GetType("Dog");

        //FieldInfo dogNameFieldInfo = dogType.GetField("dogName", bindingFlags);
        //string dogName = (string)dogNameFieldInfo.GetValue(bobDog);
        //Debug.Log(dogName);

        //FieldInfo dogAgeFieldInfo = dogType.GetField("dogAge", bindingFlags);
        //int dogAge = (int)dogAgeFieldInfo.GetValue(bobDog);
        //Debug.Log(dogAge);

        //OutputField(bobDog, "dogName");
        //OutputField(new Vector2(5, 6), "x");
        //int age = GetValueInField<int>(bobDog, "dogAge");

        MethodInfo methodInfo = typeof(Dog).GetMethod("OutputDogName", bindingFlags);
        methodInfo.Invoke(bobDog, new object[] { });
        methodInfo.Invoke(whiskerCat, new object[] { });

        methodInfo = typeof(Dog).GetMethod("SetDogIQ", bindingFlags);
        methodInfo.Invoke(bobDog, new object[] { 100 });
        methodInfo.Invoke(whiskerCat, new object[] { 56010 });
        ParameterInfo[] parameterInfos = methodInfo.GetParameters();

        PropertyInfo propertyInfo = typeof(Dog).GetProperty("isADog", bindingFlags);
        MethodInfo isADogGetMethode = propertyInfo.GetGetMethod();
        MethodInfo isADogSetMethode = propertyInfo.GetGetMethod();

        //ParameterInfo
    }

    [ExposeMethodInEditor()]
    public void HowManyFloats()
    {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        int nbOfFloatTypes = 0;
        Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var t in allTypes)
        {
            FieldInfo[] fieldInfos = t.GetFields(bindingFlags);
            foreach (var fi in fieldInfos)
            {
                if(fi.FieldType == typeof(float))
                {
                    Debug.Log("Float found: " + t + ", fieldName: " + fi.Name);
                    nbOfFloatTypes++;
                }
            }
        }
        Debug.Log(nbOfFloatTypes);
    }

    public void SomeFunct()
    {
        FieldInfo[] allFields = typeof(Dog).GetFields();
        foreach (var fi in allFields)
        {
            FindMeAttribute findMeAtt = fi.GetCustomAttribute<FindMeAttribute>();
            if(findMeAtt != null)
            {
                string secretFieldInside = findMeAtt.someCustomData;
            }
        }

        
    }

    public static void OutputField(object target, string fieldName)
    {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        Type theType = target.GetType();
        FieldInfo fi = theType.GetField(fieldName, bindingFlags);
        Debug.Log(fi.GetValue(target).ToString());
    }

    public static T GetValueInField<T>(object target, string fieldName)
    {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        Type type = target.GetType();
        return (T)(type.GetField(fieldName, bindingFlags).GetValue(target));
    }
}

[FindMe("Doggo", 4)]
public class Dog
{
    [FindMe("DoggoName", 100)] public string dogName;
    int dogAge;
    [SerializeField] float IQ;
    [HideInInspector] bool isSecretlyACat;
    public bool isADog => !isSecretlyACat;
    public float someFloat { get; set; }

    public Dog(string dogName, int dogAge, float iQ, bool isSecretlyACat)
    {
        this.dogName = dogName;
        this.dogAge = dogAge;
        IQ = iQ;
        this.isSecretlyACat = isSecretlyACat;
    }

    public void OutputDogName()
    {
        Debug.Log("Name: " + dogName);
    }

    bool SecretCatFunction()
    {
        if (isSecretlyACat)
            Debug.Log("you found " + dogName + " secret cat function, it is a cat");
        else
            Debug.Log("you found " + dogName + " secret cat function, but is not a cat");

        return isSecretlyACat;
    }

    void SetDogIQ(float newIQ)
    {
        IQ = newIQ;
        Debug.Log($"{dogName} new IQ is: {IQ}");
    }

}
