using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUnitMover : UnitMover
{
    protected override bool IsAtTargetPosition(Vector3 targetPosition)
    {
        if (Mathf.Abs(transform.position.x - targetPosition.x) <= MaxDistanceFromTargetPosition)
        {
            if (Mathf.Abs(transform.position.z - targetPosition.z) <= MaxDistanceFromTargetPosition)
            {
                return true;
            }
        }

        return false;
    }
}
