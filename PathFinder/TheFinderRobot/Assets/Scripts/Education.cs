using UnityEngine;

public class Education : MonoBehaviour {

    
    public void FirstChapt(int num) {
        gameObject.gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(num).gameObject.SetActive(true);
    }

    public void Ok() {
        gameObject.gameObject.SetActive(false);
        foreach (Transform child in gameObject.transform.GetChild(0).GetChild(0))
            child.gameObject.SetActive(false);
    }

    public void Next() {
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
    }

}
