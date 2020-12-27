using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class prova : MonoBehaviour
{
    public Text a;
    public InputField b;
    public void ChangeText()
    {
        a.text = b.text.Replace("@", "");
    }
}
