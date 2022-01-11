using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    // private int productCount = 0;

    public int productCount { get; set; }

    [SerializeField] private string id = null;

    private GameController gameController;
    private GameControllerRetroFrigde gameControllerRetroFrigde;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        productCount = 0;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.incContainerCount();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void incProductCount()
    {
        productCount++;
        //add layer info
    }

    public virtual void decProductCount()
    {
        //   Debug.LogWarning("decProductCount()");
        productCount--;
        //check layer info
        //if productCount for that layer is 0, activate next layer increase index
    }

    public string getId()
    {
        return id;
    }
}
