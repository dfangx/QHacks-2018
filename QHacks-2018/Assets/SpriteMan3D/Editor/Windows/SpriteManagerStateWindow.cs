using UnityEditor;
using UnityEngine;
using System.Linq;
using SpriteMan3D.Core;

namespace SpriteMan3D.UnityEditor
{
    public class SpriteManagerStateWindow : EditorWindow
    {
        public SerializedObject serializedObject;
        public SerializedProperty states;
        private CardinalDirection stateDirection;
        private int frameCount = -1;
        private DirectionSelector directionSelector = new DirectionSelector();

        public SerializedProperty state;
        public int stateIndex = 0;

        private Vector2 scrollPosition = Vector2.zero;

        internal class DirectionSelector
        {
            private CardinalDirection selectedDirection;

            public void SetSelected(CardinalDirection direction)
            {
                selectedDirection = direction;
            }

            public bool IsSelected(CardinalDirection direction)
            {
                return selectedDirection == direction;
            }
        }

        private static class DirectionPrefabs
        {
            public static Texture2D TwoWay { get; private set; }
            public static Texture2D TwoWayE { get; private set; }
            public static Texture2D TwoWayW { get; private set; }

            public static Texture2D FourWay { get; private set; }
            public static Texture2D FourWayN { get; private set; }
            public static Texture2D FourWayE { get; private set; }
            public static Texture2D FourWayS { get; private set; }
            public static Texture2D FourWayW { get; private set; }

            //public static Texture2D EightWay { get; private set; }
            //public static Texture2D EightWayN { get; private set; }
            //public static Texture2D EightWayNE { get; private set; }
            //public static Texture2D EightWayE { get; private set; }
            //public static Texture2D EightWaySE { get; private set; }
            //public static Texture2D EightWayS { get; private set; }
            //public static Texture2D EightWaySW { get; private set; }
            //public static Texture2D EightWayW { get; private set; }
            //public static Texture2D EightWayNW { get; private set; }

            //public static Texture2D SixteenWay { get; private set; }
            //public static Texture2D SixteenWayN { get; private set; }
            //public static Texture2D SixteenWayNNE { get; private set; }
            //public static Texture2D SixteenWayNE { get; private set; }
            //public static Texture2D SixteenWayENE { get; private set; }
            //public static Texture2D SixteenWayE { get; private set; }
            //public static Texture2D SixteenWayESE { get; private set; }
            //public static Texture2D SixteenWaySE { get; private set; }
            //public static Texture2D SixteenWaySSE { get; private set; }
            //public static Texture2D SixteenWayS { get; private set; }
            //public static Texture2D SixteenWaySSW { get; private set; }
            //public static Texture2D SixteenWaySW { get; private set; }
            //public static Texture2D SixteenWayWSW { get; private set; }
            //public static Texture2D SixteenWayW { get; private set; }
            //public static Texture2D SixteenWayWNW { get; private set; }
            //public static Texture2D SixteenWayNW { get; private set; }
            //public static Texture2D SixteenWayNNW { get; private set; }

            public static Texture2D DirectionButton { get; private set; }
            public static Texture2D DirectionButtonActive { get; private set; }

            private static Texture2D GetTextureByName(Sprite[] sprites, string frameName)
            {
                Texture2D result = null;

                var sprite = sprites.FirstOrDefault(o => o.name == frameName);
                if (sprite != null)
                {
                    var rect = sprite.rect;
                    result = new Texture2D((int)rect.width, (int)rect.height);
                    var pixels = sprite.texture.GetPixels(
                        (int)sprite.textureRect.x,
                        (int)sprite.textureRect.y,
                        (int)sprite.textureRect.width,
                        (int)sprite.textureRect.height);
                    result.SetPixels(pixels);
                    result.Apply();
                    result.hideFlags = HideFlags.HideAndDontSave;
                }

                return result;
            }

