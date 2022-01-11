using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayeredContainerManager : ContainerManager
{

    private int[] layerProductCountList;

    private int curLayerId = 0;

    public GameObject[] layers;
    private bool isEnableNextLayer = false;

    private Transform curLayerTransform;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        layerProductCountList = new int[layers.Length];
        for (int i = 0; i < layerProductCountList.Length; i++)
        {
            layerProductCountList[i] = 0;
        }
    }
    void Start()
    {
        // List<int> a= layerProductCountList[1];

        // layerProductCountList = new List<int>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isEnableNextLayer)
            {
                enableNextLayer();
                isEnableNextLayer = false;
            }
        }
    }

    public void incProductCount(int layerId)
    {
        base.incProductCount(); //increases global product number
        /*        if (layerId >=layerProductCountList.Count)
                {
                   // Debug.LogError(" layerProductCountList.Count="+ layerProductCountList.Count);
                    layerProductCountList.Add(0);
                   // Debug.LogError(" layerProductCountList=");
                    for (int i = 0; i < layerProductCountList.Count; i++)
                    {
                   //     Debug.LogError(layerProductCountList[i]);
                    }

                }*/

        //  Debug.LogError("layerId="+layerId);

        /*        layerProductCountList[layerId] = layerProductCountList[layerId] + 1;
                Debug.LogError(layerProductCountList[layerId]);*/


        /*       catch (Argument)
               {

               }
       */
        layerProductCountList[layerId]++;
    }

    public override void decProductCount()
    {
        
        base.decProductCount(); //decreases global product number
     //   Debug.LogError("decProductCount()");
        layerProductCountList[curLayerId]--;
       // Debug.LogWarning("layerProductCountList[curLayerId]="+ layerProductCountList[curLayerId]);
        if (layerProductCountList[curLayerId] <= 0)
        {
            /*            Transform curLayerTransform= layers[curLayerId].transform;
                       for (int i = 0; i < curLayerTransform.childCount; i++) //disable this layer
                        {
                            curLayerTransform.GetChild(i).GetComponent<Collider>();
                        }*/
            curLayerId++;
            if (curLayerId >= layers.Length) return;

            

            curLayerTransform = layers[curLayerId].transform; // variable for the next layer

            isEnableNextLayer = true;
        }
    }

    private void enableNextLayer()
    {
        for (int i = 0; i < curLayerTransform.childCount; i++) 
        {
            curLayerTransform.GetChild(i).GetComponent<Collider>().enabled = true;
        }
    }
}
