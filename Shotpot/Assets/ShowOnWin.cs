using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowOnWin : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private int win;
    Text r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        r.enabled = (game.winner == win);
    }
}
