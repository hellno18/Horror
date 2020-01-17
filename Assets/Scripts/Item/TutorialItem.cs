using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    public enum Item
    {
        None,
        Battery,
        Key
    }
    public Item item;

    //GameObject item
    GameObject prefapItem;
    Transform oldItem;

    //Position old item
    Vector3 oldItemPosition;
    

    //Bool item
    bool isSpawn=false;

    //Global Variable
    Battery battery;
    Key key;
    // Start is called before the first frame update
    void Start()
    {
        switch (item)
        {
            case Item.Battery:
                oldItem = this.transform.Find("Battery").GetComponent<Transform>();
                oldItemPosition = oldItem.transform.position;
                prefapItem = (GameObject)Resources.Load("Prefaps/Battery");
                battery = oldItem.GetComponent<Battery>();
                break;
            case Item.Key:
                oldItem = this.transform.Find("key_silver").GetComponent<Transform>();
                oldItemPosition = oldItem.transform.position;
                prefapItem = (GameObject)Resources.Load("Prefaps/keys/key_silver");
                key = oldItem.GetComponent<Key>();
                break;
        }
    }

    private void Update()
    {

        switch (item)
        {
            case Item.Battery:
                if (battery.isDestroy && isSpawn)
                {
                    oldItem = this.transform.Find("Battery").GetComponent<Transform>();
                    battery = oldItem.GetComponent<Battery>();
                    isSpawn = false;
                }
                break;

            case Item.Key:
                if (key.isDestroy && isSpawn)
                {
                    oldItem = this.transform.Find("key_silver").GetComponent<Transform>();
                    key = oldItem.GetComponent<Key>();
                    isSpawn = false;
                }
               
                break;
        }

        //Get component new battery
       

    }

    private void OnTriggerExit(Collider other)
    {

        switch (item)
        {
            case Item.Battery:
                if (battery.isDestroy)
                {
                    isSpawn = true;
                    GameObject newItem = Instantiate(prefapItem, oldItemPosition, Quaternion.identity, this.transform) as GameObject;
                    newItem.transform.localScale = new Vector3(0.5183195f, 0.5183195f, 0.5183195f);
                    newItem.name = "Battery";
                }
                break;

            case Item.Key:
                if (key.isDestroy)
                {
                    isSpawn = true;
                    GameObject newItem = Instantiate(prefapItem, oldItemPosition, Quaternion.identity, this.transform) as GameObject;
                    newItem.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    newItem.transform.localRotation = Quaternion.Euler(0.554f, -8.133f,0f);
                    newItem.name = "key_silver";
                }
                break;
        }
       







    }
}
