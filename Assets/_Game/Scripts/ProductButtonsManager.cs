using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductButtonsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setProductButtonsToDefaultState()
    {
        transform.GetChild(0).GetComponent<OnProductUIClick>().enableProductButton();   //only the first button is active
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<OnProductUIClick>().disableProductButton();  //other buttons are disabled
        }
    }
}
