using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericManagerSystem<EnumType, ObjType, ObjCreationData> where ObjType : MonoBehaviour, IFactoryInitializable<ObjCreationData>, IPoolable
{
    public GenericPool<EnumType, ObjType, ObjCreationData> objectPool;
    public Manager<EnumType, ObjType, ObjCreationData> manager;
    public GenericFactory<EnumType, ObjType, ObjCreationData> genericFactory;
}
