#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace LudopiaTools.Inspector {
    /**
     * @author: David Marull
     * @description: Modifies the Editor GUI in the inspector for the EasyObjectPool
     * @lastModifiedBy: David Marull
     */
    [CustomEditor( typeof( EasyObjectPool ), true )]
    public class EasyObjectInspector : Editor {

        public override void OnInspectorGUI() {
            EasyObjectPool objectPool = (EasyObjectPool)target;
            base.OnInspectorGUI();
            EditorGUILayout.BeginHorizontal();
            if( GUILayout.Button( "ReCreate Pools" ) ) {
                objectPool.ReCreatePools();
            }
            EditorGUILayout.BeginVertical();
            if( GUILayout.Button( "Create New Pool" ) ) {
                GameObject go = new GameObject( "Snorlax" );
                go.AddComponent<Image>();
                go.GetComponent<Image>().color = Color.blue;
                objectPool.CreateNewPoolInScene( "SnorlaxPool", go, 10, false );
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif
