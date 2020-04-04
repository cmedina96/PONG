using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem] // Main Thread
public class PaddleMovementSystem : JobComponentSystem // 1. Creates Job Component System
{
    // Take input data, and do stuff.
    protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		float deltaTime = Time.DeltaTime;
		float yBound = GameManager.main.yBound;

		Entities.ForEach((ref Translation trans, in PaddleMovementData data) => // Main Thread
        // JobHandle myJob = Entities.ForEach((ref Translation trans, in PaddleMovementData data) => // Job Method // 2. System query 
        {
            trans.Value.y = math.clamp(trans.Value.y + (data.speed * data.direction * deltaTime), -yBound, yBound);
		}).Run(); // Main Thread
        // }).Schedule(inputDeps); // Job Method // 4. Execution is scheduled, to run on a bunch of worker threads

        return default; // Main Thread
        // return myJob; // Job Method
    }
}

// Optimization Notes:
/*  Possible Solution:
    Job Method (for worker threads):

    This code normally would be optimal, but because what we're tasking this script with is SUPER SIMPLE, 
    we're simply moving something (a paddle) up & down, and clamping it's bounds.
    There are *only* two paddles, so this script is going to take a negligible amount of time to run this script.
    The amount of time it takes to schedule this job, and run it on a worker thread is greater than the amount of time
    it takes to do the work (execute this script)

    TL;DR:
    It's less efficient to run this on worker threads (because it's so simple), than to just run it on the main thread.
*/

/*  Optimal Solution:
    Main Thread:

    Everything marked with 'Main Thread' is the opti
*/
