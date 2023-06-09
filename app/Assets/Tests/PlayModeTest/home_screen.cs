using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


namespace home_screen_ui_tests
{
    public class ui_test: InputTestFixture
    {
        //add mouse and continue button
        public Mouse mouse;
        public GameObject c_button;
        public override void Setup()
        {
            //setup current scene as home_Screen and add mouse
            base.Setup();
            SceneManager.LoadScene("home_screen");
            mouse = InputSystem.AddDevice<Mouse>();
        }

        public void ClickUI(GameObject uiElement)
        {
            //Find the uiElement with respect to the camera and click on it
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 screenpos = camera.WorldToScreenPoint(uiElement.transform.position);
            //Vector3 mousep = Input.mousePosition;
            Set(mouse.position, screenpos);
            Click(mouse.leftButton);
        }
        [UnityTest]
        public IEnumerator continue_button_test()
        {
            //navigating from home_screen to user_location
            c_button = GameObject.Find("Canvas/Begin");
            Assert.That(SceneManager.GetActiveScene().name, Is.EqualTo("home_screen"));
            
            ClickUI(c_button);
            yield return new WaitForSeconds(2f);
            Assert.That(SceneManager.GetActiveScene().name,Is.EqualTo("user_location"));
        }

    }
}
