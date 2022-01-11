using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoorTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform doorEndPos;
    [SerializeField] private float transitionSpeed;
    private Transform currentView;


    // Start is called before the first frame update
    void Start()
    {
        currentView = transform;


    }

    private void Update()
    {
        Vector3 currentAngle = new Vector3(
    Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
    Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
    Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
    );
        transform.eulerAngles = currentAngle;
    }

    // Update is called once per frame


    public bool doorReachedDestination()
    {
        //Debug.Log();
        return Mathf.Abs(transform.rotation.eulerAngles.y - currentView.rotation.eulerAngles.y) < 1f;
    }

    public void transitionDoorToClose()
    {
        currentView = doorEndPos;
    }

}
