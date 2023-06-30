using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearchGenerator : MonoBehaviour, Generator
{
    private Vector2 _size;
    private Board _board;
    private GameObject[] _rooms;
    private GameObject _spawnLocation;

    public DepthFirstSearchGenerator(GameObject[] rooms, GameObject spawnLocation)
    {
        _size = new Vector2(10, 10);
        _rooms = rooms;
        _spawnLocation = spawnLocation;
    }

    public void Generate()
    {
        _board = new Board(_size); 
        System.Random rnd = new System.Random();
        Cell currentCell = _board.GetBoard()[rnd.Next(0, (int)_size.x)][rnd.Next(0, (int)_size.y)];
        currentCell.SetVisited(true);
        Stack<Cell> path = new Stack<Cell>();
        path.Push(currentCell);
        /// Check neighbors and push the cells to the path.
        while(path.Count != 0){   
            currentCell = path.Pop();
            List<Cell> neighbors = _board.CheckNeighbors(currentCell);

            /// Check if the cell is in the path.
            if (neighbors.Count != 0)
            {   
                path.Push(currentCell);
                int newCellInt = rnd.Next(0, neighbors.Count);
                Cell newCell = neighbors[newCellInt];
                //Check directions and move to the new cell
                //Up
                if (newCell.GetxPos() < currentCell.GetxPos())
                {
                    currentCell.SetStatus(0, true);
                    newCell.SetStatus(1, true);
                    currentCell = newCell;
                }
                //Down
                if (newCell.GetxPos() > currentCell.GetxPos())
                {
                    currentCell.SetStatus(1, true);
                    newCell.SetStatus(0, true);
                    currentCell = newCell;
                }
                //Right
                if (newCell.GetyPos() > currentCell.GetyPos())
                {
                    currentCell.SetStatus(2, true);
                    newCell.SetStatus(3, true);
                    currentCell = newCell;
                }
                //Left
                if (newCell.GetyPos() < currentCell.GetyPos())
                {
                    currentCell.SetStatus(3, true);
                    newCell.SetStatus(2, true);
                    currentCell = newCell;
                }
                currentCell.SetVisited(true);
                path.Push(currentCell);
            }
        }
    }

    public void Build()
    {
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {   
                if (_board.GetBoard()[i][j].GetVisited())
                {
                    var randomOffset = UnityEngine.Random.Range(0.001f, 0.004f);
                    var randomRoom = UnityEngine.Random.Range(0, _rooms.Length);
                    GameObject roomInstance = Instantiate(_rooms[randomRoom], new Vector3(-3.6f * i + randomOffset, randomOffset, -3.6f * j + randomOffset), Quaternion.identity);
                    roomInstance.GetComponent<RoomManager>().UpdateRoom(_board.GetBoard()[i][j].GetStatus());
                    roomInstance.transform.SetParent(_spawnLocation.transform, false);
                }
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
        this._size.x = width;
    }

    public void SetHeight(int height)
    {
        this._size.y = height;
    }

    public Board GetBoard()
    {
        return this._board;
    }

}
