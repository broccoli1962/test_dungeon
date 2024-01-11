using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InventoryObject inventory;
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
            Debug.Log("인벤저장완료");
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
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
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }

    void Move123()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        Vector3 right = cameraTransform.right;

        Vector3 RightMovement = right * playerSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
        Vector3 ForwardMovement = forward * playerSpeed * Time.smoothDeltaTime * Input.GetAxis("Vertical");
        Vector3 FinalMovement = ForwardMovement + RightMovement;
        Vector3 direction = Vector3.Normalize(FinalMovement);

        if (direction != Vector3.zero)
        {
            transform.forward = direction;
            transform.position += FinalMovement;
        }
    }
}
