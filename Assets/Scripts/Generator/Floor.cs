using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour {

    public GameObject[] roomType;

    public GameObject roof_Light;

    private Vector2 room_Dimensions;

    private int current_Rooms;
    private List<GameObject> rooms;
    private bool complete;

    public List<GameObject> GenerateLayout(int min_Room_Size, int max_Room_Size, int min_Room_Floor, int max_Room_Floor)
    {
        rooms = new List<GameObject>();
        current_Rooms = 0;

        room_Dimensions = new Vector2(Random.Range(min_Room_Size, max_Room_Size), Random.Range(min_Room_Size, max_Room_Size));
        int nb_Room = Random.Range(min_Room_Floor, max_Room_Floor);

        rooms.Add( GetRoomType(Random.Range(0, 100)));
        rooms[0].name = "Room 0";
        rooms[0].GetComponent<Room>().SetInformations(room_Dimensions);
        
        current_Rooms++;

        for ( int room = 1; room < nb_Room; room++)
        {
            rooms.Add( GetRoomType( Random.Range( 0, 100)));
            AddChild(rooms[room]);
            rooms[room].name = "Room " + room.ToString();
            current_Rooms++;
        }

        return rooms;
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
            room.GetComponent<Room>().SetInformations(position, room_Dimensions, "RIGHT", rooms[random_Value]);
        }
        else if (ok.Equals("RIGHT"))
        {
            position.z += (room_Dimensions.y + 1);
            room.GetComponent<Room>().SetInformations(position, room_Dimensions, "LEFT", rooms[random_Value]);
        }
        else if (ok.Equals("TOP"))
        {
            position.x -= (room_Dimensions.x + 1);
            room.GetComponent<Room>().SetInformations(position, room_Dimensions, "BOTTOM", rooms[random_Value]);
        }
        else if (ok.Equals("BOTTOM"))
        {
            position.x += (room_Dimensions.x + 1);
            room.GetComponent<Room>().SetInformations(position, room_Dimensions, "TOP", rooms[random_Value]);
        }
    }

    public IEnumerator FillRooms()
    {
        for( int index = 0; index < rooms.Count; index ++)
        {
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(rooms[index].GetComponent<Room>().SetFloorTile());
            StartCoroutine(rooms[index].GetComponent<Room>().SetLight(roof_Light));
        }

        complete = true;
    }

    public bool isComplete(){ return complete; }
    public void setComplete(bool ok) { complete = ok; }
    private GameObject GetRoomType(int randValue)
    {
        GameObject room = null;
        if( randValue < 30) // Normal
        {
            room = Instantiate(roomType[2], transform.position, transform.rotation) as GameObject;
        }
        else if( randValue < 45) // Infirmary
        {
            room = Instantiate(roomType[0], transform.position, transform.rotation) as GameObject;
        }
        else if (randValue < 65) // Kitchen
        {
            room = Instantiate(roomType[1], transform.position, transform.rotation) as GameObject;
        }
        else if (randValue < 85) // Storage
        {
            room = Instantiate(roomType[3], transform.position, transform.rotation) as GameObject;
        }
        else if (randValue <= 100) // Weaponry
        {
            room = Instantiate(roomType[4], transform.position, transform.rotation) as GameObject;
        }

        return room;
    }

    public void DestroyAll()
    {
        for( int index = 0; index < rooms.Count; index++)
        {
            Destroy(rooms[index]);
        }
        rooms = new List<GameObject>();
    }
}
