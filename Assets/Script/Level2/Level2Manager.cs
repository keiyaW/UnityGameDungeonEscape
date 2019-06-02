using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2Manager : MonoBehaviour {

    public GameObject player;
    public GameObject pauseObject;
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
    public GameObject wrongDoor1;
    public GameObject wrongDoor2;
    public GameObject wrongDoor4;
    
    public GameObject[] life;
    public GameObject lastLife;

    public Animator playeranim;

    bool keyholeNear = false;
    bool keyWallOpened = false;
    bool FinishDoorNear = false;
    bool levelFinish = false;
    bool wrongDoorPick = false;
    bool blink = false;

    public GameObject Door1Clue;
    public GameObject Door2Clue;
    public GameObject Door3Clue;
    public GameObject Door4Clue;

    public string Level3;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        float toFinishX = Mathf.Abs(finishDoor.transform.position.x - player.transform.position.x);
        float toFinishY = finishDoor.transform.position.y - player.transform.position.y;

        float toWrong1X = Mathf.Abs(wrongDoor1.transform.position.x - player.transform.position.x);
        float toWrong1Y = wrongDoor1.transform.position.y - player.transform.position.y;

        float toWrong2X = Mathf.Abs(wrongDoor2.transform.position.x - player.transform.position.x);
        float toWrong2Y = wrongDoor2.transform.position.y - player.transform.position.y;

        float toWrong4X = Mathf.Abs(wrongDoor4.transform.position.x - player.transform.position.x);
        float toWrong4Y = wrongDoor4.transform.position.y - player.transform.position.y;

        float dxKeyhole = Mathf.Abs(keyHole.transform.position.x - player.transform.position.x);
        float dyKeyhole = player.transform.position.y - keyHole.transform.position.y;

        if (!lastLife.activeSelf)
        {
            gameOver.SetActive(true);
            Destroy(player);
        }
        if (dxKeyhole <= 1.5 && dyKeyhole <= 1.8 && !keyWallOpened)
        {
            buttonA.SetActive(true);
            keyholeNear = true;
        }
        else if (toFinishX <= 0.5 && toFinishY <= 1 && toFinishY >= 0 && !levelFinish && !blink)
        {
            buttonA.SetActive(true);
            FinishDoorNear = true;
            Door3Clue.SetActive(true);
        }
        else if (toWrong1X <= 0.5 && toWrong1Y <= 1 && toWrong1Y >= 0 && !blink)
        {
            buttonA.SetActive(true);
            wrongDoorPick = true;
            Door1Clue.SetActive(true);
        }
        else if (toWrong2X <= 0.5 && toWrong2Y <= 1 && toWrong2Y >= 0 && !blink)
        {
            buttonA.SetActive(true);
            wrongDoorPick = true;
            Door2Clue.SetActive(true);
        }
        else if (toWrong4X <= 0.5 && toWrong4Y <= 1 && toWrong4Y >= 0 && !blink)
        {
            buttonA.SetActive(true);
            wrongDoorPick = true;
            Door4Clue.SetActive(true);
        }
        else
        {
            buttonA.SetActive(false);
            keyholeNear = false;
            FinishDoorNear = false;
            wrongDoorPick = false;
            Door1Clue.SetActive(false);
            Door2Clue.SetActive(false);
            Door3Clue.SetActive(false);
            Door4Clue.SetActive(false);
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
        else if (wrongDoorPick)
        {
            blink = true;
            if (life[2].activeSelf)
            {
                life[2].SetActive(false);
            }
            else if (life[1].activeSelf)
            {
                life[1].SetActive(false);
            }
            else if (life[0].activeSelf)
            {
                life[0].SetActive(false);
            }
            StartCoroutine(WrongDoor());
        }
        else if (FinishDoorNear)
        {
            finishDoor.SetActive(false);
            finishDoorOpen.SetActive(true);
            levelFinish = true;
            player.GetComponent<PlayerController2>().enabled = false;
            levelClear.SetActive(true);
        }
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
    
    public IEnumerator WrongDoor()
    {
        playeranim.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(3);

        playeranim.SetLayerWeight(1, 0);
        blink = false;
    }
    public void nextLevel()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(Level3);
    }

    public void mainMenu()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

    public void restartLevel()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(sceneName: "Level_2");
    }
}
