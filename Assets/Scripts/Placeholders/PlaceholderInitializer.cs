using System.Collections;
using GameScript;
using UnityEngine;

namespace Techno
{
    [DefaultExecutionOrder(-100)]
    public class PlaceholderInitializer : MonoBehaviour
    {
        // TODO - get rid of this mess
        public void Awake()
        {
            IEnumerator i = Database.Initialize();
            while (i.MoveNext()) { }
        }
    }
}
