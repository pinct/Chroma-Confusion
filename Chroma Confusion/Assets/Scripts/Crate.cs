using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private void FixedUpdate()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, 13, false);
    }
}