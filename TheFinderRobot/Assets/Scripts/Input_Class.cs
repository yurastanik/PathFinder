using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Class
{
    public int color = 0;
    public int direct = 0;
    public enum Colors {
        White,
        Blue,
        Red,
        Green
    }
    public enum Directs {
        forward,
        left,
        right,
    }

   public Input_Class(int color, int direct){
        this.color = color;
        this.direct = direct;
   }
}
