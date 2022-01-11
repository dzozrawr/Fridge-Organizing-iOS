using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxCounter : MonoBehaviour
{
    [SerializeField] ContainerManager container;
    // Start is called before the first frame update
    void Start()
    {
        container.incProductCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
