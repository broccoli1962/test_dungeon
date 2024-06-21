using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public MouseItem mouseItem = new MouseItem();

    public InventoryObject inventory;
    public InventoryObject equitment;
    public Attribute[] attributes;

    protected float playerSpeed;

    private Transform playerTransform;
    private Transform cameraTransform;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = transform;
        cameraTransform = Camera.main.transform;

        for(int i = 0; i < attributes.Length; i++) {
            attributes[i].SetParent(this);
        }
        for(int i = 0;i < equitment.GetSlots.Length; i++) {
            equitment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equitment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }

    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if(_slot.ItemObject == null)
        {
            return;
        }
        switch(_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equitment:
                print(string.Concat("삭제", _slot.ItemObject, "on", _slot.parent.inventory.type, ", 허용된 아이템: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
        {
            return;
        }
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equitment:
                print(string.Concat("배치", _slot.ItemObject, "on", _slot.parent.inventory.type, ", 허용된 아이템: ", string.Join(", ", _slot.AllowedItems)));
                
                for(int i = 0; i<_slot.item.buffs.Length; i++)
                {
                    for(int j = 0; j<attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame 
    void Update()
    {
        if(Input.anyKey)
        {
            playerSpeed = GetComponent<PlayerStats>().MoveSpeed;
            Move();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equitment.Save();
            Debug.Log("인벤저장완료");
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            equitment.Load();
            Debug.Log("인벤로드완료");
        }
    }
    
    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        Vector3 right = cameraTransform.right;

        Vector3 moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            rb.velocity = moveDirection * playerSpeed;
            transform.forward = moveDirection;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if(inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, "업데이트 완료! 현재값 : ", attribute.value.ModifiedValue));
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        equitment.Clear();
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public PlayerMovement parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(PlayerMovement _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }
    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}