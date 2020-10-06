using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RectTransform btnPrefab;
    private FeatureManager mgr;
    private Text descText;
    private Text btnText;
    private List<Button> buttons;

    // Start is called before the first frame update
    void Start()
    {
        mgr = FindObjectOfType<FeatureManager>();
        descText = transform.Find("Navigation").Find("Text").GetComponent<Text>();
        transform.Find("Navigation").Find("Previous").GetComponent<Button>().onClick.AddListener(() => mgr.PreviousChoice());
        transform.Find("Navigation").Find("Next").GetComponent<Button>().onClick.AddListener(() => mgr.NextChoice());
        InitializeFeatureButtons();
    }

    // Toggles audio on or off
    public void Mute()
    {

        AudioListener.pause = !AudioListener.pause;

    }

    // Creates a list of feature buttons based on how many features there
    // are, and stacks them according to width/height of x amount of buttons
    void InitializeFeatureButtons()
    {
        buttons = new List<Button>();

        float height = btnPrefab.rect.height;
        float width = btnPrefab.rect.width;

        int numButtons = 0;

        
        for (int i = 0; i < mgr.features.Count; i++)
        {
            // Create and set buttons as a child of Feature menu object
            RectTransform temp = Instantiate<RectTransform>(btnPrefab);
            temp.name = i.ToString();
            temp.SetParent(transform.Find("Features").GetComponent<RectTransform>());
            temp.localScale = new UnityEngine.Vector3(1, 1, 1);
            temp.localPosition = new UnityEngine.Vector3(0, 0, 0);

            // If there are more than 7 buttons, create a new column
            // to the right of the initial 7, and work downwards
            if (i >= 7)
            {
                temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width, width);
                temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, numButtons * height, height);
                numButtons++;
            }
            else
            {
                temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, width);
                temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * height, height);
            }


            Button b = temp.GetComponent<Button>();
            b.onClick.AddListener(() => mgr.SetCurrent(int.Parse(temp.name)));
            buttons.Add(b);


        }

    }

    // Display descriptive text and image within feature button, based on which feature
    // it correlates to
    void UpdateFeatureButtons()
    {
        for (int i = 0; i < mgr.features.Count; i++)
        {
            buttons[i].transform.Find("FeatureImage").GetComponent<Image>().sprite = mgr.features[i].renderer.sprite;
            if (mgr.features[i].ID == "FaceStuff")
            {
                buttons[i].transform.Find("BtnText").GetComponent<Text>().text = "Headgear";
            }
            else
            {
                buttons[i].transform.Find("BtnText").GetComponent<Text>().text = mgr.features[i].ID;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFeatureButtons();

        // Displays current feature name and index in header at the top
        if (mgr.features[mgr.currFeature].ID == "FaceStuff")
        {
            descText.text = "Headgear #" + (mgr.features[mgr.currFeature].currIndex + 1).ToString();
        }
        else
        {

            descText.text = mgr.features[mgr.currFeature].ID + " #" + (mgr.features[mgr.currFeature].currIndex + 1).ToString();

        }
    }

}
