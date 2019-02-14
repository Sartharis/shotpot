using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] Color burnColor;
    [SerializeField] float burnTime;

    private float currentBurnTime;
    private Color baseColor;
    private Movement mov;
    private SpriteRenderer spr;
    public int player;

    public bool isBurned()
    {
        return currentBurnTime > 0;
    }

    public float getBurnRatio()
    {
        return Mathf.Clamp01(currentBurnTime / burnTime);
    }


    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();    
        mov = GetComponent<Movement>();
        baseColor = spr.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<HotpotLiquid>())
        {
            currentBurnTime = burnTime;
        }
    }

    private void Update()
    {
        if (currentBurnTime > 0)
        {
            spr.color = Color.Lerp(baseColor, burnColor, currentBurnTime / burnTime);
            currentBurnTime -= Time.deltaTime;
        }
        else
        {
            spr.color = baseColor;
        }
    }
}
