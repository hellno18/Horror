using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    public enum Item
    {
        None,
        Battery
    }
    public Item item;

    //GameObject item
    GameObject prefapItem;
    Transform oldItem;

    //Position old item
    Vector3 oldItemPosition;

    //Bool item
    bool isSpawn=false;

    //Battery
    Battery battery;
    // Start is called before the first frame update
    void Start()
    {
        if(item == Item.Battery)
        {
            oldItem = this.transform.Find("Battery").GetComponent<Transform>();
            oldItemPosition = oldItem.transform.position;
            prefapItem = (GameObject)Resources.Load("Prefaps/Battery");
            battery = oldItem.GetComponent<Battery>();
        }
    }

    private void Update()
    {
        //Get component new battery
        if (battery.isDestroy&&isSpawn && item == Item.Battery)
        {
            oldItem = this.transform.Find("Battery").GetComponent<Transform>();
            battery = oldItem.GetComponent<Battery>();
            isSpawn = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (battery.isDestroy && item == Item.Battery)
        {
            isSpawn = true;
            GameObject newItem = Instantiate(prefapItem, oldItemPosition, Quaternion.identity,this.transform) as GameObject;
            newItem.transform.localScale = new Vector3(0.5183195f, 0.5183195f, 0.5183195f);
            newItem.name = "Battery";
        }
       




    }
}
