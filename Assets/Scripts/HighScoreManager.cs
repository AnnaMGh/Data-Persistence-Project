using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    public TMP_FontAsset font;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneDataPersistence.Instance != null
            && SceneDataPersistence.Instance.ListOfScore != null
            && SceneDataPersistence.Instance.ListOfScore.list != null
            && SceneDataPersistence.Instance.ListOfScore.list.Count > 0)
        {
            for (int i = SceneDataPersistence.Instance.ListOfScore.list.Count - 1; i >= 0; i--)
            {

                SceneDataPersistence.Score score = SceneDataPersistence.Instance.ListOfScore.list[i];
                GameObject obj = new GameObject();
                TextMeshProUGUI tmp = obj.AddComponent<TextMeshProUGUI>();
                tmp.text = "   " + ((SceneDataPersistence.Instance.ListOfScore.list.Count - i)) + ". " + score.name + " | " + score.points + "p";
                tmp.fontSize = 20;
                tmp.verticalAlignment = VerticalAlignmentOptions.Middle;
                tmp.font = font;
                tmp.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);
                Instantiate(obj, this.transform);
            }
        }
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene(0);
    }
}
