using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    public LayerMask Character;
    public GameObject Text;
    public GameObject Message1;
    public GameObject Message2;

    public GameObject quest;
    public TMPro.TMP_Text questText;

    public bool QuestClear = false;

    public GameObject player;

    private void Update()
    {
        if (quest.activeSelf)
        {
            questText.text = "½½¶óÀÓ : " + Player.slimeKill.ToString() + "/3" + "\n" +
                                    "ÅÍÆ² : " + Player.turtleKill.ToString() + "/3" + "\n" +
                                    "¹Ì´Ï°ñ·½ : " + Player.minigolemKill.ToString() + "/1";
        }
        if (Input.GetKeyDown(KeyCode.E) && Text.activeSelf && !QuestClear)
        {
            Text.SetActive(false);
            Controller.offplay = true;
            Message1.SetActive(true);
        }
        if (Player.slimeKill >= 3 && Player.turtleKill >= 3 && Player.minigolemKill >= 1)
        {
            quest.SetActive(false);
            QuestClear = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && Text.activeSelf && QuestClear)
        {
            Text.SetActive(false);
            Controller.offplay = true;
            Message2.SetActive(true);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((Character & (1 << other.gameObject.layer)) != 0)
        {
            Text.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Text.SetActive(false);
    }
    public void QuestAceept()
    {
        Controller.offplay = false;
        Message1.SetActive(false);
        quest.SetActive(true);
    }
    public void teleport()
    {
        Controller.offplay = false;
        Message2.SetActive(false);
        player.transform.position = new Vector3(1153, 35, -431);
        player.transform.Rotate(new Vector3(0, 180, 0));
    }
}
