using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitions : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform gameOverPos;
    [SerializeField] private float transitionSpeed;
    private Transform currentView;
    [SerializeField] private GameObject orderUI;

    private static float buildAspectRatio = 0.5625f, buildCameraFOV=60f;    //these values are HARD CODED! 

    private void Awake()
    {
        float aspect= (float)Screen.width / (float)Screen.height;
        GetComponent<Camera>().fieldOfView = (buildAspectRatio/aspect ) * buildCameraFOV;  //changing camera FOV based on screen aspect ratio 
    }
    // Start is called before the first frame update
    void Start()
    {
        currentView = transform;
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
            );
        transform.eulerAngles = currentAngle;
    }

    public bool cameraReachedDestination()
    {
        return Vector3.Distance(transform.position, currentView.position) < 0.25f;
    }

    public void transitionCameraToGameOver()  
    {
        currentView = gameOverPos;
        orderUI.SetActive(false);
    }
}
