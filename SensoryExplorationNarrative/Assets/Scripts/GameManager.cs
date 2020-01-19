using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

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
    public AudioSource audioSource;
    public AudioClip splashSound;
    public AudioClip splashSound2;
    public AudioClip heySound;

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

    void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
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
                        listenButton.onClick.AddListener(()=>MouseExit());
                        break;
                    case InteractionTypes.touch:
                        feelButton.gameObject.SetActive(true);
                        feelButtonText = interactions[i].text; 
                        feelButton.onClick.AddListener(()=>action());
                        feelButton.onClick.AddListener(()=>MouseExit());
                        break;
                    case InteractionTypes.look:
                        lookButton.gameObject.SetActive(true);
                        lookButtonText = interactions[i].text; 
                        lookButton.onClick.AddListener(()=>action());
                        lookButton.onClick.AddListener(()=>MouseExit());
                        break;
                    case InteractionTypes.smell:
                        smellButton.gameObject.SetActive(true);
                        smellButtonText = interactions[i].text; 
                        smellButton.onClick.AddListener(()=>action());
                        smellButton.onClick.AddListener(()=>MouseExit());
                        break;
                    case InteractionTypes.first:
                        firstButton.gameObject.SetActive(true);
                        firstButtonText = interactions[i].text; 
                        firstButton.onClick.AddListener(()=>action());
                        firstButton.onClick.AddListener(()=>MouseExit());
                        break;
                    case InteractionTypes.second:
                        secondButton.gameObject.SetActive(true);
                        secondButtonText = interactions[i].text; 
                        secondButton.onClick.AddListener(()=>action());
                        secondButton.onClick.AddListener(()=>MouseExit());
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
                SetText("You're surrounded by darkness. What do you want to do?"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.listen, "Listen", ()=> GoToScenario(0x1002) ),
                    new InteractionLambda(InteractionTypes.smell, "Smell", ()=> GoToScenario(0x1003) ),
                    new InteractionLambda(InteractionTypes.touch, "Touch", ()=> GoToScenario(0x1004) )
                });
                }
            )); 

        scenarios.Add(0x1002, new ScenarioLambda(() => { 
                SetText("You hear something that seems to be water. What do you think it is?"); 
                PlaySound(splashSound);

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Water puring out of an open sink.", ()=> GoToScenario(0x1005) ),
                    new InteractionLambda(InteractionTypes.second, "Someone splashing in a pool.", ()=> GoToScenario(0x1006) )
                });
                }
            ));

        scenarios.Add(0x1003, new ScenarioLambda(() => { 
                SetText("You take a big gulp of air in. As you do, an intense smell penetrates your soul. Something pungent and sharp. What do you think it was?"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "A nice mature cheddar.", ()=> GoToScenario(0x1007) ),
                    new InteractionLambda(InteractionTypes.second, "Cheese puffs", ()=> GoToScenario(0x1008) )
                });
                }
            ));
        
        scenarios.Add(0x1004, new ScenarioLambda(() => { 
                SetText("You feel around blindly with our open palm. You're reaching into water and find fuzzy, wet strands of something tingling between your fingers. What do you think it is?"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "A clump of hair.", ()=> GoToScenario(0x1041) ),
                    new InteractionLambda(InteractionTypes.second, "A plastic net.", ()=> GoToScenario(0x1042) )
                });
                }
            ));

        scenarios.Add(0x1005, new ScenarioLambda(() => { 
                SetText("You heard water pouring out of an open sink. Do you want to get closer?"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Get closer to the water.", ()=> GoToScenario(0x1011) ),
                    new InteractionLambda(InteractionTypes.second, "Ignore it.", ()=> GoToScenario(0x1001) )
                });
                }
            ));

        scenarios.Add(0x1006, new ScenarioLambda(() => { 
                SetText("You heard someone splashing in a pool. Do you want to get closer?"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Get closer to the pool.", ()=> GoToScenario(0x1011) ),
                    new InteractionLambda(InteractionTypes.second, "Ignore it.", ()=> GoToScenario(0x1001) )
                });
                }
            ));

        scenarios.Add(0x1007, new ScenarioLambda(() => { 
                SetText("You smelled a nice mature cheddar. Do you want take a bite?"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Take a bite out of the cheddar.", ()=> GoToScenario(0x1012) ),
                    new InteractionLambda(InteractionTypes.second, "Ignore it.", ()=> GoToScenario(0x1001) )
                });
                }
            ));

        scenarios.Add(0x1008, new ScenarioLambda(() => { 
                SetText("You smelled some cheese puffs. Do you want to eat them?"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Munch on those cheese puffs.", ()=> GoToScenario(0x1013) ),
                    new InteractionLambda(InteractionTypes.second, "Ignore it.", ()=> GoToScenario(0x1001) )
                });
                }
            ));
        
        scenarios.Add(0x1011, new ScenarioLambda(() => { 
                SetText("Turns out there is no pool but an open sewer, and you just fell into it."); 
                PlaySound(splashSound2);

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Time to go home and take a shower.", ()=> GoToScenario(0x9001) )
                });
                }
            ));

        scenarios.Add(0x1012, new ScenarioLambda(() => { 
                SetText("Without hesitation you reach with your mouth to take a big bite from what you think was cheese. But as soon as you have it in your mouth, you notice:"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "That wasn't cheese.", ()=> GoToScenario(0x1021) ),
                    new InteractionLambda(InteractionTypes.second, "That's the best cheddar you ever had.", ()=> GoToScenario(0x1022) )
                });
                }
            ));
        
        scenarios.Add(0x1013, new ScenarioLambda(() => { 
                SetText("Without hesitation, you fill your mouth with cheese puffs. But as soon as you have them in your mouth, you notice:"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Those are not cheese puffs.", ()=> GoToScenario(0x1023) ),
                    new InteractionLambda(InteractionTypes.second, "These cheese puffs are delicious.", ()=> GoToScenario(0x1024) )
                });
                }
            ));
        
        scenarios.Add(0x1021, new ScenarioLambda(() => { 
                SetText("If that wasn't cheese, then..."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.look, "Take a look.", ()=> GoToScenario(0x1031) )
                });
                }
            ));
        
        scenarios.Add(0x1022, new ScenarioLambda(() => { 
                SetText("The taste of this cheese is so rich and salty, but as you try to chew on it, you find yourself sucking on something leathery and wrinkled."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.look, "Take a look.", ()=> GoToScenario(0x1031) )
                });
                }
            ));
        
        scenarios.Add(0x1023, new ScenarioLambda(() => { 
                SetText("Rather than a crunchy, salty, delicious snack, you find yourself sucking on something leathery and wrinkled."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.look, "Take a look.", ()=> GoToScenario(0x1031) )
                });
                }
            ));
        
        scenarios.Add(0x1024, new ScenarioLambda(() => { 
                SetText("The taste of this cheese puff is so rich and salty, but as you try to chew on it, you find yourself sucking on something leathery and wrinkled."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.look, "Take a look.", ()=> GoToScenario(0x1031) )
                });
                }
            ));
        
        scenarios.Add(0x1031, new ScenarioLambda(() => { 
                SetText("As you look around, you see that you are actually in a public bath house, sucking on a creatures toe. The creature seems to be enjoying what you are doing."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "It's probably best to go home and think about your life for a little while.", ()=> GoToScenario(0x9001) )
                });
                }
            ));

        scenarios.Add(0x1041, new ScenarioLambda(() => { 
                SetText("You're feeling a clump of hair. What do you want to do with it?"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Grab it.", ()=> GoToScenario(0x1043) ),
                    new InteractionLambda(InteractionTypes.second, "Ignore it.", ()=> GoToScenario(0x1001) )
                });
                }
            ));
        
        scenarios.Add(0x1042, new ScenarioLambda(() => { 
                SetText("You're feeling a plastic net. What do you want to do with it?"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Grab it.", ()=> GoToScenario(0x1044) ),
                    new InteractionLambda(InteractionTypes.second, "Ignore it.", ()=> GoToScenario(0x1001) )
                });
                }
            ));

        scenarios.Add(0x1043, new ScenarioLambda(() => { 
                SetText("When you grab the hair, you feel it being tugged away from you."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Hold on to it.", ()=> GoToScenario(0x1045) ),
                    new InteractionLambda(InteractionTypes.second, "Pull.", ()=> GoToScenario(0x1046) )
                });
                }
            ));

        scenarios.Add(0x1044, new ScenarioLambda(() => { 
                SetText("When you grab the plastic net, your hand gets entangled. Suddenly, you feel it constricting your palm out of its own accord."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Try to free yourself.", ()=> GoToScenario(0x1047) ),
                    new InteractionLambda(InteractionTypes.second, "Let's see what happens.", ()=> GoToScenario(0x1048) )
                });
                }
            ));

        scenarios.Add(0x1045, new ScenarioLambda(() => { 
                SetText("The force pulls you along through the water until it stops. You collide with a fuzzy, warm mass."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.look, "Try to see what is going on.", ()=> GoToScenario(0x1052) )
                });
                }
            ));
        
        scenarios.Add(0x1046, new ScenarioLambda(() => { 
                SetText("When you pull, the hair stops moving. You hear someone yelling very close by."); 
                PlaySound(heySound);

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.look, "Try to see what is going on.", ()=> GoToScenario(0x1052) )
                });
                }
            ));

        scenarios.Add(0x1047, new ScenarioLambda(() => { 
                SetText("Moving only makes it worse! Resistance is futile."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.look, "Try and see what is going on.", ()=> GoToScenario(0x1051) )
                });
                }
            ));
        
        scenarios.Add(0x1048, new ScenarioLambda(() => { 
                SetText("Yeah, that's right. Surrender to sensation."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.look, "Try and see what is going on.", ()=> GoToScenario(0x1051) )
                });
                }
            ));
        
        scenarios.Add(0x1051, new ScenarioLambda(() => { 
                SetText("As you look around, you see you are actually in a public bathouse. There is no net but you are flailing wildy in the middle of the pool to try to get it off. The manic splashing is drawing some curious glares."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "It's probably best to go home and think about your life for a little while.", ()=> GoToScenario(0x9001) )
                });
                }
            ));
        
        scenarios.Add(0x1052, new ScenarioLambda(() => { 
                SetText("As you look around, you see you are actually in a public bathouse with a bushy tail in your hand. It belongs to an aquatic fox who is glaring at you with shock and apprehension."); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "It's probably best to go home and think about your life for a little while.", ()=> GoToScenario(0x9001) )
                });
                }
            ));

        scenarios.Add(0x9001, new ScenarioLambda(() => { 
                SetText("The End"); 

                SetInteractions(new InteractionLambda[] {
                    new InteractionLambda(InteractionTypes.first, "Once more from the start.", ()=> GoToScenario(0x1001) )
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
