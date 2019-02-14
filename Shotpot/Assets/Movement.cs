using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxRot;
    [HideInInspector] public Rigidbody2D rbody;
    private Hand hand;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        hand = GetComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + new Vector3( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * moveSpeed;
        if (hand.isBurned())
        {
            rbody.MovePosition(rbody.transform.position + new Vector3(0, hand.getBurnRatio()) * Time.deltaTime * moveSpeed);
            rbody.MoveRotation(Mathf.Lerp(rbody.rotation, 2*maxRot * Random.Range(-1.0f,1.0f), 0.4f));
        }
        else
        {
            if (hand.player == 1)
            {
                rbody.MovePosition(rbody.transform.position + new Vector3(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1")) * Time.deltaTime * moveSpeed);
                rbody.MoveRotation(Mathf.Lerp(rbody.rotation, maxRot * Input.GetAxis("Horizontal1"), 0.1f));
            }
            else if (hand.player == 2)
            {
                rbody.MovePosition(rbody.transform.position + new Vector3(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")) * Time.deltaTime * moveSpeed);
                rbody.MoveRotation(Mathf.Lerp(rbody.rotation, maxRot * Input.GetAxis("Horizontal2"), 0.1f));
            }
        }
    }
}