            static DirectionPrefabs()
            {
                var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/SpriteMan3D/Images/all-dirs.png").OfType<Sprite>().ToArray();

                TwoWay = GetTextureByName(sprites, "4-way");
                TwoWayE = GetTextureByName(sprites, "4-way-e");
                TwoWayW = GetTextureByName(sprites, "4-way-w");

                FourWay = GetTextureByName(sprites, "4-way");
                FourWayN = GetTextureByName(sprites, "4-way-n");
                FourWayE = GetTextureByName(sprites, "4-way-e");
                FourWayS = GetTextureByName(sprites, "4-way-s");
                FourWayW = GetTextureByName(sprites, "4-way-w");

                //EightWay = GetTextureByName(sprites, "8-way");
                //EightWayN = GetTextureByName(sprites, "8-way-n");
                //EightWayNE = GetTextureByName(sprites, "8-way-ne");
                //EightWayE = GetTextureByName(sprites, "8-way-e");
                //EightWaySE = GetTextureByName(sprites, "8-way-se");
                //EightWayS = GetTextureByName(sprites, "8-way-s");
                //EightWaySW = GetTextureByName(sprites, "8-way-sw");
                //EightWayW = GetTextureByName(sprites, "8-way-w");
                //EightWayNW = GetTextureByName(sprites, "8-way-nw");

                //SixteenWay = GetTextureByName(sprites, "16-way");
                //SixteenWayN = GetTextureByName(sprites, "16-way-n");
                //SixteenWayNNE = GetTextureByName(sprites, "16-way-nne");
                //SixteenWayNE = GetTextureByName(sprites, "16-way-ne");
                //SixteenWayENE = GetTextureByName(sprites, "16-way-ene");
                //SixteenWayE = GetTextureByName(sprites, "16-way-e");
                //SixteenWayESE = GetTextureByName(sprites, "16-way-ese");
                //SixteenWaySE = GetTextureByName(sprites, "16-way-se");
                //SixteenWaySSE = GetTextureByName(sprites, "16-way-sse");
                //SixteenWayS = GetTextureByName(sprites, "16-way-s");
                //SixteenWaySSW = GetTextureByName(sprites, "16-way-ssw");
                //SixteenWaySW = GetTextureByName(sprites, "16-way-sw");
                //SixteenWayWSW = GetTextureByName(sprites, "16-way-wsw");
                //SixteenWayW = GetTextureByName(sprites, "16-way-w");
                //SixteenWayWNW = GetTextureByName(sprites, "16-way-wnw");
                //SixteenWayNW = GetTextureByName(sprites, "16-way-nw");
                //SixteenWayNNW = GetTextureByName(sprites, "16-way-nnw");

                DirectionButton = GetTextureByName(sprites, "dir-button");
                DirectionButtonActive = GetTextureByName(sprites, "dir-button-active");
            }
        }

        void OnSelectionChange()
        {
            var activeObject = Selection.activeGameObject;

            if (activeObject == null)
            {
                serializedObject = null;
                state = null;

                Repaint();
            }

            if (activeObject != null)
            {
                var manager = activeObject.GetComponent<SpriteManager>();
                if (manager != null)
                {
                    var serObj = new SerializedObject(manager);
                    var states = serObj.FindProperty("stateMapping");

                    if (states.arraySize > 0)
                    {
                        var state = states.GetArrayElementAtIndex(0);

                        this.serializedObject = serObj;
                        this.states = states;
                        this.state = state;
                        this.Repaint();
                    }
                }
                else if (serializedObject != null && this.state != null)
                {
                    serializedObject = null;
                    states = null;
                    state = null;
                    this.Repaint();
                }
            }
        }

        void DisplayStates()
        {
            if (states.arraySize > 0)
            {
                var options = new string[states.arraySize];

                for (int x = 0; x < states.arraySize; x++)
                {
                    var state = states.GetArrayElementAtIndex(x);
                    var name = state.FindPropertyRelative("stateName");
                    options[x] = string.Format("[{0}] {1}", x.ToString(), name.stringValue);
                }

                int popupIndex = stateIndex;
                if (stateIndex >= states.arraySize)
                {
                    stateIndex = 0;
                }

                stateIndex = EditorGUILayout.Popup("States", stateIndex, options);
                this.state = states.GetArrayElementAtIndex(stateIndex);
            }
        }

