using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCounter : MonoBehaviour
{
    [SerializeField] public int player;
    private int scoreCount;
    private CapsuleCollider2D capsule;
    

    public int getScore()
    {
        return scoreCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        capsule = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreCount = 0;
        Collider2D[] overlaps = Physics2D.OverlapCapsuleAll((Vector2)transform.position + capsule.offset, (Vector2)transform.lossyScale * capsule.size, capsule.direction, 0);
        foreach(Collider2D overlap in overlaps)
        {
            Food food = overlap.transform.GetComponent<Food>();
            if(food && food.canBeScored())
            {
                scoreCount += food.score;
            }
        }
    }
}
