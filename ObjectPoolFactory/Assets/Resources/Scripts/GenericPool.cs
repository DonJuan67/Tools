using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenericPool<EnumType, ObjType, ObjCreationData> where ObjType : MonoBehaviour, IFactoryInitializable<ObjCreationData>, IPoolable
{
    GenericManagerSystem<EnumType, ObjType, ObjCreationData> parentManagerSystem;
    Dictionary<EnumType, Queue<ObjType>> poolDictionary;

    void Initialize(GenericManagerSystem<EnumType, ObjType, ObjCreationData> parentManagerSystem)
    {
        this.parentManagerSystem = parentManagerSystem;
        poolDictionary = new Dictionary<EnumType, Queue<ObjType>>();

        List<EnumType> enumsList = System.Enum.GetValues(typeof(EnumType)).Cast<EnumType>().ToList();

        foreach (EnumType enumType in enumsList)
        {
            poolDictionary.Add(enumType, new Queue<ObjType>());
        }
    }

    public void Pool(EnumType enumType, ObjType objToPool)
    {
        poolDictionary[enumType].Enqueue(objToPool);
    }

    public ObjType Depool(EnumType enumType)
    {
        ObjType toReturn = (poolDictionary.Count > 0) ? poolDictionary[enumType].Dequeue() : null;
        if (toReturn)
            toReturn.Depool();
        return toReturn;
    }
}
