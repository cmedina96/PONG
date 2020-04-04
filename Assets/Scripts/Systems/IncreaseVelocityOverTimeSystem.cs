using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;

[AlwaysSynchronizeSystem]
public class IncreaseVelocityOverTimeSystem : JobComponentSystem
{
	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		float deltaTime = Time.DeltaTime;

		Entities.ForEach((ref PhysicsVelocity vel, in SpeedIncreaseOverTimeData data) => // vel: Current velocity
        {
			float2 modifier = new float2(data.increasePerSecond * deltaTime);

			float2 newVel = vel.Linear.xy;

            newVel += math.lerp(-modifier, modifier, math.sign(newVel)); // Modulating by our increasePerSecond // If we're going left or down we're using negative modifier, 
                                                                         // if right or up we're using positive modifier. This way we're not dampening our speed, but increasing it.
            vel.Linear.xy = newVel; // Setting it to our new velocity.
        }).Run(); // Runs on main thread

        return default;
	}
}
