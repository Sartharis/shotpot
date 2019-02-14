﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotpotLiquid : MonoBehaviour
{

    [SerializeField] float buoyancyForce = 0.5f;
    [SerializeField] float velocitySlowdown = 0.1f;
    [SerializeField] float drag = 0.2f;
    [SerializeField] float angularDrag = 0.2f;

    private Collider2D col;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.attachedRigidbody.velocity *= velocitySlowdown;
        collision.attachedRigidbody.drag *= drag;
        collision.attachedRigidbody.angularDrag *= angularDrag;
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.attachedRigidbody.drag /= drag;
        collision.attachedRigidbody.angularDrag /= angularDrag;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.attachedRigidbody.AddForce(new Vector2(0,buoyancyForce * collision.attachedRigidbody.mass * 2 * Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(BoundsContainedPercentage(collision.bounds, col.bounds))))));
        if(BoundsContainedPercentage(collision.bounds, col.bounds) < 0.6f)
        {
            collision.attachedRigidbody.velocity *= 0.94f;
        }
    }

    private float BoundsContainedPercentage(Bounds obj, Bounds region)
    {
        float total = 1f;

        for (int i = 0; i < 3; i++)
        {
            float dist = obj.min[i] > region.center[i] ?
            obj.max[i] - region.max[i] :
            region.min[i] - obj.min[i];

            total *= Mathf.Clamp01(Mathf.Abs(1f - dist / obj.size[i]));
        }

        return total;
    }
}
