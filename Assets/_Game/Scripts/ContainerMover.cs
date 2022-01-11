using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerMover : MonoBehaviour
{
    protected GameObject mainCamera;
    [SerializeField] protected float transitionSpeed=3f;
    [SerializeField] protected Transform cameraEndPos;
    protected Transform cameraStartPos;
    protected Transform cameraDestination;

    protected bool isTransitionAcitve = false;
    protected Transform parentTransform;
    protected Vector3 destination, pullOutPos, startingPos;

    protected GameController gameController;

    [SerializeField] private Sprite orderImage=null;
    [SerializeField] private GameObject checkMark = null;
    // Start is called before the first frame update
   protected void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");    //finding the main camera 
        cameraStartPos = GameObject.Find("CameraMainPosition").transform; //finding the empty game object with the camera start pos 

        parentTransform = transform.parent;

        startingPos= pullOutPos = new Vector3(parentTransform.position.x, parentTransform.position.y, parentTransform.position.z);

        pullOutPos -= new Vector3(0,0, gameObject.GetComponent<Renderer>().bounds.size.z);

        gameController= GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (isTransitionAcitve)
        {
            parentTransform.position = Vector3.Lerp(parentTransform.position, destination, Time.deltaTime * transitionSpeed);
            moveCamera(mainCamera.transform, cameraDestination);

            if (isTransitionDone())
            {
                isTransitionAcitve = false;
               
                gameObject.GetComponent<Collider>().enabled = !gameObject.GetComponent<Collider>().enabled;

                if (!gameObject.GetComponent<Collider>().enabled)   //when the collider for the container is disabled, we are in the product putting phase
                {
                    gameController.ScrollUI.SetActive(true);
                    gameController.activateProductSelectUI();
                    gameController.setOrderImage(orderImage);   //enable specific order for this container
                }

                if (gameObject.GetComponent<Collider>().enabled)    //when the collider for the container is enabled, we are returning to the container picking phase
                {
                    gameController.IsContainerSelected = false;
                    gameController.decContainerCount();
                    gameObject.GetComponent<Collider>().enabled = false; //disable the container collider for good, because we are done with it

                   if(checkMark!=null)  checkMark.SetActive(true);  //enable checkmark to show that the container is full

                 //   gameController.disableOrderImage(); //disable specific order for this container
                    //  gameController.ProductSelectionUI.SetActive(false);
                }

                // enable UI, maybe set isTransitionAcitve=false
            }
        }

        //Vector3.Lerp()
    }

    protected void moveCamera(Transform cameraTransform, Transform cameraDestination)
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraDestination.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(cameraTransform.rotation.eulerAngles.x, cameraDestination.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(cameraTransform.rotation.eulerAngles.y, cameraDestination.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(cameraTransform.rotation.eulerAngles.z, cameraDestination.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
            );
        cameraTransform.eulerAngles = currentAngle;
    }

    public void pullOut()
    {
        destination = pullOutPos;
        cameraDestination = cameraEndPos;
        isTransitionAcitve = true;       
    }

    public void pullIn()
    {
        destination = startingPos;
        cameraDestination = cameraStartPos;
        isTransitionAcitve = true;
    }

    public bool isTransitionDone()
    {
        return (Vector3.Distance(mainCamera.transform.position, cameraDestination.position) < 0.1f ) && (Vector3.Distance(parentTransform.position, destination) < 0.1f);
    }
}
