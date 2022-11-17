using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float restartDelay = 0f;
    bool gameHasEnded = false;

    public void gameOver() 
    {
        if (gameHasEnded == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            gameHasEnded = true;
            Debug.Log("gameOver");

            //restartDelay can be used later if we want an delay between gameover and gameoverscreen
            Invoke("EndScreen", restartDelay);
        }       
    }

    void EndScreen() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
