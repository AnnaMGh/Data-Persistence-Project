using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI txtBonus;
    public TMP_InputField txtName;

    // Start is called before the first frame update
    void Start()
    {
        SetBestBonus();
        SetCurrentPlayerName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBestBonus()
    {
        if (SceneDataPersistence.Instance.BestScore != null)
        {
            txtBonus.text = "BEST SCORE: "
            + SceneDataPersistence.Instance.BestScore.name
            + ":" + SceneDataPersistence.Instance.BestScore.points;
        }
    }

    public void SetCurrentPlayerName() {
        if (SceneDataPersistence.Instance.CurrentPlayerName != null) {
            txtName.text = SceneDataPersistence.Instance.CurrentPlayerName;
        }
    }

    #region Events

    public void StartGame() {
        if (txtName.text != null && txtName.text.Length > 0)
        {
            SceneDataPersistence.Instance.SetPlayerName(txtName.text);
        }

        SceneManager.LoadScene(1);
    }

    public void HighScore()
    {
        SceneManager.LoadScene(2);
    }

    public void Setings()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    #endregion
}
