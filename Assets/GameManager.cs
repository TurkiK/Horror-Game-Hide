using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI endScreenText;
    [SerializeField] private GameObject playerGUI;
    [SerializeField] private GUIManager guiManager;
    [SerializeField] private int documentCount;
    [SerializeField] private int _Score;

    public void endGame()
    {
        playerGUI.SetActive(false);
        endScreenText.text = "The patrol bot has caught you with "+guiManager.docCount+"/7 documents!\nTime spent: "+(int)(guiManager.timeSpent);
        endScreen.SetActive(true);
        Time.timeScale = 0;
    }    
    
    public void winGame()
    {
        if(guiManager.timeSpent > 10000)
            _Score = 0;
        else
            _Score = (int)(_Score / guiManager.timeSpent * 100);

        playerGUI.SetActive(false);
        endScreenText.text = "Congratulations, you escaped with "+guiManager.docCount+"/7 documents!\nScore: "+_Score+"\nTime spent to escape: "+ (int)(guiManager.timeSpent);
        endScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void updateScore()
    {
        if(_Score == 0) 
            _Score = 1000;
        else
            _Score += 1000 * (1 + documentCount / 2);
    }
}
