using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector2[] initialPosition;

    private void Awake()
    {
        // Store the initial position of the enemies
        initialPosition = new Vector2[enemies.Length];
        for (int i = 0; i < enemies.Length; i++) {
            if (enemies[i] != null) {
                initialPosition[i] = enemies[i].transform.position;
            }
        }
    }

    public void ActivateRoom(bool _isStatus) {
        // Activate or deactivate the enemies in the room
        for (int i = 0; i < enemies.Length; i++) {
            if (enemies[i] != null) {
                enemies[i].SetActive(_isStatus);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}
