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

}
