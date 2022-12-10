using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle3Moving : MonoBehaviour
{

    Rigidbody2D rigid;
    float Angle; // 회전 각
    public float FollowSpeed; // 플레이어 추적 속도
    public float Radius; // 회전 반경

    public Transform player;
    public GameManger gameManger;
    public Transform[] Circle; //회전 서클
    public float rotdir; //회전 방향
    float doubleclickedtime = -1.0f;
    float doubleclickedtime2 = -1.0f;
    float interval = 0.25f;
    bool IsDoubleClicked = false;
    bool IsDoubleClicked2 = false;
    
    
    float PPX; //player position x
    float PPY; //player position y

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update() {
        // 카이팅
        if (Input.GetKey(KeyCode.W)){
            Radius = Mathf.Lerp(Radius,6,Time.deltaTime*10);
            rotdir = Mathf.Lerp(rotdir,Mathf.Sign(rotdir) * 2.0f,Time.deltaTime*10);
            //PlayerMoving.TotalPlayerDamage = Mathf.Lerp(PlayerMoving.TotalPlayerDamage,0,Time.deltaTime*10);
            //Debug.Log(PlayerMoving.TotalPlayerDamage);
        }
        else if(!Input.GetKey(KeyCode.W) && Radius > 2.999f || PlayerMoving.TotalPlayerDamage < 0.999f){
            Radius = Mathf.Lerp(Radius,3,Time.deltaTime*10);
            rotdir = Mathf.Lerp(rotdir,Mathf.Sign(rotdir) * 1f,Time.deltaTime*10);
            //PlayerMoving.TotalPlayerDamage = Mathf.Lerp(PlayerMoving.TotalPlayerDamage,3,Time.deltaTime*10);
            //Debug.Log(PlayerMoving.TotalPlayerDamage);
        } 
        if (Input.GetKey(KeyCode.S)){
            Radius = Mathf.Lerp(Radius,1,Time.deltaTime*10);
            rotdir = Mathf.Lerp(rotdir,Mathf.Sign(rotdir) * 0.5f,Time.deltaTime*10);
        }
        else if(!Input.GetKey(KeyCode.S) && Radius <3){
            Radius = Mathf.Lerp(Radius,3,Time.deltaTime*10);
            rotdir = Mathf.Lerp(rotdir,Mathf.Sign(rotdir) * 1.0f,Time.deltaTime*10);
        }
        if (Input.GetKeyDown(KeyCode.D)){
            rotdir = -1;
            if((Time.time-doubleclickedtime) < interval)
            {
                IsDoubleClicked = true;
                doubleclickedtime = -1.0f;
                Debug.Log(IsDoubleClicked);
            }
            else{
                IsDoubleClicked =false;
                doubleclickedtime = Time.time;
                Debug.Log(IsDoubleClicked);
            }
        }
        if (Input.GetKey(KeyCode.D) && IsDoubleClicked){
            rotdir = -10;
        }
        if(Input.GetKeyUp(KeyCode.D)){
            IsDoubleClicked = false;
        }
        if (Input.GetKeyDown(KeyCode.A)){
            rotdir = 1;
            if((Time.time-doubleclickedtime2) < interval)
            {
                IsDoubleClicked2 = true;
                doubleclickedtime2 = -1.0f;
                Debug.Log(IsDoubleClicked);
            }
            else{
                IsDoubleClicked2 =false;
                doubleclickedtime2 = Time.time;
                Debug.Log(IsDoubleClicked);
            }
        }
        if (Input.GetKey(KeyCode.A) && IsDoubleClicked2){
            rotdir = 10;
        }
        if(Input.GetKeyUp(KeyCode.A)){
            IsDoubleClicked2 = false;
        }
        // 캐릭터 추적
        rigid.position = player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 360도 마다 저장된 각도 0으로 초기화
        if (Angle  > 360)
        {
            Angle = 0;
        }

        // 서클 회전
        //this.transform.rotation = Quaternion.Euler(0, 0, Angle);
        Angle += PlayerMoving.AngleSpeed * rotdir;
        transform.GetChild(0).position = new Vector3 (PPX + Radius * Mathf.Cos(Angle * Mathf.Deg2Rad),PPY + Radius * Mathf.Sin(Angle * Mathf.Deg2Rad),-1);
        transform.GetChild(1).position = new Vector3 (PPX + Radius * Mathf.Cos((Angle+90)* Mathf.Deg2Rad),PPY + Radius * Mathf.Sin((Angle+90)* Mathf.Deg2Rad),-1);
        transform.GetChild(2).position = new Vector3 (PPX + Radius * Mathf.Cos((Angle+180)* Mathf.Deg2Rad),PPY + Radius * Mathf.Sin((Angle+180)* Mathf.Deg2Rad),-1);
        transform.GetChild(3).position = new Vector3 (PPX + Radius * Mathf.Cos((Angle+270)* Mathf.Deg2Rad),PPY + Radius * Mathf.Sin((Angle+270)* Mathf.Deg2Rad),-1);
        //플레이어 위치

        PPX = player.position.x;
        PPY = player.position.y;

        //서클 사이즈
        transform.GetChild(0).localScale = new Vector3(PlayerMoving.Size,PlayerMoving.Size,1);
        transform.GetChild(1).localScale = new Vector3(PlayerMoving.Size,PlayerMoving.Size,1);
        transform.GetChild(2).localScale = new Vector3(PlayerMoving.Size,PlayerMoving.Size,1);
        transform.GetChild(3).localScale = new Vector3(PlayerMoving.Size,PlayerMoving.Size,1);
        

        //Debug.Log(player.position);
        //Debug.Log(this.transform.position);

        // 서클 플레이어 추적 >>>>> 이거 지금은 position 따라가게 해놨는 데 물리적용해서 속도로 지정으로 딜레이도 고려 중
        // Vector3 dir = player.transform.position - rigid.transform.position;
        // rigid.position = player.position;

        //Debug.Log(transform.GetChild(0).position);
        //Debug.Log(transform.GetChild(1).position);

    }

    // void OnCollisionEnter2D(Collision2D collision) {
    //     if(collision.gameObject.tag == "Enemy"){
    //         Debug.Log("hit");
    //         OnAttack(collision.transform);
    //     }
    // }

    // public void OnAttack(Transform Enemy)

    // {
    //     EnemyMove enemymove = Enemy.GetComponent<EnemyMove>();
    //     enemymove.EnemyDamaged();
    //     gameManger.Stage_Score += 100;
    // }
}
