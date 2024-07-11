using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance == this)
            {
                return;
            }
            Destroy(this.gameObject);
        }
    }

    public Button startButton;
    public Button ReadMeButton;

    private UnityAction action;

    private void Start()
    {
        action = () => OnStartClick();
        startButton.onClick.AddListener(action);

        ReadMeButton.onClick.AddListener(delegate {OnStartClick();});

    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("Main_Logic");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main_UI", LoadSceneMode.Additive);
    }

    public void OnReadMeClick()
    {

    }


}
