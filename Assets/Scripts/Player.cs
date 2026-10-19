using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private TextMeshProUGUI textoVida;
    private float vidaAtual;
    private bool morto = false;
    float speed = 25f;
    float turnSpeed = 100f;
    public float mouseSenbility = 5000f;
    float rotationX = 0f;
    public Transform cam;
    public Transform gun;
    public GameObject bulletPrefab;
    public Transform gunTip;
    private Animator anim;

    // Movimento com colisão
    private CharacterController controller;
    public float gravity = -20f;
    private float verticalVelocity = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();
        if (controller == null)
            Debug.LogError("Player: adicione um Character Controller neste objeto");

        vidaAtual = vidaMaxima;
        AtualizarTexto();
    }

    public void TakeDamage(float dmg)
    {
        if (morto) return;

        vidaAtual -= dmg;
        vidaAtual = Mathf.Max(vidaAtual, 0f);
        AtualizarTexto();

        if (vidaAtual <= 0f)
        {
            morto = true;
            Morrer();
        }
    }

    private void AtualizarTexto()
    {
        if (textoVida != null)
            textoVida.text = "Vida: " + Mathf.CeilToInt(vidaAtual);
    }

    private void Morrer()
    {
        SceneManager.LoadScene("GameOver");
    }

    void Update()
    {
        if (controller == null) return;

        // Pega o movimento do mouse nos eixos X (esquerda/direita) e Y (cima/baixo)
        float mouseX = Input.GetAxis("Mouse X") * mouseSenbility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenbility * Time.deltaTime;

        // Gira o CORPO do jogador para a esquerda/direita (no eixo Y)
        transform.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;
        // Limita a rotação da câmera
        rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        // Gira a CAMERA do jogador para cima/baixo (no eixo X)
        cam.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Lê as teclas de movimento
        float h = 0f;
        float v = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) v += 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) v -= 1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) h -= 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) h += 1f;

        // Direção relativa à frente do jogador (normalizada para não andar mais rápido na diagonal)
        Vector3 move = transform.right * h + transform.forward * v;
        if (move.sqrMagnitude > 1f) move.Normalize();
        move *= speed;

        // Gravidade
        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        // O CharacterController respeita os colliders das paredes
        controller.Move(move * Time.deltaTime);

        bool isMoving = (h != 0f || v != 0f);
        anim.SetBool("isWalking", isMoving);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
            anim.SetTrigger("Shoot");
        }
    }
}