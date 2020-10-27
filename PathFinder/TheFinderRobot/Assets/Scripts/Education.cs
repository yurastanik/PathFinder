using UnityEngine;
using UnityEngine.SceneManagement;

public class Education : MonoBehaviour {

    public void FirstChapt(int num) {
        gameObject.gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(num).gameObject.SetActive(true);
    }

    public void Ok() {
        gameObject.SetActive(false);
    }

    public void startyem() {
        MaplevelChose.quit = true;
        StartCoroutine(GameObject.FindGameObjectWithTag("FadeInFadeOut").GetComponent<FadeInOut>().FadeIn("Menu"));
        Savegame.sv.movesf1 = null;
        Savegame.sv.movesf2 = null;
        Savegame.sv.movesf3 =  null;
        Savegame.sv.movesf4 = null;
        Savegame.sv.movesf5 = null;
        Savegame.sv.moves1 = null;
        Savegame.sv.moves2 = null;
        Savegame.sv.moves3 = null;
        Savegame.sv.moves4 = null;
        Savegame.sv.moves5 = null;
        Savegame.sv.mapNum = 0;
    }

    public void Next() {
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(4).gameObject.SetActive(true);
    }

}
