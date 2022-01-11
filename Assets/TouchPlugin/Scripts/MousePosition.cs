using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    public Image pointer;
    public bool isPressed;
    private Vector3 defaultScale;

    private void Start()
    {
        //  pointer.gameObject.SetActive(false);
       // pointer.gameObject.SetActive(true);
        defaultScale = pointer.gameObject.transform.localScale;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pointer.gameObject.SetActive(!pointer.gameObject.activeSelf);
        }

        isPressed = true;
        pointer.gameObject.transform.position = Input.mousePosition;
        /*        if (Input.GetMouseButtonDown(0))
                {

                    isPressed = true;
                }*/
        if (Input.GetMouseButtonDown(0))
        {
            pointer.gameObject.transform.localScale *= 0.75f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            pointer.gameObject.transform.localScale = defaultScale;
        }
       

/*        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }*/
        /*  if (isPressed)
          {
              pointer.gameObject.SetActive(true);
              pointer.gameObject.transform.position = Input.mousePosition;
          }else if (!isPressed)
          {
              pointer.gameObject.SetActive(false);
          }*/
    }
}
