using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public string Level1;
    public GameObject loading;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame()
    {
        // digunakan untuk berpindah ke scene selanjutnya.
        loading.SetActive(true);
        SceneManager.LoadScene(Level1);
    }

    public void Exits()
    {
        // digunakan untuk keluar dari permainan
        Application.Quit();
    }
}
