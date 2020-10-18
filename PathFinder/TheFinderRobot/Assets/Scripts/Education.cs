using UnityEngine;

public class Education : MonoBehaviour {

    
    public void FirstChapt(int num) {
        gameObject.gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(num).gameObject.SetActive(true);
    }

    public void Ok() {
        gameObject.SetActive(false);
    }

    public void Next() {
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(4).gameObject.SetActive(true);
    }

}
