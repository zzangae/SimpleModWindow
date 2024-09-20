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

        // 선택된 탭을 저장하는 변수
        private int selectedTab = 0;

        // 각 탭에 표시될 텍스트
        private string[] tabLabels = { "Label 1", "Label 2", "Label 3", "Label 4", "Label 5", "Label 6" };

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

                // TAB 버튼 관련 설정
                string[] tabTexts = { "TAB 1", "TAB 2", "TAB 3", "TAB 4", "TAB 5", "TAB 6" };
                int tabCount = tabTexts.Length;

                // 각 TAB 버튼의 크기 설정 (가로 길이를 Box 너비의 1/6로 설정)
                float tabButtonWidth = boxWidth / tabCount;  // 각 버튼의 너비는 Box 너비를 6등분
                float tabButtonHeight = Screen.height * 0.05f;  // 버튼의 높이
                float tabButtonY = boxY - tabButtonHeight;  // Box 상단에 붙여서 배치

                // GUIStyle을 사용해 텍스트의 실제 크기를 계산하고 여백을 추가할 수 있습니다
                GUIStyle buttonStyle = GUI.skin.button;

                // 6개의 TAB 버튼 생성
                for (int i = 0; i < tabCount; i++)
                {
                    // 각 TAB 버튼의 X 좌표는 균등하게 나누어 설정
                    float tabButtonX = boxX + (tabButtonWidth * i);

                    // TAB 버튼 생성
                    if (GUI.Button(new Rect(tabButtonX, tabButtonY, tabButtonWidth, tabButtonHeight), tabTexts[i]))
                    {
                        selectedTab = i;  // 버튼이 눌리면 해당 탭 인덱스를 저장
                        Debug.Log(tabTexts[i] + " clicked!");
                    }
                }

                // 선택된 탭에 따라 Label 내용 변경
                string currentLabel = tabLabels[selectedTab];

                // Label 크기와 위치 설정
                float labelWidth = Screen.width * 0.5f;
                float labelHeight = Screen.height * 0.05f;

                float labelX = boxX + (boxWidth - labelWidth) / 2;  // 박스 중앙에 배치
                float labelY = boxY + boxHeight * 0.1f;  // 박스 상단에서 10% 지점에 위치

                // 선택된 탭에 맞는 Label 표시
                GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), currentLabel, this.labelStyle);

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
