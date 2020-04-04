using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct BallTag : IComponentData
{
    // A Tag (BallTag), is purely for identification purposes
    // Does not store data, only used in queries to say: "Hey, find me everything that has maybe, say, a translation, and a velocity, and is also a ball."
}
