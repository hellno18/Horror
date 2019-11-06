using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPuzzle : MonoBehaviour
{
    Animator animWall;
    // Start is called before the first frame update
    void Start()
    {
        animWall = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<IPuzzle>();
        //print(player.GetCountPuzzle());
        if (player.GetCountPuzzle() == 2)
        {
            this.animWall.SetBool("Done",true);
        }
    }
}
