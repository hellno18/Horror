using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : PlayerBase, IPuzzle,IKey
{
    private float timer = 5;
    private float currTimer;
    // Start is called before the first frame update
    void Start()
    {
        //set default Health 
        health = 100;
        //set default stress LV
        stressLV = 80;
       
    }

    protected override void Run()
    {
        //temp
        if (Input.GetKeyDown(KeyCode.F1))
        {
            stressLV = 150;
        }

        if (Input.GetButtonDown("Quest"))
        {
            hud.QuestDisplay();
        }

        if (this.health <= 0)
        {
            StartCoroutine(ResultCoroutine());
        }

        if (flashlight.GetIsLight)
        {
            currTimer -= Time.deltaTime;
            if (currTimer < 0)
            {
                StressLV -= 1;
                currTimer = timer;
            }

            //while stress lv become 0
            if (StressLV < 80)
            {
                StressLV = 80;
            }
        }
        else
        {
            currTimer -= Time.deltaTime;
            if (StressLV < 90)
            {
                if (currTimer < 0)
                {
                    StressLV += 1;
                    //timer default 5 seconds
                    currTimer = timer;
                }
            }
            else if (StressLV >= 90 && StressLV < 120)
            {
                if (currTimer < 0)
                {
                    StressLV += 1;
                    health -= 1;
                    currTimer = 3;
                }
            }
            else if (StressLV >= 120 && StressLV < 140)
            {
                if (currTimer < 0)
                {
                    StressLV += 1;
                    health -= 1;
                    currTimer = 1;
                }
            }
            else if (StressLV >= 140 && StressLV < 160)
            {
                if (currTimer < 0)
                {
                    StressLV += 2;
                    health -= 3;
                    currTimer = 1;
                }
            }
            else if(stressLV>=160)
            {
                StressLV = 160;
                health = 0;
                //Set isShake become true/
                isShake = true;
                //shake the camera
                CameraShake();
                //called Dead function
                StartCoroutine(ResultCoroutine());
            }
        }
    }



    //For camera shake
    private void CameraShake()
    {
        if (isShake)
        {
            for (int i = 0; i < 10; i++)
            {
                mainCamera.transform.localPosition = new Vector3(Random.Range(-0.1f, 0.1f),
                    1,
                    0) * 0.8f;
            }
        }
    }

    public IEnumerator ResultCoroutine()
    {
        //Dead Show Slenderman face
        //TODO
        if (health <= 0)
        {
            PlayerPrefs.SetString("Result", "Lose");
        }
        if(health>0 && keyExit)
        {
            PlayerPrefs.SetString("Result", "Win");
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Result");
    }
    /*======================
    *Getter get Health
    ======================*/
    public float GetHealth
    {
        get
        {
            return health;
        }
    }

    /*======================
    *Setter Health
    ======================*/
    public float SetHealth
    {
        set
        {
            health = value;
        }
    }


    /*======================
    *Setter Getter StressLV
    ======================*/
    public int StressLV
    {
        get
        {
            return stressLV;
        }
        set
        {
            stressLV = value;
        }
    }

    /*======================
    *Setter Getter keyCount
    ======================*/
    public int GetKeyNCount()
    {
        return keyNCount;
    }
    public int AddKeyNCount()
    {
        return keyNCount++;
    }
    public int RemoveKeyNCount()
    {
        return keyNCount--;
    }

    /*++++++++++++
     * KEY EXIT
     +++++++++++++*/
    public bool GetKeyExit()
    {
      return keyExit;        
    }
    
    public void SetKeyExit(bool value)
    {
        keyExit = true;
    }

    /*======================
    *Setter Getter Puzzle
    ======================*/
    public int GetCountPuzzle()
    {
         return puzzleCount;
    }

    public int AddCountPuzzle()
    {
        return puzzleCount++;
    }

}
