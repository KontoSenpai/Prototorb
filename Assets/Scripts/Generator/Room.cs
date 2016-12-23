using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

    public GameObject floor_Tile;
    public GameObject wall_Tile;
    public GameObject[] corners;
    public GameObject door_Way;
    public GameObject door_Way_Child;
    public GameObject vision_Obstruction;
    private Vector2 dimensions;

    private Hashtable child_Rooms;
    private string parent_Direction;

	// Use this for initialization
	void Awake ()
    {
        child_Rooms = new Hashtable();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void SetInformations( Vector2 room_Dimension)
    {
        dimensions = room_Dimension;
    }

    public void SetInformations( Vector3 new_Position, Vector2 room_Dimension, string parent_Orientation, GameObject parentObj)
    {
        parent_Direction = parent_Orientation;

        dimensions = room_Dimension;

        transform.position = new_Position;
    }

    public string addChild( List<GameObject> all_Rooms,GameObject child_Room)
    {
        string added = null;

        bool exist = false;

        int random_Value = Random.Range(0, 100);

        if (random_Value < 25 && !child_Rooms.ContainsKey("LEFT"))
        {
            for( int index = 0; index < all_Rooms.Count; index++)
            {
                if (all_Rooms[index].transform.position == new Vector3(transform.position.x, transform.position.y, transform.position.z - (dimensions.y + 1)))
                    exist = true;
            }
            if( !exist)
            {
                child_Rooms.Add("LEFT", child_Room);
                added = "LEFT";
            }
            else
                added = RetryThree("RIGHT", "TOP", "BOTTOM", all_Rooms, child_Room);
        }
        else if (random_Value < 50 && !child_Rooms.ContainsKey("RIGHT"))
        {
            for (int index = 0; index < all_Rooms.Count; index++)
            {
                if (all_Rooms[index].transform.position == new Vector3(transform.position.x, transform.position.y, transform.position.z + (dimensions.y + 1)))
                    exist = true;
            }
            if (!exist)
            {
                child_Rooms.Add("RIGHT", child_Room);
                added = "RIGHT";
            }
            else
                added = RetryThree("LEFT", "TOP", "BOTTOM", all_Rooms, child_Room);
        }
        else if (random_Value < 75 && !child_Rooms.ContainsKey("TOP"))
        {
            for (int index = 0; index < all_Rooms.Count; index++)
            {
                if (all_Rooms[index].transform.position == new Vector3(transform.position.x - (dimensions.x + 1), transform.position.y, transform.position.z))
                    exist = true;
            }
            if (!exist)
            {
                child_Rooms.Add("TOP", child_Room);
                added = "TOP";
            }
            else
                added = RetryThree("LEFT", "RIGHT", "BOTTOM", all_Rooms, child_Room);
        }
        else if (random_Value < 100 && !child_Rooms.ContainsKey("BOTTOM"))
        {
            for (int index = 0; index < all_Rooms.Count; index++)
            {
                if (all_Rooms[index].transform.position == new Vector3(transform.position.x + (dimensions.x + 1), transform.position.y, transform.position.z))
                    exist = true;
            }
            if (!exist)
            {
                child_Rooms.Add("BOTTOM", child_Room);
                added = "BOTTOM";
            }
            else
                added = RetryThree("LEFT", "RIGHT", "TOP", all_Rooms, child_Room);
        }

        return added;
    }

    public string RetryThree(string type1, string type2, string type3, List<GameObject> all_Rooms, GameObject child_Room)
    {
        string added = null;

        bool exist = false;

        int random_Value = Random.Range(0, 100);

        if (random_Value < 33 && !child_Rooms.ContainsKey(type1))
        {
            for (int index = 0; index < all_Rooms.Count; index++)
            {
                if (all_Rooms[index].transform.position == getTypeValue(type1))
                    exist = true;
            }
            if (!exist)
            {
                child_Rooms.Add(type1, child_Room);
                added = type1;
            }
            else
            {
                added = RetryTwo( type2, type3, all_Rooms, child_Room);
            }
        }
        else if (random_Value < 66 && !child_Rooms.ContainsKey(type2))
        {
            for (int index = 0; index < all_Rooms.Count; index++)
            {
                if (all_Rooms[index].transform.position == getTypeValue(type2))
                    exist = true;
            }
            if (!exist)
            {
                child_Rooms.Add(type2, child_Room);
                added = type2;
            }
            else
            {
                added = RetryTwo(type1, type3, all_Rooms, child_Room);
            }
        }
        else if (random_Value < 100 && !child_Rooms.ContainsKey(type3))
        {
            for (int index = 0; index < all_Rooms.Count; index++)
            {
                if (all_Rooms[index].transform.position == getTypeValue(type3))
                    exist = true;
            }
            if (!exist)
            {
                child_Rooms.Add(type3, child_Room);
                added = type3;
            }
            else
            {
                added = RetryTwo(type1, type2, all_Rooms, child_Room);
            }
        }
        return added;
    }

    public string RetryTwo(string type1, string type2, List<GameObject> all_Rooms, GameObject child_Room)
    {
        string added = null;

        bool exist = false;

        int random_Value = Random.Range(0, 100);

        if (random_Value < 50 && !child_Rooms.ContainsKey(type1))
        {
            for (int index = 0; index < all_Rooms.Count; index++)
            {
                if (all_Rooms[index].transform.position == getTypeValue(type1))
                    exist = true;
            }
            if (!exist)
            {
                child_Rooms.Add(type1, child_Room);
                added = type1;
            }
            else
            {
                added = TestOne(type2, all_Rooms, child_Room);
            }
        }
        else if (random_Value < 50 && !child_Rooms.ContainsKey(type2))
        {
            for (int index = 0; index < all_Rooms.Count; index++)
            {
                if (all_Rooms[index].transform.position == getTypeValue(type2))
                    exist = true;
            }
            if (!exist)
            {
                child_Rooms.Add(type2, child_Room);
                added = type2;
            }
            else
            {
                added = TestOne(type1, all_Rooms, child_Room);
            }
        }
        return added;
    }

    private string TestOne(string type1, List<GameObject> all_Rooms, GameObject child_Room)
    {
        string added = null;

        bool exist = false;

        for (int index = 0; index < all_Rooms.Count; index++)
        {
            if (all_Rooms[index].transform.position == getTypeValue(type1))
                exist = true;
        }
        if (!exist)
        {
            child_Rooms.Add(type1, child_Room);
            added = type1;
        }
        else
        {
            added = "ERROR";
        }

        return added;
    }

    public int getMapSize()
    {
        return child_Rooms.Count;
    }

    private Vector3 getTypeValue(string type)
    {
        Vector3 future_Position = transform.position;
        if(type.Equals("LEFT"))
        {
            future_Position.z -= (dimensions.y + 1);
        }
        else if(type.Equals("RIGHT"))
        {
            future_Position.z += (dimensions.y + 1);
        }
        else if(type.Equals("TOP"))
        {
            future_Position.x -= (dimensions.x + 1);
        }
        else if(type.Equals("BOTTOM"))
        {
            future_Position.x += (dimensions.x + 1);
        }

        return future_Position;
    }

    private bool isDoor(string side)
    {
        if (side.Equals(parent_Direction) || child_Rooms[side] != null)
            return true;
        else
            return false;
    }

    private bool isDoorParent(string side)
    {
        if (child_Rooms[side] != null)
            return true;
        else
            return false;

    }

    private bool isDoorChild(string side)
    {
        if (side.Equals(parent_Direction))
            return true;
        else
            return false;
    }

    public IEnumerator SetFloorTile()
    {
        for(int xMin = 1; xMin < dimensions.x - 1; xMin++)
        {
            for(int zMin = 1; zMin < dimensions.y - 1; zMin++)
            {
                GameObject tile = Instantiate(floor_Tile, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x += xMin;
                tilePos.z += zMin;
                tile.transform.position = tilePos;
            }
        }
        yield return new WaitForSeconds(0.05f);

        SetWallTile();
    }

    public void SetWallTile()
    {
        // LEFT WALLS
        bool doorPlaced = false;
        for (int xMin = 1; xMin < dimensions.x -1; xMin++)
        {
            if( isDoorParent("LEFT") && !doorPlaced && xMin == 2)
            {
                GameObject tile = Instantiate(door_Way, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x += xMin;
                tilePos.z -= 0.5f;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, -90, 0));
                doorPlaced = true;
            }
            else if( isDoorChild("LEFT") && !doorPlaced && xMin == 2)
            {
                GameObject tile = Instantiate(door_Way_Child, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x += xMin;
                tilePos.z -= 0.5f;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, -90, 0));
                doorPlaced = true;
            }
            else
            {
                GameObject tile = Instantiate(wall_Tile, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x += xMin;
                tilePos.z -= 0.5f;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, -90, 0));
            }
        }

        // RIGHT WALLS
        doorPlaced = false;
        for (int xMin = 1; xMin < dimensions.x - 1; xMin++)
        {
            if (isDoorParent("RIGHT") && !doorPlaced && xMin == 2)
            {
                GameObject tile = Instantiate(door_Way, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x += xMin;
                tilePos.z += dimensions.y - 0.5f;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, 90, 0));
                doorPlaced = true;
            }
            else if (isDoorChild("RIGHT") && !doorPlaced && xMin == 2)
            {
                GameObject tile = Instantiate(door_Way_Child, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x += xMin;
                tilePos.z += dimensions.y - 0.5f;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, 90, 0));
                doorPlaced = true;
            }
            else
            {
                GameObject tile = Instantiate(wall_Tile, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x += xMin;
                tilePos.z += dimensions.y - 0.5f;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, 90, 0));
            }
        }


        // TOP WALLS
        doorPlaced = false;
        for (int zMin = 1; zMin < dimensions.y - 1; zMin++)
        {
            if (isDoorParent("TOP") && !doorPlaced && zMin == 2)
            {
                GameObject tile = Instantiate(door_Way, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x -= 0.5f;
                tilePos.z += zMin;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, 0, 0));
                doorPlaced = true;
            }
            else if (isDoorChild("TOP") && !doorPlaced && zMin == 2)
            {
                GameObject tile = Instantiate(door_Way_Child, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x -= 0.5f;
                tilePos.z += zMin;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, 0, 0));
                doorPlaced = true;
            }
            else
            {
                GameObject tile = Instantiate(wall_Tile, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x -= 0.5f;
                tilePos.z += zMin;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, 0, 0));
            }
        }

        // BOTTOM WALLS
        doorPlaced = false;
        for (int zMin = 1; zMin < dimensions.y - 1; zMin++)
        {
            if (isDoorParent("BOTTOM") && !doorPlaced && zMin == 2)
            {
                GameObject tile = Instantiate(door_Way, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x += dimensions.x - 0.5f;
                tilePos.z += zMin;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, 180, 0));
                doorPlaced = true;
            }
            else if (isDoorChild("BOTTOM") && !doorPlaced && zMin == 2)
            {
                GameObject tile = Instantiate(door_Way_Child, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x += dimensions.x - 0.5f;
                tilePos.z += zMin;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, 180, 0));
                doorPlaced = true;
            }
            else
            {
                GameObject tile = Instantiate(wall_Tile, transform) as GameObject;
                Vector3 tilePos = transform.position;
                tilePos.x += dimensions.x - 0.5f;
                tilePos.z += zMin;
                tile.transform.position = tilePos;
                tile.transform.Rotate(new Vector3(0, 180, 0));
            }
        }

        // TOP LEFT CORNER
        GameObject corner = Instantiate(corners[( Random.Range(0, 100) < 50) ? 0 : 1], transform) as GameObject;
        Vector3 tile_Pos = transform.position;
        corner.transform.position = tile_Pos;
        corner.transform.Rotate(new Vector3(90, 180, 0));

        // TOP RIGHT CORNER
        corner = Instantiate(corners[(Random.Range(0, 100) < 50) ? 0 : 1], transform) as GameObject;
        tile_Pos = transform.position;
        tile_Pos.z += dimensions.y -1;
        corner.transform.position = tile_Pos;
        corner.transform.Rotate(new Vector3(90, -90, 0));

        // BOTTOM LEFT CORNER
        corner = Instantiate(corners[(Random.Range(0, 100) < 50) ? 0 : 1], transform) as GameObject;
        tile_Pos = transform.position;
        tile_Pos.x += dimensions.x - 1;
        corner.transform.position = tile_Pos;
        corner.transform.Rotate(new Vector3(90, 90, 0));

        // BOTTOM LEFT CORNER
        corner = Instantiate(corners[(Random.Range(0, 100) < 50) ? 0 : 1], transform) as GameObject;
        tile_Pos = transform.position;
        tile_Pos.x += dimensions.x - 1;
        tile_Pos.z += dimensions.y - 1;
        corner.transform.position = tile_Pos;
        corner.transform.Rotate(new Vector3(90, 0, 0));


        vision_Obstruction = Instantiate(vision_Obstruction, transform) as GameObject;
        Vector3 block_Position = transform.position;
        block_Position.x += dimensions.x / 2;
        block_Position.y += 1;
        block_Position.z += dimensions.y / 2;

        vision_Obstruction.transform.position = block_Position;
        vision_Obstruction.transform.localScale = (new Vector3(dimensions.x + 0.9f, 2, dimensions.y + 0.9f));

    }

}
