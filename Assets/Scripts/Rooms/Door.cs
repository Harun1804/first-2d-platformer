using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoon;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == StringValue.PlayerTag) {
            if (collision.transform.position.x < transform.position.x) {
                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoon.GetComponent<Room>().ActivateRoom(false);
            } else { 
                cam.MoveToNewRoom(previousRoon);
                previousRoon.GetComponent<Room>().ActivateRoom(true);
                nextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
}
