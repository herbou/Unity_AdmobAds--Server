using UnityEngine ;
using UnityEngine.SceneManagement ;

public class Loading : MonoBehaviour {
   private void Start () {
      Invoke ("LoadGame", 3f) ;
   }

   private void LoadGame () {
      SceneManager.LoadScene (1) ;
   }
}
