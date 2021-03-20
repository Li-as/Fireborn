using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : Unit
{
    public override void Reset()
    {
        base.Reset();
        Rigidbody.useGravity = false;
    }
}
