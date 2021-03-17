using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int WaterPower;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;

    public Sprite Sprite => _sprite;
    public string Name => _name;
    public int WaterPowerLevel => WaterPower;
}
