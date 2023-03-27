using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
public class FindMeAttribute : Attribute
{
    public string someCustomData;
    public int moreSampleData;

    public FindMeAttribute(string someCustomData, int moreSampleData)
    {
        this.someCustomData = someCustomData;
        this.moreSampleData = moreSampleData;
    }
}
