using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTopDoorMover : ContainerMover
{
    // Start is called before the first frame update
    /*    private GameObject mainCamera;
        [SerializeField] private float transitionSpeed = 3f;
        [SerializeField] private Transform cameraEndPos;
        private Transform cameraStartPos;
        private Transform cameraDestination;

        private bool isTransitionAcitve = false;
        private Transform parentTransform;
        private Vector3 destination, pullOutPos, startingPos;

        private GameController gameController;*/
    // Start is called before the first frame update

    [SerializeField] private Sprite[] orderImages;

    private Sprite orderImage;
    [SerializeField] Collider topDoorCollider;
    public void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");    //finding the main camera 
        cameraStartPos = GameObject.Find("CameraMainPosition").transform; //finding the empty game object with the camera start pos 

        //   parentTransform = transform.parent;

        startingPos = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        pullOutPos = new Vector3(-90, 0, 0);

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        orderImage = orderImages[Random.Range(0, orderImages.Length)];
    }

    // Update is called once per frame
    public void Update()
    {
        if (isTransitionAcitve)
        {

            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(transform.rotation.eulerAngles.x, destination.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, 180, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, destination.z, Time.deltaTime * transitionSpeed)
            );
            transform.eulerAngles = currentAngle;

            //Debug.Log(Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.x, pullOutPos.x)));
            //transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * transitionSpeed);
            moveCamera(mainCamera.transform, cameraDestination);

            if (isTransitionDone())
            {

                isTransitionAcitve = false;

                //   gameObject.GetComponent<Collider>().enabled = !gameObject.GetComponent<Collider>().enabled;
                topDoorCollider.enabled = !topDoorCollider.enabled;

                // if (!gameObject.GetComponent<Collider>().enabled)   //when the collider for the container is disabled, we are in the product putting phase
                if (!topDoorCollider.enabled)
                {
                    gameController.ScrollUI.SetActive(true);
                    gameController.activateProductSelectUI();
                    gameController.setOrderImage(orderImage);
                }

                // if (gameObject.GetComponent<Collider>().enabled)    //when the collider for the container is enabled, we are returning to the container picking phase
                if (topDoorCollider.enabled)
                {
                    gameController.IsContainerSelected = false;
                    gameController.decContainerCount();
                    //  gameObject.GetComponent<Collider>().enabled = false; //disable the container collider for good, because we are done with it
                    gameObject.GetComponent<Collider>().enabled = false;    //this collider has the actual tag "Container" needed to trigger the opening
                    topDoorCollider.enabled = false;    //this collider is needed for preventing putting the products in before the container is fully opened
                }

                // enable UI, maybe set isTransitionAcitve=false
            }
        }

        //Vector3.Lerp()
    }
    /*
        private void moveCamera(Transform cameraTransform, Transform cameraDestination)
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
    */
    public bool isTransitionDone()
    {
        // return Mathf.Abs(Mathf.Abs(transform.rotation.eulerAngles.x) - Mathf.Abs(pullOutPos.x)) < 1f;        
        return Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.x, destination.x)) < 1f;
    }
}
