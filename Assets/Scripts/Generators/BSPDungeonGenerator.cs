using System.Collections.Generic;
using UnityEngine;

public class BSPDungeonGenerator : MonoBehaviour, Generator
{
    private Vector2 _size = new Vector2(10, 10);
    private int _minHeight = 3;
    private int _minWidth = 3;
    private Board _board;
    private GameObject _spawnLocation;
    private GameObject[] _cells;
    private List<GameObject> _roomInstances = new List<GameObject>();
    private List<Board> _roomList = new List<Board>();

    public BSPDungeonGenerator(GameObject[] cells, GameObject spawnLocation)
    {
        _cells = cells;
        _spawnLocation = spawnLocation;
    }

    public void Generate()
    {
        _roomInstances.Clear();
        _roomList.Clear();
        _board = new Board(_size);
        BinaryDivision(_board, _minHeight, _minWidth);
    }

    public void Build()
    {
        Vector3 builderPosition = Vector3.zero;
        Board previousRoom = null;
        int randomCell = -1;
        foreach (Board room in _roomList)
        {
            if (_roomList.IndexOf(room) != 0)
            {
                randomCell = (int)Random.Range(0, room.GetSize().y);
                builderPosition += new Vector3(0, 0, randomCell * 3.6f);
            }
            SetBorders(room);
            BuildRoom(room, builderPosition, randomCell);
            randomCell = (int)Random.Range(0, room.GetSize().y);
            builderPosition -= new Vector3(room.GetSize().x * 3.6f, 0, randomCell * 3.6f);
            if (_roomList.IndexOf(room) != _roomList.Count - 1)
            {
                builderPosition = BuildCorridor(builderPosition);
                _roomInstances[_roomList.IndexOf(room)].transform.Find("RoomCell " + (room.GetSize().x - 1) + " " + randomCell).GetComponent<RoomManager>().SetWall(1, false);
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

    private Vector3 BuildCorridor(Vector3 builderPosition)
    {
        GameObject corridor = new GameObject
        {
            name = "Corridor"
        };
        corridor.transform.SetParent(_spawnLocation.transform, false);
        corridor.transform.localPosition = builderPosition;
        int corridorLength = Random.Range(3, 10);
        Vector3 lastPosition = new Vector3(builderPosition.x, builderPosition.y, builderPosition.z);
        for (int i = 0; i < corridorLength; i++)
        {
            var randomOffset = Random.Range(0.001f, 0.004f);
            GameObject roomCell = Instantiate(_cells[Random.Range(0, _cells.Length)], new Vector3(lastPosition.x + randomOffset, randomOffset, lastPosition.z + randomOffset), Quaternion.identity);
            roomCell.GetComponent<RoomManager>().SetWall(0, false);
            roomCell.GetComponent<RoomManager>().SetWall(1, false);
            roomCell.name = "RoomCell " + i;
            roomCell.transform.SetParent(corridor.transform, true);
            lastPosition = new Vector3(lastPosition.x - 3.6f, 0, lastPosition.z);
        }
        return lastPosition;
    }

    private void BuildRoom(Board room, Vector3 roomPosition, int cellEntrance)
    {
        GameObject roomInstance = new GameObject
        {
            name = "Room"
        };
        roomInstance.transform.SetParent(_spawnLocation.transform, false);
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
        if (cellEntrance != -1)
        {
            roomInstance.transform.Find("RoomCell 0 " + cellEntrance).GetComponent<RoomManager>().SetWall(0, false);
        }
    }

    private void BinaryDivision(Board room, int minHeight, int minWidth)
    {
        Queue<Board> rooms = new Queue<Board>();
        rooms.Enqueue(room);
        while (rooms.Count > 0)
        {
            Board currentRoom = rooms.Dequeue();
            if (currentRoom.GetSize().x > minWidth && currentRoom.GetSize().y > minHeight)
            {
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
