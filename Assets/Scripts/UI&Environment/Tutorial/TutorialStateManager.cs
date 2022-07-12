using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialStateManager : MonoBehaviour
{
    [Header("States")]
    public TutorialBaseState currentState;
    public NarrationState narration = new NarrationState();
    public UnlockedState unlocked = new UnlockedState();
    public BattleState battle = new BattleState();
    public InteractionState interaction = new InteractionState();

    [Header("TXT Files")]
    public TextAsset convoFile;
    public TextAsset convoFile2;
    public TextAsset convoFile3;
    public TextMeshProUGUI optionA;
    public TextMeshProUGUI optionB;
    public TextMeshProUGUI optionC;
    public TextMeshProUGUI optionD;

    [Header("Misc")]
    public ExplorationUIController controller;
    public DialogueManagerT manager;
    public Collider2D firstCol;
    public GameObject overlay;
    public Transform player;
    public bool end = false;
    public Transform guideUi;
    public int curTurn = 1;
    public bool t1;
    public bool t2;
    public bool t3;

    [Header("Battle Ui Tutorial")]
    public GameObject[] battleUi;
    public TEmotionManager emoManager;
    public Button A;
    public Button B;
    public Button C;
    public Button D;
    public Button E;
    public RectTransform negBar;
    public GameObject dialopt;
    public GameObject eDial;
    public GameObject pDial;
    public TextMeshProUGUI pText;
    public TextMeshProUGUI eText;
    public GameObject battleObj;
    public GameObject enemy;

    [Header("Interaction")]
    public GameObject doorT;
    private void Start()
    {
        overlay.SetActive(true);
        currentState = narration;
        currentState.EnterState(this);
    }

    private void Update()
    {
        transform.position = player.position;
        if (manager.dialogueActive)
        {
            end = false;
        }
        else if (!manager.dialogueActive)
        {
            end = true;
        }
        currentState.UpdateState(this);
    }

    public void SwitchState(TutorialBaseState tutorial)
    {
        currentState = tutorial;
        tutorial.EnterState(this);
    }
    public void startConvo(TextAsset txtFile)
    {
        if (manager.dialogueContent)
            manager.startConversation(txtFile);
    }

    public void partialConvo(TextAsset txtFile, int begin, int end)
    {
        if (manager.dialogueContent)
            manager.partialLineConvo(txtFile, begin, end);
    }

    public void OnTriggerEnter2D(Collider2D trigger)
    {
        currentState.OnTriggerEnter2D(this, trigger);
    }
    public void OnCollsionEnter2D(Collision2D col)
    {
        currentState.OnCollisionEnter2D(this,col);
    }

    public void FirstEnc()
    {
        firstCol.enabled = false;
    }

    public void SetGuideUi(int y)
    {
        guideUi.localPosition = new Vector3(0, y, 0);
    }
    public void SceneChange()

    {
        controller.StartNG(1);
    }
    public void ContTurn()
    {
        if(curTurn == 1)
        {
            pText.text = $"Lets try to understand each other";
            negBar.sizeDelta = new Vector2(450, 15);
            optionA.text = $" Is that the best you can do?" + "<color=" + "green" + ">Try harder! </color>"; //R
            optionB.text = $"Come on !!We are " + "<color=" + "blue" + ">better than this!!"; // H
            optionC.text = $"Although this place is bad, you can live a " + "<color=" + "blue" + ">good life </color>" + "here"; //L
            optionD.text = $"Yea Im not good at all actually, this is the " + "<color=" + "red" + ">truth"; // A 
        }
        else if(curTurn == 2)
        {
            pText.text = $"Yea Im not good at all actually, this is the truth";
            eText.text = $"Shut your filthy mouth, you don’t deserve my time!";
            negBar.sizeDelta = new Vector2(350, 15);
            optionA.text = $" Are you " + "<color=" + "green" + ">done</color>" +"?"; //R
            optionB.text = $"Today is a bad day but tomorrow " + "<color=" + "blue" + ">it will be better"; // H
            optionC.text = $"Come here you, let me " + "<color=" + "blue" + ">shower some love" + "for you"; // L 
            optionD.text = $"If you try to " + " <color=" + "red" + ">overcome your flaws</color>" + ", you are already a winner"; //A
        }
        else if (curTurn == 3)
        {
            pText.text = $"Come here you, let me shower some love for you";
            negBar.sizeDelta = new Vector2(335, 15);
        }
    }
}
