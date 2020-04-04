using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[AlwaysSynchronizeSystem] // Method 2 // Method 3
public class PlayerInputSystem : JobComponentSystem
{
	protected override JobHandle OnUpdate(JobHandle inputDeps) // inputDeps: Input Dependencies
    {
        // inputDeps.Complete(); // Method 1

        // Write to data and read input.
        Entities.ForEach((ref PaddleMovementData moveData, in PaddleInputData inputData) => // ref: Writing to this data // in: Reading from this data 
        {
			moveData.direction = 0;
			moveData.direction += Input.GetKey(inputData.upKey) ? 1 : 0; // ?, means am I pressing 'upKey', if I am add value of '1', else add value of '0'
            moveData.direction -= Input.GetKey(inputData.downKey) ? 1 : 0; // ?, means am I pressing 'upKey', if I am add value of '1', else add value of '0'
        }).Run(); // Run on main thread

        return default; // Method 3
        // return new JobHandle(); // Method 1
        // return inputDeps; // Method 2
    }
}

// Optimization Notes:
/* Possible Solutions
    Since this is running on the main thread, the way I'm handling reading Job input dependencies and returning Job input dependencies.
    The next system in the chain is going to say, "what happened in the previous chain? Am I waiting for anything, any asynchronous work? 
    That needs to be wrapped up before it's my(this) turn?

    This script would otherwise have delays waiting for thread safety checks.
    Solution:
        Uncomment lines marked with 'Method 1'
        Uncomment lines marked with 'Method 2'
        Uncomment lines marked with 'Method 3'
*/

/* Optimal Solution
    Method 3: We're not using 'inputDeps' for anything. We're returning a 'default'. We're always synchronizing our system '[AlwaysSynchronizeSystem]', 
    because it's main thread, and we're going to finish up any main thread work that has to happen before this one, then we're going to do this one,
    and then the next system can do its thing, so the Unity scheduler can optimize this.
*/
