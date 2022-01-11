using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeFlareScript : MonoBehaviour
{
    private float rotSpeed=10;

    private static float buildAspectRatio = 0.5625f;    //these values are HARD CODED! 

    private void Awake()
    {
        float aspect = (float)Screen.width / (float)Screen.height;

        transform.localScale *= (buildAspectRatio / aspect) ;  //changing camera FOV based on screen aspect ratio 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotSpeed*Time.deltaTime);
        
    }
}
