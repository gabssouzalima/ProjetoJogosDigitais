using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 25f;
    float turnSpeed = 100f;
    public float mouseSenbility = 5000f;
    float rotationX = 0f;
    public Transform cam;
    public Transform gun;
    public GameObject bulletPrefab;
    public Transform gunTip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { }



    // Update is called once per frame
    void Update()
    {

        // Pega o movimento0 do mouse nos eixos X (esquerda/direita) e Y (cima/baixo) // 
        float mouseX = Input.GetAxis("Mouse X") * mouseSenbility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenbility * Time.deltaTime;

        // Gira o CORPO do jogador para a esquerda/direita (noeixo Y)
        transform.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;
        // Limita a rotação da câmera
        rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        //Gira a CAMERA do jogador para cima/baixo (no eixo X)
        cam.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        gun.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Clicou");
            GameObject b = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
        }
    }

} 