        void OnGUI()
        {
            if (serializedObject != null && states != null && state != null)
            {
                DisplayStates();

                UpdateDirectionCounts();

                serializedObject.Update();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                GUILayout.Label("Settings", EditorStyles.boldLabel);

                {
                    EditorGUILayout.BeginHorizontal();

                    var leftWidth = GUILayout.Width(250f);

                    {
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.PropertyField(state.FindPropertyRelative("stateName"), leftWidth);

                        var height = GUILayout.Height(40f);
                        var width = GUILayout.Width(40f);

                        var mode = (DirectionMode)serializedObject.FindProperty("directionMode").enumValueIndex;
                        var wayImage = GetWayImage(mode, stateDirection);

                        {
                            var leftStyle = new GUIStyle();
                            leftStyle.margin = new RectOffset(10, 0, 10, 0);

                            EditorGUILayout.BeginVertical(leftStyle);

                            TopButtons(mode, height, width);
                            {
                                var style = new GUIStyle();
                                style.margin = new RectOffset(0, 0, 25, 0);

                                EditorGUILayout.BeginHorizontal(style, GUILayout.Width(290f));

                                LeftButtons(mode, height, width);

                                Rect rt = GUILayoutUtility.GetRect(128f, 128f);
                                GUI.Label(rt, wayImage);

                                RightButtons(mode, height, width);

                                EditorGUILayout.EndHorizontal();
                            }
                            BottomButtons(mode, height, width);

                            EditorGUILayout.EndVertical();
                        }

                        EditorGUILayout.EndVertical();
                    }

                    {
                        EditorGUILayout.BeginVertical();

                        var directions = state.FindPropertyRelative("directions");

                        SerializedProperty direction = null;
                        int dir = 0;

                        for (int x = 0; x < directions.arraySize; x++)
                        {
                            direction = directions.GetArrayElementAtIndex(x);
                            var elemDir = direction.FindPropertyRelative("direction").intValue;

                            if (elemDir == (int)stateDirection)
                            {
                                dir = elemDir;
                                break;
                            }
                        }

                        EditorGUILayout.LabelField("Frame Count");
                        EditorGUILayout.PropertyField(state.FindPropertyRelative("frameCount"), GUIContent.none);

                        if (dir > 0)
                        {
                            GUILayout.Label(string.Format("Direction: {0}", ((CardinalDirection)dir).ToString()), EditorStyles.boldLabel);

                            var frames = direction.FindPropertyRelative("frames");

                            EditorGUILayout.LabelField(new GUIContent("Frames"));
                            for (int x = 0; x < frames.arraySize; x++)
                            {
                                EditorGUILayout.PropertyField(frames.GetArrayElementAtIndex(x), GUIContent.none);
                            }
                        }

                        EditorGUILayout.EndVertical();
                    }

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();

                serializedObject.ApplyModifiedProperties();
            }
        }

        private void UpdateDirectionCounts()
        {
            var size = state.FindPropertyRelative("frameCount").intValue;

            if (frameCount < 0)
            {
                frameCount = size;
            }
            else if (size != frameCount)
            {
                frameCount = size;

                serializedObject.Update();

                var directions = state.FindPropertyRelative("directions");
                for (int x = 0; x < directions.arraySize; x++)
                {
                    var direction = directions.GetArrayElementAtIndex(x);
                    var frames = direction.FindPropertyRelative("frames");
                    frames.arraySize = size;
                }

                serializedObject.ApplyModifiedProperties();
            }
        }

        private GUIContent GetWayImage(DirectionMode mode, CardinalDirection stateDirection)
        {
            Texture2D texture = null;

            if (mode == DirectionMode.TwoWay)
            {
                texture = DirectionPrefabs.TwoWay;
                if (stateDirection == CardinalDirection.E)
                {
                    texture = DirectionPrefabs.TwoWayE;
                }
                else if (stateDirection == CardinalDirection.W)
                {
                    texture = DirectionPrefabs.TwoWayW;
                }
            }
            else if (mode == DirectionMode.FourWay)
            {
                texture = DirectionPrefabs.FourWay;
                if (stateDirection == CardinalDirection.N)
                {
                    texture = DirectionPrefabs.FourWayN;
                }
                else if (stateDirection == CardinalDirection.E)
                {
                    texture = DirectionPrefabs.FourWayE;
                }
                else if (stateDirection == CardinalDirection.S)
                {
                    texture = DirectionPrefabs.FourWayS;
                }
                else if (stateDirection == CardinalDirection.W)
                {
                    texture = DirectionPrefabs.FourWayW;
                }
            }
            //else if(mode == DirectionMode.EightWay)
            //{
            //    texture = DirectionPrefabs.EightWay;
            //    if (stateDirection == CardinalDirection.N)
            //    {
            //        texture = DirectionPrefabs.EightWayN;
            //    }
            //    else if (stateDirection == CardinalDirection.NE)
            //    {
            //        texture = DirectionPrefabs.EightWayNE;
            //    }
            //    else if (stateDirection == CardinalDirection.E)
            //    {
            //        texture = DirectionPrefabs.EightWayE;
            //    }
            //    else if (stateDirection == CardinalDirection.SE)
            //    {
            //        texture = DirectionPrefabs.EightWaySE;
            //    }
            //    else if (stateDirection == CardinalDirection.S)
            //    {
            //        texture = DirectionPrefabs.EightWayS;
            //    }
            //    else if (stateDirection == CardinalDirection.SW)
            //    {
            //        texture = DirectionPrefabs.EightWaySW;
            //    }
            //    else if (stateDirection == CardinalDirection.W)
            //    {
            //        texture = DirectionPrefabs.EightWayW;
            //    }
            //    else if (stateDirection == CardinalDirection.NW)
            //    {
            //        texture = DirectionPrefabs.EightWayNW;
            //    }
            //}
            //else if(mode == DirectionMode.SixteenWay)
            //{
            //    texture = DirectionPrefabs.SixteenWay;
            //    if (stateDirection == CardinalDirection.N)
            //    {
            //        texture = DirectionPrefabs.SixteenWayN;
            //    }
            //    else if (stateDirection == CardinalDirection.NNE)
            //    {
            //        texture = DirectionPrefabs.SixteenWayNNE;
            //    }
            //    else if (stateDirection == CardinalDirection.NE)
            //    {
            //        texture = DirectionPrefabs.SixteenWayNE;
            //    }
            //    else if (stateDirection == CardinalDirection.ENE)
            //    {
            //        texture = DirectionPrefabs.SixteenWayENE;
            //    }
            //    else if (stateDirection == CardinalDirection.E)
            //    {
            //        texture = DirectionPrefabs.SixteenWayE;
            //    }
            //    else if (stateDirection == CardinalDirection.ESE)
            //    {
            //        texture = DirectionPrefabs.SixteenWayESE;
            //    }
            //    else if (stateDirection == CardinalDirection.SE)
            //    {
            //        texture = DirectionPrefabs.SixteenWaySE;
            //    }
            //    else if (stateDirection == CardinalDirection.SSE)
            //    {
            //        texture = DirectionPrefabs.SixteenWaySSE;
            //    }
            //    else if (stateDirection == CardinalDirection.S)
            //    {
            //        texture = DirectionPrefabs.SixteenWayS;
            //    }
            //    else if (stateDirection == CardinalDirection.SSW)
            //    {
            //        texture = DirectionPrefabs.SixteenWaySSW;
            //    }
            //    else if (stateDirection == CardinalDirection.SW)
            //    {
            //        texture = DirectionPrefabs.SixteenWaySW;
            //    }
            //    else if (stateDirection == CardinalDirection.WSW)
            //    {
            //        texture = DirectionPrefabs.SixteenWayWSW;
            //    }
            //    else if (stateDirection == CardinalDirection.W)
            //    {
            //        texture = DirectionPrefabs.SixteenWayW;
            //    }
            //    else if (stateDirection == CardinalDirection.WNW)
            //    {
            //        texture = DirectionPrefabs.SixteenWayWNW;
            //    }
            //    else if (stateDirection == CardinalDirection.NW)
            //    {
            //        texture = DirectionPrefabs.SixteenWayNW;
            //    }
            //    else if (stateDirection == CardinalDirection.NNW)
            //    {
            //        texture = DirectionPrefabs.SixteenWayNNW;
            //    }
            //}

            var result = new GUIContent();
            result.image = texture;

            return result;
        }

        private GUIStyle GetButtonStyle(CardinalDirection direction)
        {
            var buttonState = new GUIStyleState();
            buttonState.background = DirectionPrefabs.DirectionButton;

            var activeState = GetActiveButtonState();

            var selected = directionSelector.IsSelected(direction);

            var style = new GUIStyle();
            style.normal = selected ? activeState : buttonState;
            style.active = activeState;
            style.alignment = TextAnchor.MiddleCenter;
            return style;
        }

        private GUIStyleState GetActiveButtonState()
        {
            var activeState = new GUIStyleState();
            activeState.background = DirectionPrefabs.DirectionButtonActive;

            return activeState;
        }

        private void TopButtons(DirectionMode mode, GUILayoutOption height, GUILayoutOption width)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(250f));

