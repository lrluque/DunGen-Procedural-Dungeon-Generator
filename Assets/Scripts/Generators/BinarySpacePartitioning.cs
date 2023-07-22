using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BinarySpacePartitioning
{
        
    
    public static List<Board> BinaryDivision(Board room, int minHeight, int minWidth)
    {
        List<Board> list = new List<Board>();
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
                        list.Add(currentRoom);
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
                        list.Add(currentRoom);
                    }
                }
            }
        }
        return list;
    }
        
        
        
    private static void SplitHorizontal(Board room, int minHeight, int minWidth, Queue<Board> roomsQueue)
    {
        int split = Random.Range(minHeight, (int)room.GetSize().y - minHeight);
        Board room1 = new Board(new Vector2(room.GetSize().x, split));
        Board room2 = new Board(new Vector2(room.GetSize().x, room.GetSize().y - split));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitVertical(Board room, int minHeight, int minWidth, Queue<Board> roomsQueue)
    {
        int split = Random.Range(minWidth, (int)room.GetSize().x - minWidth);
        Board room1 = new Board(new Vector2(split, room.GetSize().y));
        Board room2 = new Board(new Vector2(room.GetSize().x - split, room.GetSize().y));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}
