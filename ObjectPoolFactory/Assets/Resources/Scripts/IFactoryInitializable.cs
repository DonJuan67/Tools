using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactoryInitializable<T>
{
    void Initalize(T data);
}
