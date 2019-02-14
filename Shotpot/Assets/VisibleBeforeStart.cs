using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibleBeforeStart : MonoBehaviour
{
    [SerializeField]
    private Game game;
    Text r;
    Image i;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Text>();
        i = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(r) r.enabled = game.showIntro;
        if(i) i.enabled = game.showIntro;
    }
}