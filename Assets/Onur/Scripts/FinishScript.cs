using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour
{
    public static bool isPassedFinishHole = false;
    public Image Sprite;
    public GameObject ProgressBar;
    public TextMeshProUGUI LevelText;
    public GameObject human_score;
    bool isLevelTextFadeOut = false;
    public static int temp = 0;
    // Start is called before the first frame update
    void Start()
    {
        human_score = GameObject.Find("Humanoid_score");
        human_score.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("human_score").ToString();
        temp = PlayerPrefs.GetInt("human_score");
    }

    // Update is called once per frame
    void Update()
    {
        
        human_score = GameObject.Find("Humanoid_score");
        human_score.transform.GetChild(1).GetComponent<Text>().text = temp.ToString();
        LevelText = GameObject.FindGameObjectWithTag("level_text").GetComponent<TextMeshProUGUI>();
        ProgressBar = GameObject.FindGameObjectWithTag("progress_panel");
        if (!isPassedFinishHole)
        {

            isLevelTextFadeOut = false;
            human_score.transform.GetChild(0).GetComponent<Image>().enabled = false;
            human_score.transform.GetChild(1).GetComponent<Text>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "flying_human")
        {
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            isPassedFinishHole = true;
            if (!isLevelTextFadeOut)
            {
                //StartCoroutine(set_fadeOut());
                human_score.transform.GetChild(0).GetComponent<Image>().enabled = true;
                human_score.transform.GetChild(1).GetComponent<Text>().enabled = true;
                isLevelTextFadeOut = true;
            }
            Image sprite = Instantiate(Sprite, Vector3.zero, new Quaternion(0, 0, 0, 0)) as Image;
            sprite.transform.SetParent(ProgressBar.transform);
            sprite.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            float y = Random.Range(-50, 50);
            sprite.GetComponent<RectTransform>().localPosition = new Vector3(0, y, 0);
            Destroy(other.gameObject);
            LeanTween.move(sprite.rectTransform, new Vector3(151.48f, 354, 0), 0.3f).setOnComplete(() =>
            {
                LeanTween.scale(human_score.GetComponent<RectTransform>(), new Vector3(1.2f, 1.2f, 1.2f), 0.05f).setOnComplete(()=> {

                    LeanTween.scale(human_score.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.05f);

                });
                Destroy(sprite.gameObject);
                iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
                LevelManager.human_count--;
                LevelManager.human_score_count++;
                temp++;
                
                

            });



        }
    }

    IEnumerator set_fadeOut()
    {
        for (float a = 1f; a >= 0f; a -= 0.2f)
        {
            Color c = LevelText.GetComponent<TextMeshProUGUI>().faceColor;
            Color b = LevelText.GetComponent<TextMeshProUGUI>().outlineColor;
            c.a = a;
            b.a = a;
            LevelText.GetComponent<TextMeshProUGUI>().faceColor = c;
            LevelText.GetComponent<TextMeshProUGUI>().outlineColor = b;
            yield return new WaitForSeconds(0.05f);
        }
        LevelText.GetComponent<TextMeshProUGUI>().faceColor = new Color(255, 255, 255, 0);
        LevelText.GetComponent<TextMeshProUGUI>().outlineColor = new Color(0, 0, 0, 0);



    }
}
