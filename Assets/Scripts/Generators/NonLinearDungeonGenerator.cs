using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*

NOT FINISHED

*/
public class NonLinearDungeonGenerator : MonoBehaviour, Generator
{
    [SerializeField] 
    private Vector2 _size = new Vector2(10, 10);
    [SerializeField]
    private int _minHeight = 3;
    [SerializeField]
    private int _minWidth = 3;
    [SerializeField]
    private GameObject[] _cells;



    private Board _board;
    private GameObject _dungeon; //This is the parent of all the rooms
    private List<Board> _roomList = new List<Board>(); //This list will contain all the rooms before they are built
    private List<GameObject> _roomInstances = new List<GameObject>(); //This list will contain all the rooms after they are built


    public NonLinearDungeonGenerator(GameObject[] cells, GameObject dungeon)
    {
        _cells = cells;
        _dungeon = dungeon;
    }

    public void Generate()
    {
        _roomInstances.Clear();
        _roomList.Clear();
        _board = new Board(_size);
        _roomList = BinarySpacePartitioning.BinaryDivision(_board, _minHeight, _minWidth);
    }

    public void Build()
    {
        Vector3 builderPosition = Vector3.zero;
        string previousDirection = "";
        int randomCell = -1;
        foreach (Board room in _roomList)
        {
            //First we build a room. If previousDirection == "left" we open a random cell in the right, if previousDirection == "bottom" we open a random cell in the top. 
            //We also move it vertically or horizontally depending on the previous direction
            SetBorders(room);
            BuildRoom(room, builderPosition);
            if (previousDirection == "left")
            {
                randomCell = Random.Range(1, (int)room.GetSize().y - 1);
                //We open the entrance
                _roomInstances[_roomInstances.Count - 1].transform.Find("RoomCell 0 " + randomCell).GetComponent<RoomManager>().SetWall(0, false);
                //We move the actual room
                builderPosition += new Vector3(0, 0, 3.6f * randomCell);
                _roomInstances[_roomInstances.Count - 1].transform.localPosition += new Vector3(0, 0, 3.6f * randomCell);
            }
            else if (previousDirection == "bottom")
            {
                randomCell = Random.Range(1, (int)room.GetSize().x - 1);
                //We open the entrance
                _roomInstances[_roomInstances.Count - 1].transform.Find("RoomCell " + randomCell + " 0").GetComponent<RoomManager>().SetWall(3, false);
                //We move the actual room
                builderPosition += new Vector3(3.6f * randomCell, 0, 0);
                _roomInstances[_roomInstances.Count - 1].transform.localPosition += new Vector3(3.6f * randomCell, 0, 0);
            }
            //If is the last room we do not build a corridor
            if (room != _roomList[_roomList.Count - 1])
                {
                //We now build a corridor in a random direction, left or bottom and then select a random cell to build it
                string direction = Random.value > 0.5f ? "left" : "bottom";
                if (direction == "left")
                {
                    randomCell = Random.Range(1, (int)room.GetSize().y - 1);
                    builderPosition -= new Vector3(3.6f * (room.GetSize().x), 0, 3.6f * randomCell);
                    builderPosition = BuildCorridor(builderPosition, direction);
                    previousDirection = "left";
                    //We set the entrance of the room to false
                    _roomInstances[_roomInstances.Count - 1].transform.Find("RoomCell " + (room.GetSize().x - 1) + " " + randomCell).GetComponent<RoomManager>().SetWall(1, false);
                }
                else
                {
                    randomCell = Random.Range(1, (int)room.GetSize().x - 1);
                    builderPosition -= new Vector3(3.6f * randomCell, 0, 3.6f * (room.GetSize().y));
                    builderPosition = BuildCorridor(builderPosition, direction);
                    previousDirection = "bottom";
                    //We set the entrance of the room to false
                    _roomInstances[_roomInstances.Count - 1].transform.Find("RoomCell " + randomCell + " " + (room.GetSize().y - 1)).GetComponent<RoomManager>().SetWall(2, false);
                }
            }

        }
    }

