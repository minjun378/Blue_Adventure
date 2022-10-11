using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static int slimeKill = 0;
    public static int turtleKill = 0;
    public static int minigolemKill = 0;

    public GameObject GameOverScene;

    Animator playerAnim;

    public bool comboPossible;
    public int comboStep;
    bool inputRoll;
    bool is_defense = false;

    #region 플레이어 스텟
    public static float atk = 200; //플레이어 공격력
    public static float attakdmg; //최종 데미지
    public static float def = 50.0f;
    public static float speed;
    public static float stamina = 30;
    public static float hp = 2000.0f;
    public static float hp_cur;
    #endregion

    public GameObject hitbox;

    public static float Stamina_cur;
    public Image Stamina_Bar_Front;
    public Image Stamina_Bar_Back;
    public bool _use_stamina = false;

    public AudioSource sound;
    public AudioClip attacksound;
    public AudioClip defendsound;
    public AudioClip hitsound;

    float time = 0.0f;
    public float stime = 0.0f;

    // Start is called before the first frame update
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        Stamina_cur = stamina;
        hp_cur = hp;
    }
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_use_stamina && Stamina_cur<30)
        {
            stime += Time.deltaTime;
            if(stime>2.0f) Stamina_cur += 0.05f;
            if (Stamina_cur >= 30)
            {
                stime = 0;
                Stamina_cur = 30;
            }
        }
        if (_use_stamina) stime = 0;
        Player_Stamina_SyncBar();
        if (!Controller.offplay)
        {
            if (Input.GetMouseButtonDown(0)) NormalAttack();
            if (Input.GetMouseButton(1))
            {
                if (Stamina_cur >= 1)
                {
                    RollingAttack();
                }
                if (Stamina_cur < 1)
                {
                    EndRollingAttack();
                }
                attakdmg = 2500;
                speed = 1.0f;
            }
            if (Input.GetMouseButtonUp(1))
            {
                EndRollingAttack();
                speed = 15.0f;
            }
            if (Input.GetKey(KeyCode.LeftShift) && !is_defense)
            {
                is_defense = true;
                playerAnim.SetBool("Is_Defens", true);
                playerAnim.Play("WGS_Defend_Start");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && is_defense)
            {
                is_defense = false;
                playerAnim.SetBool("Is_Defens", false);
            }
        }
    }

    void Player_Stamina_SyncBar()
    {
        Stamina_Bar_Front.fillAmount = Stamina_cur / stamina;


        if (Stamina_Bar_Front.fillAmount < 0.2f)
        {
            if (time < 0.5f)
            {
                Stamina_Bar_Back.color = new Color(1, 0, 0, 0.7f-time); 
            }
            else
            {
                Stamina_Bar_Back.color = new Color(1, 0, 0, time-0.3f); 
                if (time > 1.0f)
                {
                    time = 0.0f;
                }
            }
            time += Time.deltaTime;
        }
        else if (Stamina_Bar_Front.fillAmount >= 0.2f)
        {
            Stamina_Bar_Back.color = new Color(0, 0, 0, 0);
        }
    }

    public void ComboPossible()
    {
        comboPossible = true;
    }

    public void NextAttack()
    {
        if (!inputRoll)
        {
            if (comboStep == 2)
            {
                attakdmg = atk * (float)System.Math.Truncate(Random.Range(0.9f, 1.3f)*100)/100;
                playerAnim.Play("WGS_attackA2");
            }
            if (comboStep == 3)
            {
                attakdmg = atk * (float)System.Math.Truncate(Random.Range(0.9f, 1.3f) * 100) / 100;
                playerAnim.Play("WGS_attackA3");
                comboStep += 1;
            }

            if (comboStep == 5)
            {
                attakdmg = atk * (float)System.Math.Truncate(Random.Range(0.9f, 1.3f) * 100) / 100;
                playerAnim.Play("WGS_attackA5");
            }
        }
        if (inputRoll)
        {
            playerAnim.SetBool("Rolling", true);
            playerAnim.Play("WGS_ChargeSkillA_Start");
        }
        
    }

    public void ResetCombo()
    {
        comboPossible = false;
        inputRoll = false;
        comboStep = 0;
    }
    void NormalAttack()
    {
        if (comboStep == 0)
        {
            attakdmg = atk * (float)System.Math.Truncate(Random.Range(0.9f, 1.3f) * 100) / 100;
            playerAnim.Play("WGS_attackA1");
            comboStep = 1;
            return;
        }
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }
    }
    void RollingAttack()
    {
        playerAnim.SetBool("Rolling", true);
        if(!inputRoll) playerAnim.Play("WGS_ChargeSkillA_Start");
        inputRoll = true;
        _use_stamina = true;
    }
    void EndRollingAttack()
    {
        playerAnim.SetBool("Rolling", false);
        inputRoll = false;
        _use_stamina = false;
    }


    void setMoveTriggerTrue()
    {
        Controller.pMove = true;
    }
    void setMoveTriggerFalse()
    {
        Controller.pMove = false;
    }

    void ChangeState(string s)
    {
        hitbox.layer = LayerMask.NameToLayer(s);
    }

    void UseStamina()
    {
        Stamina_cur -= 1;
    }

    public void playattacksound()
    {
        sound.clip = attacksound;
        sound.Play();
    }

    public void playdefendsound()
    {
        sound.clip = defendsound;
        sound.Play();
    }
    public void playhitsound()
    {
        sound.clip = hitsound;
        sound.Play();
    }

    public void GameOver()
    {
        GameOverScene.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void restart()
    {
        SceneManager.LoadSceneAsync("start");
    }

}
