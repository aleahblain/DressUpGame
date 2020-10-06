using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class FeatureManager : MonoBehaviour
{
    public List<Feature> features;
    public int currFeature;

    void OnEnable()
    {
        LoadFeatures();
    }
    void OnDisable()
    {
        SaveFeatures();
    }

    // Loads in assets as features
    void LoadFeatures()
    {
        features = new List<Feature>();
        features.Add(new Feature("Hairs", transform.Find("Face").Find("Hair").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Eyes", transform.Find("Face").Find("Eyes").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Mouths", transform.Find("Face").Find("Mouth").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Blushes", transform.Find("Face").Find("Blushes").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Shirts", transform.Find("Shirts").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Pants", transform.Find("Pants").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Jackets", transform.Find("Jackets").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Shoes", transform.Find("Shoes").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Necklaces", transform.Find("Necklaces").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Earrings", transform.Find("Earrings").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Bracelets", transform.Find("Bracelets").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("FaceStuff", transform.Find("FaceStuff").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Items", transform.Find("Items").GetComponent<SpriteRenderer>()));

        for (int i = 0; i < features.Count; i++)
        {
            string key = "FEATURE_" + i;
            if (PlayerPrefs.HasKey(key))
                PlayerPrefs.SetInt(key, features[i].currIndex);
            features[i].currIndex = PlayerPrefs.GetInt(key);
            features[i].UpdateFeatures();
        }

    }

    void SaveFeatures()
    {
        for (int i = 0; i < features.Count; i++)
        {
            string key = "FEATURE_" + i;
            PlayerPrefs.SetInt(key, features[i].currIndex);
        }
        PlayerPrefs.Save();
    }

    // Sets current feature
    public void SetCurrent(int index)
    {
        if(features == null)
        {
            return;
        }
        currFeature = index;
    }

    // Brings player to next feature
    public void NextChoice()
    {
        if(features == null)
        {
            return;
        }

        features[currFeature].currIndex++;
        features[currFeature].UpdateFeatures();
}

    // Brings player to previous feature
    public void PreviousChoice()
    {
        if (features == null)
        {
            return;
        }

        features[currFeature].currIndex--;
        features[currFeature].UpdateFeatures();
    }
}


[System.Serializable]
public class Feature
{
    public string ID;
    public int currIndex;
    public Sprite[] choices;
    public SpriteRenderer renderer;
    
    // Custom constructor
    public Feature(string id, SpriteRenderer rend)
    {
        ID = id;
        renderer = rend;
        UpdateFeatures();
    }

    // Loops player choices around if they go too an index too high
    // or too low
    public void UpdateFeatures()
    {
        choices = Resources.LoadAll<Sprite>("K5 Textures/" + ID);

        if (choices == null || renderer == null)
        {
            return;
        }
        if (currIndex < 0)
        {
            currIndex = choices.Length - 1;
        }
        if (currIndex >= choices.Length)
        {
            currIndex = 0;
        }

        renderer.sprite = choices[currIndex];

    }

}