using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxLayeredCounter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayeredContainerManager container;

    [SerializeField] private int layerId;
    // Start is called before the first frame update
    void Start()
    {
        container.incProductCount(layerId);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
