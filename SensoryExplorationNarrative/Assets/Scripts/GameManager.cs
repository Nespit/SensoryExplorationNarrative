using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
/*     private Scenario[] scenarios = new Scenario[10]; */
    private int scenarioCount;
    public Text mainText;
    public Text buttonText;
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

        scenarios[0x1001].action();
    }

    public void ListenMouseEnter()
    {
        buttonText.text = listenButtonText;
    }
    public void FeelMouseEnter()
    {
        buttonText.text = feelButtonText;
    }
    public void LookMouseEnter()
    {
        buttonText.text = lookButtonText;
    }
    public void SmellMouseEnter()
    {
        buttonText.text = smellButtonText;
    }
    public void FirstMouseEnter()
    {
        buttonText.text = firstButtonText;
    }
    public void SecondMouseEnter()
    {
        buttonText.text = secondButtonText;
    }
    public void MouseExit()
    {
        buttonText.text = "";
    }

/* 
    void Start()
    {
        scenarioCount = scenarios.Length;

        for(int i = 0; i < scenarioCount; ++i)
        {
            switch (i)
            {
                case 0:
                    Interaction[] interaction = new Interaction[3];
                    int outcome = 1;
                    interaction[0] = new Interaction(InteractionTypes.listen, "Listen", outcome);
                    outcome = 2;
                    interaction[1] = new Interaction(InteractionTypes.smell, "Smell", outcome);
                    outcome = 3;
                    interaction[2] = new Interaction(InteractionTypes.touch, "Touch", outcome);

                    Scenario scenario = new Scenario("You suddenly gain consciousness again…. Everythings is dark around…. What do you want to do?", interaction);
                    scenarios[i] = scenario;
                    break;
                case 1:
                    interaction = new Interaction[2];
                    outcome = 4;
                    interaction[0] = new Interaction(InteractionTypes.first, "1.Water pouring out of an open sink.", outcome);
                    outcome = 5;
                    interaction[1] = new Interaction(InteractionTypes.second, "Someone splashing in a pool.", outcome);
                    
                    scenario = new Scenario("You hear something that seems to be water - What do you think it was?", interaction);
                    scenarios[i] = scenario;
                    break;
                case 2:
                    interaction = new Interaction[2];
                    outcome = 6;
                    interaction[0] = new Interaction(InteractionTypes.first, "1. A nice mature cheddar", outcome);
                    outcome = 7;
                    interaction[1] = new Interaction(InteractionTypes.second, "2.Cheese puffs", outcome);

                    scenario = new Scenario("You take a big gulp of air in. As you do, an intense smell penetrates your lung. Something pungent and sharp. Like a old mature cheddar - What do you think it was?", interaction);
                    scenarios[i] = scenario;
                    break;
                case 3:
                    interaction = new Interaction[2];
                    outcome = 8;
                    interaction[0] = new Interaction(InteractionTypes.first, "1. A clump of hair.", outcome);
                    outcome = 9;
                    interaction[1] = new Interaction(InteractionTypes.second, "2. A plastic net.", outcome);

                    scenario = new Scenario("You feel around blindly with our outstretched palm. They are underwater. You find fuzzy, wet strands tingling between your fingers. - What do you think it is?", interaction);
                    scenarios[i] = scenario;
                    break;
                case 4:
                    interaction = new Interaction[2];
                    outcome = 0;
                    interaction[0] = new Interaction(InteractionTypes.first, "Get closer to it.", outcome);
                    outcome = 0;
                    interaction[1] = new Interaction(InteractionTypes.second, "Ignore it.", outcome);

                    scenario = new Scenario("Water pouring out of an open sink. What do you want to do?", interaction);
                    scenarios[i] = scenario;
                    break;
                case 5:
                    interaction = new Interaction[2];
                    outcome = 0;
                    interaction[0] = new Interaction(InteractionTypes.first, "Get closer to it.", outcome);
                    outcome = 0;
                    interaction[1] = new Interaction(InteractionTypes.second, "Ignore it.", outcome);

                    scenario = new Scenario("Someone splashing in a pool. What do you want to do?", interaction);
                    scenarios[i] = scenario;
                    break;
                case 6:
                    interaction = new Interaction[2];
                    outcome = 0;
                    interaction[0] = new Interaction(InteractionTypes.first, "Take a bite out of it.", outcome);
                    outcome = 0;
                    interaction[1] = new Interaction(InteractionTypes.second, "Nothing", outcome);

                    scenario = new Scenario("A nice mature cheddar. What do you want to do?", interaction);
                    scenarios[i] = scenario;
                    break;
                case 7:
                    interaction = new Interaction[2];
                    outcome = 0;
                    interaction[0] = new Interaction(InteractionTypes.first, "Eat them!", outcome);
                    outcome = 0;
                    interaction[1] = new Interaction(InteractionTypes.second, "Nothing", outcome);

                    scenario = new Scenario("Cheese puffs. What do you want to do?", interaction);
                    scenarios[i] = scenario;
                    break;
                case 8:
                    interaction = new Interaction[2];
                    outcome = 0;
                    interaction[0] = new Interaction(InteractionTypes.first, "Grab it.", outcome);
                    outcome = 0;
                    interaction[1] = new Interaction(InteractionTypes.second, "Nothing", outcome);

                    scenario = new Scenario("A clump of hair. What do you want to do?", interaction);
                    scenarios[i] = scenario;
                    break;
                case 9:
                    interaction = new Interaction[2];
                    outcome = 0;
                    interaction[0] = new Interaction(InteractionTypes.first, "Grab it.", outcome);
                    outcome = 0;
                    interaction[1] = new Interaction(InteractionTypes.second, "Nothing", outcome);

                    scenario = new Scenario("A plastic net. What do you want to do?", interaction);
                    scenarios[i] = scenario;
                    break;
            }
        }
    } */
}
