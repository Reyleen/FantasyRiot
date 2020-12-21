using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characterPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        LoadCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadCharacter()
    {
        int characterIndex = PlayerPrefs.GetInt("CharacterIndex");
        Instantiate(characterPrefabs[characterIndex]);
    }
}
