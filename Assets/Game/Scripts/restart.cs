using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restart : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yourButton;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		RestartGame();
	}
     public void RestartGame() {
             SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); // loads current scene
             Time.timeScale = 1f;
         }
}
