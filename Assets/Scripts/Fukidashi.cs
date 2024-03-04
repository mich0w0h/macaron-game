using UnityEngine;
using TMPro;

public class Fukidashi : MonoBehaviour {
    private GameObject normal;
    private TextMeshProUGUI textMeshPro;
    private RectTransform textRect;

    private void Awake() {
        normal = transform.Find("Normal").gameObject;
        Transform mokumoku = transform.Find("Mokumoku");
        
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        textRect = textMeshPro.GetComponent<RectTransform>();

        if (PlayerData.IsSecondDay()) {
            mokumoku.transform.Find("Macaron").gameObject.SetActive(true);
            mokumoku.transform.Find("Bard").gameObject.SetActive(false);
        } else if (PlayerData.IsThirdDay()) {
            mokumoku.transform.Find("Macaron").gameObject.SetActive(false);
            mokumoku.transform.Find("Bard").gameObject.SetActive(true);
        }
        
        HideNormal();
    }

    private void UpdateText(string line) {
        GameEvents.onSETime.Invoke("fukidashi");

        if (line.Contains('\n')) {
            textRect.anchoredPosition = new Vector2(textRect.anchoredPosition.x, 15f);
            textRect.sizeDelta = new Vector2(textRect.sizeDelta.x, 112f);
        } else {
            textRect.anchoredPosition = new Vector2(textRect.anchoredPosition.x, 14.5f);
            textRect.sizeDelta = new Vector2(textRect.sizeDelta.x, 70f);
        }

        textMeshPro.text = line;
    }

    
    public void ShowNormal(string line) {
        UpdateText(line);
        normal.SetActive(true);        
    }

    public void ShowGoron() {
        UpdateText("ごろん");
        normal.SetActive(true);
    }

    public void HideNormal() {
        normal.SetActive(false);
    }
}
