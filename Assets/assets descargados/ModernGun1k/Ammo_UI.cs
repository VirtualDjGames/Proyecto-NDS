using UnityEngine;
using UnityEngine.UI;

public class Ammo_UI : MonoBehaviour
{
    public Text ammoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = Variables.Aammo.ToString() + "/" + Variables.Rammo.ToString();
    }
}
