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
    private DepthFirstSearchGenerator _depthFirstSearchGenerator;

    public BSPDungeonGenerator(GameObject[] rooms, GameObject spawnLocation)
    {
        _size = new Vector2(10, 10);
        _rooms = rooms;
        _spawnLocation = spawnLocation;
        _levels = 3;
    }

    public void Generate()
    {
        _board = new Board(_size);
        SetBorders();
        BinaryDivision(0, 0, 0, (int)_size.x - 1, (int)_size.y - 1);
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
                roomInstance.GetComponent<RoomManager>().UpdateRoom(_board.GetBoard()[i][j].GetStatus());
                roomInstance.transform.SetParent(_spawnLocation.transform, false);
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

    private void SetBorders()
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
                }
                if (i == _size.x - 1)
                {
                    _board.GetBoard()[i][j].SetStatus(1, false);
                }
                if (j == 0)
                {
                    _board.GetBoard()[i][j].SetStatus(3, false);
                }
                if (j == _size.y - 1)
                {
                    _board.GetBoard()[i][j].SetStatus(2, false);
                }
            }
        }
    }

    private void EmptyWalls()
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

    private void BinaryDivision(int currentLevel, int i0, int j0, int endI, int jn)
    {
        Debug.Log("Current level: " + currentLevel);
        Debug.Log("i0: " + i0);
        Debug.Log("j0: " + j0);
        Debug.Log("endI: " + endI);
        Debug.Log("jn: " + jn);
        if (currentLevel == _levels)
        {
            return;
        }
        else
        {
            int randomDirection = UnityEngine.Random.Range(0, 2);
            bool collision = false;
            //0 = horizontal
            //1 = vertical
            if (randomDirection == 0)
            {
                int j = j0;
                int randomWall = UnityEngine.Random.Range(1, (endI - i0) - 1);
                Debug.Log("Random wall: " + randomWall);
                while (j <= jn && !collision)
                {
                    Debug.Log("j: " + j);
                    _board.GetBoard()[randomWall][j].SetStatus(1, false);
                    collision = j != jn && !_board.GetBoard()[randomWall][j + 1].GetStatus()[3];
                    j++;
                    Debug.Log(j + "<=" + jn + "=" + (j <= jn));
                }
                //Open one of the walls
                int randomEntrance = UnityEngine.Random.Range(j0 + 1, jn - 1);
                _board.GetBoard()[randomWall][randomEntrance].SetStatus(1, true);
                //Upper half
                BinaryDivision(currentLevel + 1, i0, j0, randomWall, (j-1));
                //Lower half
                BinaryDivision(currentLevel + 1, randomWall, j0, endI, (j-1));
            }
            else
            {
                int i = i0;
                int randomWall = UnityEngine.Random.Range(1, (jn - j0) - 1);
                Debug.Log("Random wall: " + randomWall);
                while (i <= endI && !collision)
                {
                    Debug.Log("i: " + i);
                    _board.GetBoard()[i][randomWall].SetStatus(3, false);
                    collision = i != endI && !_board.GetBoard()[i][randomWall].GetStatus()[1];
                    i++;
                }
                //Open one of the walls
                int randomEntrance = UnityEngine.Random.Range(i0 + 1, endI - 1);
                _board.GetBoard()[randomEntrance][randomWall].SetStatus(3, true);
                //Left half
                BinaryDivision(currentLevel + 1, i0, j0, i, randomWall);
                //Right half
                BinaryDivision(currentLevel + 1, i0, randomWall, i, jn);
            }
        }
    }

}
