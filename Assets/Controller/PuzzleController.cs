using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    static int maxWidth = 5;
    static int maxHeight = 5;
    public GameObject[,] icons = new GameObject[maxWidth, maxHeight];
    public int[,] status = new int[maxWidth, maxHeight];
    public GameObject puzzle_icon_1_spadePrefab;
    public GameObject puzzle_icon_2_heartPrefab;
    public GameObject puzzle_icon_3_clubPrefab;
    public GameObject puzzle_icon_4_diamondPrefab;
    GameObject GameDirector;

    // Start is called before the first frame update
    void Start()
    {
        this.GameDirector = GameObject.Find("GameDirector");
        for (int i = 0; i < maxWidth; i++)
        {
            for (int j = 0; j < maxHeight; j++)
            {
                GameObject icon;
                int dice = Random.Range(1, 5);
                if (dice == 1)
                {
                    icon = Instantiate(puzzle_icon_1_spadePrefab) as GameObject;
                    icon.GetComponent<PuzzleIconController>().type = 1;
                }
                else if (dice == 2)
                {
                    icon = Instantiate(puzzle_icon_2_heartPrefab) as GameObject;
                    icon.GetComponent<PuzzleIconController>().type = 2;
                }
                else if (dice == 3)
                {
                    icon = Instantiate(puzzle_icon_3_clubPrefab) as GameObject;
                    icon.GetComponent<PuzzleIconController>().type = 3;
                }
                else
                {
                    icon = Instantiate(puzzle_icon_4_diamondPrefab) as GameObject;
                    icon.GetComponent<PuzzleIconController>().type = 4;
                }
                icon.GetComponent<PuzzleIconController>().posx = i;
                icon.GetComponent<PuzzleIconController>().posy = j;
                icon.GetComponent<PuzzleIconController>().SetVector();

                icons[i, j] = icon;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckIcon(int x, int y)
    {
        for (int i = 0; i < maxWidth; i++)
        {
            for (int j = 0; j < maxHeight; j++)
            {
                this.status[i, j] = this.icons[i, j].GetComponent<PuzzleIconController>().GetType();
            }
        }
        int count = 0;
        CheckIconloop(x, y, ref count);
        if (count > 0)
        {
            this.GameDirector.GetComponent<GameDirector>().giveDamage(this.icons[x, y].GetComponent<PuzzleIconController>().GetType(), count * count);
            DeleteIconloop(x, y);
        }
        this.GameDirector.GetComponent<GameDirector>().gameStatus = 2;
    }
    void CheckIconloop(int x, int y, ref int count)
    {
        // Debug.Log("CheckIconloop:" + x + "," + y);
        int type = this.status[x, y];
        this.status[x, y] = -1;
        count++;

        if (x + 1 < maxWidth && this.status[(x + 1), y] == type) CheckIconloop(x + 1, y, ref count);
        if (y + 1 < maxHeight && this.status[x, (y + 1)] == type) CheckIconloop(x, y + 1, ref count);
        if (x - 1 >= 0 && this.status[(x - 1), y] == type) CheckIconloop(x - 1, y, ref count);
        if (y - 1 >= 0 && this.status[x, (y - 1)] == type) CheckIconloop(x, y - 1, ref count);
    }
    void DeleteIconloop(int x, int y)
    {
        int type = this.status[x, y];
        this.status[x, y] = -2;

        if (x + 1 < maxWidth && this.status[(x + 1), y] == type) DeleteIconloop(x + 1, y);
        if (y + 1 < maxHeight && this.status[x, (y + 1)] == type) DeleteIconloop(x, y + 1);
        if (x - 1 >= 0 && this.status[(x - 1), y] == type) DeleteIconloop(x - 1, y);
        if (y - 1 >= 0 && this.status[x, (y - 1)] == type) DeleteIconloop(x, y - 1);

        this.icons[x, y].GetComponent<PuzzleIconController>().DestroyIcon();
        this.icons[x, y] = null;
    }

}
