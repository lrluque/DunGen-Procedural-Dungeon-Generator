using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPDungeonGenerator : MonoBehaviour, Generator
{
    //First generate corridors, then generate rooms.
    private Vector2 _size;
    private int _numberOfRooms;
    private float _roomDensity = 0.5f;
    private Board _board;
    private GameObject _spawnLocation;
    private GameObject[] _rooms;
    private DepthFirstSearchGenerator _depthFirstSearchGenerator;

    public BSPDungeonGenerator(GameObject[] rooms, GameObject spawnLocation, float roomDensity)
    {
        _size = new Vector2(10, 10);
        _rooms = rooms;
        _spawnLocation = spawnLocation;
        _roomDensity = roomDensity;
        _numberOfRooms = CalculateNumberOfRooms();
    }

    private int CalculateNumberOfRooms()
    {
        //Calculate the number of aproximate number of rooms based on the room density
        return (int)Mathf.Floor((_roomDensity)*((_size.x * _size.y / 4) - 2) + 2);
    }



    public void Generate()
    {
        _board = new Board(_size);
        _numberOfRooms = CalculateNumberOfRooms();
        Debug.Log("n=" + _numberOfRooms);
        SetBorders();
        BinaryDivision(_numberOfRooms);
    }

    public void SetBorders()
    {
        //Creates the borders of the board
        EmptyWalls();
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                if (i == 0)
                {
                    _board.GetBoard()[i][j].SetStatus(0, false);
                }if (i == _size.x - 1)
                {
                    _board.GetBoard()[i][j].SetStatus(1, false);
                }if (j == 0)
                {
                    _board.GetBoard()[i][j].SetStatus(3, false);
                }if (j == _size.y - 1)
                {
                    _board.GetBoard()[i][j].SetStatus(2, false);
                }
            }
        }
       
    }

    public void EmptyWalls()
    {
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                _board.GetBoard()[i][j].SetStatus(0, true);
                _board.GetBoard()[i][j].SetStatus(1, true);
                _board.GetBoard()[i][j].SetStatus(2, true);
                _board.GetBoard()[i][j].SetStatus(3, true);
            }
        }
    }

    public void BinaryDivision(int _numberOfRooms)
    {
        /*
            1. Choose direction (vertical or horizontal) 50/50
            2. Choose a random point on the board on i axis (vertical) or j axis (horizontal)
            3. Create a wall on the axis until it encounters another wall
            4. Repeat process _numberOfRooms times
        */
        int direction = UnityEngine.Random.Range(0, 2);

        for (int room = 0; room < _numberOfRooms; room++)
        {
            //Choose direction
            int randomPoint;
            //Choose random point
            //Create wall
            if (direction == 0)
            {
                //Horizontal
                randomPoint = UnityEngine.Random.Range(2, (int)_size.y - 2);
                Debug.Log(randomPoint);
                _board.GetBoard()[randomPoint][0].SetStatus(1, false); 
                for (int j = 0; j < _size.x; j++)
                {
                    Debug.Log(randomPoint + " " + j);
                    _board.GetBoard()[randomPoint][j].SetStatus(1, false);   
                    if (j == _size.x - 1 || _board.GetBoard()[randomPoint][j + 1].GetStatus()[3] == false)
                    {
                        int randomWall = UnityEngine.Random.Range(0, j);
                        _board.GetBoard()[randomPoint][randomWall].SetStatus(1, true);
                        break;
                    }
                }
                direction = 1;
            }
            else
            {
                //Vertical
                randomPoint = UnityEngine.Random.Range(2, (int)_size.x - 2);
                Debug.Log(randomPoint);
                for (int i = 0; i < _size.y; i++)
                {
                    Debug.Log(i + " " + randomPoint);
                    _board.GetBoard()[i][randomPoint].SetStatus(3, false);
                    if (i == _size.y - 1)
                    {
                        int randomWall = UnityEngine.Random.Range(0, i);
                        _board.GetBoard()[randomWall][randomPoint].SetStatus(3, true);
                        break;
                    }else if (_board.GetBoard()[i + 1][randomPoint].GetStatus()[1] == false)
                    {
                        int randomWall = UnityEngine.Random.Range(0, i);
                        _board.GetBoard()[randomWall][randomPoint].SetStatus(3, true);
                        _board.GetBoard()[i + 1][randomPoint].SetStatus(3, false);
                        break;
                    }
                }
                direction = 0;
            }
        }

    }

    
    public void Build()
    {
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                var randomOffset = UnityEngine.Random.Range(0.001f, 0.004f);
                var randomRoom = UnityEngine.Random.Range(0, _rooms.Length);
                GameObject roomInstance = Instantiate(_rooms[randomRoom], new Vector3(-3.6f * i + randomOffset, randomOffset, -3.6f * j + randomOffset), Quaternion.identity);
                roomInstance.name = "Room " + i + " " + j;
                roomInstance.GetComponent<RoomController>().UpdateRoom(_board.GetBoard()[i][j].GetStatus());
                roomInstance.transform.SetParent(_spawnLocation.transform, false);
            }
        }
    }

    public void Destroy()
    {
        foreach (Transform child in _spawnLocation.transform) {
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

}