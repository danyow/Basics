using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public int AverageFPS { get; private set;}
    public int HighestFPS { get; private set; }
    public int LowestFPS { get; private set; }
    public int frameRange = 60;
    int[] fpsBuffer;
    int fpsBufferIndex;

    private void Update() {
        if (fpsBuffer == null || fpsBuffer.Length != frameRange) {
            InitializeBuffer();
        }
        UpdateBuffer();
        CalculateFPS();
    }

    void InitializeBuffer() {
        if (frameRange <= 0) {
            frameRange = 1;
        }
        fpsBuffer = new int[frameRange];
        fpsBufferIndex = 0;

    }

    void UpdateBuffer() {
        // 1 除以当前帧时间增量(deltaTime)就是每秒的帧数
        // 但是这个时间增量不是处理最后一帧的时间 它呗当前时间的尺度所压缩 除非这个时间尺度一直为1 不然就是错误的 所以采用未被压缩的增量
        fpsBuffer[fpsBufferIndex ++] = (int)(1f / Time.unscaledDeltaTime);
        if (fpsBufferIndex >= frameRange) {
            fpsBufferIndex = 0;
        }
    } 

    void CalculateFPS() {
        int sum = 0;
        int highest = 0;
        int lowest = int.MaxValue;
        for (int i = 0; i < frameRange; i++) {
            int fps = fpsBuffer[i];
            sum += fps;
            if (fps > highest) {
                highest = fps;
            }
            if (fps < lowest) {
                lowest = fps;
            }
        }
        AverageFPS = sum / frameRange;
        HighestFPS = highest;
        LowestFPS = lowest;
    }
}
