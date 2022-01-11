using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Tabtale.TTPlugins;

public class GameControllerRetroFrigde : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject scrollUI;


    [SerializeField] private GameObject[] productListSideDoor, productList10, products14ProteinList, products14YoghurtList, productList15, productList16, productList18, productList20;
    [SerializeField] private GameObject productsSideDoorUI, products10UI, products14ProteinUI, products14YoghurtUI, products15UI, products16UI, products18UI, products20UI;
    private GameObject curActiveProductsUI;
    private GameObject[] selectedProductList;
    //  [SerializeField] private GameObject theCan, theYoghurt;

    [SerializeField] private GameObject fridgeBigDoor, fridgeSmallDoor;
    private GameObject selectedProduct;  //should be a list later on, so that the user can choose which product to place
    private static int maxProductN = 8; //constant now, could be different later
    private int curProductN = 0; //

    [SerializeField] private bool isContainerSelected = false;
    private ContainerMover selectedContainerMover;

    public bool IsContainerSelected { get => isContainerSelected; set => isContainerSelected = value; }
    public GameObject ScrollUI { get => scrollUI; set => scrollUI = value; }

    private ContainerManager containerManager;

    private int containerCount = 0;

    private bool isCameraToGameOverTransition = false, isFridgeDoorClosingTransition = false, isGameOver = false;

    [SerializeField] private GameObject fakeFlare, floor;
    private Material floorMaterial, fakeFlareMaterial;
    [SerializeField] private Material skyBoxMaterial;
    [SerializeField] private GameObject nextButton;

    private Color skyBoxDefaultColor;

    private float fadeOutSpeed = 2f;

    public GameObject orderUI;

    // private bool playGameOverSequence;
    //selected container
    // Start is called before the first frame update
    private void Awake()
    {
        TTPCore.Setup();
    }
    void Start()
    {
        skyBoxDefaultColor = RenderSettings.skybox.GetColor("_Tint");

        floorMaterial = floor.GetComponent<Renderer>().material;
        fakeFlareMaterial = fakeFlare.GetComponent<Renderer>().material;

    //    selectedProductList = productList8;
     //   selectedProduct = selectedProductList[0];
     
    }

    private void OnDestroy()
    {
        RenderSettings.skybox.SetColor("_Tint", skyBoxDefaultColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            Color c = fakeFlareMaterial.color;
            c.a += fadeOutSpeed * Time.deltaTime;
            if (c.a > 1) c.a = 1;

            fakeFlareMaterial.color = c;

            c = floorMaterial.color;
            c.a -= fadeOutSpeed * Time.deltaTime;
            if (c.a < 0)
            {
                c.a = 0;
                nextButton.SetActive(true);
            }


            floorMaterial.color = c;

            c = RenderSettings.skybox.GetColor("_Tint");
            c.r -= fadeOutSpeed * 0.1f * Time.deltaTime;
            c.g -= fadeOutSpeed * 0.1f * Time.deltaTime;
            c.b -= fadeOutSpeed * 0.1f * Time.deltaTime;

            if (c.r < 0.1568f) c.r = 0.1568f;
            if (c.g < 0.1568f) c.g = 0.1568f;
            if (c.b < 0.1568f) c.b = 0.1568f;
            //skyBoxMaterial.SetColor("_Tint", c);
            RenderSettings.skybox.SetColor("_Tint", c);
            return;     //idle state after game over
        }

        if (isFridgeDoorClosingTransition)
        {
            if (fridgeBigDoor.GetComponent<FridgeDoorTransition>().doorReachedDestination())
            {
                // Debug.Log("Game Over");
                isGameOver = true;
                //playGameOverSequence();
            }

            return;
        }

        if (isCameraToGameOverTransition)
        {
            if (mainCamera.GetComponent<CameraTransitions>().cameraReachedDestination())
            {
                fridgeBigDoor.GetComponent<FridgeDoorTransition>().transitionDoorToClose();
                fridgeSmallDoor.GetComponent<FridgeDoorTransition>().transitionDoorToClose();

                isCameraToGameOverTransition = false;
                isFridgeDoorClosingTransition = true;
            }
            return;
        }

        if (!isContainerSelected && containerCount == 0)    //!isContainerSelected is here to wait for the camera to zoom out completely and then initiate game over
        {
            mainCamera.GetComponent<CameraTransitions>().transitionCameraToGameOver();
            //camera transition and then fridge door rotation
            //  Debug.Log("Game Over");
            isCameraToGameOverTransition = true;
        }

        //if (curProductN == maxProductN) //this condition will be changed probably
        if (isContainerSelected && containerManager.productCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //  curProductN = 0;
                selectedContainerMover.pullIn();
                scrollUI.SetActive(false);  //disables the parent UI
                deactivateProductSelectUI(); //disables the specific products UI
                disableOrderImage();    //disables the order for the specific container

                //Debug.Log("Animacija vracanja kontejnera");
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Hitbox"))
                {
                    GameObject newProduct = Instantiate(selectedProduct);
                    newProduct.transform.parent = hitObject.transform.parent;
                    newProduct.transform.position = hitObject.transform.position;
                    newProduct.transform.position = new Vector3(newProduct.transform.position.x, newProduct.transform.position.y - hit.collider.bounds.size.y / 2, newProduct.transform.position.z); //addresing the pivot problem

                    //curProductN++;
                    containerManager.decProductCount();

                    hitObject.tag = "Untagged"; //this prevents multiple product appearances, because the hitbox remains but it is without effect
                    //hit.collider.enabled = false;

                    //newProduct.local
                    //   instantiate prefab
                }

                if (hitObject.CompareTag("Container"))
                {
                    if (!isContainerSelected)
                    {
                        // containerManager = hitObject.transform.parent.GetComponent<LayeredContainerManager>();
                        containerManager = hitObject.transform.parent.GetComponent<ContainerManager>();  //we hit the container model, so we have to get its parent to reach the script
                        if (containerManager == null)   //added because of bear top container
                        {
                            containerManager = hitObject.transform.GetComponent<ContainerManager>();

                        }
                        //containerManager=
                        //get containerManager
                        selectedContainerMover = hitObject.GetComponent<ContainerMover>();
                        if (selectedContainerMover == null) //added because of bear top container
                        {
                            selectedContainerMover = hitObject.GetComponent<BearTopDoorMover>();
                        }

                        selectedContainerMover.pullOut();

                    }
                    isContainerSelected = true;
                    //make container move
                }
            }
        }
    }

    public void activateProductSelectUI()
    {

        switch (containerManager.getId())
        {
            case "SideDoor":
                selectedProductList = productListSideDoor; //list of sodas and milk
                curActiveProductsUI = productsSideDoorUI;
                //set products8UI in such a way that the first product is selected
                /*                products8UI.GetComponent<ProductButtonsManager>().setProductButtonsToDefaultState();
                                products8UI.SetActive(true);
                                curActiveProductsUI = products8UI;*/
                break;
          /*  case 10:
                selectedProductList = productList10;
                curActiveProductsUI = products10UI;
                break;
            case 14:
                if (containerManager.getId().Equals("MidRight14"))
                {
                    selectedProductList = products14ProteinList;
                    curActiveProductsUI = products14ProteinUI;
                }

                if (containerManager.getId().Equals("SmallRight14"))
                {
                    selectedProductList = products14YoghurtList;
                    curActiveProductsUI = products14YoghurtUI;
                }
                break;
            case 15:
                selectedProductList = productList15;
                curActiveProductsUI = products15UI;
                break;
            case 16:
                selectedProductList = productList16;
                curActiveProductsUI = products16UI;

                break;
            case 18:
                selectedProductList = productList18;
                curActiveProductsUI = products18UI;
                break;
            case 20:
                selectedProductList = productList20;
                curActiveProductsUI = products20UI;
                break;*/
            default:
             //   selectedProductList = productList8; //list of sodas and milk
             //   curActiveProductsUI = products8UI;
                break;
        }
        selectedProduct = selectedProductList[0];
        curActiveProductsUI.GetComponent<ProductButtonsManager>().setProductButtonsToDefaultState();
        curActiveProductsUI.SetActive(true);
        scrollUI.GetComponent<ScrollRect>().content = curActiveProductsUI.GetComponent<RectTransform>();
    }

    public void deactivateProductSelectUI()
    {
        curActiveProductsUI.SetActive(false);
    }

    public void incContainerCount()
    {
        containerCount++;
        // Debug.Log(containerCount);
    }

    public void decContainerCount()
    {
        containerCount--;
        // Debug.Log(containerCount);
    }

    public void selectProduct(int i)
    {
        selectedProduct = selectedProductList[i];
    }

    public void goToFirstLevel()
    {
        RenderSettings.skybox.SetColor("_Tint", skyBoxDefaultColor);
        SceneManager.LoadScene("FridgeOrganizing");
    }

    public void goToNextLevel()
    {
        RenderSettings.skybox.SetColor("_Tint", skyBoxDefaultColor);
        SceneManager.LoadScene("FridgeOrganizingBear");
    }

    public void setOrderImage(Sprite img)
    {
        if (img == null) return;
        orderUI.GetComponent<Image>().sprite = img;
        orderUI.SetActive(true);
    }

    public void disableOrderImage()
    {

        orderUI.SetActive(false);
        orderUI.GetComponent<Image>().sprite = null;
    }
}
