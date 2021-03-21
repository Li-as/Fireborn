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

    public FireSource FireSource => _fireSource;

    public PathPoint[] TryGetPath(Unit unit)
    {
        PathPoint[] pathPoints;
        if (unit is FireEngine)
        {
            pathPoints = _fireEnginePath.GetComponentsInChildren<PathPoint>();
        }
        else if (unit is Helicopter)
        {
            pathPoints = _helicopterPath.GetComponentsInChildren<PathPoint>();
        }
        else if (unit is Airplane)
        {
            pathPoints = _airplanePath.GetComponentsInChildren<PathPoint>();
        }
        else
        {
            return null;
        }

        return pathPoints;
    }
}