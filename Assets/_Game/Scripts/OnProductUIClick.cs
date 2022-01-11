using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnProductUIClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite notActiveImg, activeImg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        

        if (gameObject.tag.Equals("SelectedButton"))    //if im clicked on again, no effect
        {
            return;
/*            gameObject.tag = "Untagged";
            GetComponent<Image>().sprite = notActiveImg;*/
        }
        else                                            //if im clicked once i select
        {
            enableProductButton();

            //here i check if one of my brothers was selected so that i can deselect him

            Transform parent = gameObject.transform.parent;

            for (int i = 0; i < parent.childCount; i++)
            {
                if((parent.GetChild(i)!= gameObject.transform) && (parent.GetChild(i).tag.Equals("SelectedButton")))
                {
                    parent.GetChild(i).GetComponent<OnProductUIClick>().disableProductButton();
                    break;
                }
            }
        }
    }

    public void disableProductButton()
    {
        gameObject.tag = "Untagged";
        gameObject.GetComponent<Image>().sprite = notActiveImg;
    }

    public void enableProductButton()
    {
        gameObject.tag = "SelectedButton";
        gameObject.GetComponent<Image>().sprite = activeImg;
    }
}
