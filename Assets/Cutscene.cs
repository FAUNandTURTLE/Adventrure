using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public CameraFolow cameraScript;
    public QuestSystem questSystem;
    public TMP_Text botText;

    [Header("PlayerComponents")]
    public Animator animatorPlayer;
    public Rigidbody2D rbPlayer;
    public HeroKnight playerScript;

    [Header("Настройки")]
    public Transform pointPlayer1;
    public GameObject dialogPanel;
    public GameObject InputField;

    private float speedPlayer;

    private bool CutEnabled;
    void Start()
    {

        StartCoroutine(CutSceneStart());
        CutEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (CutEnabled)
        {
            rbPlayer.velocity = Vector2.right * speedPlayer;
        }
        
    }

    private IEnumerator CutSceneStart()
    {
        cameraScript.enabled = false;
        playerScript.enabled = false;
        speedPlayer = 2;
        animatorPlayer.SetInteger("AnimState", 1);
        while (rbPlayer.transform.position.x < pointPlayer1.position.x)
        {
            yield return null;
        }
        speedPlayer = 0;
        animatorPlayer.SetInteger("AnimState", 0);
    }

    public void ClickSend()
    {
        InputField.SetActive(false);

        StartCoroutine(DialogClose());
    }

    IEnumerator DialogClose()
    {
        botText.text = "Я думаю...";
        yield return new WaitForSeconds(5);
        cameraScript.enabled = true;
        playerScript.enabled = true;
        CutEnabled= false;
        dialogPanel.transform.Find("Message Area").Find("Close Button").gameObject.SetActive(true);
        questSystem.SetQuest();
    }

}
