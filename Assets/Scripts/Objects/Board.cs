using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private Vector2 _size;
    private Cell[][] _board;

    public Board(Vector2 size)
    {
        this._size = size;
        this._board = new Cell[(int)_size.x][];
        for (int i = 0; i < _size.x; i++)
        {
            this._board[i] = new Cell[(int)_size.y];
            for (int j = 0; j < _size.y; j++)
            {
                this._board[i][j] = new Cell(i, j);
            }
        }
    }

    public Cell[][] GetBoard()
    {
        return _board;
    }


    /// Checks the neighbors of a cell and returns a list of cells that are reachable from the cell. This is used to detect if a cell has to be added to the _board
    /// 
    /// @param cell - The cell to check the neighbors of
    /// 
    /// @return A list of cells that are reachable from the cell in the form of a list of cells that are
    public List<Cell> CheckNeighbors(Cell cell)
    {   

        List<Cell> neighbors = new List<Cell>();
        Cell newCell;

        //up
        if (cell.GetxPos() > 0 && !_board[cell.GetxPos() - 1][cell.GetyPos()].GetVisited())
        {
            newCell = _board[cell.GetxPos() - 1][cell.GetyPos()];
            neighbors.Add(newCell);
        }
        //down
        if (cell.GetxPos() < _size.x - 1 && !_board[cell.GetxPos() + 1][cell.GetyPos()].GetVisited())
        {
            newCell = _board[cell.GetxPos() + 1][cell.GetyPos()];
            neighbors.Add(newCell);
        }
        //right
        if (cell.GetyPos() < _size.y - 1 && !_board[cell.GetxPos()][cell.GetyPos() + 1].GetVisited())
        {
            newCell = _board[cell.GetxPos()][cell.GetyPos() + 1];
            neighbors.Add(newCell);
        }
        //left
        if (cell.GetyPos() > 0 && !_board[cell.GetxPos()][cell.GetyPos() - 1].GetVisited())
        {
            newCell = _board[cell.GetxPos()][cell.GetyPos() - 1];
            neighbors.Add(newCell);
        }

        return neighbors;
    }

    
}
