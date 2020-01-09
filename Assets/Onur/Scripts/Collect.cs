using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{

    public GameObject human;
    public GameObject PlusOneText;
    public GameObject goal_text;
    bool isCollected = false;
    bool isLeanDone = false;

    // Start is called before the first frame update
    void Start()
    {
        goal_text = GameObject.Find("goal_text");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x > GameObject.FindGameObjectWithTag("finish").transform.position.x + 0.4f)
        {
            Destroy(this.gameObject);
            LevelManager.human_count--;
            FinishScript.isPassedFinishHole = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectable_human")
        {
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            GameObject plusOne = Instantiate(PlusOneText, new Vector3(this.gameObject.transform.position.x - 10, this.gameObject.transform.position.y + 10, this.gameObject.transform.position.z + 5), new Quaternion(0, 0, 0, 0)) as GameObject;
            StartCoroutine(set_fadeOut(plusOne));
            LeanTween.move(plusOne, new Vector3(plusOne.transform.position.x, plusOne.transform.position.y + 5, plusOne.transform.position.z), 0.7f).setOnComplete(() =>
            {

                Destroy(plusOne);

            });
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
            Destroy(other.gameObject);
            GameObject newHuman = Instantiate(human, other.gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            newHuman.transform.Rotate(0, 90, 0);
            if (!isLeanDone)
            {
                isLeanDone = true;
                LeanTween.scale(goal_text, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setLoopPingPong(1).setOnComplete(() =>
                  {
                      isLeanDone = false;
                      LeanTween.scale(goal_text, Vector3.one, 0.1f);
                  });
            }

            LevelManager.human_count++;
            LeanTween.move(newHuman, new Vector3(this.gameObject.transform.position.x - Random.Range(2, 4), this.gameObject.transform.position.y + Random.Range(2, 4), this.gameObject.transform.position.z), 0.2f).setOnComplete(() =>
            {


            });
            newHuman.transform.SetParent(GameObject.FindGameObjectWithTag("human_parent").transform);

        }
    }

    IEnumerator set_fadeOut(GameObject plus1)
    {


        if (plus1 != null)
        {
            for (float a = 1f; a >= 0f; a -= 0.05f)
            {
                if (plus1 != null)
                {
                    Color c = plus1.GetComponent<TextMesh>().color;
                    c.a = a;
                    plus1.GetComponent<TextMesh>().color = c;
                    yield return new WaitForSeconds(0.005f);
                }
            }

        }


    }
}
