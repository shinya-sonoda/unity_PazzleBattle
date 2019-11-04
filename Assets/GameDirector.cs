using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public int gameStatus = 0;
    float delta = 0;
    GameObject PuzzleController;

    public float myLifeMax = 100f;
    public float myLife = 100f;

    public float blueLifeMax = 50f;
    public float blueLife = 50f;
    public float redLifeMax = 50f;
    public float redLife = 50f;
    public float greenLifeMax = 50f;
    public float greenLife = 50f;
    public float yellowLifeMax = 50f;
    public float yellowLife = 50f;
    GameObject myHpGauge;
    GameObject hpGaugeBlue;
    GameObject hpGaugeRed;
    GameObject hpGaugeGreen;
    GameObject hpGaugeYellow;
    GameObject blueEnemy;
    GameObject redEnemy;
    GameObject greenEnemy;
    GameObject yellowEnemy;
    GameObject TextGameover1;
    GameObject TextGameover2;
    GameObject obi1;
    GameObject obi2;

    public AudioClip attackSE;
    public AudioClip damageSE;
    public AudioClip deleteSE;
    public AudioClip clearSE;
    AudioSource aud;

    bool deleteFlag1 = false;
    bool deleteFlag2 = false;
    bool deleteFlag3 = false;
    bool deleteFlag4 = false;

    // Start is called before the first frame update
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        this.PuzzleController = GameObject.Find("PuzzleController");
        this.myHpGauge = GameObject.Find("myHpGauge");
        this.hpGaugeBlue = GameObject.Find("hpGaugeBlue");
        this.hpGaugeRed = GameObject.Find("hpGaugeRed");
        this.hpGaugeGreen = GameObject.Find("hpGaugeGreen");
        this.hpGaugeYellow = GameObject.Find("hpGaugeYellow");
        this.blueEnemy = GameObject.Find("blueEnemy");
        this.redEnemy = GameObject.Find("redEnemy");
        this.greenEnemy = GameObject.Find("greenEnemy");
        this.yellowEnemy = GameObject.Find("yellowEnemy");
        this.TextGameover1 = GameObject.Find("TextGameover1");
        this.TextGameover2 = GameObject.Find("TextGameover2");
        this.TextGameover1.GetComponent<Text>().text = "";
        this.TextGameover2.GetComponent<Text>().text = "";
        this.obi1 = GameObject.Find("obi1");
        this.obi2 = GameObject.Find("obi2");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.myLife <= 0)
        {
            this.gameStatus = 2;
            if (this.delta <= 1.0f)
            {
                this.delta += Time.deltaTime;
            }
            Debug.Log("ゲームオーバー");
            this.obi1.transform.position = new Vector2(0, -0.5f);
            this.TextGameover1.GetComponent<Text>().text = "Gameover";
            this.TextGameover2.GetComponent<Text>().text = "⇒tap to restart";
            if (Input.GetMouseButtonUp(0) && this.delta > 1.0f)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else if (this.blueLife <= 0 && this.redLife <= 0 && this.greenLife <= 0 && this.yellowLife <= 0)
        {
            this.gameStatus = 2;
            if (this.delta <= 1.0f)
            {
                this.delta += Time.deltaTime;
            }
            Debug.Log("クリアー");
            this.obi2.transform.position = new Vector2(0, 0);
            this.aud.PlayOneShot(this.clearSE);
            this.TextGameover1.GetComponent<Text>().text = "Clear!!";
            if (Input.GetMouseButtonUp(0) && this.delta > 1.0f)
            {
                SceneManager.LoadScene("ClearScene");
            }
        }

    }

    public void giveDamage(int type, int damage)
    {
        switch (type)
        {
            case 1:
                if (this.blueLife > 0 && damage > 4)
                {
                    this.aud.PlayOneShot(this.attackSE);
                    this.blueLife -= damage;
                }
                else
                {
                    this.aud.PlayOneShot(this.damageSE);
                    this.myLife -= damage;
                }
                break;
            case 2:
                if (this.redLife > 0 && damage > 4)
                {
                    this.aud.PlayOneShot(this.attackSE);
                    this.redLife -= damage;
                }
                else
                {
                    this.aud.PlayOneShot(this.damageSE);
                    this.myLife -= damage;
                }
                break;
            case 3:
                if (this.greenLife > 0 && damage > 4)
                {
                    this.aud.PlayOneShot(this.attackSE);
                    this.greenLife -= damage;
                }
                else
                {
                    this.aud.PlayOneShot(this.damageSE);
                    this.myLife -= damage;
                }
                break;
            case 4:
                if (this.yellowLife > 0 && damage > 4)
                {
                    this.aud.PlayOneShot(this.attackSE);
                    this.yellowLife -= damage;
                }
                else
                {
                    this.aud.PlayOneShot(this.damageSE);
                    this.myLife -= damage;
                }
                break;
            default:
                this.aud.PlayOneShot(this.damageSE);
                this.myLife -= damage;
                break;
        }
        this.myHpGauge.GetComponent<Image>().fillAmount = this.myLife / this.myLifeMax;
        this.hpGaugeBlue.GetComponent<Image>().fillAmount = this.blueLife / this.blueLifeMax;
        this.hpGaugeRed.GetComponent<Image>().fillAmount = this.redLife / this.redLifeMax;
        this.hpGaugeGreen.GetComponent<Image>().fillAmount = this.greenLife / this.greenLifeMax;
        this.hpGaugeYellow.GetComponent<Image>().fillAmount = this.yellowLife / this.yellowLifeMax;
        Debug.Log("myLife:" + this.myLife + ", blueLife:" + this.blueLife + ", redLife:" + this.redLife + ", greenLife:" + this.greenLife + ", yellowLife:" + this.yellowLife);
        Debug.Log("this.hpGaugeBlue.transform.position:" + this.blueEnemy.transform.position);
        if (this.blueLife <= 0 && !this.deleteFlag1)
        {
            this.aud.PlayOneShot(this.deleteSE);
            this.blueEnemy.transform.position = new Vector2(20, this.blueEnemy.transform.position.y);
            this.deleteFlag1 = true;
        }
        if (this.redLife <= 0 && !this.deleteFlag2)
        {
            this.aud.PlayOneShot(this.deleteSE);
            this.redEnemy.transform.position = new Vector2(20, this.redEnemy.transform.position.y);
            this.deleteFlag2 = true;
        }
        if (this.greenLife <= 0 && !this.deleteFlag3)
        {
            this.aud.PlayOneShot(this.deleteSE);
            this.greenEnemy.transform.position = new Vector2(20, this.greenEnemy.transform.position.y);
            this.deleteFlag3 = true;
        }
        if (this.yellowLife <= 0 && !this.deleteFlag4)
        {
            this.aud.PlayOneShot(this.deleteSE);
            this.yellowEnemy.transform.position = new Vector2(20, this.yellowEnemy.transform.position.y);
            this.deleteFlag4 = true;
        }
    }
}
