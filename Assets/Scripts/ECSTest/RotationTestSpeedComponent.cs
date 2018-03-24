using System;
using Unity.Entities;

[Serializable]
public struct RotationTestSpeed : IComponentData
{
    public float Value;
}

public class RotationTestSpeedComponent : ComponentDataWrapper<RotationTestSpeed> { } 
