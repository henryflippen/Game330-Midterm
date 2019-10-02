using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamplePlayerMovement : MonoBehaviour {

    public float TurnSpeed = 120.0f;
    public float MoveSpeed = 8.0f;

    private bool gameWin = false;

    public Image Crosshair;

    public Image ConversationPanel;
    public Text ConversationSpeaker;
    public Text ConversationText;

    public Image InfoPanel;
    public Text InfoText;
    public Text InfoName;

    public Image DetectivePanel;
    public Text Accuse1;
    public Text Accuse2;
    public Text Accuse3;
    public Text Accuse4;
    public Image Cursor1;
    public Image Cursor2;
    public Image Cursor3;
    public Image Cursor4;

    public Image GameOver;
    public Text GameOverText;

    public Image GameVictory;
    public Text GameVictoryText;

    public int Selection = 0;

    public float damage = 10f;

    public bool isTalking = false;
    public bool isTalkingD = false;
    

    public Camera fpsCam;

    public void Start()
    {
        InfoPanel.enabled = false;
        InfoText.enabled = false;
        InfoName.enabled = false;

        ConversationPanel.enabled = false;
        ConversationSpeaker.enabled = false;
        ConversationText.enabled = false;

        DetectivePanel.enabled = false;
        Accuse1.enabled = false;
        Accuse2.enabled = false;
        Accuse3.enabled = false;
        Accuse4.enabled = false;

        Cursor1.enabled = false;
        Cursor2.enabled = false;
        Cursor3.enabled = false;
        Cursor4.enabled = false;

        GameOver.enabled = false;
        GameOverText.enabled = false;

        GameVictory.enabled = false;
        GameVictoryText.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        //INTERACTION WITH NPCS/ITEMS

        if (!gameWin)
        {
            if (!isTalking)
            {
                
                if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
                {
                    transform.Rotate(Vector3.up, -TurnSpeed * Time.deltaTime);
                }
                else if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton))
                {
                    transform.Rotate(Vector3.up, TurnSpeed * Time.deltaTime);
                }

                if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton))
                {
                    Shoot();
                }
            }
            else if (isTalking)
            {
                //disables info panel when talking figure out a way to not do it this way because this is garbage
                InfoPanel.enabled = false;
                InfoText.enabled = false;
                InfoName.enabled = false;
                //
                Crosshair.enabled = false;
                if (isTalkingD)
                {
                    Debug.Log(Selection);
                    if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.LeftButton))
                    {
                        if (Selection > 0)
                        {
                            Selection--;
                        }
                    }
                    else if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.RightButton))
                    {
                        if (Selection < 3)
                        {
                            Selection++;
                        }
                    }

                    //cursor Movement
                    switch (Selection)
                    {
                        case 0:
                            DisableCursor();
                            Cursor1.enabled = true;
                            break;
                        case 1:
                            DisableCursor();
                            Cursor2.enabled = true;
                            break;
                        case 2:
                            DisableCursor();
                            Cursor3.enabled = true;
                            break;
                        case 3:
                            DisableCursor();
                            Cursor4.enabled = true;
                            break;
                    }

                    //Action Button while talking to the detective
                    if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton))
                    {
                        switch (Selection)
                        {
                            case 0:
                                DisableUI();
                                Crosshair.enabled = true;
                                break;
                            case 1:
                                QuitGame();
                                break;
                            case 2:
                                QuitGame();
                                break;
                            case 3:
                                WinGame();
                                break;
                        }
                    }
                }
                else
                {
                    if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton))
                    {
                        DisableUI();
                        Crosshair.enabled = true;
                    }
                }

            }
            
        }
            
     }
       

    //shoots out raycast from camera if it hits something that can be interacted with starts interaction
    void Shoot ()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                if (target.Suspect)
                {
                    Debug.Log(target.Quote);
                    EnableConvoUI();

                    ConversationSpeaker.text = target.Speaker;
                    ConversationText.text = target.Quote;
                }
                else
                {
                    isTalkingD = true;
                    Debug.Log(target.Quote);
                    EnableConvoUI();

                    DetectivePanel.enabled = true;
                    Accuse1.enabled = true;
                    Accuse2.enabled = true;
                    Accuse3.enabled = true;
                    Accuse4.enabled = true;

                    ConversationSpeaker.text = target.Speaker;
                    ConversationText.text = target.Quote;


                }
                isTalking = true;
            }
        }
    }

    //QUITS GAME FOR GAME OVER
    void QuitGame ()
    {
        DisableUI();

        GameOver.enabled = true;
        GameOverText.enabled = true;
        Debug.Log("QUITING GAME...");
        Application.Quit();
    }

    //TRIGGERS WINGAME
    void WinGame ()
    {
        DisableUI();

        gameWin = true;

        GameVictory.enabled = true;
        GameVictoryText.enabled = true;
    }

    //DISABLES GENERAL UI
    void DisableUI ()
    {
        isTalking = false;
        ConversationPanel.enabled = false;
        ConversationSpeaker.enabled = false;
        ConversationText.enabled = false;

        DetectivePanel.enabled = false;
        Accuse1.enabled = false;
        Accuse2.enabled = false;
        Accuse3.enabled = false;
        Accuse4.enabled = false;

        Cursor1.enabled = false;
        Cursor2.enabled = false;
        Cursor3.enabled = false;
        Cursor4.enabled = false;

        Crosshair.enabled = false;
    }

    void EnableConvoUI()
    {
        

        ConversationPanel.enabled = true;
        ConversationSpeaker.enabled = true;
        ConversationText.enabled = true;
    }

    //DISABLES CURSOR
    void DisableCursor ()
    {
        Cursor1.enabled = false;
        Cursor2.enabled = false;
        Cursor3.enabled = false;
        Cursor4.enabled = false;
    }

    //DOESNT WORK BUT SHOULD DISABLE MOVEMENT
    void DisableMovement ()
    {
        if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
        {
            
        }
        else if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton))
        {
            
        }

        if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton))
        {
            
        }
    }
}
