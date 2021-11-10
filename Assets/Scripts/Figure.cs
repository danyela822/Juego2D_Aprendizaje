using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure 
{

   public int principal;
   public int valueC;
   public int fig;

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
