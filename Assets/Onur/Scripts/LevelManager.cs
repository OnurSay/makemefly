using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
	public GameObject Humanoid;
    public GameObject human_parent;
	public Image TouchImage;
    public GameObject cam;
    public GameObject target;
    public GameManager gameManagerScr;
	public GameObject Confetti;
	public static int human_count = 1;
	public GameObject MainMenu;
	public GameObject SuccessPanel;
	public GameObject FailPanel;
	public GameObject LevelProggressPanel;
	public GameObject[] Levels;
    public GameObject FeverBar;
    public GameObject FeverText;
    public GameObject Clouds;
    bool isPanelActive = false;
    public static bool FeverActive = false;
	GameObject current_level;
    Color fillColor = new Color(0, 255, 197);
    public RuntimeAnimatorController animatorController;
    public static int human_score_count;
	public int index_counter = 0;
	int current_level_number = 1;
	bool isLevelFinished = false;
	public static int pixel_length = 0;
	public bool is_level_started = false;
    public GameObject goal_text;
    public int[] level_goals;
    public int level_goal;
    bool boost1 = false;
    bool boost2 = false;
   public static bool boost0 = false;
    bool IsScale = false;
    GameObject[] humans;
    bool isSetToProgBarInt = false;

    public GameObject Plane_Blue;
    public GameObject Plane_Green;
    public GameObject Plane_Purple;
    public GameObject Plane_Pink;

    // Start is called before the first frame update
    void Start()
	{
        //PlayerPrefs.SetInt("current_level", 49);
        //PlayerPrefs.SetInt("index_counter", 18);
        //PlayerPrefs.SetInt("human_score", 0);

        if (PlayerPrefs.GetInt("current_level") == 0)
		{
			PlayerPrefs.SetInt("current_level", 1);
		}
        MainMenu.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("human_score").ToString();
		LevelProggressPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Level "+ PlayerPrefs.GetInt("current_level").ToString();
        current_level_number = PlayerPrefs.GetInt("current_level");
		//index_counter = PlayerPrefs.GetInt("index_counter");
        if (current_level_number > 20)
        {
            index_counter = (current_level_number % 20) - 1;
        }
        else
        {
            index_counter = current_level_number - 1;
        }
        GameObject level = Instantiate(Levels[index_counter], Vector3.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
        level_goal = level_goals[index_counter];
        
        goal_text.GetComponent<TextMeshProUGUI>().text = "0/" + level_goal;
        GameObject.Find("goalTextfirst").GetComponent<TextMesh>().text = "Save 0/" + level_goal + "!";
        current_level = level;
        human_score_count = 0;
        //MainMenu.transform.GetChild(5).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("humanoid_score").ToString();
       
        Invoke("Set_level_started", 0.05f);


		/*if (GameObject.Find("icon"))
        {
            isLevelHaveIcon = true;
        }
        else
        {
            isLevelHaveIcon = false;
        }

        if (GameObject.FindGameObjectWithTag("shape_particule"))
        {
            isLevelHaveCube = true;
        }
        else
        {
            isLevelHaveCube = false;
        }*/
	}

	// Update is called once per frame
	void Update()
	{
        
        if ((human_count) < 0)
        {
            goal_text.GetComponent<TextMeshProUGUI>().text = 0 + "/" + level_goal;
        }
        else
        {
            goal_text.GetComponent<TextMeshProUGUI>().text =  (human_count  + "/" + level_goal );
        }

        //Plane Color Changing - START
        if (current_level_number % 20 >= 1 && current_level_number % 20 <= 5)
        {
            Plane_Blue.SetActive(true);
            Plane_Pink.SetActive(false);
            Plane_Purple.SetActive(false);
            Plane_Green.SetActive(false);
        }
        else if (current_level_number % 20 >= 6 && current_level_number % 20 <= 10)
        {
            Plane_Blue.SetActive(false);
            Plane_Pink.SetActive(true);
            Plane_Purple.SetActive(false);
            Plane_Green.SetActive(false);
        }
        else if (current_level_number % 20 >= 11 && current_level_number % 20 <= 15)
        {
            Plane_Blue.SetActive(false);
            Plane_Pink.SetActive(false);
            Plane_Purple.SetActive(true);
            Plane_Green.SetActive(false);
        }
        else if (current_level_number % 20 >= 16)
        {
            Plane_Blue.SetActive(false);
            Plane_Pink.SetActive(false);
            Plane_Purple.SetActive(false);
            Plane_Green.SetActive(true);
        }
        //Plane Color Changing - END

        if (human_count == 0)
		{
            if (!FinishScript.isPassedFinishHole)
            {
                TouchImage.raycastTarget = false;
                MoveForward.forwardSpeed = 0;
                Invoke("OpenFailPanel", 0.2f);
            }



            if(FinishScript.isPassedFinishHole && human_score_count >= level_goal)
            { 
                PlayerPrefs.SetInt("human_score", FinishScript.temp);
                TouchImage.raycastTarget = false;
                MoveForward.forwardSpeed = 0;
                if(isPanelActive == false)
                {
                    Invoke("OpenSuccessPanel", 0.2f);
                }
                
            }
            else
            {
                TouchImage.raycastTarget = false;
                MoveForward.forwardSpeed = 0;
                if(isPanelActive == false)
                {
                    Invoke("OpenFailPanel", 0.2f);
                }
               
            }
			
       
		}

        if(Boost.boostCount == 1 && !boost1)
        {
            LevelProggressPanel.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = fillColor;
            LeanTween.scale(LevelProggressPanel.transform.GetChild(3).GetChild(0).gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setLoopPingPong(1);
            LeanTween.scale(LevelProggressPanel.transform.GetChild(3).GetChild(1).gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setLoopPingPong(1);
            boost1 = true;

        }
        else if(Boost.boostCount == 2 && !boost2)
        {
            humans = GameObject.FindGameObjectsWithTag("flying_human");
            LevelProggressPanel.transform.GetChild(3).GetChild(2).GetComponent<Image>().color = fillColor;
            LeanTween.scale(LevelProggressPanel.transform.GetChild(3).GetChild(2).gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setLoopPingPong(1);
            LeanTween.scale(LevelProggressPanel.transform.GetChild(3).GetChild(3).gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setLoopPingPong(1);
            
            if (!IsScale)
            {
                for(int i = 0; i< humans.Length; i++)
                {
                    humans[i].GetComponent<CapsuleCollider>().radius = 3;
                }
                
                
                FeverText.SetActive(true);
                IsScale = true;
                FeverActive = true;
                LeanTween.scale(FeverText, new Vector3(1.5f, 1.5f, 1.5f), 0.4f).setOnComplete(() => {

                    LeanTween.scale(FeverText, new Vector3(0.1f, 0.1f, 0.1f), 0.4f).setDelay(1f).setOnComplete(()=> {
                        FeverText.SetActive(false);
                    });
                    
                });
               
               
                

            }

            boost2 = true;
        }
        else if (Boost.boostCount == 0 && !boost0)
        {
            boost0 = true;
            humans = GameObject.FindGameObjectsWithTag("flying_human");
            LeanTween.scale(LevelProggressPanel.transform.GetChild(3).GetChild(0).gameObject, new Vector3(1f, 1f, 1f), 0.1f);
            LeanTween.scale(LevelProggressPanel.transform.GetChild(3).GetChild(1).gameObject, new Vector3(1f, 1f, 1f), 0.1f);
            LeanTween.scale(LevelProggressPanel.transform.GetChild(3).GetChild(2).gameObject, new Vector3(1f, 1f, 1f), 0.1f);
            LeanTween.scale(LevelProggressPanel.transform.GetChild(3).GetChild(3).gameObject, new Vector3(1f, 1f, 1f), 0.1f);
            LevelProggressPanel.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Color.gray;
            for (int i = 0; i < humans.Length; i++)
            {
                humans[i].GetComponent<CapsuleCollider>().radius = 0.258545f;
            }
           
           
            LevelProggressPanel.transform.GetChild(3).GetChild(2).GetComponent<Image>().color = Color.gray;
            IsScale = false;
            FeverActive = false;
            
        }

	}

	public void Confetti_play()
	{
		Confetti.GetComponent<ParticleSystem>().Play();
	}


	public void Set_level_started()
	{
		is_level_started = true;
	}

	public void Continue_Level()
	{
        
        Invoke("cont_level", 0.2f);
    }

    public void Retry_Level()
	{
        Invoke("retr_level", 0.2f);
	}


    //IEnumerator DestroySnake()
    //{

    //    for (int i = Snake.transform.childCount - 1; i >= 0; i--)
    //    {
    //        Destroy(Snake.transform.GetChild(i).gameObject);
    //        GameObject SnakePart = Instantiate(SnakePartDivided, Snake.transform.GetChild(i).transform.position, Snake.transform.GetChild(i).transform.rotation);
    //        SnakePart.transform.SetParent(Snake.transform);
    //        yield return new WaitForSeconds(1 / 2 * Snake.transform.childCount);
    //    }
    //    FailPanel.SetActive(true);
    //    GameObject.FindGameObjectWithTag("SnakeObj").GetComponent<SnakeMovement>().BodyParts.Clear();
    //    GameObject.FindGameObjectWithTag("SnakeObj").GetComponent<SnakeMovement>().BodyParts.Add(GameObject.FindGameObjectWithTag("SnakeObj").transform.GetChild(0));
    //}



    public void OpenFailPanel()
	{
        isPanelActive = true;
        GameManager.gameStarted = false;
        LevelProggressPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().outlineColor = new Color(0, 0, 0, 0);
        LevelProggressPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().faceColor = new Color(255, 255, 255, 0);
        goal_text.GetComponent<TextMeshProUGUI>().outlineColor = new Color(0, 0, 0, 0);
        goal_text.GetComponent<TextMeshProUGUI>().faceColor = new Color(255, 255, 255, 0);
        FailPanel.SetActive(true);
        human_count = 1;
        
    }

	public void OpenSuccessPanel()
	{
        isPanelActive = true;
        Debug.Log("Success panel opened");
        GameManager.gameStarted = false;
        SuccessPanel.SetActive(true);
        human_count = 1;
    }

    public void CloseFever()
    {
        FeverText.SetActive(false);
    }

    public void cont_level()
    {
        boost0 = false;
        boost1 = false;
        boost2 = false;
        Clouds.transform.position = new Vector3(0, Clouds.transform.position.y, Clouds.transform.position.z);
        Boost.boostCount = 0;
        FeverActive = false;
        FeverBar.SetActive(false);
        TapToFly.isRestarted = true;
        FinishScript.isPassedFinishHole = false;
        SuccessPanel.SetActive(false);
        human_count = 1;
        TouchControl.isTextDone = false;
        target.transform.position = new Vector3(1.39f, 0, 0);
        Camera.main.transform.position = new Vector3(0f, 0, -39);
        GameManager.gameStarted = false;
        MainMenu.SetActive(true);
        isPanelActive = false;
        Destroy(Humanoid);
        Destroy(current_level);
        human_score_count = 0;
        index_counter++;
        current_level_number++;
        if (current_level_number > 20)
        {
            index_counter = (current_level_number % 20) - 1;
        }
        else
        {
            index_counter = current_level_number - 1;
        }
        //PlayerPrefs.SetInt("index_counter", index_counter);
        PlayerPrefs.SetInt("current_level", current_level_number);
        GameObject level = Instantiate(Levels[index_counter], Vector3.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
        GameObject hum = Instantiate(human_parent, Vector3.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
        hum.transform.Rotate(-45, 90, 0);
        Humanoid = hum;
        level_goal = level_goals[index_counter];
        goal_text.GetComponent<TextMeshProUGUI>().text = "0/" + level_goal;
        GameObject.Find("goalTextfirst").GetComponent<TextMesh>().text = "Save 0/" + level_goal + "!";
        target.GetComponent<Rigidbody>().useGravity = false;
        isSetToProgBarInt = false;
        current_level = level;
        target.GetComponent<Rigidbody>().isKinematic = true;
        isLevelFinished = false;
        TouchImage.raycastTarget = true;
        LevelProggressPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Level " + PlayerPrefs.GetInt("current_level").ToString();
        MainMenu.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("human_score").ToString();
        Invoke("Set_level_started", 0.05f);
        TouchControl.isAnimationFinished = false;
        target.GetComponent<MoveForward>().enabled = false;
        human_parent.transform.GetChild(0).GetComponent<MoveForward>().enabled = false;
        human_parent.transform.GetChild(0).GetComponent<Follow>().enabled = false;
        human_parent.GetComponent<Follow>().enabled = false;
        goal_text.GetComponent<TextMeshProUGUI>().outlineColor = new Color(0, 0, 0, 0);
        goal_text.GetComponent<TextMeshProUGUI>().faceColor = new Color(255, 255, 255, 0);
        LevelProggressPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().outlineColor = new Color(0, 0, 0, 0);
        LevelProggressPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().faceColor = new Color(255, 255, 255, 0);
        human_parent.GetComponent<Animator>().SetBool("isStarted", false);
        Camera.main.GetComponent<Animator>().SetBool("isStarted", false);
        Camera.main.GetComponent<MoveForward>().enabled = false;
        human_parent.GetComponent<Animator>().SetBool("isFinished", false);
        Camera.main.GetComponent<Animator>().SetBool("isFinished", false);
        Camera.main.GetComponent<Animator>().enabled = true;
    }

    public void retr_level()
    {
        boost0 = false;
        boost1 = false;
        boost2 = false;
        Clouds.transform.position = new Vector3(0, Clouds.transform.position.y, Clouds.transform.position.z);
        Boost.boostCount = 0;
        FeverActive = false;
        FeverBar.SetActive(false);
        GameManager.gameStarted = false;
        target.transform.position = new Vector3(1.39f, 0, 0);
        Camera.main.transform.position = new Vector3(0, 0, -39);
        TouchControl.isTextDone = false;
        target.GetComponent<Rigidbody>().isKinematic = true;
        MainMenu.SetActive(true);
        isPanelActive = false;
        FinishScript.isPassedFinishHole = false;
        human_score_count = 0;
        Destroy(Humanoid);
        TapToFly.isRestarted = true;
        FailPanel.SetActive(false);
        Destroy(current_level);
        GameObject level = Instantiate(Levels[index_counter], Vector3.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
        GameObject hum = Instantiate(human_parent, Vector3.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
        hum.transform.Rotate(-45, 90, 0);
        TouchControl.isAnimationFinished = false;
        Humanoid = hum;
        current_level = level;
        TouchImage.raycastTarget = true;
        target.GetComponent<Rigidbody>().useGravity = false;
        Invoke("Set_level_started", 0.05f);
        level_goal = level_goals[index_counter];
        goal_text.GetComponent<TextMeshProUGUI>().text = "0/" + level_goal;
        GameObject.Find("goalTextfirst").GetComponent<TextMesh>().text = "Save 0/" + level_goal + "!";
        target.GetComponent<MoveForward>().enabled = false;
        LevelProggressPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Level " + PlayerPrefs.GetInt("current_level").ToString();
        MainMenu.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("human_score").ToString();
        human_parent.transform.GetChild(0).GetComponent<MoveForward>().enabled = false;
        human_parent.transform.GetChild(0).GetComponent<Follow>().enabled = false;
        human_parent.GetComponent<Follow>().enabled = false;
        LevelProggressPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().outlineColor = new Color(0, 0, 0, 0);
        LevelProggressPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().faceColor = new Color(255, 255, 255, 0);
        goal_text.GetComponent<TextMeshProUGUI>().outlineColor = new Color(0, 0, 0, 0);
        goal_text.GetComponent<TextMeshProUGUI>().faceColor = new Color(255, 255, 255, 0);
        human_parent.GetComponent<Animator>().SetBool("isStarted", false);
        Camera.main.GetComponent<Animator>().SetBool("isStarted", false);
        Camera.main.GetComponent<MoveForward>().enabled = false;
        human_parent.GetComponent<Animator>().SetBool("isFinished", false);
        Camera.main.GetComponent<Animator>().SetBool("isFinished", false);
        Camera.main.GetComponent<Animator>().enabled = true;
    }
}
