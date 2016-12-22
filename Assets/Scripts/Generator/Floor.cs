using UnityEngine;
using System.Collections.Generic;

public class Floor : MonoBehaviour {

    public GameObject[] roomType;

    private Vector3 base_Room_Position = Vector3.zero;

    private const int min_Room = 2;
    private const int max_Room = 5;

    private int min_Room_Floor = 0;
    private int max_Room_Floor = 0;

    private int current_Floor = 1;

    private const int min_Room_Size = 4;
    private const int max_Room_Size = 8;

    private int min_Size = 0;
    private int max_Size = 0;

    private Vector2 room_Dimensions;

    private int current_Rooms;
    private List<GameObject> rooms;

	void Start ()
    {
        min_Room_Floor = min_Room + Mathf.FloorToInt( current_Floor / 2);
        max_Room_Floor += max_Room + Mathf.FloorToInt(current_Floor / 2) * 2;

        min_Size = min_Room_Size + (Mathf.FloorToInt(current_Floor / 5));
        max_Size = max_Room_Size;

        room_Dimensions = new Vector2(Random.Range(min_Room_Size, max_Size), Random.Range(min_Room_Size, max_Size));

        rooms = new List<GameObject>();

        GenerateLayout();
	}
	
	void Update ()
    {
	
	}

    private void GenerateLayout()
    {
        int nb_Room = Random.Range(min_Room_Floor, max_Room_Floor);

        rooms.Add( GetRoomType(Random.Range(0, 100)));
        rooms[0].name = "Room 0";
        StartCoroutine(rooms[0].GetComponent<Room>().SetInformations(room_Dimensions));
        
        current_Rooms++;

        for ( int room = 1; room < nb_Room; room++)
        {
            rooms.Add( GetRoomType( Random.Range( 0, 100)));
            AddChild(rooms[room]);
            rooms[room].name = "Room " + room.ToString();
            current_Rooms++;
        }
    }

    private void AddChild(GameObject room)
    {
        string ok = null;
        int random_Value = -1;
        do
        {
            random_Value = Random.Range(0, current_Rooms - 1);
            if ( rooms[random_Value].GetComponent<Room>().getMapSize() < 4)
            {
               ok = rooms[random_Value].GetComponent<Room>().addChild( rooms, room);
            }
        } while (ok == null || ok.Equals("ERROR"));

        Vector3 position = rooms[random_Value].transform.position;
        if( ok.Equals("LEFT"))
        {
            position.z -= (room_Dimensions.y + 1);
            StartCoroutine(room.GetComponent<Room>().SetInformations(position, room_Dimensions, "RIGHT", rooms[random_Value]));
        }
        else if (ok.Equals("RIGHT"))
        {
            position.z += (room_Dimensions.y + 1);
            StartCoroutine(room.GetComponent<Room>().SetInformations(position, room_Dimensions, "LEFT", rooms[random_Value]));
        }
        else if (ok.Equals("TOP"))
        {
            position.x -= (room_Dimensions.x + 1);
            StartCoroutine(room.GetComponent<Room>().SetInformations(position, room_Dimensions, "BOTTOM", rooms[random_Value]));
        }
        else if (ok.Equals("BOTTOM"))
        {
            position.x += (room_Dimensions.x + 1);
            StartCoroutine(room.GetComponent<Room>().SetInformations(position, room_Dimensions, "TOP", rooms[random_Value]));
        }
    }

    private GameObject GetRoomType(int randValue)
    {
        GameObject room = null;
        if( randValue < 100)
        {
            room = Instantiate(roomType[0], transform.position, transform.rotation) as GameObject;
        }

        return room;
    }
}
