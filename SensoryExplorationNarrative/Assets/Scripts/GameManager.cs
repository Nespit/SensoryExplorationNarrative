using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private int scenarioCount;
    public Text mainText;
    public Text gameButtonText;
    public Text menuButtonText;
    public Button listenButton;
    private string listenButtonText;
    public Button feelButton;
    private string feelButtonText;
    public Button lookButton;
    private string lookButtonText;
    public Button smellButton;
    private string smellButtonText;
    public Button firstButton;
    private string firstButtonText;
    public Button secondButton;
    private string secondButtonText;
    public Button startButton;
    public Button resumeButton;
    public Button restartButton;
    public Button endButton;
    public Canvas menuUI;
    public Canvas gameUI;
    GameState gameState;
    Dictionary<uint, ScenarioLambda> scenarios = new Dictionary<uint, ScenarioLambda>();

    public static GameManager Instance
    {  
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }
 
    struct ScenarioLambda
    {
       public System.Action action;
        public ScenarioLambda(System.Action action)
        {  
            this.action = action;
        } 
    }

    struct InteractionLambda
    {
       public System.Action action;
       public string text;
       public InteractionTypes type;
        public InteractionLambda(InteractionTypes type, string text, System.Action action)
        {  
            this.type = type;
            this.text = text;
            this.action = action;
        } 
    }

    void SetBackground(string name)
    {

    }

    void SetText(string text)
    { 
        mainText.text = text;
    }

    void PlaySound(string name)
    {

    }

    void SetInteractions(InteractionLambda[] interactions)
         {
            listenButton.onClick.RemoveAllListeners();
            listenButton.gameObject.SetActive(false);
            listenButtonText = "";
            feelButton.onClick.RemoveAllListeners();
            feelButton.gameObject.SetActive(false);
            feelButtonText = "";
            lookButton.onClick.RemoveAllListeners();
            lookButton.gameObject.SetActive(false);
            lookButtonText = "";
            smellButton.onClick.RemoveAllListeners();
            smellButton.gameObject.SetActive(false);
            smellButtonText = "";
            firstButton.onClick.RemoveAllListeners();
            firstButton.gameObject.SetActive(false);
            firstButtonText = "";
            secondButton.onClick.RemoveAllListeners();
            secondButton.gameObject.SetActive(false);
            secondButtonText = "";

            for (int i = 0; i < interactions.Length; ++i)
            {
                Debug.Log(interactions[i].type);

                System.Action action = interactions[i].action;
                    
                switch (interactions[i].type)
                {
                    case InteractionTypes.listen:
                        listenButton.gameObject.SetActive(true);
                        listenButtonText = interactions[i].text; 
                        listenButton.onClick.AddListener(()=>action());
                        break;
                    case InteractionTypes.touch:
                        feelButton.gameObject.SetActive(true);
                        feelButtonText = interactions[i].text; 
                        feelButton.onClick.AddListener(()=>action());
                        break;
                    case InteractionTypes.look:
                        lookButton.gameObject.SetActive(true);
                        lookButtonText = interactions[i].text; 
                        lookButton.onClick.AddListener(()=>action());
                        break;
                    case InteractionTypes.smell:
                        smellButton.gameObject.SetActive(true);
                        smellButtonText = interactions[i].text; 
                        smellButton.onClick.AddListener(()=>action());
                        break;
                    case InteractionTypes.first:
                        firstButton.gameObject.SetActive(true);
                        firstButtonText = interactions[i].text; 
                        firstButton.onClick.AddListener(()=>action());
                        break;
                    case InteractionTypes.second:
                        secondButton.gameObject.SetActive(true);
                        secondButtonText = interactions[i].text; 
                        secondButton.onClick.AddListener(()=>action());
                        break;
                }
            };
         }
    void GoToScenario(uint scenario)
    {
        Debug.Log("GoTotScenario called for scenario" + scenario);
        scenarios[scenario].action();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        gameUI.gameObject.SetActive(false);
        menuUI.gameObject.SetActive(true);
        gameState = GameState.initial;

        scenarios.Add(0x1001, new ScenarioLambda(() => { 
                SetText("You suddenly gain consciousness again…. Everythings is dark around…. What do you want to do?"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.listen, "Listen", ()=> GoToScenario(0x1002) ),
                    new InteractionLambda(InteractionTypes.smell, "Smell", ()=> GoToScenario(0x1003) ),
                    new InteractionLambda(InteractionTypes.touch, "Touch", ()=> GoToScenario(0x1004) )
                });
                }
            )); 

        scenarios.Add(0x1002, new ScenarioLambda(() => { 
                SetText("You can't hear anything and feel very disappointed."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Back to the start.", ()=> GoToScenario(0x1001) )
                });
                }
            ));

        scenarios.Add(0x1003, new ScenarioLambda(() => { 
                SetText("You can't smell anything and feel very disappointed."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Back to the start.", ()=> GoToScenario(0x1001) )
                });
                }
            ));
        
        scenarios.Add(0x1004, new ScenarioLambda(() => { 
                SetText("You can't see anything and feel very disappointed."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Back to the start.", ()=> GoToScenario(0x1001) )
                });
                }
            ));
    }

    void Update()
    {   
        if(gameState == GameState.game && Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }

        else if(gameState == GameState.menu && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    public void StartGame()
    {
        gameUI.gameObject.SetActive(true);
        menuUI.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameState = GameState.game;
        MouseExit();
        scenarios[0x1001].action();
    }

    public void ResumeGame()
    {
        gameUI.gameObject.SetActive(true);
        menuUI.gameObject.SetActive(false);
        MouseExit();
        gameState = GameState.game;
    }

    public void OpenMenu()
    {
        gameUI.gameObject.SetActive(false);
        menuUI.gameObject.SetActive(true);
        MouseExit();
        gameState = GameState.menu;
    }
    public void EndGame()
    {
        Application.Quit();
    }

    public void ListenMouseEnter()
    {
        gameButtonText.text = listenButtonText;
    }
    public void FeelMouseEnter()
    {
        gameButtonText.text = feelButtonText;
    }
    public void LookMouseEnter()
    {
        gameButtonText.text = lookButtonText;
    }
    public void SmellMouseEnter()
    {
        gameButtonText.text = smellButtonText;
    }
    public void FirstMouseEnter()
    {
        gameButtonText.text = firstButtonText;
    }
    public void SecondMouseEnter()
    {
        gameButtonText.text = secondButtonText;
    }
    public void StartMouseEnter()
    {
        menuButtonText.text = "Start the game.";
    }
    public void ResumeMouseEnter()
    {
        menuButtonText.text = "Resume the game.";
    }
    public void RestartMouseEnter()
    {
        menuButtonText.text = "Restart the game.";
    }
    public void EndMouseEnter()
    {
        menuButtonText.text = "End the game.";
    }
    public void MouseExit()
    {
        gameButtonText.text = "";
        menuButtonText.text = "";
    }
}

public enum GameState
{
    initial,
    game,
    menu
}
