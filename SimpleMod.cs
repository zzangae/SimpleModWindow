using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using UnityEngine;

namespace SimpleModWindow
{
    public class SimpleMod : MonoBehaviour
    {        
        private bool visible = false;
        protected GUIStyle labelStyle;
        private string inputText = ""; // 입력 필드 값 저장용 변수

        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            new GameObject("__SimpleMod__").AddComponent<SimpleMod>();
        }

        private void OnGUI()
        {
            if (visible)
            {
                // use ModAPI Skin
                GUI.skin = ModAPI.Interface.Skin;

                // apply label style if not existing
                if (this.labelStyle == null)
                {
                    this.labelStyle = new GUIStyle(GUI.skin.label);
                    this.labelStyle.fontSize = 12;
                }

                // 화면 비율 기반으로 UI 요소 크기와 위치를 설정

                // Box (배경) 크기 설정
                float boxWidth = Screen.width * 0.6f;  // 화면 너비의 60% 크기
                float boxHeight = Screen.height * 0.6f;  // 화면 높이의 60% 크기

                float boxX = (Screen.width - boxWidth) / 2;  // 화면 중앙에 배치
                float boxY = (Screen.height - boxHeight) / 2;

                // Box 생성 (배경으로 사용)
                GUI.Box(new Rect(boxX, boxY, boxWidth, boxHeight), "Background Layer", GUI.skin.window);

                // Label 크기와 위치 설정
                float labelWidth = Screen.width * 0.5f;
                float labelHeight = Screen.height * 0.05f;

                float labelX = boxX + (boxWidth - labelWidth) / 2;  // 박스 중앙에 배치
                float labelY = boxY + boxHeight * 0.1f;  // 박스 상단에서 10% 지점에 위치

                // Label 표시
                GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "Dynamic Label", this.labelStyle);

                // 텍스트 필드 크기와 위치 설정
                float textFieldWidth = Screen.width * 0.3f;
                float textFieldHeight = Screen.height * 0.05f;

                float textFieldX = boxX + (boxWidth - textFieldWidth) / 2;
                float textFieldY = boxY + boxHeight * 0.3f;

                // 텍스트 필드 생성
                GUI.TextField(new Rect(textFieldX, textFieldY, textFieldWidth, textFieldHeight), "Input here", GUI.skin.textField);

                // 버튼 크기와 위치 설정
                float buttonWidth = Screen.width * 0.05f;
                float buttonHeight = Screen.height * 0.03f;

                float buttonX = boxX + (boxWidth - buttonWidth) / 2;
                float buttonY = boxY + boxHeight * 0.5f;

                // GUI.Box(new Rect(0f, 0f, 0f, 0f), "Mod Menu", GUI.skin.window);
                // GUI.Label(new Rect(0f, 0f, 0f, 0f), "Enter some text:", this.labelStyle);
                // inputText = GUI.TextField(new Rect(0f, 0f, 0f, 0f), inputText, GUI.skin.textField);
                // if (GUI.Button(new Rect(0f, 0f, 0f, 0f), "Close"))

                if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Close"))
                {
                    visible = false;
                    LocalPlayer.FpCharacter.UnLockView();
                }                
            }
        }
        private void Update()
        {
            // if clicked button
            if (ModAPI.Input.GetButtonDown("showkey"))
            {
                // show cursor
                if (visible)
                {
                    LocalPlayer.FpCharacter.UnLockView();
                }
                else
                {
                    LocalPlayer.FpCharacter.LockView(true);
                }
                // toggle menu
                visible = !visible;
            }
        }
    }

}
