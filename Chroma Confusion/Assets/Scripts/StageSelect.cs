using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelect : MonoBehaviour
{
    public StageContainer[] stageContainers;
    public int highestStage;

    public Image[] stars;

    // Start is called before the first frame update
    void Start()
    {
        highestStage = PlayerPrefs.GetInt("HighestStage", 1);

        CheckLevelUnlocks();

        for(int i = 0; i < highestStage; i++)
        {
            stars = new Image[PlayerPrefs.GetInt("Stage" + (i + 1) + "Stars")];

            for(int j = 0; j < stars.Length; j++)
            {
                stars[j] = stageContainers[i].transform.GetChild(1).GetChild(j).GetComponent<Image>();
            }

            StartCoroutine(AwardStars(stars));
        }

        AudioManager.GetSound("StarSound").source.pitch = 1.75f;
    }

    [ContextMenu("Delete Progress")]
    public void DeletaData()
    {
        PlayerPrefs.DeleteAll();
    }

    public IEnumerator AwardStars(Image[] _stars)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < _stars.Length; i++)
        {
            yield return new WaitForSeconds(0.025f);

            for (int n = 0; n < 20; n++)
            {
                _stars[i].fillAmount += 0.05f;
                yield return new WaitForSeconds(0.0125f);
            }

            _stars[i].transform.localScale += Vector3.one * 0.325f;
            AudioManager.GetSound("StarSound").source.pitch += 0.25f;
            AudioManager.PlaySound("StarSound", true);
        }
    }

    public void CheckLevelUnlocks()
    {
        for(int i = 0; i < stageContainers.Length; i++)
        {
            if(i + 1 > highestStage)
            {
                stageContainers[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = false;
                stageContainers[i].GetComponent<MenuInteraction>().enabled = false;
                stageContainers[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Locked";
                stageContainers[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Locked";
            }

            else
            {
                stageContainers[i].GetComponent<MenuInteraction>().enabled = true;
                stageContainers[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Stage " + (i + 1);
                
                if (PlayerPrefs.HasKey("Stage" + stageContainers[i].stage + "Stars"))
                {
                    stageContainers[i].bestTimeText.text = $"Best Time\n{PlayerPrefs.GetInt("Stage" + stageContainers[i].stage + "H"):00}H : " +
                        $"{PlayerPrefs.GetInt("Stage" + stageContainers[i].stage + "M"):00}M : " +
                        $"{PlayerPrefs.GetInt("Stage" + stageContainers[i].stage + "S"):00}S";
                }

                else
                {
                    stageContainers[i].bestTimeText.text = "No Record";
                }
            }
        }
    }

    public void SetStageName(string _inputStage)
    {
        PlayerPrefs.SetString("CurrentStage", _inputStage);
        LoadStage(_inputStage);
    }

    void LoadStage(string _stageToLoad)
    {
        SceneManager.LoadScene(_stageToLoad);
    }
}
