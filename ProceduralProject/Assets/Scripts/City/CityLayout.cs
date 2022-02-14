using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Void,       // 0
    Skyscraper,     // 1
    RandomPOI,  // 2
    Merchant,   // 3
    Shrine,     // 4
    QuestGiver, // 5
    Loot,       // 6
    FloorEnter, // 7
    FloorExit   // 8
}


public class CityLayout
{
    private int lilsPerBig = 5;
    private int res = 0;
    private int hires = 0;
    private int[,] lilblocks;
    private int[,] bigblocks;

    public void Generate(int size)
    {
        res = size;
        hires = res * lilsPerBig;

        bigblocks = new int[res, res];
        lilblocks = new int[hires, hires];

        WalkBlocks(BlockType.FloorEnter, BlockType.FloorExit);

        WalkBlocks(BlockType.RandomPOI, BlockType.RandomPOI);
        //walk()
        //walk()

        MakeBigBlocks();
        // PunchHoles()

    }

    private void WalkBlocks(BlockType a, BlockType b)
    {
        //starting block
        int x = Random.Range(0, hires);
        int y = Random.Range(0, hires);

        int half = hires / 2;

        //end blocks
        int tx = Random.Range(0, half);
        int ty = Random.Range(0, half);

        if (x < half) tx += half;
        if (y < half) ty += half;

        // insert two blocks into city:
        SetLilBlock(x, y, (int)a);
        SetLilBlock(tx, ty, (int)b);


        // walk to target block:
        while (x != tx || y != ty)
        {

            int dir = Random.Range(0, 4);
            int dis = Random.Range(2, 6);

            // get distances to target
            int dx = tx - x;
            int dy = ty - y;

            // sometimes...
            if (Random.Range(0, 100) < 50)
            { // pick best direction
                if (Mathf.Abs(dx) > Mathf.Abs(dy))
                {
                    dir = (dx > 0) ? 3 : 2;
                }
                else
                {
                    dir = (dy > 0) ? 1 : 0;

                }

            }

            // step into next room
            for (int i = 0; i < dis; i++)
            {

                if (dir == 0) y--;
                if (dir == 1) y++;
                if (dir == 2) x--;
                if (dir == 3) x++;

                x = Mathf.Clamp(x, 0, hires - 1);
                y = Mathf.Clamp(y, 0, hires - 1);

                if (GetLilBlock(x, y) == 0)
                {
                    SetLilBlock(x, y, 1);

                }

            } // ends for


        } // ends while

    } // ends WalkBlocks()

    private void MakeBigBlocks()
    {
        for (int x = 0; x < lilblocks.GetLength(0); x++)
        {
            for (int y = 0; y < lilblocks.GetLength(1); y++)
            {

                int val = GetLilBlock(x, y);

                int xb = x / lilsPerBig;
                int yb = y / lilsPerBig;
                // if bigblock val < lilblock val
                if (GetBigBlock(xb, yb) < val)
                {
                    SetBigBlock(xb, yb, val); // set bigblock = lilclock
                }

            }
        }
    }

    public int[,] GetBlocks()
    {
        if (bigblocks == null)
        {
            Debug.LogError("CityLayout: must call Generate() before calling GetBlocks()");
            return new int[0, 0];
        }

        // make an empty array, same size:
        int[,] copy = new int[bigblocks.GetLength(0), bigblocks.GetLength(1)];

        // copy data to new array:
        System.Array.Copy(bigblocks, 0, copy, 0, bigblocks.Length);

        // return the copy:
        return copy;
    }


    private int GetLilBlock(int x, int y)
    {
        if (lilblocks == null) return 0;
        if (x < 0) return 0;
        if (y < 0) return 0;
        if (x >= lilblocks.GetLength(0)) return 0;
        if (y >= lilblocks.GetLength(1)) return 0;


        return lilblocks[x, y];
    }

    private void SetLilBlock(int x, int y, int val)
    {
        if (lilblocks == null) return;
        if (x < 0) return;
        if (y < 0) return;
        if (x >= lilblocks.GetLength(0)) return;
        if (y >= lilblocks.GetLength(1)) return;


        lilblocks[x, y] = val;
    }

    private int GetBigBlock(int x, int y)
    {
        if (bigblocks == null) return 0;
        if (x < 0) return 0;
        if (y < 0) return 0;
        if (x >= bigblocks.GetLength(0)) return 0;
        if (y >= bigblocks.GetLength(1)) return 0;

        return bigblocks[x, y];
    }

    private void SetBigBlock(int x, int y, int val)
    {
        if (bigblocks == null) return;
        if (x < 0) return;
        if (y < 0) return;
        if (x >= bigblocks.GetLength(0)) return;
        if (y >= bigblocks.GetLength(1)) return;

        bigblocks[x, y] = val;
    }
}
