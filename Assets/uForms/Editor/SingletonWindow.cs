using UnityEditor;

namespace uForms
{
    public class SingletonWindow<T> : EditorWindow where T : EditorWindow 
    {
        static T instance = null;

        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = GetWindow<T>(typeof(T).Name);
                }
                return instance;
            }
        }

        public static T OpenWindowIfNotExists()
        {
            return Instance;
        }

        public static void CloseIfExists()
        {
            if(instance != null)
            {
                instance.Close();
            }
        }

        protected virtual void OnEnable()
        {
            if(instance == null)
            {
                instance = this as T;
            }
        }

        protected virtual void OnDisable()
        {

        }
    }
}
