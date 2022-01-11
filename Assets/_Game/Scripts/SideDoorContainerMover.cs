using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDoorContainerMover : ContainerMover
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        pullOutPos = startingPos;   //this line makes the travel destination of the container be the same startring pos, making it not move at all
    }

    // Update is called once per frame

}
