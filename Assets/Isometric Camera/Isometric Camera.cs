using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class IsometricCamera : MonoBehaviour
{
    public float targetAngle = 45f;
    public float currentAngle = 0f;
    public float mouseSensitivity = 2f;
    public float rotationSpeed = 5f;
    public float Raydistance = 100f;
    public Transform player;
    public Camera playerCam;
    private RaycastHit hit;
    private RaycastHit lasthit;

    private void Start()
    {
        playerCam = Camera.main;
    }
    void Update()
    {
    //카메라 플레이어 추적
    transform.position = player.position;

        //카메라 회전
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(0))
        {
            targetAngle += mouseX * mouseSensitivity;
        }
        else
        {
            targetAngle = Mathf.Round(targetAngle / 45);
            targetAngle *= 45;
        }
        if(targetAngle< 0) 
        {
            targetAngle += 360;
        }
        if (targetAngle > 360)
        {
            targetAngle -= 360;
        }

        currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(30, currentAngle, 0);
    }

    private void FixedUpdate()
    {
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 rayDirection = playerCam.transform.forward;
        Ray ray = new Ray(rayOrigin, rayDirection);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);


        if (lasthit.transform != null) //오브젝트 활성화
        {
            lasthit.transform.gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Opacity", 1);
        }
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, Raydistance)) //감지된 오브젝트 비활성화
        {
            if (hit.transform.CompareTag("wall"))
            {
                //hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Opacity", 0.3f);
                lasthit = hit;
                Debug.Log("벽 감지완료");
            }
        }
    }

    
    private void setColor(Material m, float a)
    {
        m.color = new Color(m.color.r,m.color.g, m.color.b, a);
    }
}