    public void Destroy()
    {
        foreach (Transform child in _dungeon.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetWidth(int width)
    {
        _size.x = width;
    }

    public void SetHeight(int height)
    {
        _size.y = height;
    }

    private void SetBorders(Board board)
    {
        EmptyWalls(board);
        for (int i = 0; i < board.GetSize().x; i++)
        {
            for (int j = 0; j < board.GetSize().y; j++)
            {
                if (i == 0)
                {
                    board.GetBoard()[i][j].SetStatus(0, false);
                }
                if (i == board.GetSize().x - 1)
                {
                    board.GetBoard()[i][j].SetStatus(1, false);
                }
                if (j == 0)
                {
                    board.GetBoard()[i][j].SetStatus(3, false);
                }
                if (j == board.GetSize().y - 1)
                {
                    board.GetBoard()[i][j].SetStatus(2, false);
                }
            }
        }
    }

    private void EmptyWalls(Board board)
    {
        for (int i = 0; i < board.GetSize().x; i++)
        {
            for (int j = 0; j < board.GetSize().y; j++)
            {
                board.GetBoard()[i][j].SetStatus(0, true);
                board.GetBoard()[i][j].SetStatus(1, true);
                board.GetBoard()[i][j].SetStatus(2, true);
                board.GetBoard()[i][j].SetStatus(3, true);
            }
        }
    }


    private Vector3 BuildCorridor(Vector3 builderPosition, string direction)
    {
        GameObject corridor = new GameObject
        {
            name = "Corridor"
        };
        corridor.transform.SetParent(_dungeon.transform, false);
        corridor.transform.localPosition = builderPosition;
        int corridorLength = Random.Range(3, 10);
        Vector3 lastPosition = new Vector3(builderPosition.x, builderPosition.y, builderPosition.z);
        for (int i = 0; i < corridorLength; i++)
        {
            if (direction == "left")
            {
                var randomOffset = Random.Range(0.001f, 0.004f);
                GameObject roomCell = Instantiate(_cells[Random.Range(0, _cells.Length)], new Vector3(lastPosition.x + randomOffset, randomOffset, lastPosition.z + randomOffset), Quaternion.identity);
                roomCell.GetComponent<RoomManager>().SetWall(0, false);
                roomCell.GetComponent<RoomManager>().SetWall(1, false);
                roomCell.name = "RoomCell " + i;
                roomCell.transform.SetParent(corridor.transform, true);
                lastPosition = new Vector3(lastPosition.x - 3.6f, 0, lastPosition.z);
            }
            else
            {
                var randomOffset = Random.Range(0.001f, 0.004f);
                GameObject roomCell = Instantiate(_cells[Random.Range(0, _cells.Length)], new Vector3(lastPosition.x + randomOffset, randomOffset, lastPosition.z + randomOffset), Quaternion.identity);
                roomCell.GetComponent<RoomManager>().SetWall(2, false);
                roomCell.GetComponent<RoomManager>().SetWall(3, false);
                roomCell.name = "RoomCell " + i;
                roomCell.transform.SetParent(corridor.transform, true);
                lastPosition = new Vector3(lastPosition.x, 0, lastPosition.z - 3.6f);
            }
        }
        return lastPosition;
    }

    private void BuildRoom(Board room, Vector3 roomPosition)
    {
        GameObject roomInstance = new GameObject
        {
            name = "Room"
        };
        roomInstance.transform.SetParent(_dungeon.transform, false);
        roomInstance.transform.localPosition = roomPosition;
        for (int i = 0; i < room.GetSize().x; i++)
        {
            for (int j = 0; j < room.GetSize().y; j++)
            {
                var randomOffset = Random.Range(0.001f, 0.004f);
                GameObject roomCell = Instantiate(_cells[Random.Range(0, _cells.Length)], new Vector3(-3.6f * i + randomOffset, randomOffset, -3.6f * j + randomOffset), Quaternion.identity);
                roomCell.name = "RoomCell " + i + " " + j;
                roomCell.GetComponent<RoomManager>().UpdateRoom(room.GetBoard()[i][j].GetStatus());
                roomCell.transform.SetParent(roomInstance.transform, false);
            }
        }
        _roomInstances.Add(roomInstance);
    }

}

