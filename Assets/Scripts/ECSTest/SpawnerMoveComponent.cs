using System;
using Unity.Entities;

[Serializable]
public struct SpawnerMove : IComponentData
{
    public float Value;
}

public class SpawnerMoveComponent : ComponentDataWrapper<SpawnerMove> { } 
