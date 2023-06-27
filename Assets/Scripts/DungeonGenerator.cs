using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
        public int xPos, yPos;

        public void Reset() {
            visited = false;
            status = new bool[4];
        }
    }

    public Vector2 size;

    public Cell[][] board;

    public GameObject room;

    public GameObject mazeObj;


    /// Starts the maze generator and dungeon build process. Should be called at the start of the game
    void Start()
    {   
        newMaze();
    }

    void Update()
    {

    }

    public void setWidth(int width)
    {
        size.x = width;
    }
    
    public void setHeight(int height)
    {
        size.y = height;
    }

    void newMaze()
    {
        MazeGenerator();
        BuildMaze();
    }

    public void restartMaze()
    {
        CreateBoard();
        newMaze();
    }

    void CreateBoard()
    {
        board = new Cell[(int)size.x][];

        /// Creates a new board with the board size.
        for (int i = 0; i < size.x; i++)
        {
            board[i] = new Cell[(int)size.y];
            for (int j = 0; j < size.y; j++)
            {
                board[i][j] = new Cell();
                board[i][j].xPos = i;
                board[i][j].yPos = j;
            }
        }
    }

    public void DestroyMaze()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board[i][j].Reset();
            }
        }
        foreach (Transform child in mazeObj.transform) {
            Destroy(child.gameObject);
        }
    }
    /// Maze Generator This is the main function of the game. It generates a maze by walking the board using BFS
    void MazeGenerator()
    {
        System.Random rnd = new System.Random();

        CreateBoard();

        Cell currentCell = board[rnd.Next(0, (int)size.x)][rnd.Next(0, (int)size.y)];
        currentCell.visited = true;
        Stack<Cell> path = new Stack<Cell>();
        path.Push(currentCell);
        /// Check neighbors and push the cells to the path.
        while(path.Count != 0){   
            currentCell = path.Pop();
            List<Cell> neighbors = CheckNeighbors(currentCell);

            /// Check if the cell is in the path.
            if (neighbors.Count != 0)
            {   
                path.Push(currentCell);
                int newCellInt = rnd.Next(0, neighbors.Count);
                Cell newCell = neighbors[newCellInt];
                //Check directions and move to the new cell
                //Up
                if (newCell.xPos < currentCell.xPos)
                {
                    currentCell.status[0] = true;
                    newCell.status[1] = true;
                    currentCell = newCell;
                }
                //Down
                if (newCell.xPos > currentCell.xPos)
                {
                    currentCell.status[1] = true;
                    newCell.status[0] = true;
                    currentCell = newCell;
                }
                //Right
                if (newCell.yPos > currentCell.yPos)
                {
                    currentCell.status[2] = true;
                    newCell.status[3] = true;
                    currentCell = newCell;
                }
                //Left
                if (newCell.yPos < currentCell.yPos)
                {
                          currentCell.status[3] = true;
                    newCell.status[2] = true;
                    currentCell = newCell;
                }
                currentCell.visited = true;
                path.Push(currentCell);
            }
        }
    }

    /// Checks the neighbors of a cell and returns a list of cells that are reachable from the cell. This is used to detect if a cell has to be added to the board
    /// 
    /// @param cell - The cell to check the neighbors of
    /// 
    /// @return A list of cells that are reachable from the cell in the form of a list of cells that are
    List<Cell> CheckNeighbors(Cell cell)
    {   

        Debug.Log("Current Cell: [" + cell.xPos + ", " + cell.yPos + "]");
        List<Cell> neighbors = new List<Cell>();
        Cell newCell;

        //up
        if (cell.xPos > 0 && !board[cell.xPos - 1][cell.yPos].visited)
        {
            newCell = board[cell.xPos - 1][cell.yPos];
            neighbors.Add(newCell);
        }
        //down
        if (cell.xPos < size.x - 1 && !board[cell.xPos + 1][cell.yPos].visited)
        {
            newCell = board[cell.xPos + 1][cell.yPos];
            neighbors.Add(newCell);
        }
        //right
        if (cell.yPos < size.y - 1 && !board[cell.xPos][cell.yPos + 1].visited)
        {
            newCell = board[cell.xPos][cell.yPos + 1];
            neighbors.Add(newCell);
        }
        //left
        if (cell.yPos > 0 && !board[cell.xPos][cell.yPos - 1].visited)
        {
            newCell = board[cell.xPos][cell.yPos - 1];
            neighbors.Add(newCell);
        }

        return neighbors;
    }

    /// Builds dungeon from game board. Used to update rooms on the map and the gameplay when player visits
    void BuildMaze()
    {   

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {   
                if (board[i][j].visited)
                {
                    var randomOffset = UnityEngine.Random.Range(0.001f, 0.004f);
                    GameObject roomInstance = Instantiate(room, new Vector3(-3.6f * i + randomOffset, randomOffset, -3.6f * j + randomOffset), transform.rotation);
                    roomInstance.GetComponent<RoomBehaviour>().UpdateRoom(board[i][j].status);
                    roomInstance.transform.SetParent(mazeObj.transform, false);
                }
            }
        }
    }
}