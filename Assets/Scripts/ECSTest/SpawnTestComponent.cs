using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct SpawnTest : ISharedComponentData
{
    public GameObject prefab;
    public int count;
}

public class SpawnTestComponent : SharedComponentDataWrapper<SpawnTest> { }