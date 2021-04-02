using UnityEngine ;
using UnityEngine.SceneManagement ;
using UnityEngine.Events ;

public class GameOverSimulation : MonoBehaviour {
   public UnityAction OnGameover ;

   bool isclicked = false ;

   public void Gameover () {
      if (!isclicked) {
         isclicked = true ;
         if (OnGameover != null)
            OnGameover.Invoke () ;

         Invoke ("Reload", 3f) ;
      }
   }

   void Reload () {
      SceneManager.LoadScene (1) ;
   }
}
