using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaceOnFire : MonoBehaviour
{
    [SerializeField] private FireSource _fireSource;
    [SerializeField] private Transform _firefightersPath;
    [SerializeField] private Transform _fireEnginePath;
    [SerializeField] private Transform _helicopterPath;
    [SerializeField] private Transform _airplanePath;

    public PathPoint[] TryGetPath(Unit unit)
    {
        PathPoint[] pathPoints;
        if (unit is FireEngine)
        {
            pathPoints = _fireEnginePath.GetComponentsInChildren<PathPoint>();
            return pathPoints;
        }
        else
        {
            return null;
        }
    }
}
