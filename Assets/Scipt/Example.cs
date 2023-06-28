//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//public enum POSITION
//{
//    STAND,
//    SIT,
//    MOVEMENT
//}

//public enum ROL
//{
//    //Stand
//    ANGRY,
//    EXITED,
//    TYPE,
//    LEAN,
//    PRESENT,
//    LISTEN,
//    KNOCK,
//    COFEE,
//    ARGUING,
//    YELLING,

//    //Sit
//    LAUGHT,
//    TALK,
//    NO_TALK,
//    TALK_LESS,

//    //Movment
//    PATROL
//}

//public class Example : MonoBehaviour
//{
//    public POSITION position;
//    public ROL rol;

//    private ROL[] standRols = { ROL.ANGRY, ROL.EXITED, ROL.TYPE, ROL.LEAN, ROL.PRESENT, ROL.LISTEN, ROL.KNOCK, ROL.COFEE, ROL.ARGUING, ROL.YELLING };
//    private ROL[] sitRols = { ROL.LAUGHT, ROL.TALK, ROL.NO_TALK, ROL.TALK_LESS };
//    private ROL[] movementRols = { ROL.PATROL };

//#if UNITY_EDITOR
//    private void OnValidate()
//    {
//        switch (position)
//        {
//            case POSITION.STAND:
//                rol = (ROL)EditorGUILayout.EnumPopup("ROL", rol, EditorStyles.popup, standRols);
//                break;
//            case POSITION.SIT:
//                rol = (ROL)EditorGUILayout.EnumPopup("ROL", rol, EditorStyles.popup, sitRols);
//                break;
//            case POSITION.MOVEMENT:
//                rol = (ROL)EditorGUILayout.EnumPopup("ROL", rol, EditorStyles.popup, movementRols);
//                break;
//            default:
//                break;
//        }
//    }

//    private string[] GetRolOptions(ROL[] rols)
//    {
//        string[] options = new string[rols.Length];
//        for (int i = 0; i < rols.Length; i++)
//        {
//            options[i] = rols[i].ToString();
//        }
//        return options;
//    }
//#endif
//}
