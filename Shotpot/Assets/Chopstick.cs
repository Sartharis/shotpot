using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopstick : MonoBehaviour
{
    [SerializeField] float rotSpeed;
    [SerializeField] float rotLimit;
    [SerializeField] float startRotLimit;
    float startRot;
    private Rigidbody2D rbody;
    private Hand hand;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        hand = GetComponentInParent<Hand>();
        startRot = rbody.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(((hand.player == 1 && Input.GetButton("Fire1")) || (hand.player == 2 && Input.GetButton("Fire2"))) && !hand.isBurned())
        {
            rbody.MoveRotation(Mathf.Lerp(rbody.rotation, rotLimit, rotSpeed*Time.deltaTime));
        }   
        else
        {
            rbody.MoveRotation(Mathf.Lerp(rbody.rotation, startRot + startRotLimit, rotSpeed*Time.deltaTime));
        }
    }
}
