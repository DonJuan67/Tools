using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericFactory<EnumType, ObjType, ObjCreationData> where ObjType : MonoBehaviour, IFactoryInitializable<ObjCreationData>, IPoolable
{
    public Dictionary<EnumType, GameObject> resourceDir;
    GenericManagerSystem<EnumType, ObjType, ObjCreationData> parentManagerSystem;

    void Initialize(GenericManagerSystem<EnumType, ObjType, ObjCreationData> parentManagerSystem)
    {
        this.parentManagerSystem = parentManagerSystem;

        resourceDir = new Dictionary<EnumType, GameObject>();
        List<EnumType> enumsList = System.Enum.GetValues(typeof(EnumType)).Cast<EnumType>().ToList();

        foreach (EnumType enumType in enumsList)
        {
            try
            {
                GameObject resourceObj = Resources.Load<GameObject>("Prefabs/" + enumType.ToString());
                if (!resourceObj)
                {
                    Debug.Log("Error, could not cache resource: " + enumsList.ToList());
                }
                resourceDir.Add(enumType, resourceObj);
            }
            catch (System.Exception e)
            {
                Debug.Log("Error, could not cache resource: " + enumsList.ToString() + ", e: " + e.Message.ToString());
            }
        }
    }

    public ObjType Create(EnumType enumType, ObjCreationData creationData)
    {
        ObjType toRet = DepoolObject(enumType);
        if(toRet == null)
        {
            toRet = _Create(enumType);
        }
        toRet.Initalize(creationData);
        return toRet;
    }

    private ObjType _Create(EnumType enumType)
    {
        return GameObject.Instantiate(resourceDir[enumType]).GetComponent<ObjType>();
    }

    private ObjType DepoolObject(EnumType enumType)
    {
        return parentManagerSystem.objectPool.Depool(enumType);
    }
}
