using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private Dictionary<int, Scenario> scenarios;
    public int scenarioCount;

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

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        scenarios = new Dictionary<int, Scenario>();

        for(int i = 0; i < scenarioCount; ++i)
        {
            switch (i)
            {
                case 0:
                    Interaction[] interaction = new Interaction[3];
                    int outcome = 11;
                    interaction[0] = new Interaction(interactionTypes.listen, "Listen", outcome);
                    outcome = 12;
                    interaction[1] = new Interaction(interactionTypes.smell, "Smell", outcome);
                    outcome = 13;
                    interaction[2] = new Interaction(interactionTypes.touch, "Touch", outcome);

                    int scenarioID = 0;
                    Scenario scenario = new Scenario(scenarioID, "You suddenly gain consciousness again…. Everythings is dark around…. A very thick darkness…", interaction);
                    break;
                case 11:
                    interaction = new Interaction[3];
                    outcome = 11;
                    interaction[0] = new Interaction(interactionTypes.listen, "Listen", outcome);
                    outcome = 12;
                    interaction[1] = new Interaction(interactionTypes.smell, "Smell", outcome);
                    outcome = 13;
                    interaction[2] = new Interaction(interactionTypes.touch, "Touch", outcome);

                    scenarioID = 0;
                    scenario = new Scenario(scenarioID, "You hear something that seems to be water - What do you think it was?", interaction);
                    break;
                case 12:
                    interaction = new Interaction[3];
                    outcome = 11;
                    interaction[0] = new Interaction(interactionTypes.listen, "Listen", outcome);
                    outcome = 12;
                    interaction[1] = new Interaction(interactionTypes.smell, "Smell", outcome);
                    outcome = 13;
                    interaction[2] = new Interaction(interactionTypes.touch, "Touch", outcome);

                    scenarioID = 0;
                    scenario = new Scenario(scenarioID, "You hear something that seems to be water - What do you think it was?", interaction);
                    break;
                case 13:
                    interaction = new Interaction[3];
                    outcome = 11;
                    interaction[0] = new Interaction(interactionTypes.listen, "Listen", outcome);
                    outcome = 12;
                    interaction[1] = new Interaction(interactionTypes.smell, "Smell", outcome);
                    outcome = 13;
                    interaction[2] = new Interaction(interactionTypes.touch, "Touch", outcome);

                    scenarioID = 0;
                    scenario = new Scenario(scenarioID, "You hear something that seems to be water - What do you think it was?", interaction);
                    break;
            }
        }
    }
}
