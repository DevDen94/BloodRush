using UnityEngine;
using UnityEngine.SceneManagement;
namespace MarsFPSKit
{
    namespace UI
    {
        public class Kit_MenuExitScreen : MonoBehaviour
        {
            /// <summary>
            /// Menu Manager reference
            /// </summary>
            public Kit_MenuManager menuManager;
            /// <summary>
            /// Menu id of the exit screen
            /// </summary>
            public int exitScreenId;

            public void Exit()
            {
                //Close game
                SceneManager.LoadScene(1);
            }

            public void Abort()
            {
                //Go back to main screen
                menuManager.SwitchMenu(menuManager.mainScreen);
            }
        }
    }
}