using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    private GameObject playerRefs;

    public int docCount = 0;
    public float timeSpent;

    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI countText;

    private void Start()
    {
        playerRefs = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        timeSpent += Time.deltaTime;
        timeText.text = "Time: "+((int)timeSpent);
        countText.text = "Documents Aquired: " + docCount+"/7";
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    public void DocumentGet()
    {
        docCount++;
    }
}
