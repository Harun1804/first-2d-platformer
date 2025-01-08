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
            } else { 
                cam.MoveToNewRoom(previousRoon);
            }
        }
    }
}
