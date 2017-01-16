using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles AI movement and shooting
/// </summary>
[System.Serializable]
public class AIHandler : TankManager
{
    private AIMovement _AIMove;

    public override void Setup()
    {
        _AIMove = m_Instance.GetComponent<AIMovement>();
        _AIMove.m_PlayerNumber = -1;

        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
        for(int i = 0;i < renderers.Length;i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }

    public override void DisableControl()
    {
        _AIMove.enabled = false;
        m_CanvasGameObject.SetActive(false);
    }

    public override void EnableControl()
    {
        _AIMove.enabled = true;
        m_CanvasGameObject.SetActive(false);
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override TankMovement getTankMovement()
    {
        return base.getTankMovement();
    }
}