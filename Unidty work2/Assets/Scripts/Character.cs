using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float mouseX;
    
    [SerializeField] Camera virtualCamera;
    [SerializeField] CharacterController characterController;
    
    [SerializeField] Vector3 direction; 

    private void Awake()
    {
        characterController = 
            GetComponent<CharacterController>();
    }

    void Start()
    {
        DisableCamera();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Control();

            Move();

            Rotate();
        }
    }

    public void Control()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();

        // mouseX에 마우스로 입력한 값을 저장함

        mouseX += Input.GetAxisRaw
            ("Mouse X") * rotationSpeed * Time.deltaTime;

        characterController.transform.
            TransformDirection(direction);

    }
    
    public void Move()
    {
        // 방향 * 속도 * 시간 
        characterController.Move
            (characterController.transform.TransformDirection
            (direction) * speed * Time.deltaTime);

    }

    public void Rotate()
    {
        transform.eulerAngles = 
            new Vector3(0,mouseX,0);
    }

    public void DisableCamera()
    {
        //현재 플레이어가 나라면
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            virtualCamera.gameObject.SetActive(true);

            virtualCamera.GetComponent
                <AudioListener>().enabled = false;

        }

    }


}
