using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera topDownCamera; // Reference to the top-down camera (assigned to "1" key)
    public Camera camera2;       // Reference to camera 2 (assigned to "2" key)
    public Camera camera3;       // Reference to camera 3 (assigned to "3" key)
    public Camera menuCamera;    // Reference to the menu camera (assigned to "Escape" key)
    public GameObject menuCanvas; // Reference to the UI menu Canvas

    private Camera activeCamera; // The currently active camera

    private void Start()
    {
        // Initialize the active camera to the top-down camera
        activeCamera = topDownCamera;

        // Ensure the top-down camera is initially enabled, and other cameras are disabled
        topDownCamera.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
        menuCamera.enabled = false;

        // Ensure the menu starts as closed
        CloseMenu();
    }

    private void Update()
    {
        // Check for camera switching keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToCamera(topDownCamera);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToCamera(camera2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToCamera(camera3);
        }

        // Check for the "Escape" key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activeCamera == menuCamera)
            {
                // If the menu camera is active, close the menu and switch to the previously active camera
                CloseMenu();
                SwitchToCamera(activeCamera);
            }
            else
            {
                // If a different camera is active, open the menu and switch to the menu camera
                OpenMenu();
                SwitchToCamera(menuCamera);
            }
        }
    }

    private void SwitchToCamera(Camera newCamera)
    {
        // Disable the currently active camera
        activeCamera.enabled = false;

        // Set the new active camera
        activeCamera = newCamera;

        // Enable the new active camera
        activeCamera.enabled = true;
    }

    private void OpenMenu()
    {
        // Enable the menu Canvas
        menuCanvas.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }

    private void CloseMenu()
    {
        // Disable the menu Canvas
        menuCanvas.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;
    }
}