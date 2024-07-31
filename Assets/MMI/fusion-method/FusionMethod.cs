using hci.mmi.speech.SpeechRecognitionSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FusionMethod : MonoBehaviour
{
    [SerializeField]
    private GameObject speechRecognitionSystemGameObject;

    [SerializeField]
    private GameObject poiManagerGameObject;

    private SpeechRecognitionSystem speechRecognitionSystem;
    private POIManager poiManager;
    [SerializeField]
    private MapTouchHandler mapTouchHandler;

    private List<Word> wordBuffer = new List<Word>();
    private float sentenceEndThreshold = 2.0f; // Time in seconds to consider the end of a sentence
    private float lastWordEndTime = 0f;
    public float scalingFactor = 15.0f; 

    public TMP_Text distanceText;

    void Start()
    {
        speechRecognitionSystem = speechRecognitionSystemGameObject.GetComponent<SpeechRecognitionSystem>();
        poiManager = poiManagerGameObject.GetComponent<POIManager>();
        speechRecognitionSystem.OnRecognized += OnSpeechRecognized;
    }

    void Update()
    {
        //Debug.Log("Fusion Update");
        if (wordBuffer.Count > 0 && Time.realtimeSinceStartup - lastWordEndTime > sentenceEndThreshold)
        {
            ProcessSentence();
            wordBuffer.Clear();
        }
    }

    private void OnSpeechRecognized(object sender, Word word)
    {
        wordBuffer.Add(word);
        Debug.Log(word +" Word recognized");
        lastWordEndTime = Time.realtimeSinceStartup; // Update the time with current time to detect the pause
    }

    private List<string> lastTwoPoints = new List<string>();
    private void ProcessSentence()
    {
        string sentence = string.Join(" ", wordBuffer.ConvertAll(w => w.text));
        Debug.Log("[Fusion Method] Recognized Sentence: " + sentence);

        // Process commands
        string lowerSentence = sentence.ToLower();
        if (lowerSentence.Contains("display") && lowerSentence.Contains("places to eat"))
        {
            poiManager.ShowPOIs("PlacesToEat");
        }
        else if (lowerSentence.Contains("hide") && lowerSentence.Contains("places to eat"))
        {
            poiManager.HidePOIs("PlacesToEat");
        }
        else if (lowerSentence.Contains("display") && lowerSentence.Contains("parks"))
        {
            poiManager.ShowPOIs("Parks");
        }
        else if (lowerSentence.Contains("hide") && lowerSentence.Contains("parks"))
        {
            poiManager.HidePOIs("Parks");
        }
        else if (lowerSentence.Contains("display") && lowerSentence.Contains("shops"))
        {
            poiManager.ShowPOIs("Shops");
        }
        else if (lowerSentence.Contains("hide") && lowerSentence.Contains("shops"))
        {
            poiManager.HidePOIs("Shops");
        }
        else if (lowerSentence.Contains("what is this"))
        {
            var poi = mapTouchHandler.HandleMapTouch(mapTouchHandler.GetLastTouchPoint());
            //poiManager.ShowPoiName(poi.name);
            Debug.Log("Showing Place name "+poi.name);
            if (lastTwoPoints.Count >= 2)
            {
                Debug.Log($"The last 2 points are {lastTwoPoints[0]} and {lastTwoPoints[1]}");
                lastTwoPoints[0] = lastTwoPoints[1];
                lastTwoPoints[1] = poi.name;
            }
            lastTwoPoints.Add(poi.name);
            distanceText.text = poi.name;
        }
        else if (lowerSentence.Contains("display") && lowerSentence.Contains("in") && lowerSentence.Contains("area")) // Some issues in the functionality
        {
            mapTouchHandler.StartDrawingCircle();
            poiManager.ShowPOIsInCircle(mapTouchHandler.GetCircleCenter(), mapTouchHandler.GetCircleRadius(), "Shops");
        }
        else if (lowerSentence.Contains("distance") && lowerSentence.Contains("between") && lowerSentence.Contains("last") && lowerSentence.Contains("two") && lowerSentence.Contains("points"))
        {
            var poi1 = poiManager.GetPoiByName(lastTwoPoints[0]);
            var poi2 =  poiManager.GetPoiByName(lastTwoPoints[1]);
            
            if (poi1 != null && poi2 != null)
            {
                float distance = Vector3.Distance(poi1.transform.position, poi2.transform.position);
                string scaledDistance = (distance * scalingFactor).ToString("F1") + " kms";
                distanceText.text = $"Distance between {poi1.name} and {poi2.name}: {scaledDistance}";
                Debug.Log($"Distance between {poi1.name} and {poi2.name}: {distance} units");
            }
            else
            {
                Debug.Log("Not enough POIs selected to calculate distance.");
            }
        }
    }
}
