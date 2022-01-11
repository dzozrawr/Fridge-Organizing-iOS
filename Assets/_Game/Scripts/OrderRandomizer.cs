using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderRandomizer : MonoBehaviour
{
    public Sprite[] spriteList;    
    private Image imageComponent;
    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<Image>();
        imageComponent.sprite=spriteList[Random.Range(0, spriteList.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
