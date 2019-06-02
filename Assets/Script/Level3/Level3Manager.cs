using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Manager : MonoBehaviour {
    public GameObject player;
    public GameObject pauseObject;
    public GameObject lastLife;
    public GameObject gameOver;
    public GameObject levelClear;
    public GameObject loading;

    public GameObject keyHole;
    public GameObject keyHoleOpen;
    public GameObject buttonA;
    public GameObject keyWall;
    public GameObject key;
    public GameObject finishDoor;
    public GameObject finishDoorOpen;

    public Animator bigNeedle1;
    public Animator bigNeedle2;
    public Animator Saw1;
    public Animator Saw2;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject resetButton;

    bool nearbutton1 = false;
    bool nearbutton2 = false;
    bool nearbutton3 = false;
    bool nearresetbutton = false;

    public GameObject lamp1Off;
    public GameObject lamp1On;
    public GameObject lamp2Off;
    public GameObject lamp2On;
    public GameObject lamp3Off;
    public GameObject lamp3On;
    public GameObject lamp4Off;
    public GameObject lamp4On;
    public GameObject lamp5Off;
    public GameObject lamp5On;

    bool lamp1_On = false;
    bool lamp2_On = false;
    bool lamp3_On = true;
    bool lamp4_On = false;
    bool lamp5_On = false;

    bool keyholeNear = false;
    bool keyWallOpened = false;
    bool FinishDoorNear = false;
    bool levelFinish = false;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        Saw1.SetInteger("Level", 3);
        Saw2.SetInteger("Level", 3);
        bigNeedle1.SetInteger("Code", 3);
        bigNeedle2.SetInteger("Code", 31);
    }

    // Update is called once per frame
    void Update()
    {
        if (!lastLife.activeSelf)
        {
            gameOver.SetActive(true);
            Destroy(player);
        }
        float toFinishX = Mathf.Abs(finishDoor.transform.position.x - player.transform.position.x);
        float toFinishY = Mathf.Abs(finishDoor.transform.position.y - player.transform.position.y);

        float dxKeyhole = Mathf.Abs(keyHole.transform.position.x - player.transform.position.x);
        float dyKeyhole = Mathf.Abs(player.transform.position.y - keyHole.transform.position.y);

        float button1distx = Mathf.Abs(button1.transform.position.x - player.transform.position.x);
        float button1disty = Mathf.Abs(button1.transform.position.y - player.transform.position.y);

        float button2distx = Mathf.Abs(button2.transform.position.x - player.transform.position.x);
        float button2disty = Mathf.Abs(button2.transform.position.y - player.transform.position.y);

        float button3distx = Mathf.Abs(button3.transform.position.x - player.transform.position.x);
        float button3disty = Mathf.Abs(button3.transform.position.y - player.transform.position.y);

        float resetbuttondistx = Mathf.Abs(resetButton.transform.position.x - player.transform.position.x);
        float resetbuttondisty = Mathf.Abs(resetButton.transform.position.y - player.transform.position.y);
        
        if (dxKeyhole <= 1.5 && dyKeyhole <= 1.8 && !keyWallOpened)
        {
            buttonA.SetActive(true);
            keyholeNear = true;
        }
        else if (toFinishX <= 0.5 && toFinishY <= 1 && toFinishY >= 0 && !levelFinish)
        {
            buttonA.SetActive(true);
            FinishDoorNear = true;
        }
        else if (button1distx<1 && button1disty<0.7)
        {
            buttonA.SetActive(true);
            nearbutton1 = true;
        }
        else if (button2distx<1 && button2disty<0.7)
        {
            buttonA.SetActive(true);
            nearbutton2 = true;
        }
        else if (button3distx<=1 && button3disty<0.7)
        {
            buttonA.SetActive(true);
            nearbutton3 = true;
        }
        else if (resetbuttondistx<1 && resetbuttondisty<0.7)
        {
            buttonA.SetActive(true);
            nearresetbutton = true;
        }
        else
        {
            buttonA.SetActive(false);
            keyholeNear = false;
            FinishDoorNear = false;
            nearbutton1 = false;
            nearbutton2 = false;
            nearbutton3 = false;
            nearresetbutton = false;
        }
    }

    public void buttonAclicked()
    {
        if (keyholeNear && key.activeSelf)
        {
            buttonA.SetActive(false);
            keyWallOpened = true;
            keyWall.GetComponent<KeyWallOpen>().enabled = true;

            Destroy(key);
            keyHole.SetActive(false);
            keyHoleOpen.SetActive(true);
            keyholeNear = false;
        }
        else if (nearbutton1)
        {
            lamp1_On = !lamp1_On;
            lamp3_On = !lamp3_On;
            if (lamp3_On)
            {
                lamp3Off.SetActive(false);
                lamp3On.SetActive(true);
            }
            else if (!lamp3_On)
            {
                lamp3Off.SetActive(true);
                lamp3On.SetActive(false);
            }
            if (lamp1_On)
            {
                lamp1Off.SetActive(false);
                lamp1On.SetActive(true);
            }
            else if (!lamp1_On)
            {
                lamp1Off.SetActive(true);
                lamp1On.SetActive(false);
            }
        }
        else if (nearbutton2)
        {
            lamp4_On = !lamp4_On;
            lamp3_On = !lamp3_On;
            if (lamp3_On)
            {
                lamp3Off.SetActive(false);
                lamp3On.SetActive(true);
            }
            else if (!lamp3_On)
            {
                lamp3Off.SetActive(true);
                lamp3On.SetActive(false);
            }
            if (lamp4_On)
            {
                lamp4Off.SetActive(false);
                lamp4On.SetActive(true);
            }
            else if (!lamp4_On)
            {
                lamp4Off.SetActive(true);
                lamp4On.SetActive(false);
            }
        }
        else if (nearbutton3)
        {
            lamp2_On = !lamp2_On;
            lamp5_On = !lamp5_On;
            if (lamp2_On)
            {
                lamp2Off.SetActive(false);
                lamp2On.SetActive(true);
            }
            else if (!lamp2_On)
            {
                lamp2Off.SetActive(true);
                lamp2On.SetActive(false);
            }
            if (lamp5_On)
            {
                lamp5Off.SetActive(false);
                lamp5On.SetActive(true);
            }
            else if (!lamp5_On)
            {
                lamp5Off.SetActive(true);
                lamp5On.SetActive(false);
            }
        }
        else if (nearresetbutton)
        {
            lamp1_On = false;
            lamp2_On = false;
            lamp3_On = true;
            lamp4_On = false;
            lamp5_On = false;

            lamp1Off.SetActive(true);
            lamp1On.SetActive(false);
            lamp2Off.SetActive(true);
            lamp2On.SetActive(false);
            lamp3Off.SetActive(false);
            lamp3On.SetActive(true);
            lamp4Off.SetActive(true);
            lamp4On.SetActive(false);
            lamp5Off.SetActive(true);
            lamp5On.SetActive(false);
        }
        else if (FinishDoorNear)
        {
            if(lamp1_On  && lamp2_On && lamp3_On && lamp4_On && lamp5_On)
            {
                finishDoor.SetActive(false);
                finishDoorOpen.SetActive(true);
                levelFinish = true;
                StartCoroutine(Wait(1));
                player.GetComponent<PlayerController3>().enabled = false;
                levelClear.SetActive(true);
            }
        }
    }

    IEnumerator Wait(float s)
    {
        yield return new WaitForSeconds(s);
    }

    public void Pause()
    {
        pauseObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void mainMenu()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

    public void restartLevel()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(sceneName: "Level_3");
    }
}
