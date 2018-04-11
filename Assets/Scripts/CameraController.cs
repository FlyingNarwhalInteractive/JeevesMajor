using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float HorizontalLimit = 10.0f;
    public float VerticalLimit = 10.0f;
    public float ZoomLimit = 2.0f;
    public float PanSpeed = 1.0f;

    public bool followJeeves = false;

    public Vector3 cameraZeroRef = new Vector3(-23.85f, 24.52f, -9.6f);

    

    Vector3 CameraOrigin;
    float CameraSizeOrigin;
    Camera thisCamera;
    Vector3 lastMousePosition;

    GameObject jeevesRef;

    private void Start()
    {
        CameraOrigin = transform.position;
        thisCamera = GetComponent<Camera>();
        CameraSizeOrigin = thisCamera.orthographicSize;

        jeevesRef = GameObject.FindGameObjectWithTag("Jeeves");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            {
                // get input
                Vector3 change = new Vector3(0, 0, 0);
                float zoomChange = 0;

                // keyboard
                change.x += Input.GetAxis("Horizontal") * PanSpeed * 0.25f;
                change.z -= Input.GetAxis("Horizontal") * PanSpeed * 0.25f;

                change.x += Input.GetAxis("Vertical") * PanSpeed * 0.25f;
                change.z += Input.GetAxis("Vertical") * PanSpeed * 0.25f;

                // mouse
                zoomChange -= Input.mouseScrollDelta.y;
                if (SaveData.rightMousePan ? Input.GetMouseButton(1) : Input.GetMouseButton(2))
                {
                    if (SaveData.rightMousePan ? Input.GetMouseButtonDown(1) : Input.GetMouseButtonDown(2))
                        lastMousePosition = Input.mousePosition;
                    else
                    {
                        Vector2 mouseDelta = Input.mousePosition - lastMousePosition;
                        // trim based on window size
                        mouseDelta = new Vector2(mouseDelta.x / Screen.width, mouseDelta.y / Screen.height);

                        change.x -= mouseDelta.x * PanSpeed * 0.6f;
                        change.z += mouseDelta.x * PanSpeed * 0.6f;

                        change.x -= mouseDelta.y * PanSpeed * 0.6f;
                        change.z -= mouseDelta.y * PanSpeed * 0.6f;
                    }
                }

                // apply change
                transform.position += change;
                thisCamera.orthographicSize += zoomChange;
                //if (zoomChange != 0)
                //    transform.position = cameraZeroRef + jeevesRef.transform.localPosition;

                if (followJeeves)
                    transform.position = cameraZeroRef + jeevesRef.transform.localPosition;

                // clamp bounds
                Vector3 DeviationFromOrigin = transform.position - CameraOrigin;
                DeviationFromOrigin.x = Mathf.Clamp(DeviationFromOrigin.x, -HorizontalLimit, HorizontalLimit);
                DeviationFromOrigin.z = Mathf.Clamp(DeviationFromOrigin.z, -HorizontalLimit, HorizontalLimit);
                float DeviationFromSizeOrigin = thisCamera.orthographicSize - CameraSizeOrigin;
                DeviationFromSizeOrigin = Mathf.Clamp(DeviationFromSizeOrigin, -CameraSizeOrigin / ZoomLimit, 0);

                // apply clamp
                transform.position = CameraOrigin + DeviationFromOrigin;
                thisCamera.orthographicSize = CameraSizeOrigin + DeviationFromSizeOrigin;
            }

        }
    }

    public void ToggleJeevesLock()
    {
        followJeeves = !followJeeves;
    }
}
