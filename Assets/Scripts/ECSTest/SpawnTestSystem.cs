using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class SpawnTestSystem : ComponentSystem
{
    struct Group
    {
        [ReadOnly]
        public SharedComponentDataArray<SpawnTest> Spawner;
        public EntityArray                         Entity;
        public int                                 Length;
    }

    [Inject] Group m_Group;

    protected override void OnUpdate()
    {
        while (m_Group.Length != 0)
        {
            var spawner = m_Group.Spawner[0];
            var sourceEntity = m_Group.Entity[0];
            //var center = m_Group.Position[0].Value;
            
            var entities = new NativeArray<Entity>(spawner.count, Allocator.Temp);
            EntityManager.Instantiate(spawner.prefab, entities);
            
            var positions = new NativeArray<float3>(spawner.count, Allocator.Temp);

            for (int i = 0; i < spawner.count; i++)
            {
                var position = new LocalPosition
                {
                    Value = positions[i] + new float3
                    {
                        x = Random.Range(-10f, 10f),
                        y = Random.Range(-10f, 10f),
                        z = Random.Range(-10f, 10f)
                    }
                };


                EntityManager.SetComponentData(entities[i], position);

                EntityManager.AddComponentData(entities[i], new RotationTestSpeed { Value = Random.Range(0.5f, 3f) });
                EntityManager.AddComponentData(entities[i], new TestId { Value = i });
                EntityManager.AddComponentData(entities[i], new TransformParent { Value = sourceEntity });
            }

            entities.Dispose();
            positions.Dispose();
            
            EntityManager.RemoveComponent<SpawnTest>(sourceEntity);

            // Instantiate & AddComponent & RemoveComponent calls invalidate the injected groups,
            // so before we get to the next spawner we have to reinject them  
            UpdateInjectedComponentGroups();
        }
    }
}

