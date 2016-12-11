using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private float maxRotX = 90;
    [SerializeField]
    private float minRotX = -90;
    [SerializeField]
    private float sensitivity = 10;
    private float cameraRotationX = 0;

	// Update is called once per frame
	private void Update () {
        LookUpDown();
	}

    private void LookUpDown()
    {
        cameraRotationX += -Input.GetAxis("Mouse Y");
        if (cameraRotationX > minRotX && cameraRotationX < maxRotX)
        {
            transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0));
        }
        else
        {
            cameraRotationX -= -Input.GetAxis("Mouse Y") * sensitivity;
        }
    }
}
