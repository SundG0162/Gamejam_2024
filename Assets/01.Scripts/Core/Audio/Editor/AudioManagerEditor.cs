using System.Text;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace BSM.Core.Audios
{
    [CustomEditor(typeof(AudioManager))]
    public class AudioManagerEditor : Editor
    {
        private AudioManager _audioManager;

        private void OnEnable()
        {
            _audioManager = (AudioManager)target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.Label("Audio Base");
            GUILayout.BeginHorizontal();
            _audioManager.audioBase = EditorGUILayout.ObjectField(_audioManager.audioBase, typeof(AudioBaseSO), false) as AudioBaseSO;

            if (GUILayout.Button("New"))
            {
                var audioBase = ScriptableObject.CreateInstance<AudioBaseSO>();
                CreateaudioBaseAsset(audioBase);
            }

            if (_audioManager.audioBase != null)
            {
                if (GUILayout.Button("Clone"))
                {
                    var audioBase = Instantiate(_audioManager.audioBase);
                    CreateaudioBaseAsset(audioBase);
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(20);

            if (_audioManager.audioBase != null)
            {
                _audioManager.audioPairs = _audioManager.audioBase.pairs;
                var audioBaseArrayObject = serializedObject.FindProperty("audioPairs");
                EditorGUILayout.PropertyField(audioBaseArrayObject, true);
                serializedObject.ApplyModifiedProperties();
                _audioManager.audioBase.pairs = _audioManager.audioPairs;

                serializedObject.Update();
            }
        }
        private void CreateaudioBaseAsset(AudioBaseSO cloneAudioBaseSO)
        {
            var uniqueFileName = AssetDatabase.GenerateUniqueAssetPath($"Assets/New Audio Base.asset");
            AssetDatabase.CreateAsset(cloneAudioBaseSO, uniqueFileName);
            _audioManager.audioBase = cloneAudioBaseSO;
            EditorUtility.SetDirty(_audioManager.audioBase);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
