using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleIconController : MonoBehaviour
{
    public int posx = 0;
    public int posy = 0;
    float centerx = 0;
    float centery = -2.08f;
    float realposx;
    float realposy;

    GameObject PuzzleController;
    GameObject GameDirector;
    GameObject clickedGameObject;

    bool tapFlag = false;

    public int type = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.PuzzleController = GameObject.Find("PuzzleController");
        this.GameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.realposy > transform.position.y)
        {
            transform.Translate(0, (0.96f / 3), 0);
            if (this.realposy <= transform.position.y)
            {
                transform.position = new Vector2(this.realposx, this.realposy);
                tapFlag = true;
            }
        }
        // 壊してはいけない時
        if (this.GameDirector.GetComponent<GameDirector>().gameStatus == 2)
        {

        }
        // 壊してもいい時
        else if (this.GameDirector.GetComponent<GameDirector>().gameStatus == 0)
        {
            // タップされたら
            if (Input.GetMouseButtonDown(0))
            {
                this.GameDirector.GetComponent<GameDirector>().gameStatus = 1;
                clickedGameObject = null;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

                if (hit2d)
                {
                    clickedGameObject = hit2d.transform.gameObject;

                    if (clickedGameObject.tag == "Icon")
                    {
                        tapFlag = false;
                        // Debug.Log("tap!" + clickedGameObject.GetComponent<PuzzleIconController>().posx + ", " + clickedGameObject.GetComponent<PuzzleIconController>().posy);
                        // つながっているオブジェクトをエフェクトを出して破棄
                        this.PuzzleController.GetComponent<PuzzleController>().CheckIcon(clickedGameObject.GetComponent<PuzzleIconController>().posx, clickedGameObject.GetComponent<PuzzleIconController>().posy);

                    }
                    else
                    {
                        this.GameDirector.GetComponent<GameDirector>().gameStatus = 0;
                    }
                }
                else
                {
                    this.GameDirector.GetComponent<GameDirector>().gameStatus = 0;
                }
            }
        }
    }
    public void SetVector()
    {
        this.realposx = -(this.posx - 2) * 0.96f + this.centerx;
        this.realposy = -(this.posy - 2) * 0.96f + this.centery;
        float newPosy = -(this.posy + 1 - 2) * 0.96f + this.centery;
        // transform.position = new Vector2(this.realposx, this.realposy);
        // if(transform.position.y > newPosy){
        // transform.position = new Vector2(this.realposx, transform.position.y);
        // }
        // else {
        transform.position = new Vector2(this.realposx, newPosy);
        // }
    }

    public void DestroyIcon()
    {
        Destroy(gameObject);
    }
    public int GetType()
    {
        return this.type;
    }

}
