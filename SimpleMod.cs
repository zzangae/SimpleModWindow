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

        // 선택된 탭을 저장하는 변수 : Variable that stores the selected tab
        private int selectedTab = 0;

        // 각 탭에 표시될 텍스트 : Text to display on each tab
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

                // 화면 비율 기반으로 UI 요소 크기와 위치를 설정 : Set UI element size and position based on screen ratio

                // Box (배경) 크기 설정 : Set Box (Background) Size
                float boxWidth = Screen.width * 0.6f;  // 화면 너비의 60% 크기 : 60% size of screen width
                float boxHeight = Screen.height * 0.6f;  // 화면 높이의 60% 크기 : 60% size of screen height

                float boxX = (Screen.width - boxWidth) / 2;  // 화면 중앙에 배치 : Centered on the screen
                float boxY = (Screen.height - boxHeight) / 2;

                // Box 생성 (배경으로 사용) : Create a Box (to be used as a background)
                GUI.Box(new Rect(boxX, boxY, boxWidth, boxHeight), "Background Layer", GUI.skin.window);

                // TAB 버튼 관련 설정 : TAB button related settings
                string[] tabTexts = { "TAB 1", "TAB 2", "TAB 3", "TAB 4", "TAB 5", "TAB 6" };
                int tabCount = tabTexts.Length;

                // 각 TAB 버튼의 크기 설정 (가로 길이를 Box 너비의 1/6로 설정) : Set the size of each TAB button (set the width to 1/6 of the Box width)
                float tabButtonWidth = boxWidth / tabCount;  // 각 버튼의 너비는 Box 너비를 6등분 : The width of each button is divided into six equal parts of the box width.
                float tabButtonHeight = Screen.height * 0.05f;  // 버튼의 높이 : Height of button
                float tabButtonY = boxY - tabButtonHeight;  // Box 상단에 붙여서 배치 : Place it on top of the box

                // GUIStyle을 사용해 텍스트의 실제 크기를 계산하고 여백을 추가할 수 있습니다 : Using GUIStyle you can calculate the actual size of the text and add margins.
                GUIStyle buttonStyle = GUI.skin.button;

                // 6개의 TAB 버튼 생성 : Create 6 TAB buttons
                for (int i = 0; i < tabCount; i++)
                {
                    // 각 TAB 버튼의 X 좌표는 균등하게 나누어 설정 : The X coordinate of each TAB button is set to be evenly divided
                    float tabButtonX = boxX + (tabButtonWidth * i);

                    // TAB 버튼 생성 : Create a TAB button
                    if (GUI.Button(new Rect(tabButtonX, tabButtonY, tabButtonWidth, tabButtonHeight), tabTexts[i]))
                    {
                        selectedTab = i;  // 버튼이 눌리면 해당 탭 인덱스를 저장 : When a button is pressed, it saves the corresponding tab index.
                        Debug.Log(tabTexts[i] + " clicked!");
                    }
                }

                // 선택된 탭에 따라 Label 내용 변경 : Change Label content depending on selected tab
                string currentLabel = tabLabels[selectedTab];

                // Label 크기와 위치 설정 : Setting Label Size and Position
                float labelWidth = Screen.width * 0.5f;
                float labelHeight = Screen.height * 0.05f;

                float labelX = boxX + (boxWidth - labelWidth) / 2;  // 박스 중앙에 배치 : Placed in the center of the box
                float labelY = boxY + boxHeight * 0.1f;  // 박스 상단에서 10% 지점에 위치 : Located at 10% from the top of the box

                // 선택된 탭에 맞는 Label 표시 : Display Label for selected tab
                GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), currentLabel, this.labelStyle);

                // 텍스트 필드 크기와 위치 설정 : Setting the text field size and position
                float textFieldWidth = Screen.width * 0.3f;
                float textFieldHeight = Screen.height * 0.05f;

                float textFieldX = boxX + (boxWidth - textFieldWidth) / 2;
                float textFieldY = boxY + boxHeight * 0.3f;

                // 텍스트 필드 생성 : Create a text field
                GUI.TextField(new Rect(textFieldX, textFieldY, textFieldWidth, textFieldHeight), "Input here", GUI.skin.textField);

                // 버튼 크기와 위치 설정 : Setting button size and position
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
