using UnityEngine;

public class CameraController : MonoBehaviour
{
    // How close cursor need to get the the edge of the screen so the camera will move
    public float panBorderThickness = 10.0f; 
    public float panSpeed = 20.0f;
    public float panLimit = 200f;

    private GameObject player;

    public float scrollSpeed = 5.0f;
    public float minScroll;
    public float maxScroll;

    private void Start()
    {
        //Fix this to Player later
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update(){
        Vector3 pos = transform.position;

        // move camera up
        if (Input.mousePosition.y >= Screen.height - panBorderThickness){
            pos.z += panSpeed * Time.deltaTime;
        }

        // move camera down
        if (Input.mousePosition.y <= panBorderThickness){
            pos.z -= panSpeed * Time.deltaTime;
        }

        // move camera right
        if (Input.mousePosition.x >= Screen.width - panBorderThickness){
            pos.x += panSpeed * Time.deltaTime;
        }

        // move camera left
        if (Input.mousePosition.x <= panBorderThickness){
            pos.x -= panSpeed * Time.deltaTime;
        }

        // go to player position
        if (Input.GetKey("f")){
            pos.x = player.transform.position.x;
            pos.z = player.transform.position.y - pos.y;
        }

        // Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += scrollSpeed * scroll * 100f * Time.deltaTime; 
        pos.y = Mathf.Clamp(pos.y, minScroll, maxScroll);

        // Limit
        pos.z = Mathf.Clamp(pos.z, -panLimit, panLimit);
        pos.x = Mathf.Clamp(pos.x, -panLimit, panLimit);

        transform.position = pos;
    }
}
