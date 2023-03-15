using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager<EnumType, ObjType, ObjCreationData> where ObjType : MonoBehaviour, IFactoryInitializable<ObjCreationData>, IPoolable
{
    HashSet<ObjType> collection = new HashSet<ObjType>();

    public void Initialize()
    {

    }

    public void Refresh()
    {

    }

    public void FixedRefresh()
    {

    }

    public void addObjToManager()
    {

    }

    public void RemoveObjFromManager()
    {

    }
}
