using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RotationTestSpeedSystem : JobComponentSystem
{
    [ComputeJobOptimization]
    struct RotationTestSpeedRotation : IJobProcessComponentData<Rotation, RotationTestSpeed, TestId>
    {
        public float dt;

        public void Execute(ref Rotation rotation, [ReadOnly]ref RotationTestSpeed speed, [ReadOnly]ref TestId id)
        {
            if (id.Value % 2 == 0)
            {
                rotation.Value = math.mul(math.normalize(rotation.Value), math.axisAngle(math.up(), speed.Value * dt));
            }
            else
            {
                rotation.Value = math.mul(math.normalize(rotation.Value), math.axisAngle(new float3{x=1f,y=0f,z=0f}, speed.Value * dt));
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new RotationTestSpeedRotation() { dt = Time.deltaTime };
        return job.Schedule(this, 64, inputDeps);
    }
}
