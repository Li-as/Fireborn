using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneResetter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Airplane plane))
        {
            plane.Reset();
        }
    }
}
