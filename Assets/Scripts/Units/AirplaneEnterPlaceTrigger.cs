using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneEnterPlaceTrigger : MonoBehaviour
{
    [SerializeField] private PlaceOnFire _place;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Airplane airplane))
        {
            airplane.EnterPlace(_place);
        }
    }
}
