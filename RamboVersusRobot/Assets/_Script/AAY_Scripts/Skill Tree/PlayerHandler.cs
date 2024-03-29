﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree
{


    public class PlayerHandler : MonoBehaviour
    {

        public PlayerStats Player;

        [SerializeField]
        private Canvas m_Canvas = null;
        private bool m_SeeCanvas;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("tab"))
            {
                if (m_Canvas)
                    {
                        m_SeeCanvas = !m_SeeCanvas;
                        m_Canvas.gameObject.SetActive(m_SeeCanvas);
                    }
            }
        }
    }
}
