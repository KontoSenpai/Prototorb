using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject generator;
    public GameObject player;

    public GameObject start_Base;
    public GameObject exit_Base;

    private int current_Floor = 1;
    private List<GameObject> floor_Rooms;

    private const int min_Room = 2; // Minimal room per floor
    private const int max_Room = 5; // Maximal room per floor

    private int min_Room_Floor = 0;
    private int max_Room_Floor = 0;

    private const int min_Room_Size = 4;
    private const int max_Room_Size = 8;

    private int min_Size = 0;
    private int max_Size = 0;

    private int start_Room_Index = -1;

    void Start ()
    {
        StartCoroutine(LoadNextFloor(true));
    }

    // Update is called once per frame
    void Update ()
    {
        if (generator.GetComponent<Floor>().isComplete() && !player.active)
            PlayerStart();

        if( player.GetComponent<TopDownCharacterMovement>().exitReached())
        {
            current_Floor++;
            generator.GetComponent<Floor>().setComplete(false);
            player.SetActive(false);
            player.GetComponent<TopDownCharacterMovement>().SetExitReached(false);
            StartCoroutine( LoadNextFloor(false));
        }
	}

    private IEnumerator LoadNextFloor(bool first_Floor)
    {
        if( !first_Floor)
        {
            generator.GetComponent<Floor>().DestroyAll();
            yield return new WaitForSeconds(2);
            start_Room_Index = -1;
        }
        player.SetActive(false);
        floor_Rooms = new List<GameObject>();
        min_Room_Floor = min_Room + Mathf.FloorToInt(current_Floor / 2);
        max_Room_Floor += max_Room + Mathf.FloorToInt(current_Floor / 2) * 2;

        min_Size = min_Room_Size + (Mathf.FloorToInt(current_Floor / 5));
        max_Size = max_Room_Size;

        floor_Rooms = generator.GetComponent<Floor>().GenerateLayout(min_Size, max_Size, min_Room_Floor, max_Room_Floor);

        StartCoroutine(generator.GetComponent<Floor>().FillRooms());
    }

    private void PlayerStart()
    {
        player.SetActive(true);
        int random_Room = Random.Range(0, floor_Rooms.Count);
        Vector3 start_Position = floor_Rooms[random_Room].transform.position;

        start_Position.x += 1;
        start_Position.y = 0.01f;
        start_Position.z += 1;
        PlaceStart(floor_Rooms[random_Room], start_Position);

        start_Position.y = 0.7f;
        player.transform.position = start_Position;

        start_Room_Index = random_Room;

        do
        {
            random_Room = Random.Range(0, floor_Rooms.Count);
        } while (random_Room == start_Room_Index);

        Vector3 exit_Position = floor_Rooms[random_Room].transform.position;
        exit_Position.x += 1;
        exit_Position.y = 0.01f;
        exit_Position.z += 1;
        PlaceExit(floor_Rooms[random_Room], exit_Position);
    }

    private void PlaceStart( GameObject start_Room, Vector3 start_Position)
    {
        GameObject start = Instantiate(start_Base, start_Room.transform) as GameObject;
        start.transform.position = start_Position;
    }

    private void PlaceExit(GameObject exit_Room, Vector3 exit_Position)
    {
        GameObject exit = Instantiate(exit_Base, exit_Room.transform) as GameObject;
        exit.transform.position = exit_Position;
    }
}
