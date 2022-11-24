using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;

    private Vector3 previousPosition;
    private float zoom = -20f;
    private float zoomChangeAmount = 20f;
    PhotonView view;



    // Start is called before the first frame update
    void Start()
    {

        view = GetComponent<PhotonView>();
        
        


        
        

    }

    void Update()
    {
        if (view.IsMine)
        {
            HandleZoom();



            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            if (Input.GetMouseButton(2))
            {




                cam.transform.Rotate(new Vector3(0.5f, 0, 0), direction.y * 45);
                cam.transform.Rotate(new Vector3(0, -0.5f, 0), direction.x * 45, Space.World);



            }
            //The code that makes the camera follow the ship
            cam.transform.position = target.position;
            cam.transform.Translate(new Vector3(0, 5f, zoom));
        }
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
            

    }

    private void HandleZoom()
    {
        if (view.IsMine)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                zoom += zoomChangeAmount * Time.deltaTime * 10f;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                zoom -= zoomChangeAmount * Time.deltaTime * 10f;
            }
            zoom = Mathf.Clamp(zoom, -40f, -5f);
        }
        

        


    }


}
