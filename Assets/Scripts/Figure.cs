using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure 
{

    //id del la figura principal
    public int principal;
    //equivalencia de la figura
    public int valueC;
    //ide de la figura repetida
    public int fig;

    //contructos de la figura
    public Figure (int principal, int valueC, int fig){

       this.principal = principal;
       this.valueC = valueC;
       this.fig = fig;
    }

   public int Principal{

       get{
           return principal;
       }

       set{
           principal = value;
       }
   }

   public int ValueC{

       get{
           return valueC;
       }

       set{
           valueC = value;
       }
   }

   public int Fig{

       get{
           return fig;
       }

       set{
           fig = value;
       }
   }
}
