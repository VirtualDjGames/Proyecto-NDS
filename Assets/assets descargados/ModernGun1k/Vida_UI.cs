using UnityEngine;
using UnityEngine.UI;

public class Vida_UI : MonoBehaviour
{
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = Variables.hp.ToString();

    }
}