            //if (mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("NW", "Northwest");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.NW);
            //    buttonStyle.margin = new RectOffset(35, 0, 25, 0);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.NW;
            //        directionSelector.SetSelected(CardinalDirection.NW);
            //    }
            //}
            //else
            //{
            GUILayout.Label("", height, GUILayout.Width(70f));
            //}

            //if (mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("NNW", "North-Northwest");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.NNW);
            //    buttonStyle.margin = new RectOffset(0, 0, 7, 0);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.NNW;
            //        directionSelector.SetSelected(CardinalDirection.NNW);
            //    }
            //}
            //else
            //{
            GUILayout.Label("", height, width);
            //}

            if (mode == DirectionMode.FourWay)// || mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay)
            {
                var buttonContent = new GUIContent("N", "North");

                var buttonStyle = GetButtonStyle(CardinalDirection.N);
                buttonStyle.margin = new RectOffset(3, 3, 0, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.N;
                    directionSelector.SetSelected(CardinalDirection.N);
                }
            }

            //if (mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("NNE", "North-Northeast");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.NNE);
            //    buttonStyle.margin = new RectOffset(0, 0, 7, 0);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.NNE;
            //        directionSelector.SetSelected(CardinalDirection.NNE);
            //    }
            //}
            //else
            //{
            GUILayout.Label("", height, width);
            //}

