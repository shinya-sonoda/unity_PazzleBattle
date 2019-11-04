using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleIconGenerator : MonoBehaviour
{
    static int maxWidth = 5;
    static int maxHeight = 5;
    public GameObject puzzle_icon_1_spadePrefab;
    public GameObject puzzle_icon_2_heartPrefab;
    public GameObject puzzle_icon_3_clubPrefab;
    public GameObject puzzle_icon_4_diamondPrefab;

    GameObject PuzzleController;
    GameObject GameDirector;

    float delta = 0;
    float span = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        this.PuzzleController = GameObject.Find("PuzzleController");
        this.GameDirector = GameObject.Find("GameDirector");

    }

    // Update is called once per frame
    void Update()
    {
        PuzzleController puzzleController = this.PuzzleController.GetComponent<PuzzleController>();
        if (this.GameDirector.GetComponent<GameDirector>().gameStatus == 2)
        {
            this.delta += Time.deltaTime;
        }
        if (this.delta > this.span)
        {
            for (int i = 0; i < maxWidth; i++)
            {
                for (int n = 0; n < maxHeight; n++)
                {
                    for (int j = 0; j < maxHeight - 1; j++)
                    {
                        if (puzzleController.icons[i, j] == null)
                        {
                            for (int k = j; k < maxHeight - 1; k++)
                            {
                                puzzleController.icons[i, k] = puzzleController.icons[i, k + 1];
                                if (puzzleController.icons[i, k] != null)
                                {
                                    puzzleController.icons[i, k].GetComponent<PuzzleIconController>().posx = i;
                                    puzzleController.icons[i, k].GetComponent<PuzzleIconController>().posy = k;
                                    puzzleController.icons[i, k].GetComponent<PuzzleIconController>().type = puzzleController.icons[i, k + 1].GetComponent<PuzzleIconController>().type;
                                    puzzleController.icons[i, k].GetComponent<PuzzleIconController>().SetVector();
                                }
                                puzzleController.icons[i, k + 1] = null;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < maxWidth; i++)
            {
                for (int j = 0; j < maxHeight; j++)
                {
                    if (puzzleController.icons[i, j] == null)
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

                        puzzleController.icons[i, j] = icon;
                    }
                }
            }
            this.GameDirector.GetComponent<GameDirector>().gameStatus = 0;
            this.delta = 0;
        }
    }
}
