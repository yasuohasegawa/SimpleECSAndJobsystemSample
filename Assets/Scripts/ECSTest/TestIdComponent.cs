using System;
using Unity.Entities;

[Serializable]
public struct TestId : IComponentData
{
    public int Value;
}

public class TestIdComponent : ComponentDataWrapper<TestId> { } 
