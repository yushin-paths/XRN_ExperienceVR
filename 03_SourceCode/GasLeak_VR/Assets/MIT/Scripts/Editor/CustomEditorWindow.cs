using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Oculus.Interaction;


#if UNITY_EDITOR

public class Meta_Quest_Test_Window : EditorWindow
{
	[MenuItem("Window/Custom_Windows/Meta_Quest_Testing_Window")]
	public static void LoginWindow()
	{
		Meta_Quest_Test_Window w = (Meta_Quest_Test_Window)GetWindow(typeof(Meta_Quest_Test_Window));
		w.minSize = new Vector2(300, 500);
	}
	private void OnGUI()
	{
		if (Selection.gameObjects.Length != 0)
		{
			if (Selection.gameObjects[0].GetComponent<Button>())
			{
				if (GUILayout.Button(new GUIContent($"{Selection.gameObjects[0].GetComponent<Button>().name} OnClick"))) { Selection.gameObjects[0].GetComponent<Button>().onClick.Invoke(); }
			}
			else if (Selection.gameObjects[0].GetComponent<InteractableUnityEventWrapper>())
			{
				var interactable = Selection.gameObjects[0].GetComponent<InteractableUnityEventWrapper>();

				if (GUILayout.Button(new GUIContent($"{interactable} Hover"))) { interactable.WhenHover.Invoke(); }
				if (GUILayout.Button(new GUIContent($"{interactable} UnHover"))) { interactable.WhenUnhover.Invoke(); }
				if (GUILayout.Button(new GUIContent($"{interactable} Select"))) { interactable.WhenSelect.Invoke(); }
				if (GUILayout.Button(new GUIContent($"{interactable} UnSelect"))) { interactable.WhenUnselect.Invoke(); }
			}

				//if (manager_Login.panelList[0].activeSelf)
				//{
				//	//admin_Login = manager_Login.toggleGroup.GetComponentsInChildren<Toggle>()[0];
				//	//user_Liogin = manager_Login.toggleGroup.GetComponentsInChildren<Toggle>()[1];

				//	EditorGUILayout.BeginHorizontal();

				//	//admin_Login.isOn = EditorGUILayout.ToggleLeft("Admin", admin_Login.isOn, GUILayout.Width(55));
				//	//user_Liogin.isOn = EditorGUILayout.ToggleLeft("User", user_Liogin.isOn, GUILayout.Width(55));
				//	//if (GUILayout.Toggle(admin_Login.isOn, new GUIContent("Admin"), GUILayout.Width(55))) { admin_Login.isOn = admin_Login.isOn ? false : true; }
				//	//if (GUILayout.Toggle(user_Liogin.isOn, new GUIContent("User"))) { user_Liogin.isOn = user_Liogin.isOn ? false : true; }
				//	EditorGUILayout.EndHorizontal();

				//	//if (GUILayout.Button(new GUIContent("Back"))) { manager_Login.btnBack.onClick.Invoke(); }
				//	//manager_Login.id.text = EditorGUILayout.TextField("ID", manager_Login.id.text);
				//	//manager_Login.password.text = EditorGUILayout.TextField("Password", manager_Login.password.text);
				//	//if (GUILayout.Button(new GUIContent("Input ID/PW"))) { manager_Login.id.text = id; manager_Login.password.text = password; EditorApplication.ExecuteMenuItem("Window/General/Game"); /*manager_Login.id.ActivateInputField(); manager_Login.password.ActivateInputField();*/ }
				//	//if (GUILayout.Button(new GUIContent("Login", "\"Input ID/PW\"버튼 먼저 클릭 후 클릭!!"))) { manager_Login.btnLogin.onClick.Invoke(); }
				//}
				////if (GUILayout.Button("PanelAccount")) { manager_Login.btnPanelAccount.onClick.Invoke(); }
				//if (manager_Login.panelList[1].activeSelf)
				//{
				//	//if (GUILayout.Button("CreateAccount")) { manager_Login.btnCreateAccount.onClick.Invoke(); }
				//}
		}
		else
		{
			EditorGUILayout.LabelField("None");

			if (GUILayout.Button(new GUIContent("Selected Manager")))
			{
			}
			//if (FindFirstObjectByType<ManagerLogin>())
			//{
			//	Selection.activeGameObject = FindAnyObjectByType<ManagerLogin>().gameObject;
			//}
			//else if(FindFirstObjectByType<ManagerMapList>())
			//{
			//	Selection.activeGameObject = FindAnyObjectByType<ManagerMapList>().gameObject;
			//}
			//else if(FindFirstObjectByType<ActivateObject>())
			//{
			//	Selection.activeGameObject = FindAnyObjectByType<ActivateObject>().gameObject;
			//}
			//else if(FindFirstObjectByType<ManagerSpawnArea>())
			//{
			//	Selection.activeGameObject = FindAnyObjectByType<ManagerSpawnArea>().gameObject;
			//}
		}
	}
}

//public class MapList : EditorWindow
//{
//    ManagerMapList manager;
//    float value = 0;

//    [MenuItem("Window/Triage_Custom_Windows/MapList")]
//    public static void MapListWindow()
//    {
//        MapList w = (MapList)GetWindow(typeof(MapList));
//        w.minSize = new Vector2(300, 500);
//    }
//    private void OnGUI()
//    {
//        if (Selection.gameObjects != null)
//        {
//            if (Selection.gameObjects[0].GetComponent<ManagerMapList>())
//            {
//                manager = Selection.gameObjects[0].GetComponent<ManagerMapList>();

//                if (GUILayout.Button(new GUIContent("Tutorial"))) { manager.btnTutorial.onClick.Invoke(); }
//                if (GUILayout.Button(new GUIContent("Fire Toggle"))) { manager.toggleSNR1.isOn = !manager.toggleSNR1.isOn; }
//                if (GUILayout.Button(new GUIContent("Collapse Toggle"))) { manager.toggleSNR2.isOn = !manager.toggleSNR2.isOn; }
//                value = EditorGUILayout.Slider("선택할 실습 모드 번호", value, 0f, 30f);
//                value = (int)value;
//                if (GUILayout.Button(new GUIContent("Select Mode"))) { manager.items[(int)value].GetComponent<Button>().onClick.Invoke(); }
//            }
//        }

//    }
//}

//public class Tutorial : EditorWindow
//{
//    TutorialManager manager;

//    [MenuItem("Window/Triage_Custom_Windows/Tutorial")]
//    public static void TutorialWindow()
//    {
//        Tutorial w = (Tutorial)GetWindow(typeof(Tutorial));
//        w.minSize = new Vector2(300, 500);
//    }
//    private void OnGUI()
//    {
//        if (Selection.gameObjects != null)
//        {
//            if (Selection.gameObjects[0].GetComponent<TutorialManager>())
//            {
//                manager = Selection.gameObjects[0].GetComponent<TutorialManager>();


//            }
//        }
//    }
//}

#endif