using UnityEngine;
using UnityEngine.UI;

public class HSVRotation : MonoBehaviour {
    float HSV = 0f;

    private Text textObj;

    private void Start() {
        textObj = GetComponent<Text>();
    }

    private void Update() {
        textObj.color = Color.HSVToRGB(HSV, 1, 1);

        HSV += 0.0025f;
        if (HSV > 1)
            HSV = 0;
    }
}
