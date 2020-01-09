using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchControl : MonoBehaviour
{
    public Camera orto_camera;
    //public GameObject Fan_Object;
    public Animator start_animator;
    public GameObject target;
    public GameObject humanpar;
    public GameObject obj_for_bounds;
    public GameObject MainMenu;
    public GameObject text;
    public GameObject LevelText;
    public GameObject FeverBar;
    private Vector3 firstPos, lastPos;
    private float distanceX, distanceY, fingerSens = 10f;
    public bool isTouched = false;
    public static bool isAnimationFinished = false;
    float timer = 2.5f;
    public static bool isTextDone = false;
    public bool isForceDone = false;

    public Vector2 screen_bounds;

    void Start()
    {

    }

    void Update()
    {
      
        start_animator = GameObject.FindGameObjectWithTag("human_parent").GetComponent<Animator>();
        humanpar = GameObject.FindGameObjectWithTag("human_parent");
        if (GameManager.gameStarted)
        {
            AnimatorStateInfo info = start_animator.GetCurrentAnimatorStateInfo(0);

            if (info.normalizedTime % info.length >= 1)
            {

             

                target.GetComponent<MoveForward>().enabled = true;
                if(humanpar.transform.childCount != 0)
                {
                    humanpar.transform.GetChild(0).GetComponent<MoveForward>().enabled = true;
                    humanpar.transform.GetChild(0).GetComponent<Follow>().enabled = true;
                    
                }
                target.GetComponent<Rigidbody>().useGravity = true;
                target.GetComponent<Rigidbody>().isKinematic = false;
                humanpar.GetComponent<Follow>().enabled = true;
                Camera.main.GetComponent<Animator>().StopPlayback();
                start_animator.SetBool("isStarted", false);
                Camera.main.GetComponent<Animator>().SetBool("isStarted", false);
                Camera.main.GetComponent<MoveForward>().enabled = true;
                Camera.main.GetComponent<Animator>().enabled = false;
                start_animator.enabled = false;
                isAnimationFinished = true;


            }
            else
            {
                target.transform.position = new Vector3(1.39f, 0, 0);
                isAnimationFinished = false;
            }



        }

        screen_bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        if (isTouched && isAnimationFinished && !isForceDone)
        {
            
            target.GetComponent<Rigidbody>().velocity = Vector3.zero;

            target.GetComponent<Rigidbody>().AddForce(Vector3.up*9.81f*6.5f, ForceMode.Impulse);
            isForceDone = true;
           
        }
        /*else if (!isTouched && isAnimationFinished)
        {
            if (target.GetComponent<Rigidbody>().velocity.y >= Vector3.zero.y)
            {
                target.GetComponent<Rigidbody>().velocity = -1 * target.GetComponent<Rigidbody>().velocity;

            }
        }*/
       
    }

    private void LateUpdate()
    {
        /*Vector3 current_pos = target.transform.position;
        current_pos.x = Mathf.Clamp(current_pos.x, obj_for_bounds.transform.position.x-35f, obj_for_bounds.transform.position.x);
        current_pos.y = Mathf.Clamp(current_pos.y, -22f, 22f);
        target.transform.position = current_pos;*/

    }

    public void TouchDown()
    {
        if (!GameManager.gameStarted )
        {
            StartCoroutine(set_fadeIn());
            MoveForward.forwardSpeed = 12;
            firstPos = orto_camera.ScreenToWorldPoint(Input.mousePosition);
            isTouched = true;
            if(start_animator != null)
            {
                start_animator.SetBool("isStarted", true);
                Camera.main.GetComponent<Animator>().SetBool("isStarted", true);
            }
            
            
            MainMenu.SetActive(false);
            GameManager.gameStarted = true;

        }
        else
        {
            if(start_animator != null)
            {
                start_animator.SetBool("isStarted", true);
                Camera.main.GetComponent<Animator>().SetBool("isStarted", true);
            }
         
            firstPos = orto_camera.ScreenToWorldPoint(Input.mousePosition);
            isTouched = true;
            MainMenu.SetActive(false);

        }
    }
    public void TouchUp()
    {
        isTouched = false;
        isForceDone = false;
    }

    public void Set_isTouched(bool state)
    {
        isTouched = state;
    }

    IEnumerator set_text_alpha()
    {
        for (float a = 0.05f; a <= 1f; a += 0.05f)
        {
            Color c = text.GetComponent<TextMeshProUGUI>().outlineColor;
            Color b = text.GetComponent<TextMeshProUGUI>().faceColor;
            b.a = a;
            c.a = a;
            text.GetComponent<TextMeshProUGUI>().faceColor = b;
            text.GetComponent<TextMeshProUGUI>().outlineColor = c;
            yield return new WaitForSeconds(0.05f);
        }

    }
    IEnumerator set_fadeIn()
    {
        for (float a = 0.05f; a <= 1f; a += 0.05f)
        {
            Color c = LevelText.GetComponent<TextMeshProUGUI>().faceColor;
            Color b = LevelText.GetComponent<TextMeshProUGUI>().outlineColor;
            c.a = a;
            b.a = a;
            LevelText.GetComponent<TextMeshProUGUI>().faceColor = c;
            LevelText.GetComponent<TextMeshProUGUI>().outlineColor = b;
            yield return new WaitForSeconds(0.05f);
        }
        FeverBar.SetActive(true);
        //StartCoroutine(set_fadeOut());
        StartCoroutine(set_text_alpha());


    }
    IEnumerator set_fadeOut()
    {
        for (float a = 1f; a >= 0f; a -= 0.05f)
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
