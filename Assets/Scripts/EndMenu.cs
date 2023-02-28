using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public GameObject QuitMenu;

    CanvasGroup endMenuGroup;

    // Start is called before the first frame update
    void Start()
    {
        EndMenuButton();
        endMenuGroup = GameObject.Find("EndCanvas").GetComponent<CanvasGroup>();

    }


    public void EndMenuButton()
    {
        // Show Main Menu
        QuitMenu.SetActive(true);
        Debug.Log("coucou");
    }
    public void RestartButton()
    {
        // Show Main Menu
        Debug.Log("coucou2");
        endMenuGroup.alpha = 0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}
