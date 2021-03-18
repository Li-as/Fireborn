using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaceOnFire : MonoBehaviour
{
    [SerializeField] private FireSource _fireSource;
    [SerializeField] private Transform[] _firefightersPath;
    [SerializeField] private Transform[] _fireEnginePath;
    [SerializeField] private Transform[] _helicopterPath;
    [SerializeField] private Transform[] _airplanePath;

    public Transform[] TryGetPath(Unit unit)
    {
        if (unit is FireEngine)
        {
            return _fireEnginePath;
        }
        else
        {
            return null;
        }
    }
}
