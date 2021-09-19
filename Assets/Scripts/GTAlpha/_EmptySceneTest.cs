using System;
using System.Collections;
using System.Collections.Generic;
using GTAlpha;
using UnityEngine;

public class _EmptySceneTest : MonoBehaviour
{
    private PlayerAttackMotion mAttackMotion;
    private float mCooldown;
    
    private void Start()
    {
        mAttackMotion = PlayerAttackSystem.GetPlayerAttackMotionStart(0);
        print($"Start Attack Motion Key : {mAttackMotion.Key}");

        mCooldown = 1.0f;
    }

    private void Update()
    {
        if (mCooldown > 0.0f)
        {
            mCooldown -= Time.deltaTime;
        }
        else
        {
            mCooldown = 1.0f;
            mAttackMotion = PlayerAttackSystem.GetPlayerAttackMotionNext(0, mAttackMotion);
            print($"Next Attack Motion Key : {mAttackMotion.Key}");
        }
    }
}
