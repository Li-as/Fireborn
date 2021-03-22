using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneExitPlaceTrigger : MonoBehaviour
{
    [SerializeField] private PlaceOnFire _place;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Airplane airplane))
        {
            airplane.ExitPlace(_place);
        }
    }
}
