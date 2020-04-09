using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameColors {

    public static Color GetColor(int col) {
        if (col == 1)
            return Color.blue;
        else if (col == 2)
            return Color.green;
        return Color.black;
    }

}
