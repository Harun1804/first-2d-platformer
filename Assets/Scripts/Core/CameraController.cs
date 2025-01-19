using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;

    // Room Camera
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Player Camera
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    private float lookAhead;


    private void Update()
    {
        // Room Camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, cameraSpeed);

        // Player Camera
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom) 
    {
        currentPosX = _newRoom.position.x;
    }
}
