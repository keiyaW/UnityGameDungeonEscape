using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour {

    public GameObject player;
    public GameObject pauseObject;
    public GameObject gameOver;
    public GameObject lastLife;
    public GameObject levelClear;
    public GameObject loading;
    public GameObject[] life;

    public GameObject keyHole;
    public GameObject keyHoleOpen;
    public GameObject buttonA;
    public GameObject keyWall;
    public GameObject key;
    public GameObject puzzle;
    public GameObject puzzleDisplay;
    public InputField puzzleAnswer;
    public GameObject puzzleWall;
    public GameObject finishDoor;
    public GameObject finishDoorOpen;

    bool keyholeNear = false;
    bool keyWallOpened = false;
    bool puzzleSolved = false;
    bool puzzleNear = false;
    bool FinishDoorNear = false;
    bool levelFinish = false;
    bool blink;

    public Animator playeranim;

    public string Level2;
    public string Menu;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        float toFinishX = Mathf.Abs(finishDoor.transform.position.x - player.transform.position.x);
        float toFinishY = finishDoor.transform.position.y - player.transform.position.y;

        float dxKeyhole = Mathf.Abs(keyHole.transform.position.x - player.transform.position.x);
        float dyKeyhole = player.transform.position.y - keyHole.transform.position.y;

        float dxPuzzle = Mathf.Abs(puzzle.transform.position.x - player.transform.position.x);
        float dyPuzzle = player.transform.position.y - puzzle.transform.position.y;

        if(!lastLife.activeSelf)
        {
            gameOver.SetActive(true);
            Destroy(player);
        }
        if (dxKeyhole <= 1.5 && dyKeyhole <= 1.8 && !keyWallOpened)
        {
            buttonA.SetActive(true);
            keyholeNear = true;
        }
        else if (dxPuzzle <= 1.5 && dyPuzzle <= 1.8 && !puzzleSolved && !blink)
        {
            buttonA.SetActive(true);
            puzzleNear = true;
        }
        else if (toFinishX <= 0.5 && toFinishY <= 1 && toFinishY >= 0 && !levelFinish)
        {
            buttonA.SetActive(true);
            FinishDoorNear = true;
        }
        else
        {
            buttonA.SetActive(false);
            keyholeNear = false;
            puzzleNear = false;
            FinishDoorNear = false;
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
        else if (puzzleNear)
        {
            puzzleDisplay.SetActive(true);
            puzzleAnswer.text = "";
        }
        else if (FinishDoorNear)
        {
            finishDoor.SetActive(false);
            finishDoorOpen.SetActive(true);
            levelFinish = true;
            player.GetComponent<PlayerController>().enabled = false;
            levelClear.SetActive(true);
        }
    }

    public void back()
    {
        puzzleDisplay.SetActive(false);
    }

    public void SubmitPuzzle()
    {
        puzzleAnswer.text = puzzleAnswer.text.Trim();
        if (puzzleAnswer.text == "45")
        {
            puzzleSolved = true;
            puzzleWall.GetComponent<PuzzleWallOpen>().enabled = true;
            Destroy(puzzleDisplay);
        }
        else
        {
            puzzleAnswer.text = "";
            puzzleDisplay.SetActive(false);
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
            StartCoroutine(WrongAnswer());
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

    public void nextLevel()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(Level2);
    }

    public void mainMenu()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(Menu);
    }

    public void restartLevel()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(sceneName: "Level_1");
    }
    public IEnumerator WrongAnswer()
    {
        playeranim.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(3);

        playeranim.SetLayerWeight(1, 0);
        blink = false;
    }
}
