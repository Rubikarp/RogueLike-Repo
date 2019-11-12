using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree
{
    [CreateAssetMenu (menuName = "Generator/Player/Create Skill")]
    public class Attributes : ScriptableObject
    {
        public string Description;
        public Sprite Thumbnail;
    }
}
