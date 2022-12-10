using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManger : MonoBehaviour
{


    //public int Health_Point;
    public PlayerMoving player;
    public GameObject[] Stages;
    public GameObject Restart_Button;
    public GameObject menuSet;  

    //Canvas����
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public Image portraitImg;
    public GameObject storePanel;   
    public StoreBuyText storeBuyText;
    public GameObject playerStatPanel;
    public PlayerStatText playerStatText;
    public GameObject HelpPanel;
    public Text playerLevelText;
    public Text playerHpText;
    public GameObject barricadeobj; //성익 수정
    // private float timer; // 성익 시간 저장 변수

    // private int waitingTime; // 성익 지연 시간 저장 변수 

    // Inspector���� ���x
    static int StageIndex;
    static int isLoad;
    public bool isSlow;
    public GameObject scanObject;
    public bool isTalkPanelActive;
    public bool isPlayerStatPanel;
    public bool isHelpPanel;
    public int talkIndex;
    public int AttackPoint_Price = 10;
    public int HpPoint_Price = 10;
    int[] SceneKey = new int[3];
    int[] PaperNo = new int[10];
    int[] StoneNo = new int[10];

    void Start()
    {
        // timer = 0; //성익
        // waitingTime = 200; //성익

        if(isLoad == 0)
        {
            isLoad = 2;
        }
        else if (isLoad == 1)
        {
            GameLoad();
            isLoad++;
        }
        else if(isLoad == 2)
        {
            SceneLoad();
        }
    }
    private void Awake()
    {
        playerLevelText.text = "Lv . " + player.PlayerLevel.ToString();
        playerHpText.text = player.CurrentHp.ToString() + " / " + player.PlayerHp.ToString();
        isSlow = false;
        isPlayerStatPanel = false;
        isHelpPanel = false;

    }

    private void Update()
    {
        // timer += Time.deltaTime;

        if (Input.GetButtonDown("Cancel"))
        {
            MenuSetButton();
        }
    }
    private void FixedUpdate()
    {
        playerLevelText.text = "Lv . " + player.PlayerLevel.ToString();
        playerHpText.text = player.CurrentHp.ToString() + " / " + player.PlayerHp.ToString();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerReposition();
            HealthDown(10); //���� 10
        }
    }
  
    public void HealthDown(float damage)
    {
        if (player.CurrentHp > 0)
        {
            player.CurrentHp -= damage;
            if(player.CurrentHp <= 0) 
            {
                player.OnDie();
                Text buttonText = Restart_Button.GetComponentInChildren<Text>();
                Restart_Button.SetActive(true);
            }
        }
        else
        {
            player.CurrentHp -= damage;
            player.OnDie();
            Text buttonText = Restart_Button.GetComponentInChildren<Text>();
            Restart_Button.SetActive(true);
        }
    }
    public void PlayerReposition()
    {
        player.transform.position=new Vector3(0, 0, 0);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene0");
    }
    public void MenuSetStart()
    {
        menuSet.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("PlayerLv", player.PlayerLevel);
        PlayerPrefs.SetFloat("PlayerCurrentExp", player.CurrentExp);
        PlayerPrefs.SetFloat("PlayerAtKDMG", player.PlayerAtkDmg);
        PlayerPrefs.SetFloat("PlayerHp", player.PlayerHp);
        PlayerPrefs.SetFloat("PlayerCurrentHp", player.CurrentHp);
        PlayerPrefs.SetInt("SceneIndex", StageIndex);
        PlayerPrefs.SetInt("AbilityPoint", player.AbilityPoint);

        string strArr = ""; // ���ڿ� ����
        for (int i = 0; i < SceneKey.Length; i++) // �迭�� ','�� �����ư��� tempStr�� ����
        {
            strArr = strArr + SceneKey[i];
            if (i < SceneKey.Length - 1) // �ִ� ������ -1������ ,�� ����
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("SceneKeyData", strArr); // PlyerPrefs�� ���ڿ� ���·� ����
        PlayerPrefs.Save();
    }
    public void GameLoad()
    {
        //if (!PlayerPrefs.HasKey("PlayerX"))
        //{
        //    return;
        //}
        //if(SceneManager.GetActiveScene().name == "Scene0")
        //{
        //    return;
        //}
        //if (SceneManager.sceneCount < 2)
        //{
        //    return;
        //}
   
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int PlayerLevel= PlayerPrefs.GetInt("PlayerLv");
        float CurrentExp = PlayerPrefs.GetFloat("PlayerCurrentExp");
        float PlayerAtkDmg = PlayerPrefs.GetFloat("PlayerAtKDMG");
        float PlayerHp = PlayerPrefs.GetFloat("PlayerHp");
        float CurrentHp = PlayerPrefs.GetFloat("PlayerCurrentHp");
        int abilityPoint = PlayerPrefs.GetInt("AbilityPoint");

        string[] dataArr = PlayerPrefs.GetString("SceneKeyData").Split(','); // PlayerPrefs���� �ҷ��� ���� Split �Լ��� ���� ���ڿ��� ,�� �����Ͽ� �迭�� ����
        int[] sceneKey = new int[dataArr.Length]; // ���ڿ� �迭�� ũ�⸸ŭ ������ �迭 ����
        for (int i = 0; i < dataArr.Length; i++)
        {
            sceneKey[i] = System.Convert.ToInt32(dataArr[i]); // ���ڿ� ���·� ����� ���� ���������� ��ȯ�� ����
        }
        SceneKey = sceneKey;

        StageIndex = PlayerPrefs.GetInt("SceneIndex");
        player.transform.position = new Vector3(x, y, 0);
        player.PlayerLevel = PlayerLevel;
        player.CurrentExp = CurrentExp;
        player.PlayerAtkDmg = PlayerAtkDmg;
        player.PlayerHp = PlayerHp;
        player.CurrentHp = CurrentHp;
        player.AbilityPoint = abilityPoint;

    }
    public void ExitGame()
    {
        Time.timeScale = 1;
        StageIndex = 0;
        SceneManager.LoadScene(0);
        //Application.Quit();
    }
    public void SceneSave()
    {
        PlayerPrefs.SetInt("PlayerLv", player.PlayerLevel);
        PlayerPrefs.SetFloat("PlayerCurrentExp", player.CurrentExp);
        PlayerPrefs.SetFloat("PlayerAtKDMG", player.PlayerAtkDmg);
        PlayerPrefs.SetFloat("PlayerHp", player.PlayerHp);
        PlayerPrefs.SetFloat("PlayerCurrentHp", player.CurrentHp);
        PlayerPrefs.SetInt("AbilityPoint", player.AbilityPoint);
        string strArr = ""; // ���ڿ� ����
        for (int i = 0; i < SceneKey.Length; i++) // �迭�� ','�� �����ư��� tempStr�� ����
        {
            strArr = strArr + SceneKey[i];
            if (i < SceneKey.Length - 1) // �ִ� ������ -1������ ,�� ����
            {
                strArr = strArr + ",";
            }
        }
        PlayerPrefs.SetString("SceneKeyData", strArr); // PlyerPrefs�� ���ڿ� ���·� ����
        PlayerPrefs.Save();
    }
    public void SceneLoad()
    {
        //if (!PlayerPrefs.HasKey("PlayerCurrentExp"))
        //{
        //    return;
        //}
        //if (SceneManager.GetActiveScene().name == "Scene0")
        //{
        //    return;
        //}
        //if (SceneManager.sceneCount < 2)
        //{
        //    return;
        //}
      
        int PlayerLevel = PlayerPrefs.GetInt("PlayerLv");
        float CurrentExp = PlayerPrefs.GetFloat("PlayerCurrentExp");
        float PlayerAtkDmg = PlayerPrefs.GetFloat("PlayerAtKDMG");
        float PlayerHp = PlayerPrefs.GetFloat("PlayerHp");
        float CurrentHp = PlayerPrefs.GetFloat("PlayerCurrentHp");
        int abilityPoint = PlayerPrefs.GetInt("AbilityPoint");
        string[] dataArr = PlayerPrefs.GetString("SceneKeyData").Split(','); // PlayerPrefs���� �ҷ��� ���� Split �Լ��� ���� ���ڿ��� ,�� �����Ͽ� �迭�� ����
        int[] sceneKey = new int[dataArr.Length]; // ���ڿ� �迭�� ũ�⸸ŭ ������ �迭 ����
        for (int i = 0; i < dataArr.Length; i++)
        {
            sceneKey[i] = System.Convert.ToInt32(dataArr[i]); // ���ڿ� ���·� ����� ���� ���������� ��ȯ�� ����
        }
        SceneKey = sceneKey;

        player.PlayerLevel = PlayerLevel;
        player.CurrentExp = CurrentExp;
        player.PlayerAtkDmg = PlayerAtkDmg;
        player.PlayerHp = PlayerHp;
        player.CurrentHp = CurrentHp;
        player.AbilityPoint = abilityPoint;
    }
    public void NextStage()
    {
        StageIndex = 1;
        SceneSave();
        SceneManager.LoadScene(StageIndex);

    }
    public void GameStartButton()
    {
        isLoad = 0;
        StageIndex = 1;

        SceneManager.LoadScene(StageIndex);
    }
    public void GameLoadButton()
    {
        isLoad = 1;
        int SceneIndex = PlayerPrefs.GetInt("SceneIndex");
        StageIndex = SceneIndex;

        SceneManager.LoadScene(StageIndex);
    }
    public void ItemSlowSkill()
    {
        isSlow = true;
        Invoke("ItemSlowSkillEnd", 20f);
    }
    public void ItemSlowSkillEnd()
    {
        isSlow = false;
    }
    public async void SearchAction(GameObject scan_Object)
    {
        scanObject = scan_Object;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        if (objData.isNpc == true)
        {
            Talk(objData.id, objData.isNpc);
            talkPanel.SetActive(isTalkPanelActive);
        }
        else if (objData.isStore == true)
        {   
            if(objData.id == 200)
            {
                OpenStore();
            }
        }
        else if (objData.isKey == true)
        {
            if(objData.id == 602)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
                SceneKey[0] = 1;
                scanObject.SetActive(false);
            }
            if (objData.id == 603)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
                SceneKey[1] = 1;
                scanObject.SetActive(false);
            }
            if (objData.id == 604)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
                SceneKey[2] = 1;
                scanObject.SetActive(false);
            }
        }
        else if (objData.isSign == true) //성익
        {
            if(objData.id == 401)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
            }
            if(objData.id == 402)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
            }
            if(objData.id == 403)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
            }
            if(objData.id == 404)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
            }
            if(objData.id == 405)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
            }
            if(objData.id == 406)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
            }
            if(objData.id == 407)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
            }
            if(objData.id == 408)
            {
                Talk(objData.id, objData.isNpc);
                talkPanel.SetActive(isTalkPanelActive);
            }
        }
        else if (objData.isDoor == true)
        {
            if (objData.id == 702)
            {
                if (SceneKey[0] == 1)
                {
                    Talk(objData.id, objData.isNpc);
                    talkPanel.SetActive(isTalkPanelActive);
                    OpenFinalSceneDoor(scanObject);
                }
                else
                {
                    Talk(705, false);
                    talkPanel.SetActive(isTalkPanelActive);
                }
            }
            if (objData.id == 703)
            {
                if (SceneKey[1] == 1)
                {
                    Talk(objData.id, objData.isNpc);
                    talkPanel.SetActive(isTalkPanelActive);
                    OpenFinalSceneDoor(scanObject);
                }
                else
                {
                    Talk(705, false);
                    talkPanel.SetActive(isTalkPanelActive);
                }
            }
            if (objData.id == 704)
            {
                if (SceneKey[2] == 1)
                {
                    Talk(objData.id, objData.isNpc);
                    talkPanel.SetActive(isTalkPanelActive);
                    OpenFinalSceneDoor(scanObject);
                }
                else
                {
                    Talk(705, false);
                    talkPanel.SetActive(isTalkPanelActive);
                }
            }
            if (objData.id == 901) //성익
            {
                if (PaperNo[0] == 1 && PaperNo[1] == 1)
                {
                    Debug.Log("check");
                    Animator anim = objData.GetComponent<Animator>();
                    anim.SetTrigger("Blockbreak");
                    StartCoroutine("Breakdown");
                }
            }
            if (objData.id == 902) //성익
            {
                if (PaperNo[2] == 1 && PaperNo[3] == 1)
                {
                    Animator anim = objData.GetComponent<Animator>();
                    anim.SetTrigger("Blockbreak");
                    StartCoroutine("Breakdown");
                }
            }
        }
        else if(objData.isTeleport == true)
        {
            if(objData.id== 500)
            {
                StageIndex = 2;
                SceneSave();
                SceneManager.LoadScene(2);
            }
            else if (objData.id == 501)
            {
                StageIndex = 3;
                SceneSave();
                SceneManager.LoadScene(3);
            }
            else if (objData.id == 502)
            {
                StageIndex = 4;
                SceneSave();
                SceneManager.LoadScene(4);
            }
            if (objData.id == 503)
            {
                StageIndex = 5;
                SceneSave();
                SceneManager.LoadScene(5);
            }
        }
        else if(objData.isPaper == true) //성익
        {
            if(objData.id == 801)
            {
                Debug.Log("ok");
                Debug.Log(scanObject);
                //Talk(objData.id, objData.isNpc);
                //talkPanel.SetActive(isTalkPanelActive);
                PaperNo[0] = 1;
                scanObject.SetActive(false);
            }
            else if(objData.id == 802)
            {
                PaperNo[1] = 1;
                scanObject.SetActive(false);
            }
             else if(objData.id == 803)
            {
                PaperNo[2] = 1;
                scanObject.SetActive(false);
            }
             else if(objData.id == 804)
            {
                PaperNo[3] = 1;
                scanObject.SetActive(false);
            }
        }
        else if(objData.isStone == true) //성익
        {
            if(objData.id == 1001)
            {
                StoneNo[0] = 1;
                scanObject.SetActive(false);
                if(StoneNo[0] == 1 && StoneNo[1] == 1 && StoneNo[2] == 1)
                {
                    barricadeobj.SetActive(false);
                }
            }
            if(objData.id == 1002)
            {
                StoneNo[1] = 1;
                scanObject.SetActive(false);
                if(StoneNo[0] == 1 && StoneNo[1] == 1 && StoneNo[2] == 1)
                {
                    barricadeobj.SetActive(false);
                }
            }
            if(objData.id == 1003)
            {
                StoneNo[2] = 1;
                scanObject.SetActive(false);
                if(StoneNo[0] == 1 && StoneNo[1] == 1 && StoneNo[2] == 1)
                {
                    barricadeobj.SetActive(false);
                }
            }
        }
        //from jeongik
        else if (objData.isWaterMapObject == true)
        {   
            if(objData.id == 0)
            {
                //Debug.Log("watermapobject");
                WaterMapController.ScanObject(scanObject);
            }
            
        }
    }
    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if (talkData == null)
        {
            isTalkPanelActive = false;
            talkIndex = 0;
            return;
        }
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isTalkPanelActive = true;
        talkIndex++;
    }
    void OpenStore()
    {
        storePanel.SetActive(true);
    }
    public void CloseStore()
    {
        storePanel.SetActive(false);
    }
    public void BuyItem(string whatItem)
    {
        switch (whatItem)
        {
            case "ATTACKPOINT":
                if (player.Money >= AttackPoint_Price)
                {
                    player.Money -= AttackPoint_Price;
                    AttackPoint_Price += 10;
                    storeBuyText.Buy_ATTACKPOINT_Text();
                    player.PlayerAtkDmg += 1;
                    playerStatText.PlayerATK_Text();
                    playerStatText.PlayerTotalDmg_Text();
                }
                break;
            case "HPPOINT":
                if (player.Money >= HpPoint_Price)
                {
                    player.Money -= HpPoint_Price;
                    HpPoint_Price += 10;
                    storeBuyText.Buy_HPPOINT_Text();
                    player.PlayerHp += 10;
                    player.CurrentHp += 10;
                    playerStatText.PlayerHP_Text();
                }
                break;
        }
    }
    public void Buy_ATTACKPOINT_Button()   //�������� Atk �춧 ��ư ȣȯ
    {
        BuyItem("ATTACKPOINT");
    }
    public void Buy_HPPOINT_Button()
    {
        BuyItem("HPPOINT");
    }
    public void OpenPlayerStat()   // ����â ���� ȣȯ
    {
        playerStatPanel.SetActive(true);
        isPlayerStatPanel = true;
    }
    public void ClosePlayerStat()
    {
        playerStatPanel.SetActive(false);
        isPlayerStatPanel = false;
    }
    public void PlayerStatButton()
    {
        if(isPlayerStatPanel == false)
        {
            OpenPlayerStat();
        }
        else
        {
            ClosePlayerStat();
        }
    }
    public void OpenHelp()   //������ ���� ȣȯ
    {
        HelpPanel.SetActive(true);
        isHelpPanel = true;
    }
    public void CloseHelp()
    {
        HelpPanel.SetActive(false);
        isHelpPanel = false;
    }
    public void HelpButton()
    {
        if (isHelpPanel == false)
        {
            OpenHelp();
        }
        else
        {
            CloseHelp();
        }
    }
    public void MenuSetButton()
    {
        if (menuSet.activeSelf)
        {
            menuSet.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            menuSet.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void OpenFinalSceneDoor(GameObject scanObject)
    {
        scanObject.SetActive(false);
    }
    IEnumerator Breakdown()
    {
        yield return new WaitForSeconds(2f);
        scanObject.SetActive(false);
    }
    // private static DateTime Delay(int MS)
    // {
    // DateTime ThisMoment = DateTime.Now;
    // TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
    // DateTime AfterWards = ThisMoment.Add(duration);
 
    // while (AfterWards >= ThisMoment)
    // {
    //    System.Windows.Forms.Application.DoEvents();
    //    ThisMoment = DateTime.Now;
    // }
 
    // return DateTime.Now;
    // }

}
