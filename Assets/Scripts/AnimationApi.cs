using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimMode { Once, PingPong, Loop }

public class AnimationApi : MonoBehaviour
{

    [System.Serializable]
    public struct KeyFrameCustom
    {
        public Vector3 pos;
        public int timeFrame;

        public KeyFrameCustom(Vector3 posParam, int timeFrameParam)
        {
            pos = posParam;
            timeFrame = timeFrameParam;
        }

        public KeyFrameCustom(KeyFrameCustom frameCopy)
        {
            pos = frameCopy.pos;
            timeFrame = frameCopy.timeFrame;
        }

        public static bool CompareTimeFrame(KeyFrameCustom frameA, KeyFrameCustom frameB)
        {
            if (frameA.timeFrame > frameB.timeFrame)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }



    public AnimMode animMode;
    public int selectedFrameKey = 0;
    public int currentFrame = 0;
    public int KeyFrameSamplesSize
    {
        get
        {
            return keyFrameSamples.Length;
        }
        set
        {
            if (value != keyFrameSamples.Length)
            {
                keyFrameSamples = ClassicArrayExtension.Resize<KeyFrameCustom>(keyFrameSamples, value);
            }
        }
    }
    

    [SerializeField]
    private KeyFrameCustom[] keyFrameSamples = new KeyFrameCustom[1];
    private bool reverse = false;

    public void OnEnable()
    {
        Sort();
    }

    public void Update()
    {
        KeyFrameCustom fromFrame = new KeyFrameCustom(new Vector3(0.0f, 0.0f, 0.0f), 0);
        KeyFrameCustom toFrame = new KeyFrameCustom(new Vector3(0.0f, 0.0f, 0.0f), 0);
        bool addedToFrame = false;
        for (int i = 0; i < keyFrameSamples.Length; i++)
        {

            if (keyFrameSamples[i].timeFrame > currentFrame)
            {
                toFrame = keyFrameSamples[i];
                addedToFrame = true;
                break;
            }
            else
            {
                fromFrame = keyFrameSamples[i];


            }
        }

        if (!addedToFrame)
        {
            
            switch (animMode)
            {
                case AnimMode.Once:
                    transform.position = fromFrame.pos;
                    break;
                case AnimMode.Loop:
                    currentFrame = 0;
                    break;
                case AnimMode.PingPong:
                    reverse = true;
                    break;
            }
        }
        else
        {
            float unlerp = ((float)(currentFrame - fromFrame.timeFrame) / (float)(toFrame.timeFrame - fromFrame.timeFrame));
            transform.position = fromFrame.pos * ((unlerp - 1.0f) / (-1.0f)) + toFrame.pos * unlerp;
        }

        if (!reverse)
        {
            currentFrame++;
        }
        else
        {
            currentFrame--;
            if (currentFrame < 0)
            {
                currentFrame = 0;
                reverse = false;
            }
        }
    }

    public void Sort()
    {
        keyFrameSamples = ClassicArrayExtension.Sort<AnimationApi.KeyFrameCustom>(keyFrameSamples, KeyFrameCustom.CompareTimeFrame);
    }

    public void Remove(int selectedKeyFrame)
    {
        keyFrameSamples = ClassicArrayExtension.RemoveFrom<AnimationApi.KeyFrameCustom>(keyFrameSamples, selectedKeyFrame);
    }
    
    public KeyFrameCustom GetKeyFrame(int i)
    {
        return keyFrameSamples[i];
    }

    public void SetKeyFrameTimeFrame(int index, int timeParam)
    {
        keyFrameSamples[index].timeFrame = timeParam;
    }
    public void SetKeyFramePos(int index, Vector3 posParam)
    {
        keyFrameSamples[index].pos = posParam;
    }

}