            //if (mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("NE", "Northeast");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.NE);
            //    buttonStyle.margin = new RectOffset(0, 0, 25, 0);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.NE;
            //        directionSelector.SetSelected(CardinalDirection.NE);
            //    }
            //}
            //else
            //{
            GUILayout.Label("", height, GUILayout.Width(70f));
            //}

            EditorGUILayout.EndHorizontal();
        }

        private void LeftButtons(DirectionMode mode, GUILayoutOption height, GUILayoutOption width)
        {
            EditorGUILayout.BeginVertical(GUILayout.Height(135f));
            GUILayout.FlexibleSpace();

            //if (mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("WNW", "West-Northwest");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.WNW);
            //    buttonStyle.margin = new RectOffset(7, 0, 0, 0);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.WNW;
            //        directionSelector.SetSelected(CardinalDirection.WNW);
            //    }
            //}

            if (mode == DirectionMode.TwoWay || mode == DirectionMode.FourWay) // || mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay)
            {
                var buttonContent = new GUIContent("W", "West");

                var buttonStyle = GetButtonStyle(CardinalDirection.W);
                buttonStyle.margin = new RectOffset(0, 22, 5, 5);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.W;
                    directionSelector.SetSelected(CardinalDirection.W);
                }
            }

            //if (mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("WSW", "West-Southwest");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.WSW);
            //    buttonStyle.margin = new RectOffset(7, 0, 0, 0);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.WSW;
            //        directionSelector.SetSelected(CardinalDirection.WSW);
            //    }
            //}

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();
        }

        private void RightButtons(DirectionMode mode, GUILayoutOption height, GUILayoutOption width)
        {
            EditorGUILayout.BeginVertical(GUILayout.Height(135f));
            GUILayout.FlexibleSpace();

            //if (mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("ENE", "East-Northeast");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.ENE);
            //    buttonStyle.margin = new RectOffset(15, 7, 0, 0);

            //    if (GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.ENE;
            //        directionSelector.SetSelected(CardinalDirection.ENE);
            //    }
            //}

            if (mode == DirectionMode.TwoWay || mode == DirectionMode.FourWay) // || mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay)
            {
                var buttonContent = new GUIContent("E", "East");

                var buttonStyle = GetButtonStyle(CardinalDirection.E);
                buttonStyle.margin = new RectOffset(22, 0, 5, 5);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.E;
                    directionSelector.SetSelected(CardinalDirection.E);
                }
            }

            //if (mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("ESE", "East-Southeast");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.ESE);
            //    buttonStyle.margin = new RectOffset(15, 7, 0, 0);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.ESE;
            //        directionSelector.SetSelected(CardinalDirection.ESE);
            //    }
            //}

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();
        }

        private void BottomButtons(DirectionMode mode, GUILayoutOption height, GUILayoutOption width)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(250f));

            //if (mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("SW", "Southwest");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.SW);
            //    buttonStyle.margin = new RectOffset(35, 0, 0, 0);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.SW;
            //        directionSelector.SetSelected(CardinalDirection.SW);
            //    }
            //}
            //else
            //{
            GUILayout.Label("", height, GUILayout.Width(70f));
            //}

            //if (mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("SSW", "South-Southwest");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.SSW);
            //    buttonStyle.margin = new RectOffset(0, 0, 18, 0);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.SSW;
            //        directionSelector.SetSelected(CardinalDirection.SSW);
            //    }
            //}
            //else
            //{
            GUILayout.Label("", height, width);
            //}

            if (mode == DirectionMode.FourWay) // || mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay)
            {
                var buttonContent = new GUIContent("S", "South");

                var buttonStyle = GetButtonStyle(CardinalDirection.S);
                buttonStyle.margin = new RectOffset(3, 3, 25, 0);

                if (GUILayout.Button(buttonContent, buttonStyle, height, width))
                {
                    stateDirection = CardinalDirection.S;
                    directionSelector.SetSelected(CardinalDirection.S);
                }
            }

            //if (mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("SSE", "South-Southeast");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.SSE);
            //    buttonStyle.margin = new RectOffset(0, 0, 18, 0);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.SSE;
            //        directionSelector.SetSelected(CardinalDirection.SSE);
            //    }
            //}
            //else
            //{
            GUILayout.Label("", height, width);
            //}

            //if (mode == DirectionMode.EightWay || mode == DirectionMode.SixteenWay)
            //{
            //    var buttonContent = new GUIContent("SE", "Southeast");

            //    var buttonStyle = GetButtonStyle(CardinalDirection.SE);
            //    buttonStyle.margin = new RectOffset(0, 0, 0, 25);

            //    if(GUILayout.Button(buttonContent, buttonStyle, height, width))
            //    {
            //        stateDirection = CardinalDirection.SE;
            //        directionSelector.SetSelected(CardinalDirection.SE);
            //    }
            //}
            //else
            //{
            GUILayout.Label("", height, GUILayout.Width(70f));
            //}

            EditorGUILayout.EndHorizontal();
        }
    }
}
