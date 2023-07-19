using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPDungeonGenerator : MonoBehaviour, Generator
{
    //First generate corridors, then generate rooms.
    private Vector2 _size;
    private int _levels;
    private float _roomDensity;
    private Board _board;
    private GameObject _spawnLocation;
    private GameObject[] _rooms;
    private List<Board> _roomList;
    private int _minHeight, _minWidth;
    private int _offset;

    public BSPDungeonGenerator(GameObject[] rooms, GameObject spawnLocation)
    {
        _size = new Vector2(10, 10);
        _roomList = new List<Board>();
        _minHeight = 3;
        _minWidth = 3;
        _rooms = rooms;
        _spawnLocation = spawnLocation;
        _offset = 5;
    }

    public void Generate()
    {
        _roomList.Clear();
        _board = new Board(_size);
        BinaryDivision(_board, _minHeight, _minWidth);
    }

    public void Build()
    {
        Vector3 roomPosition = new Vector3(0, 0, 0);
        Board previousRoom = null;
        foreach (Board room in _roomList)
        {
            roomPosition += GetSpawnCoordinates(room, previousRoom);
            SetBorders(room);
            BuildRoom(room, roomPosition);
            previousRoom = room;
        }  
    }

    private Vector3 GetSpawnCoordinates(Board room, Board previousRoom)
    {
        Vector3 roomPosition = new Vector3(0, 0, 0);
        if (previousRoom != null)
            {
            if (Random.value > 0.5f)
            {
                //Builds the room below the previous room
                roomPosition = new Vector3(0, 0, previousRoom.GetSize().y * -3.6f - _offset);
            }
            else
            {
                //Builds the room to the right of the previous room
                roomPosition = new Vector3((room.GetSize().x) * -3.6f - _offset, 0, 0);
            }
        }
        return roomPosition;
    }

    private void BuildRoom(Board room, Vector3 roomPosition)
    {
        GameObject roomInstance = new GameObject();
        roomInstance.name = "Room";
        roomInstance.transform.SetParent(_spawnLocation.transform, false);
        roomInstance.transform.localPosition = roomPosition + new Vector3(room.GetSize().x * 3.6f, 0, 0);
        for (int i = 0; i < room.GetSize().x; i++)
            {
                for (int j = 0; j < room.GetSize().y; j++)
                {
                    var randomOffset = UnityEngine.Random.Range(0.001f, 0.004f);
                    var randomRoom = UnityEngine.Random.Range(0, _rooms.Length);
                    GameObject roomCell = Instantiate(_rooms[randomRoom], new Vector3(-3.6f * i + randomOffset, randomOffset, -3.6f * j + randomOffset), Quaternion.identity);
                    roomCell.name = "RoomCell " + i + " " + j;
                    roomCell.GetComponent<RoomManager>().UpdateRoom(room.GetBoard()[i][j].GetStatus());
                    roomCell.transform.SetParent(roomInstance.transform, false);
                }

            }
    }


    public void Destroy()
    {
        foreach (Transform child in _spawnLocation.transform)
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
        //Creates the borders of the board
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

    private void BinaryDivision(Board room, int minHeight, int minWidth)
    {
        Queue<Board> rooms = new Queue<Board>();
        rooms.Enqueue(room);
        while (rooms.Count > 0)
        {
            Board currentRoom = rooms.Dequeue();
            if (currentRoom.GetSize().x > minWidth && currentRoom.GetSize().y > minHeight){
                if (Random.value > 0.5f)
                {
                    if (currentRoom.GetSize().y > 2 * minHeight)
                    {
                        SplitHorizontal(currentRoom, minHeight, minWidth, rooms);
                    }
                    else if (currentRoom.GetSize().x > 2 * minWidth)
                    {
                        SplitVertical(currentRoom, minHeight, minWidth, rooms);
                    }
                    else if (currentRoom.GetSize().x > minWidth && currentRoom.GetSize().y > minHeight)
                    {
                        _roomList.Add(currentRoom);
                    }
                }
                else
                {
                    if (currentRoom.GetSize().x > 2 * minWidth)
                    {
                        SplitVertical(currentRoom, minHeight, minWidth, rooms);
                    }
                    else if (currentRoom.GetSize().y > 2 * minHeight)
                    {
                        SplitHorizontal(currentRoom, minHeight, minWidth, rooms);
                    }
                    else if (currentRoom.GetSize().x > minWidth && currentRoom.GetSize().y > minHeight)
                    {
                        _roomList.Add(currentRoom);
                    }
                }
            }
        }

    }

    private void SplitHorizontal(Board room, int minHeight, int minWidth, Queue<Board> roomsQueue)
    {
        int split = Random.Range(minHeight, (int)room.GetSize().y - minHeight);
        Board room1 = new Board(new Vector2(room.GetSize().x, split));
        Board room2 = new Board(new Vector2(room.GetSize().x, room.GetSize().y - split));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private void SplitVertical(Board room, int minHeight, int minWidth, Queue<Board> roomsQueue)
    {
        int split = Random.Range(minWidth, (int)room.GetSize().x - minWidth);
        Board room1 = new Board(new Vector2(split, room.GetSize().y));
        Board room2 = new Board(new Vector2(room.GetSize().x - split, room.GetSize().y));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }




}
