using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static bool offplay = false;
    public static bool pMove = true;
    Animator playerAnim;
    [Header("Camera")]
    public Transform CamArm;
    public Transform cam;
    public float camSpeed;
    float mouseX;
    float mouseY;
    public float Wheel;

    [Header("Player")]
    public Transform playerAxis;
    public Transform player;
    Vector3 movement;

    public bool inven_menu = false;
    public bool is_pause = false;
    public GameObject inven;
    public GameObject playercanvas;
    // Start is called before the first frame update
    void Start()
    {
        Wheel = -5.0f;
        mouseY = 4.0f;
        Player.speed = 15.0f;
        playerAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!offplay)
        {
            CameraMove();
            CameraZoom();
            if (pMove)
            {
                PlayerMove();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inven_menu && !is_pause)
            {
                inven.SetActive(true);
                inven_menu = true;
                is_pause = true;
                Time.timeScale = 0;
                Controller.offplay = true;
                playercanvas.SetActive(false);
            }
            else if (inven_menu)
            {
                inven.SetActive(false);
                inven_menu = false;
                is_pause = false;
                Time.timeScale = 1;
                Controller.offplay = false;
                playercanvas.SetActive(true);
            }
        }
        
    }

    void CameraMove()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y") * -1;

        if (mouseY > 10) mouseY = 10;
        if (mouseY < 0) mouseY = 0;

        CamArm.rotation = Quaternion.Euler(new Vector3(CamArm.rotation.x + mouseY, CamArm.rotation.y + mouseX, 0) * camSpeed);
    }

    void CameraZoom()
    {
        Wheel += Input.GetAxis("Mouse ScrollWheel") * 10.0f;
        if (Wheel >= -5) Wheel = -5;
        if (Wheel <= -10) Wheel = -10;
        cam.localPosition = new Vector3(0, 0, Wheel);
    }

    void PlayerMove()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (movement != Vector3.zero)
        {
            playerAxis.rotation = Quaternion.Euler(new Vector3(0, CamArm.rotation.y + mouseX, 0) * camSpeed);
            playerAxis.Translate(movement * Time.deltaTime * Player.speed);
            player.localRotation = Quaternion.Slerp(player.localRotation, Quaternion.LookRotation(movement), 5 * Time.deltaTime);

            playerAnim.SetBool("Walk", true);
        }
        if (movement == Vector3.zero) playerAnim.SetBool("Walk", false);
        CamArm.position = new Vector3(player.position.x, player.position.y + 5.0f, player.position.z);
        
    }

}
