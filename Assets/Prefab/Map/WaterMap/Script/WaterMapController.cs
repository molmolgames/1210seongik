using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMapController : WaterObjectData
{
    public static GameObject waterobject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void ScanObject(GameObject scanObject)
    {
        waterobject = scanObject;
        WaterMapController Waterdata = waterobject.GetComponent<WaterMapController>();
        if(Waterdata.isChest == true){
            Debug.Log("WaterChest");
            Animator anim = waterobject.GetComponent<Animator>();
            anim.SetTrigger("ChestOpen");
            WaterMapController dropitem = waterobject.GetComponent<WaterMapController>();
            dropitem.DropItem();
            Debug.Log("drop");
            CircleCollider2D collision = waterobject.GetComponent<CircleCollider2D>();
            collision.enabled = false;
        }
        
    }
    public void DropItem() //아이템드랍함수
    {
            GameObject item = Instantiate(dropItem);
            Transform objectposition = waterobject.GetComponent<Transform>();
            item.transform.position = objectposition.position;
            item.SetActive(true);
    }
}

