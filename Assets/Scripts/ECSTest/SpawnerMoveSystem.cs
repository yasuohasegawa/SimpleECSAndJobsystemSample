using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SpawnerMoveSystem : JobComponentSystem
{
    private float outerCount = 0;
    [ComputeJobOptimization]
    struct SpawnerMovePosition : IJobProcessComponentData<Position, Rotation>
    {
        public float dt;
        public float count;

        public void Execute(ref Position position, ref Rotation rotation)
        {
            rotation.Value = math.mul(math.normalize(rotation.Value), math.axisAngle(math.up(), 0.5f * dt));
            position.Value = new float3 { x = 0f, y = math.sin(count)*3f, z = 0f };
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        outerCount += 0.02f;
        var job = new SpawnerMovePosition() { dt = Time.deltaTime, count = outerCount };
        return job.Schedule(this, 64, inputDeps);
    }
}
