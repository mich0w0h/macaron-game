using TMPro;
using UnityEngine;

public class PlaceTitle : MonoBehaviour {

    private TextMeshProUGUI textMeshPro;
    
    private void Awake() {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        if (PlayerData.IsAlreadyFriend()) {
            textMeshPro.text = "マカロンのばしょ";
        } else {
            textMeshPro.text = "マカロンとマブのばしょ";
        }
    }
    
}
